using Newtonsoft.Json;
using System;

namespace WebApi.Models
{
    public class CouponUsersModel
    {
        public int PriceDiscount { get; set; }
        public bool CouponValidation { get; set; }
        public DateTime ExpireDate { get; set; }
        public string NameUser { get; set; }
        public string LastNameUser { get; set; }
        public string Email { get; set; }
    }
}