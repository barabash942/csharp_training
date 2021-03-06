﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace Addressbook.Web.Tests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactData GetContactInfoFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(lastName, firstName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();

            ClearGroupFilter();
            SelectContactById(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddedContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(5))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        private void SelectContactById(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void CommitAddedContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();

            SelectGroupForContactRemoving(group.Name);
            SelectContactById(contact.Id);
            SelectGroupForContactRemoving(group.Name);
            DeleteContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(5))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void SelectGroupForContactRemoving(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        private void DeleteContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void AbleToAddContactInGroupCheck(List<GroupData> groupContactList)
        {
            List<GroupData> allGroups = GroupData.GetAllFromDb();

            if (allGroups.Count.Equals(groupContactList.Count))
            {
                ContactData contact = new ContactData("lastName1", "firstName1");
                Create(contact);
            }
        }

        public void ContactAddedInGroupCheck(ContactData contact, List<ContactData> groupList, GroupData group)
        {
            if (groupList.Count == 0)
            {
                AddContactToGroup(contact, group);
            }
        }

        public ContactData GetContactInfoFromDetails(int index)
        {
            manager.Navigator.OpenHomePage();
            Thread.Sleep(250);
            GoToContactDetails(0);

            string allContactDetails = driver.FindElement(By.XPath("//div[@id='content']")).Text;

            return new ContactData()
            {
                AllContactDetails = allContactDetails
            };
        }

        public ContactData GetContactInfoFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(0);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(lastName, firstName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();

                manager.Navigator.OpenHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name = entry]"));

                foreach (IWebElement element in elements)
                {
                    var lastName = element.FindElement(By.XPath(".//td[2]"));
                    var firstName = element.FindElement(By.XPath(".//td[3]"));
                    contactCache.Add(new ContactData(lastName.Text, firstName.Text));
                }
            }
            return new List<ContactData>(contactCache);
        }

        public void OpenHomePageCheck()
        {
            if (!IsHomePageOpen())
            {
                manager.Navigator.OpenHomePage();
            }
        }

        public void ContactCreatedCheck()
        {
            if (!IsAnyContactCreated())
            {
                ContactData newcontact = new ContactData();
                newcontact.FirstName = "Phill";
                newcontact.LastName = "X";
                Create(newcontact);
            }
        }

        public bool IsAnyContactCreated()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public bool IsHomePageOpen()
        {
            return driver.Url == manager.Navigator.baseURL;
        }

        public ContactHelper Create(ContactData contactData)
        {
            InitContactCreation();
            FillContactForm(contactData);
            SubmitContactCreation();
            return this;
        }

        public ContactHelper Modify(int v, int e, ContactData newData)
        {
            SelectContact(v);
            InitContactModification(e);
            FillContactForm(newData);
            SubmitContactModification();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            SelectContact(p);
            DeleteContact();
            SubmitContactDeleting();
            new WebDriverWait(driver, TimeSpan.FromSeconds(5))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            SelectContact(contact.Id);
            DeleteContact();
            SubmitContactDeleting();
            return this;
        }

        private ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath("(//input[@name= 'selected[]' and @value='"+id+"'])")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name= 'selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper GoToContactDetails(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value= 'Delete']")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactDeleting()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        protected ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        protected ContactHelper FillContactForm(ContactData contactData)
        {
            Type(By.Name("firstname"), contactData.FirstName);
            Type(By.Name("lastname"), contactData.LastName);
            return this;
        }

        protected ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Enter']")).Click();
            contactCache = null;
            return this;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();

            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
