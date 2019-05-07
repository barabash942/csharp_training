using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace Addressbook.Web.Tests.AppManager
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contactData)
        {
            manager.Navigator.OpenHomePage();

            InitContactCreation();
            FillContactForm(contactData);
            SubmitContactCreation();
            return this;
        }

        public ContactHelper Modify(int v, int e, ContactData newData)
        {
            manager.Navigator.OpenHomePage();

            SelectContact(v);
            InitContactModification(e);
            FillContactForm(newData);
            SubmitGroupModification();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            manager.Navigator.OpenHomePage();

            SelectContact(p);
            DeleteContact();
            SubmitContactDeleting();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name= 'selected[]'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(.//input[@name= 'selected[]'])[" + index + "][1]/following::img[2]")).Click();
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
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contactData.FirstName);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contactData.LastName);
            return this;
        }

        protected ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Enter']")).Click();
            return this;
        }
    }
}
