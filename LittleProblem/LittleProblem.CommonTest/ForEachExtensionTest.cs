using System;
using System.Collections.Generic;
using FakeItEasy;
using LittleProblem.Common.Colllections;
using Moq;
using NUnit.Framework;

namespace LittleProblem.CommonTest
{
    [TestFixture]
    public class ForEachExtensionTest
    {

        [Test]
        public void DontExecuteActionWhenEmptyEnumerable()
        {
            int count = 0; // Used to check that the action was not called.
            IEnumerable<int> empty = new List<int>();

            var fakeAction = new Action<int>(x => count++);

            empty.ForEach(fakeAction);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void ExecuteActionForEveryElementInEnumerable()
        {
            int count = 0; // Used to check that the action was not called.
            IEnumerable<int> empty = new List<int> { 1, 2, 3, 4, 5 };

            var fakeAction = new Action<int>(x => count++);

            empty.ForEach(fakeAction);
            Assert.That(count, Is.EqualTo(5));
        }
    }
}
