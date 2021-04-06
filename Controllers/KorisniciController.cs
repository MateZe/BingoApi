using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using BingoTestAPI.Models;

namespace BingoTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisniciController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public KorisniciController(IConfiguration configuration)
        {
            this.configuration = configuration;

        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SelectKorisniciAll";
            DataTable table = new();
            string sqlDataSource = configuration.GetConnectionString("BingoAppConnection");
            SqlDataReader myReader;
            using (SqlConnection myConnection = new(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new(query, myConnection) {CommandType = CommandType.StoredProcedure})
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
        public JsonResult Post(Korisnici korisnici)
        {
            string query = @"InsertKorisnik";
            string sqlDataSource = configuration.GetConnectionString("BingoAppConnection");
            SqlDataAdapter myAdapter = new();
            using (SqlConnection myConnection = new(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConnection) { CommandType = CommandType.StoredProcedure })
                {
                    myCommand.Parameters.Add("@Ime_prezime", SqlDbType.VarChar, 50);
                    myCommand.Parameters["@Ime_prezime"].Value = korisnici.Ime_prezime;
                    myCommand.Parameters.Add("@Balans", SqlDbType.Decimal, 18);
                    myCommand.Parameters["@Balans"].Value = korisnici.Balans;
                    myAdapter.InsertCommand = myCommand;
                    myAdapter.InsertCommand.ExecuteNonQuery();

                    myAdapter.Dispose();
                    myCommand.Dispose();
                    myConnection.Close();
                }
            }
            return new JsonResult("Spremanje korisnika uspješno!");
        }

        [HttpPut]
        public JsonResult Put(Korisnici korisnici)
        {
            string query = @"UpdateKorisniciBalans";

            string sqlDataSource = configuration.GetConnectionString("BingoAppConnection");
            SqlDataAdapter myAdapter = new();
            using (SqlConnection myConnection = new(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new(query, myConnection) { CommandType = CommandType.StoredProcedure})
                {
                    myCommand.Parameters.Add("@Id_korisnika", SqlDbType.Int);
                    myCommand.Parameters["@Id_korisnika"].Value = korisnici.Id_korisnika;
                    myCommand.Parameters.Add("@Balans", SqlDbType.Decimal, 18);
                    myCommand.Parameters["@Balans"].Value = korisnici.Balans;
                    myAdapter.UpdateCommand = myCommand;
                    myAdapter.UpdateCommand.ExecuteNonQuery();

                    myCommand.Dispose();
                    myAdapter.Dispose();
                    myConnection.Close();
                }
            }

            return new JsonResult("Ažuriranje korisnika uspješno!");
        }
        [HttpDelete]
        public JsonResult Delete(Korisnici korisnici)
        {
            string query = @"DeleteKorisnik";

            string sqlDataConnection = configuration.GetConnectionString("BingoAppConnection");
            SqlDataAdapter myAdapter = new();

            using(SqlConnection myConnection = new(sqlDataConnection))
            {
                myConnection.Open();
                using(SqlCommand myCommand = new(query, myConnection) { CommandType = CommandType.StoredProcedure})
                {
                    myCommand.Parameters.Add("@Id_korisnika", SqlDbType.Int);
                    myCommand.Parameters["@Id_korisnika"].Value = korisnici.Id_korisnika;
                    myAdapter.DeleteCommand = myCommand;
                    myAdapter.DeleteCommand.ExecuteNonQuery();

                    myCommand.Dispose();
                    myAdapter.Dispose();
                    myConnection.Close();
                }
            }


            return new JsonResult("Brisanje korisnika uspješno!");
        }

    }
}
