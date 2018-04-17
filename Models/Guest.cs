using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models{
    public class Guest{
        public int GuestId {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public int WeddingId {get;set;}
        public Wedding Wedding {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        public Guest(){
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }
    }
}