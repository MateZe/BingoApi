using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using BingoTestAPI.Models;

namespace BingoTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListicController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ListicController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SelectListiciAll";

            DataTable table = new();

            string sqlDataSource = configuration.GetConnectionString("BingoAppConnection");
            SqlDataReader myReader;
            using (SqlConnection myConnection = new(sqlDataSource))
            {
                myConnection.Open();
                using SqlCommand myCommand = new(query, myConnection);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myConnection.Close();
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Listic listic)
        {
            string query = @"InsertListic";

            string sqlDataSource = configuration.GetConnectionString("BingoAppConnection");
            SqlDataAdapter myAdapter = new();

            using(SqlConnection myConnection = new(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConnection) { CommandType = CommandType.StoredProcedure })
                {
                    myCommand.Parameters.AddWithValue("@Korisnik_id", listic.Korisnik_id);
                    myCommand.Parameters.AddWithValue("@Runda_id", listic.Runda_id);
                    myCommand.Parameters.AddWithValue("@Odigrani_brojevi", listic.Odigrani_brojevi);
                    myCommand.Parameters.AddWithValue("@Izvuceni_brojevi", listic.Izvuceni_brojevi);
                    myCommand.Parameters.AddWithValue("@Ulog", listic.Ulog);
                    myCommand.Parameters.AddWithValue("@Dobit", listic.Dobit);

                    myAdapter.InsertCommand = myCommand;
                    myAdapter.InsertCommand.ExecuteNonQuery();

                    myCommand.Dispose();
                    myCommand.Dispose();
                    myConnection.Close();
                }
            }

            return new JsonResult("Spremanje listića uspješno!");
        }

        [HttpPut]
        public JsonResult Put(Listic listic)
        {
            string query = @"UpdateListicUlog";

            string sqlDataConnection = configuration.GetConnectionString("BingoAppConnection");
            SqlDataAdapter myAdapter = new();

            using(SqlConnection myConnection = new(sqlDataConnection))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new(query, myConnection) { CommandType = CommandType.StoredProcedure })
                {
                    myCommand.Parameters.AddWithValue("@Id_listica", listic.Id_listica);
                    myCommand.Parameters.AddWithValue("@Ulog", listic.Ulog);

                    myAdapter.UpdateCommand = myCommand;
                    myAdapter.UpdateCommand.ExecuteNonQuery();

                    myCommand.Dispose();
                    myAdapter.Dispose();
                    myConnection.Close();
                }
            }
            return new JsonResult("Ažuriranje uloga listića uspješno!");
        }

        [HttpDelete]
        public JsonResult Delete(Listic listic)
        {
            string query = @"DeleteListic";
            string sqlDataConnection = configuration.GetConnectionString("BingoAppConnection");
            SqlDataAdapter myAdapter = new();
            using(SqlConnection myConnection = new(sqlDataConnection)){
                myConnection.Open();
                using(SqlCommand myCommand = new(query, myConnection) { CommandType= CommandType.StoredProcedure})
                {
                    myCommand.Parameters.AddWithValue("@Id_listica", listic.Id_listica);
                    myAdapter.DeleteCommand = myCommand;
                    myAdapter.DeleteCommand.ExecuteNonQuery();

                    myAdapter.Dispose();
                    myCommand.Dispose();
                    myConnection.Close();
                }

            }

            return new JsonResult("Brisanje listića je uspješno!");
        }
    }
}
