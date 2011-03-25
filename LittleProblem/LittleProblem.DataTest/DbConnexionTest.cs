using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittleProblem.Data;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using NUnit.Framework;

namespace LittleProblem.DataTest
{
    [TestFixture]
    public class DbConnexionTest
    {
        [Test]
        public void GetConnexionWithoutDbName()
        {
            IConnexion connexion = new DbConnexion();
            Assert.That(connexion, Is.Not.Null);
        }

        [Test]
        public void GetConnexionWithDbName()
        {
            IConnexion connexion = new DbConnexion("Test_LittleProblem");
            Assert.That(connexion, Is.Not.Null);
        }

        [Test]
        public void GetCollectionFromConnexion()
        {
            IConnexion connexion = new DbConnexion();
            var collection = connexion.Collection<Member>(CollectionNames.Member);

            Assert.That(collection, Is.Not.Null);
            Assert.That(collection.Name, Is.EqualTo(CollectionNames.Member));
        }
    }
}
