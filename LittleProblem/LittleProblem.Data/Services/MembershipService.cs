using System;
using System.Linq;
using FluentMongo.Linq;
using LittleProblem.Data.MapReduce;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NLog;

namespace LittleProblem.Data.Services
{
    public class MembershipService : IMembershipService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IConnexion _connexion;
        private readonly MongoCollection<Member> _membersCollection;

        public MembershipService(IConnexion connexion)
        {
            _connexion = connexion;
            _membersCollection = _connexion.Collection<Member>(CollectionNames.Member);
        }

        public Member LogIn(string openId)
        {
            return LogIn(openId, null);
        }

        public Member LogIn(string openId, string email)
        {
            var member = _membersCollection.AsQueryable()
                .FirstOrDefault(m => m.OpenId == openId);
            if (member == null)
            {
                member = CreateOnFirstLogIn(openId, email);
            } else
            {
                member.LastConnection = DateTime.Now;
                _membersCollection.Save(member);
            }
            return member;
        }

        private Member CreateOnFirstLogIn(string openId, string email)
        {
            int nb = _membersCollection.Count() + 1;
            var member = new Member
                                {
                                    OpenId = openId,
                                    UserName = "user" + nb,
                                    FirstConnection = DateTime.Now,
                                    LastConnection = DateTime.Now
                                };
            if(!String.IsNullOrEmpty(email)) member.Email = email;
            _membersCollection.Save(member);

            Logger.Info("A new person has logged in LittleProblem. "+
                " Its account has been created with temp username : " + member.UserName + 
                " and it will be linked to the openId : " + member.OpenId);

            return member;
        }

        public void EditMemberProfile(Member member)
        {
            var dbMember = _membersCollection.AsQueryable()
                .FirstOrDefault(m => m.OpenId == member.OpenId);

            dbMember.UserName = member.UserName;
            dbMember.Email = member.Email;
            _membersCollection.Save(dbMember);

            Logger.Info("The user "+ member.UserName + " has updated its profile.");
        }

        public void UpdateUserNote()
        {
            var result = _membersCollection.MapReduce(
                MapReduceCodeLoader.Load("ResponsesUserNote.map.js"), 
                MapReduceCodeLoader.Load("Notes.reduce.js"),
                MapReduceOptions.SetOutput(MapReduceOutput.Replace(CollectionNames.Member)));

            if (result.Ok)
            {
                Logger.Info("Map reduce for user note completed");
            }
            else
            {
                Logger.Error("Map reduce for user note failed");
            }
        }
    }
}
