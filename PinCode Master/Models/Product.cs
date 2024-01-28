using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PinCode_Master.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Pincode")]
        public string Pincode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        public string Country { get; set; }


        [Required]
        public bool Active { get; set; }
    }
}