using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Course
{
    [Table("Users")]
    public class User
    {
        private string login, password, role, name;
        
        [Key]
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public User() { }

        public User(string Login, string Password, string Role, string Name)
        {
            this.Login = Login;
            this.Password = Password;
            this.Role = Role;
            this.Name = Name;
        }
    }
}
