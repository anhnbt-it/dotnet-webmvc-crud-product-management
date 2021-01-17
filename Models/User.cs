using System;
using System.Web;

namespace crud_web_application.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
    }
}