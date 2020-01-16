using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MsBuildTests
    {
        XamlTemplates.MSBuild.XamlTemplates task;

        [SetUp]
        public void Setup()
        {
            task = new XamlTemplates.MSBuild.XamlTemplates();
        }
        [Test]
        public void Generate()
        {
            task.Execute().Should().BeTrue();
        }

        [Test]
        public void Folder()
        {
            task.Filter = "Templates.taml";
            task.Folder = "Templates";
            task.Namespace = "Test";

            task.Execute().Should().BeTrue();
        }
    }
}