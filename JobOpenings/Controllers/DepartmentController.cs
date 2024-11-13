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
    public class DeparmentController : ApiController
    {
        private readonly SqlConnection _con;

        public DeparmentController()
        {
            _con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        }
        [HttpPost]
        public IHttpActionResult Insert_Department(Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.Title))
            {
                return BadRequest("Invalid department data.");
            }

            string query = "INSERT INTO Department (Title) " +
                           "VALUES (@Title); " +
                           "SELECT SCOPE_IDENTITY();"; 

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _con))
                {
                   
                    cmd.Parameters.AddWithValue("@Title", department.Title);

                    _con.Open();
                    var newId = cmd.ExecuteScalar();
                    if (newId != null)
                    {
                        return Ok(newId.ToString());
                    }
                    else
                    {
                        return BadRequest("Failed to insert department.");
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
        }


        [HttpPut]
        public IHttpActionResult Update(int id, Department Department)
        {
            if (Department == null)
            {
                return BadRequest("Invalid Department ID.");
            }

            try
            {
                using (var cmd = new SqlCommand("UPDATE department SET Title = @Title WHERE DepartmentId = @Id", _con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Title", Department.Title);


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
                    "SELECT DepartmentId," +
                    "Title " +
                    "FROM department " +
                    "WHERE DepartmentId = @Id", _con))
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
