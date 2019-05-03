using NUnit.Framework;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class GroupCreationTestCase : TestBase
    {
        [Test]
        public void GroupCreationCaseTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToGroupsPage();
            InitGroupCreation();
            GroupData group = new GroupData();
            group.Name = "Test1";
            group.Header = "Text";
            group.Footer = "another text";
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            Logout();
        }
    }
}