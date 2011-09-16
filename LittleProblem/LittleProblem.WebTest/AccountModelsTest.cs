using LittleProblem.Web.Models;
using NUnit.Framework;

namespace LittleProblem.WebTest
{
    [TestFixture]
    public class AccountModelsTest
    {

        [Test]
        public void NoteModelDivideNoteIntoGoldSilverAndBronze()
        {
            const int memberNote = 123456789;

            var noteModel = new NoteModel(memberNote);

            Assert.That(noteModel.Bronze, Is.EqualTo(89));
            Assert.That(noteModel.Silver, Is.EqualTo(67));
            Assert.That(noteModel.Gold, Is.EqualTo(12345));
        }

    }
}
