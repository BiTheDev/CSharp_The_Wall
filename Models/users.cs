using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class users{

        public users(){
            Usermessages = new List<messages>();
            UserComment = new List<comments>();
        }
        
        [Key]
        public long id{get;set;}
        

        [Required]
        public string first_name{get; set;}

        [Required]
        public string last_name{get; set;}


        [Required]
        public string email {get; set;}


        [Required]
        public string password {get; set;}

        public DateTime created_at{get;set;}
        public DateTime updated_at{get; set;}

        public List<messages> Usermessages{get;set;}

        public List<comments> UserComment{get; set;}


    }
}