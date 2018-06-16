using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace WeddingPlanner.Models{
    public class CreateWeddingViewModel{
        [Required(ErrorMessage = "Wedder One required")]
        public string wedderone {get;set;}
        [Required(ErrorMessage = "Wedder Two required")]
        public string weddertwo {get;set;}
        [Required(ErrorMessage = "Wedding Date required")]
        public DateTime? date {get;set;}
        [Required(ErrorMessage = "Wedding Address required")]
        public string address {get;set;}
    }
}