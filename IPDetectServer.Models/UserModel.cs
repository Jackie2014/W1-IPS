using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Models
{
    public class UserModel
    {
        public int UserType
        {
            get;
            set;
        }

        public string LoginName
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string Province
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public string LastUpdatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public DateTime LastUpdatedDate
        {
            get;
            set;
        }

        public DateTime LastLoginDate
        {
            get;
            set;
        }


    }
}
