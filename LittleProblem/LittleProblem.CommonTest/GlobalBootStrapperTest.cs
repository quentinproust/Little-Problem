using LittleProblem.Common.BootStrap;
using Moq;
using NUnit.Framework;

namespace LittleProblem.CommonTest
{
    [TestFixture]
    public class GlobalBootStrapperTest
    {
        [Test]
        public void InitializeNothing()
        {
            var globalBoot = new GlobalBootStrapper();

            globalBoot.Initialize(); // Should not do anything
        }

        [Test]
        public void InitializeOneBootStrapper()
        {
            var bootStrap = new Mock<IBootStrap>();
            bootStrap.Setup(x => x.Initialize()).Verifiable();

            var globalBoot = new GlobalBootStrapper();
            globalBoot.Register(bootStrap.Object);

            globalBoot.Initialize();

            bootStrap.Verify();
        }

        [Test]
        public void InitializeBootStrappers()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var globalBoot = new GlobalBootStrapper();
            for (int i = 0; i < 10; i++)
            {
                var bootStrap = mockRepository.Create<IBootStrap>();
                bootStrap.Setup(x => x.Initialize()).Verifiable();
                globalBoot.Register(bootStrap.Object);
            }

            globalBoot.Initialize();

            mockRepository.Verify();
        }
    }
}
