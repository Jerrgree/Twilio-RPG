using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Password
    {
        public int Id { get; set; }
        public string PasswordText { get; set; }
        public int User_Id { get; set; }
        public User User { get; set; }
        public bool IsActive { get; set; }
    }
}
