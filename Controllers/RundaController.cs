using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using BingoTestAPI.Models;

namespace BingoTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RundaController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public RundaController(IConfiguration configuration)
        {
            this.configuration = configuration; 
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SelectRundaAll";

            DataTable table = new();

            string sqlDataSource = configuration.GetConnectionString("BingoAppConnection");
            SqlDataReader myReader;
            using (SqlConnection myConnection = new(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new(query, myConnection) { CommandType = CommandType.StoredProcedure })
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCommand.Dispose();
                    myConnection.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(Runda runda)
        {
            string query = @"InsertRunda";

            string sqlDataSource = configuration.GetConnectionString("BingoAppConnection");
            SqlDataAdapter myAdapter = new();
            using(SqlConnection myConnection = new(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new(query, myConnection) { CommandType = CommandType.StoredProcedure})
                {
                    myCommand.Parameters.Add("@Izvuceni_brojevi", SqlDbType.VarChar, 104);
                    myCommand.Parameters["@Izvuceni_brojevi"].Value = runda.Izvuceni_brojevi;
                    myCommand.Parameters.Add("@Pocetak_runde", SqlDbType.DateTime2, 0);
                    myCommand.Parameters["@Pocetak_runde"].Value = runda.Pocetak_runde;
                    myCommand.Parameters.Add("@Kraj_runde", SqlDbType.DateTime2, 0);
                    myCommand.Parameters["@Kraj_runde"].Value = runda.Kraj_runde;

                    myAdapter.InsertCommand = myCommand;
                    myAdapter.InsertCommand.ExecuteNonQuery();

                    myAdapter.Dispose();
                    myCommand.Dispose();
                    myConnection.Close();
                }
            }

            return new JsonResult("Spremanje runde uspješno!");

        }

        [HttpPut]
        public JsonResult Put(Runda runda)
        {
            string query = @"UpdateRunda";

            string sqlDataSource = configuration.GetConnectionString("BingoAppConnection");
            SqlDataAdapter myAdapter = new();
            using(SqlConnection myConnection = new(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new(query, myConnection) { CommandType = CommandType.StoredProcedure })
                {
                    myCommand.Parameters.AddWithValue("@Id_runde", runda.Id_runde);
                    myCommand.Parameters.Add("@Kraj_runde", SqlDbType.DateTime2, 0);
                    myCommand.Parameters["@Kraj_runde"].Value = runda.Kraj_runde;

                    myAdapter.UpdateCommand = myCommand;
                    myAdapter.UpdateCommand.ExecuteNonQuery();

                    myAdapter.Dispose();
                    myCommand.Dispose();
                    myConnection.Close();
                }
            }
            return new JsonResult("Ažuriranje runde uspješno!");
        }
        [HttpDelete]
        public JsonResult Delete(Runda runda)
        {
            string query = @"DeleteRunda";

            string sqlDataConnection = configuration.GetConnectionString("BingoAppConnection");
            SqlDataAdapter myAdapter = new();
            using(SqlConnection myConnection = new(sqlDataConnection))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new(query, myConnection) { CommandType = CommandType.StoredProcedure})
                {
                    myCommand.Parameters.AddWithValue("@Id_runde", runda.Id_runde);
                    myAdapter.DeleteCommand = myCommand;
                    myAdapter.DeleteCommand.ExecuteNonQuery();

                    myAdapter.Dispose();
                    myCommand.Dispose();
                    myConnection.Close();
                }
            }

            return new JsonResult("Brisanje runde uspješno!");
        }

    }
}
