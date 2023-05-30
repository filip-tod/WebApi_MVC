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

namespace WebApi.Controllers
{
    public class CouponController : ApiController
    {
        private static readonly string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=playerdb;";

        //Bildabilno i radi
        public HttpResponseMessage Get()
        {
            List<object> coupon = new List<object>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                {
                    string query = $"SELECT  * FROM coupon";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int Id = reader.GetInt32(reader.GetOrdinal("id"));
                                int Pricediscount = reader.GetInt32(reader.GetOrdinal("pricediscount"));
                                bool Couponvalidation = reader.GetBoolean(reader.GetOrdinal("couponvalidation"));
                                DateTime Expiredate = reader.GetDateTime(reader.GetOrdinal("expiredate"));
                                long Fk_coupon_users = reader.GetInt64(reader.GetOrdinal("fk_coupon_users"));


                                var newCoupon = new { id = Id, pricediscount = Pricediscount, couponvalidation = Couponvalidation, expiredate = Expiredate, fk_coupon_users = Fk_coupon_users };
                                coupon.Add(newCoupon);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, coupon);
        }
        // bildabilno i radi
        public HttpResponseMessage GetElementById(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM coupon WHERE id = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("id"));
                            int Pricediscount = reader.GetInt32(reader.GetOrdinal("pricediscount"));
                            bool Couponvalidation = reader.GetBoolean(reader.GetOrdinal("couponvalidation"));
                            DateTime Expiredate = reader.GetDateTime(reader.GetOrdinal("expiredate"));
                            long Fk_coupon_users = reader.GetInt64(reader.GetOrdinal("fk_coupon_users"));

                            var couponById = new { Id = id, pricediscount = Pricediscount, couponvalidation = Couponvalidation,
                                                       expiredate = Expiredate, fk_coupon_users = Fk_coupon_users};
                            return Request.CreateResponse(HttpStatusCode.OK, couponById);

                        }
                    }
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        //bildabilno i radi
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
                    command.Parameters.AddWithValue("@pricediscount", couponData.pricediscount);
                    command.Parameters.AddWithValue("@couponvalidation", couponData.couponvalidation);
                    command.Parameters.AddWithValue("@expiredate", couponData.expiredate);
                    command.Parameters.AddWithValue("@fk_coupon_users", couponData.fk_coupon_users);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.Created, "you have inserted data successfully!");
        }
        // bildabilno i radi
        public HttpResponseMessage Put(int id, CouponModel coupon)
        {
            if (id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request!!!");
            }
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE coupon SET pricediscount = @pricediscount, couponvalidation = @couponvalidation, expiredate =@expiredate, fk_coupon_users = @fk_coupon_users WHERE id = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@pricediscount", coupon.pricediscount);
                    command.Parameters.AddWithValue("@couponvalidation", coupon.couponvalidation);
                    command.Parameters.AddWithValue("@expiredate", coupon.expiredate);
                    command.Parameters.AddWithValue("@fk_coupon_users", coupon.fk_coupon_users);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.Created, "you have inserted data successfully!");
        }
        //bildabilno i radi
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