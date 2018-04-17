using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models{
    public class User{
        public int UserId {get;set;}
        public string firstname {get;set;}
        public string lastname {get;set;}
        public string email {get;set;}
        public string password {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        public List<Guest> guests {get;set;}
        public List<Wedding> weddings {get;set;}
        public User(){
            weddings = new List<Wedding>();
            guests = new List<Guest>();
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }
    }
}