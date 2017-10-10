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
        private const char SEPERATOR = ';';
        

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


        public static string toCompactString(User user)
        {
            return user.ID + SEPERATOR + user.Name + SEPERATOR + user.Role + SEPERATOR + user.Email;
        }

        public static User toUserObject(string user)
        {
            string[] temp = user.Split(SEPERATOR);

            return new User(temp[0], temp[1], temp[2], temp[3]);
        }


    }
}