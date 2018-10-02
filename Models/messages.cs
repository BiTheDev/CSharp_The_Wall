using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class messages{

        public messages(){
            MsgCmt = new List<comments>();
        }

        [Key]
        public long id{get;set;}
        public long usersid{get;set;}

        public string message{get;set;}
        public DateTime created_at{get;set;}
        public DateTime updated_at{get;set;}

        public List<comments> MsgCmt{get;set;}

        public users user{get;set;}

    }
}