using System;
using System.Linq;
using FluentMongo.Linq;
using LittleProblem.Data.Model;
using MongoDB.Driver;

namespace LittleProblem.Data.Services
{
    internal class MembershipService : IMembershipService
    {
        public Member LogIn(string openId)
        {
            return LogIn(openId, null);
        }

        public Member LogIn(string openId, string email)
        {
            var mongoServ = MongoServer.Create("mongodb://localhost");

            var db = mongoServ.GetDatabase("LittleProblem");
            var members = db.GetCollection<Member>("members");

            var member = members.AsQueryable()
                .FirstOrDefault(m => m.OpenId.Equals(openId));
            if (member == null)
            { return CreateOnFirstLogIn(openId, email);}
            else
            {
                return member;
            }

        }
        
        public Member CreateOnFirstLogIn(string openId, string email)
        {
            var mongoServ = MongoServer.Create("mongodb://localhost");

            var db = mongoServ.GetDatabase("LittleProblem");
            var members = db.GetCollection<Member>("members");
            int nb = members.Count() + 1;
            Member member = new Member
                                {
                                    OpenId = openId,
                                    UserName = "user" + nb,
                                    FirstConnection = DateTime.Now,
                                    LastConnection = DateTime.Now
                                };
            members.Insert(member);
            return member;
        }
    }
}
