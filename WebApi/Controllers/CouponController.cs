using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using BackUp_MVC.Models;
using Microsoft.Ajax.Utilities;
using System.Web.Helpers;

namespace WebApi.Controllers
{
    public class CouponController : ApiController
    {
        private static readonly string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=playerdb;";


        public HttpResponseMessage Get()
        {
            List<CouponUsersModel> coupon = new List<CouponUsersModel>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                {
                    string query = $"select a.pricediscount , a.couponvalidation, a.expiredate, b.nameuser , b.lastnameuser , b.email  FROM coupon a inner join users b on a.fk_coupon_users  = b.id  ";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               
                                int Pricediscount = reader.GetInt32(reader.GetOrdinal("pricediscount"));
                                bool Couponvalidation = reader.GetBoolean(reader.GetOrdinal("couponvalidation"));
                                DateTime Expiredate = reader.GetDateTime(reader.GetOrdinal("expiredate"));
                                string NameUser = reader.GetString(reader.GetOrdinal("nameuser"));
                                string LastNameUser = reader.GetString(reader.GetOrdinal("lastnameuser"));
                                string Email = reader.GetString(reader.GetOrdinal("email"));


                                var newCoupon = new CouponUsersModel
                                {
                                    PriceDiscount = Pricediscount,
                                    CouponValidation = Couponvalidation,
                                    ExpireDate = Expiredate,
                                    NameUser = NameUser,
                                    LastNameUser = LastNameUser,
                                    Email = Email

                                };
                                coupon.Add(newCoupon);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, coupon);
        }
 
        public HttpResponseMessage GetElementById(int id)
        {
            List<CouponUsersModel> coupon = new List<CouponUsersModel>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT a.pricediscount , a.couponvalidation, a.expiredate, b.nameuser , b.lastnameuser , b.email  FROM coupon a inner join users b on a.fk_coupon_users  = b.id  WHERE a.id = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            int Pricediscount = reader.GetInt32(reader.GetOrdinal("pricediscount"));
                            bool Couponvalidation = reader.GetBoolean(reader.GetOrdinal("couponvalidation"));
                            DateTime Expiredate = reader.GetDateTime(reader.GetOrdinal("expiredate"));
                            string NameUser = reader.GetString(reader.GetOrdinal("nameuser"));
                            string LastNameUser = reader.GetString(reader.GetOrdinal("lastnameuser"));
                            string Email = reader.GetString(reader.GetOrdinal("email"));


                            var newCoupon = new CouponUsersModel
                            {
                                PriceDiscount = Pricediscount,
                                CouponValidation = Couponvalidation,
                                ExpireDate = Expiredate,
                                NameUser = NameUser,
                                LastNameUser = LastNameUser,
                                Email = Email

                            };
                            coupon.Add(newCoupon);
                            return Request.CreateResponse(HttpStatusCode.OK, coupon);

                        }
                    }
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        
        public HttpResponseMessage Post(CouponModel couponData)
        { 
            if (couponData == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Coupon data is empty or missing something");
            }
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO coupon  (pricediscount, couponvalidation, expiredate, fk_coupon_users) VALUES (@pricediscount, @couponvalidation, @expiredate, @fk_coupon_users)";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@pricediscount", couponData.Pricediscount);
                    command.Parameters.AddWithValue("@couponvalidation", couponData.Couponvalidation);
                    command.Parameters.AddWithValue("@expiredate", couponData.Expiredate);
                    command.Parameters.AddWithValue("@fk_coupon_users", couponData.Fk_coupon_users);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.Created, "you have inserted data successfully!");
        }
      
        public HttpResponseMessage Put(int id, CouponModel coupon)
        {
            if (id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request!!!");
            }
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE coupon SET ";
                List<string> updateFields = new List<string>();

                if (coupon.Pricediscount != 0)
                {
                    updateFields.Add("pricediscount = @pricediscount");
                }

                if (coupon.Couponvalidation != false)
                {
                    updateFields.Add("couponvalidation = @couponvalidation");
                }

                if (coupon.Expiredate != null)
                {
                    updateFields.Add("expiredate = @expiredate");
                }

                if (coupon.Fk_coupon_users != 0)
                {
                    updateFields.Add("fk_coupon_users = @fk_coupon_users");
                }

                query += string.Join(", ", updateFields);
                query += " WHERE id = @id";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    if (coupon.Pricediscount != 0)
                    {
                        command.Parameters.AddWithValue("@pricediscount", coupon.Pricediscount);
                    }

                    if (coupon.Couponvalidation != false)
                    {
                        command.Parameters.AddWithValue("@couponvalidation", coupon.Couponvalidation);
                    }

                    if (coupon.Expiredate != null)
                    {
                        command.Parameters.AddWithValue("@expiredate", coupon.Expiredate);
                    }

                    if (coupon.Fk_coupon_users != 0)
                    {
                        command.Parameters.AddWithValue("@fk_coupon_users", coupon.Fk_coupon_users);
                    }

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.Created, "you have inserted data successfully!");
        }
    
        public HttpResponseMessage Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM coupon WHERE id = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent, "no connection");
                    }
                }
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "coupon was deleted!");
            }
        }
    }
}