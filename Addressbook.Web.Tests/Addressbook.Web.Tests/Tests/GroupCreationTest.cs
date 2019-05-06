using NUnit.Framework;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class GroupCreationTestCase : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData();
            group.Name = "Test1";
            group.Header = "Text";
            group.Footer = "another text";

            app.Groups.Create(group);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData();
            group.Name = "";
            group.Header = "";
            group.Footer = "";

            app.Groups.Create(group);
        }
    }
}