using LittleProblem.Data.MapReduce;
using NUnit.Framework;

namespace LittleProblem.DataTest
{
    [TestFixture]
    public class MapReduceCodeLoaderTest
    {
        [Test]
        public void LoadCodeFromFile()
        {
            const string file = "Notes.reduce.js";
            var code = MapReduceCodeLoader.Load(file);
            Assert.That(code, Is.Not.Null);
        }

        [Test]
        public void ThrowExceptionIfCodeFileNotFound()
        {
            const string file = "NotFoundCodeFile";
            Assert.That(() => MapReduceCodeLoader.Load(file), Throws.Exception);
        }
    }
}
