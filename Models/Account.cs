using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AuthenticationService.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25), MinLength(2)]
        public string Username { get; set; }
        [Required]
        public byte[] Password { get; set; }
        [Required]
        public byte[] Salt { get; set; }
        public string Token { get; set; }
    }
    public static class ExtensionAccount
    {
        public static Account WithoutPassword(this Account account)
        {
            account.Password = null;
            return account;
        }
    }
}
