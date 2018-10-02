using System;
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class comments{

        
        [Key]
        public long id{get;set;}
        public long usersid{get;set;}
        public long messagesid{get;set;}
        public messages message{get; set;}
        public users user{get;set;}
        public string comment{get; set;}

        public DateTime created_at{get;set;}
        public DateTime updated_at{get;set;}
    }
}