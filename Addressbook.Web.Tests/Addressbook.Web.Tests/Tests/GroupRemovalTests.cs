using NUnit.Framework;
using OpenQA.Selenium;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class GroupRemovalTests: AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            
            app.Groups.Remove(1);
        }
    }
}