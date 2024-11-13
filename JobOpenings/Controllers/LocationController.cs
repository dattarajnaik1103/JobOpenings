using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using JobOpenings.Models;
using Newtonsoft.Json;
using System.Data;
using System;
using System.Drawing;

namespace JobOpenings.Controllers
{
    public class LocationController : ApiController
    {
        private readonly SqlConnection _con;

        public LocationController()
        {
            _con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        }
        [HttpPost]
        public IHttpActionResult Insert_Location(Location location)
        {
            if (location == null || string.IsNullOrEmpty(location.Title) || string.IsNullOrEmpty(location.City) || string.IsNullOrEmpty(location.Country))
            {
                return BadRequest("Invalid location data.");
            }

            string query = "INSERT INTO Location (Id,Title, City, State, Country, Zip) " +
                           "VALUES (@Id,@Title, @City, @State, @Country, @Zip); " +
                           "SELECT SCOPE_IDENTITY();"; 

            try
            {
               
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                    cmd.Parameters.AddWithValue("@Id", location.Id);
                    cmd.Parameters.AddWithValue("@Title", location.Title);
                    cmd.Parameters.AddWithValue("@City", location.City);
                    cmd.Parameters.AddWithValue("@State", location.State ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Country", location.Country);
                    cmd.Parameters.AddWithValue("@Zip", (object)location.Zip ?? DBNull.Value);

                    _con.Open();
                    var newId = cmd.ExecuteScalar(); // Get ID of the newly inserted row
                    if (newId != null)
                    {
                        return Ok(location.Id.ToString());
                    }
                    else
                    {
                        return BadRequest("Failed to insert location.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return InternalServerError(sqlEx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                _con.Close();
            }
        }

        [HttpPut]
        public IHttpActionResult Update(int id, Location location)
        {
            if (location == null || string.IsNullOrEmpty(location.Title) || string.IsNullOrEmpty(location.City) || string.IsNullOrEmpty(location.Country))
            {
                return BadRequest("Invalid location data.");
            }

            string query = "UPDATE Location SET Id=@Id, Title = @Title, City = @City, State = @State, Country = @Country, Zip = @Zip WHERE Id = @Id";

            try
            {
                using (var cmd = new SqlCommand(query, _con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Title", location.Title);
                    cmd.Parameters.AddWithValue("@City", location.City);
                    cmd.Parameters.AddWithValue("@State", (object)location.State ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Country", location.Country);
                    cmd.Parameters.AddWithValue("@Zip", (object)location.Zip ?? DBNull.Value);

                    _con.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Update successful");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                _con.Close();
            }
        }



        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            string query = "SELECT Id, Title, City, State, Country, Zip FROM Location WHERE Id = @Id";

            try
            {
                using (var cmd = new SqlCommand(query, _con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    _con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var location = new Location
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                City = reader.GetString(reader.GetOrdinal("City")),
                                State = reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString(reader.GetOrdinal("State")),
                                Country = reader.GetString(reader.GetOrdinal("Country")),
                                Zip = (int)(reader.IsDBNull(reader.GetOrdinal("Zip")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Zip")))
                            };

                            return Ok(location);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                if (_con.State == ConnectionState.Open)
                {
                    _con.Close();
                }
            }
        }






    }
}
