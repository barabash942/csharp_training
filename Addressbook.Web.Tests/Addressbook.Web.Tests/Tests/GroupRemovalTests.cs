using NUnit.Framework;
using OpenQA.Selenium;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class GroupRemovalTests: AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            app.Groups.GroupPageOpenCheck();
            app.Groups.GroupCreatedCheck();
        }

        [Test]
        public void GroupRemovalTest()
        {
            
            app.Groups.Remove(1);
        }
    }
}