﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentMongo.Linq;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using MongoDB.Driver;

namespace LittleProblem.Data.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IConnexion _connexion;
        private readonly MongoCollection<Member> _membersCollection;

        public MemberRepository(IConnexion conn)
        {
            _connexion = conn;
            _membersCollection = _connexion.Collection<Member>(CollectionNames.Member);
        }

        public Member GetMember(string openId)
        {
            return _membersCollection.AsQueryable().FirstOrDefault(x => x.OpenId == openId);
        }

    }
}