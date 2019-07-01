using NUnit.Framework;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class GroupRemovalTests: GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            if (GroupData.GetAllFromDb().Count == 0)
            {
                string name = "GroupNameForTest";
                GroupData groupForRemoving = new GroupData(name);
                app.Groups.Create(groupForRemoving);
            }

            List<GroupData> oldGroups = GroupData.GetAllFromDb();
            GroupData toBeRemoved = oldGroups[0];

            app.Groups.Remove(toBeRemoved);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupsCount());

            List<GroupData> newGroups = GroupData.GetAllFromDb();
            
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}