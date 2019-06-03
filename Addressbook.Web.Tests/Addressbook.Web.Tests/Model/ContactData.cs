using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Addressbook.Web.Tests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allContactDetails;

        public ContactData(string lastName, string firstName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public ContactData()
        {

        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Id { get; set; }

        public string Address { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanupPhones(HomePhone) + CleanupPhones(MobilePhone) + CleanupPhones(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (MakeEmailToConcat(Email) + MakeEmailToConcat(Email2) + MakeEmailToConcat(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllContactDetails
        {
            get
            {
                if (allContactDetails != null)
                {
                    return allContactDetails;
                }
                else
                {
                    return MakeContactDetailsToTest(allContactDetails).Trim();
                }
            }
            set
            {
                allContactDetails = value;
            }
        }

        public string MakeContactDetailsToTest(string allContactDetails)
        {
            if (FirstName == null || FirstName == "")
            {
                return "";
            }
            else
            {
                return FirstName;
            }

            if (LastName == null || LastName == "")
            {
                return "";
            }
            else
            {
                return LastName;
            }

            if (Address == null || Address == "")
            {
                return "";
            }
            else
            {
                return Address;
            }

            if (HomePhone == null || HomePhone == "")
            {
                return "";
            }
            else
            {
                return HomePhone;
            }

            if (MobilePhone == null || MobilePhone == "")
            {
                return "";
            }
            else
            {
                return MobilePhone;
            }

            if (WorkPhone == null || WorkPhone == "")
            {
                return "";
            }
            else
            {
                return WorkPhone;
            }

            if (Email == null || Email == "")
            {
                return "";
            }
            else
            {
                return Email;
            }

            if (Email2 == null || Email2 == "")
            {
                return "";
            }
            else
            {
                return Email2;
            }

            if (Email3 == null || Email3 == "")
            {
                return "";
            }
            else
            {
                return Email3;
            }

            allContactDetails = FirstName + " " + LastName + "\r\n"
                                + Address + "\r\n" + "\r\n" + "H: " + HomePhone + "\r\n"
                                + "M: " + MobilePhone + "\r\n" + "W: " + WorkPhone + "\r\n"
                                + Email + "\r\n" + Email2 + "\r\n" + Email3;
            return allContactDetails;
        }

        private string CleanupPhones(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        private string MakeEmailToConcat(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";
        }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        private string DisplayName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            return
                LastName.CompareTo(other.LastName) != 0
                    ? LastName.CompareTo(other.LastName)
                    : FirstName.CompareTo(other.FirstName);
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return LastName == other.LastName && FirstName == other.FirstName;
        }

        public override int GetHashCode()
        {
            return DisplayName.GetHashCode();
        }

        public override string ToString()
        {
            return "FirstName= " + FirstName + "LastName= " + LastName 
                + "\nAddress = " + Address
                + "\nHomePhone = " + HomePhone + "\nMobilePhone = " 
                + MobilePhone + "\nWorkPhone = " + WorkPhone
                + "\nEmail = " + Email + "\nEmail2 = " 
                + Email2 + "\nEmail3 = " + Email3;
        }
    }
}