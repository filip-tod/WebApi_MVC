using System;

namespace WebApi.Models
{
    public class CouponModel
    {
        public int Id { get; set; }
        public int pricediscount { get; set; }
        public bool couponvalidation { get; set; }
        public DateTime expiredate { get; set; }

        public long fk_coupon_users { get; set; }
    }
}