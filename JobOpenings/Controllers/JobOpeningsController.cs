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
    public class JobOpeningsController : ApiController
    {
        private readonly SqlConnection _con;

        public JobOpeningsController()
        {
            _con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        }
        [HttpPost]
        public IHttpActionResult Insert(JobOpeningsModel jobOpening)
        {
            if (jobOpening == null)
            {
                return BadRequest("Invalid job opening data.");
            }

            var response = string.Empty;

          
            jobOpening.ClosingDate = jobOpening.ClosingDate == default ? DateTime.Now.AddMonths(1) : jobOpening.ClosingDate;

            string query = "INSERT INTO JobOpenings (Title, Description, LocationId, DepartmentId, ClosingDate) " +
                           "VALUES (@Title, @Description, @LocationId, @DepartmentId, @ClosingDate); " +
                           "SELECT SCOPE_IDENTITY();";

            try
            {
                using (var cmd = new SqlCommand(query, _con))
                {
                    cmd.Parameters.AddWithValue("@Title", jobOpening.Title);
                    cmd.Parameters.AddWithValue("@Description", jobOpening.Description);
                    cmd.Parameters.AddWithValue("@LocationId", jobOpening.Location.Id);
                    cmd.Parameters.AddWithValue("@DepartmentId", jobOpening.Department.DepartmentId);
                    cmd.Parameters.AddWithValue("@ClosingDate", jobOpening.ClosingDate);

                    _con.Open();
                    var newId = cmd.ExecuteScalar();
                    response = newId != null ? newId.ToString() : "error";
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

            return Ok(response);
        }


        [HttpPut]
        public IHttpActionResult Update(int id, JobOpeningsModel jobOpening)
        {
            if (jobOpening == null)
            {
                return BadRequest("Invalid job opening data.");
            }

            try
            {
                using (var cmd = new SqlCommand("UPDATE JobOpenings SET Title = @Title, Description = @Description, " +
                                                "LocationId = @LocationId, DepartmentId = @DepartmentId, ClosingDate = @ClosingDate WHERE JobOpeningId = @Id", _con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Title", jobOpening.Title);
                    cmd.Parameters.AddWithValue("@Description", jobOpening.Description);
                    cmd.Parameters.AddWithValue("@LocationId", jobOpening.Location.Id);
                    cmd.Parameters.AddWithValue("@DepartmentId", jobOpening.Department.DepartmentId);
                    cmd.Parameters.AddWithValue("@ClosingDate", jobOpening.ClosingDate);

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
            try
            {
                using (var cmd = new SqlCommand(
                    "SELECT jo.JobOpeningId, 'JOB-' + RIGHT('00' + CAST(jo.JobOpeningId AS VARCHAR(2)), 2) AS Code, " +
                    "jo.Title, jo.Description, jo.ClosingDate " +
                    "FROM JobOpenings jo " +
                    "WHERE jo.JobOpeningId = @Id", _con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    _con.Open();
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    _con.Close();

                    if (dt.Rows.Count == 0)
                    {
                        return NotFound();
                    }

                    return Ok(dt);
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
