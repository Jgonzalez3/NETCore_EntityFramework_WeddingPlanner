using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models{
    public class Wedding{
        public int WeddingId {get;set;}
        public string wedderone {get;set;}
        public string weddertwo {get;set;}
        public DateTime date {get;set;}
        public string address {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public List<Guest> guests {get;set;}
        public Wedding(){
            guests = new List<Guest>();
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }
    }
}