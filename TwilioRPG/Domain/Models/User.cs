using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public bool IsActive { get; set; }
        public int CurrentPasswordId { get; set; }
        public DbSet<Password> Passwords { get; set; }
    }
}