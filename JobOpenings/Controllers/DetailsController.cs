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
    public class DetailsController : ApiController
    {
        private readonly SqlConnection _con;

        public DetailsController()
        {
            _con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        }


        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var cmd = new SqlCommand(
                    "SELECT jo.JobOpeningId, jo.Title, jo.Description, jo.ClosingDate, " +
                    "l.Id AS LocationId, l.Title AS LocationTitle, l.City, l.State, l.Country, l.Zip, " +
                    "d.DepartmentId, d.Title AS DepartmentTitle " +
                    "FROM JobOpenings jo " +
                    "JOIN Location l ON jo.LocationId = l.Id " +
                    "JOIN Department d ON jo.DepartmentId = d.DepartmentId " +
                    "WHERE jo.JobOpeningId = @Id", _con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    _con.Open();
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        return NotFound();
                    }

                    JobOpeningsModel jobOpening = null;
                    if (reader.Read())
                    {
                        jobOpening = new JobOpeningsModel
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("JobOpeningId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),       
                            ClosingDate = reader.GetDateTime(reader.GetOrdinal("ClosingDate")),
                            Location = new Location
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("LocationId")),
                                Title = reader.GetString(reader.GetOrdinal("LocationTitle")),
                                City = reader.GetString(reader.GetOrdinal("City")),
                                State = reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString(reader.GetOrdinal("State")),
                                Country = reader.GetString(reader.GetOrdinal("Country")),
                                Zip = (int)(reader.IsDBNull(reader.GetOrdinal("Zip")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Zip")))
                            },
                            Department = new Department
                            {
                                DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                                Title = reader.GetString(reader.GetOrdinal("DepartmentTitle"))
                            }
                        };
                    }

                    _con.Close();

            
                    var response = new
                    {
                        id = jobOpening.Id,
                        code = jobOpening.Code,
                        title = jobOpening.Title,
                        description = jobOpening.Description,
                        location = new
                        {
                            id = jobOpening.Location.Id,
                            title = jobOpening.Location.Title,
                            city = jobOpening.Location.City,
                            state = jobOpening.Location.State,
                            country = jobOpening.Location.Country,
                            zip = jobOpening.Location.Zip
                        },
                        department = new
                        {
                            id = jobOpening.Department.DepartmentId,
                            title = jobOpening.Department.Title
                        },
                        closingDate = jobOpening.ClosingDate.ToString("o")
                    };

                    return Ok(response);
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
