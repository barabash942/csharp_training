﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Addressbook.Web.Tests
{
    public class GroupHelper: HelperBase
    {

    public GroupHelper(ApplicationManager manager) : base(manager)
    {
    }

        public GroupHelper Create(GroupData groupdata)
        {
            manager.Navigator.GoToGroupsPage();

            InitGroupCreation();
            FillGroupForm(groupdata);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Modify(int v, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();

            if (!IsElementPresent(By.Name("selected[]")))
            {
                GroupData groupData = new GroupData();
                groupData.Name = "Qwe";
                groupData.Header = "rty";
                groupData.Footer = "ewyt";
                Create(groupData);
            }

            SelectGroup(v);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }


        public GroupHelper Remove(int p)
        {
            manager.Navigator.GoToGroupsPage();

            if (!IsElementPresent(By.Name("selected[]")))
            {
                GroupData groupData = new GroupData();
                groupData.Name = "Qwe";
                groupData.Header = "rty";
                groupData.Footer = "ewyt";
                Create(groupData);
            }
            SelectGroup(p);
            RemoveGroup();
            ReturnToGroupsPage();

            return this;
        }

    public GroupHelper InitGroupCreation()
       {
        driver.FindElement(By.Name("new")).Click();
        return this;
        }

    public GroupHelper FillGroupForm(GroupData groupdata)
        {
            Type(By.Name("group_name"), groupdata.Name);
            Type(By.Name("group_header"), groupdata.Header);
            Type(By.Name("group_footer"), groupdata.Footer);
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
        driver.FindElement(By.Name("submit")).Click();
        return this;
        }

    public GroupHelper SelectGroup(int index)
    {
        driver.FindElement(By.XPath("(//input[@name= 'selected[]'])[" + index + "]")).Click();
        return this;
    }

    public GroupHelper ReturnToGroupsPage()
    {
        driver.FindElement(By.LinkText("groups")).Click();
        return this;
    }

    public GroupHelper RemoveGroup()
    {
        driver.FindElement(By.Name("delete")).Click();
        return this;
    }
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }
    }
}
