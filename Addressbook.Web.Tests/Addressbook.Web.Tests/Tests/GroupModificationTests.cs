using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            if (GroupData.GetAllFromDb().Count == 0)
            {
                string name = "GroupNameForTest";
                GroupData groupForModication = new GroupData(name);
                app.Groups.Create(groupForModication);
            }

            GroupData newData = new GroupData();
            newData.Name = "UpdatedText";
            newData.Header = null;
            newData.Footer = null;

            List<GroupData> oldGroups = GroupData.GetAllFromDb();
            GroupData oldData = oldGroups[0];

            app.Groups.Modify(oldData, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupsCount());

            List<GroupData> newGroups = GroupData.GetAllFromDb();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
