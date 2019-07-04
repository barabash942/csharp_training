using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Addressbook.Web.Tests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAdiingContactToGroup()
        {
            //Arrange
            app.Groups.GroupCreatedCheck();
            app.Contacts.ContactCreatedCheck();

            GroupData group = GroupData.GetAllFromDb()[0];
            ContactData contact = ContactData.GetAllFromDb()[0];
            List<ContactData> oldList = group.GetContacts();
            List<GroupData> allGroups = contact.GetGroups();
            app.Contacts.AbleToAddContactInGroupCheck(allGroups);
            contact = ContactData.GetAllFromDb().Except(group.GetContacts()).First();

            //Act
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            //Assert
            Assert.AreEqual(oldList, newList);
        }
    }
}
