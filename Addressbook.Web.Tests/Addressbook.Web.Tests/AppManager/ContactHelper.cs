using System;
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

        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();
            manager.Navigator.OpenHomePage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name = entry]"));

            foreach (IWebElement element in elements)
            {
                var lastName = element.FindElement(By.XPath(".//td[2]"));
                var firstName = element.FindElement(By.XPath(".//td[3]"));
                contacts.Add(new ContactData(lastName.Text, firstName.Text));
            }

            return contacts;
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
            SubmitGroupModification();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            SelectContact(p);
            DeleteContact();
            SubmitContactDeleting();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name= 'selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(.//input[@name= 'selected[]'])[" + (index + 1) + "][1]/following::img[2]")).Click();
            return this;
        }
        //В InitContactModification добавлен index, чтобы редактировать именно тот контакт, который выделен. 
        //Иначе неизвестно, какой контакт будет редактироваться, т.к. значок карандаша напротив каждого контакта.

        public ContactHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value= 'Delete']")).Click();
            return this;
        }

        public ContactHelper SubmitContactDeleting()
        {
            driver.SwitchTo().Alert().Accept();
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
            return this;
        }
    }
}
