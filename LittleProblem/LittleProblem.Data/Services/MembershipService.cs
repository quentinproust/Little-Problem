using System;
using System.Linq;
using FluentMongo.Linq;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using MongoDB.Driver;

namespace LittleProblem.Data.Services
{
    public class MembershipService : IMembershipService
    {
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
            return member ?? CreateOnFirstLogIn(openId, email);
        }
        
        public Member CreateOnFirstLogIn(string openId, string email)
        {
            int nb = _membersCollection.Count() + 1;
            Member member = new Member
                                {
                                    OpenId = openId,
                                    UserName = "user" + nb,
                                    FirstConnection = DateTime.Now,
                                    LastConnection = DateTime.Now
                                };
            _membersCollection.Save(member);
            return member;
        }

        public void EditMemberProfile(Member member)
        {
            var dbMember = _membersCollection.AsQueryable()
                .FirstOrDefault(m => m.OpenId == member.OpenId);

            dbMember.UserName = member.UserName;
            dbMember.Email = member.Email;
            _membersCollection.Save(dbMember);
        }
    }
}
