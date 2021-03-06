﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.View;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthenticationService.Models
{
    public class Account
    {
        [BsonId]
        public Guid Id { get; set; }
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

        public static Account TakeOverVariables(this Account account, Account toTakeOver)
        {
            account.Username = toTakeOver.Username;
            account.Password = toTakeOver.Password;
            account.Salt = toTakeOver.Salt;
            account.Token = toTakeOver.Token;
            return account;
        }

        public static ViewAccount toViewAccount(this Account account)
        {
            return new ViewAccount()
            {
                Id = account.Id,
                Name = account.Username
            };
        }
    }
}
