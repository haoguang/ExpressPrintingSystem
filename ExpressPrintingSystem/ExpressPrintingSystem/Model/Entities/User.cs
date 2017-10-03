using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class User
    {
        private string id;
        private string name;
        private string role;
        private string email;

        public User(string id, string name, string role, string email)
        {
            this.id = id;
            this.name = name;
            this.role = role;
            this.email = email; 
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }




    }
}