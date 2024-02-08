//using CarRent.Api.Models;
//using Npgsql;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//Nije gotovo

//namespace CarRent.Api.Controllers
//{
//    public class ClientController : ApiController
//    {
//        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=CarRent";

//        [HttpGet]
//        public HttpResponseMessage Get(Guid id)
//        {
//            try
//            {
//                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
//                {
//                    connection.Open();

//                    string query = "SELECT * FROM \"Client\" WHERE \"Id\" = @Id";
//                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
//                    command.Parameters.AddWithValue("@Id", id);

//                    NpgsqlDataReader reader = command.ExecuteReader();

//                    if (reader.Read())
//                    {
//                        Client client = new Client()
//                        {
//                            Id = (Guid)reader["Id"],
//                            Name = reader["Name"].ToString(),
//                            Surname = reader["Surname"].ToString(),
//                            AcquiredCar = (bool)reader["AcquiredCar"],
//                            ContractDuration = (int)reader["ContractDuration"]
//                        };
//                        return Request.CreateResponse(HttpStatusCode.OK, client);
//                    }
//                    else
//                    {
//                        return Request.CreateResponse(HttpStatusCode.NotFound, "No such ID in base!");
//                    }
//                }
//            }
//            catch
//            {
//                return Request.CreateResponse(HttpStatusCode.InternalServerError);
//            }
//        }

//        [HttpPost]
//        public HttpResponseMessage Post([FromBody] Client client)
//        {
//            try
//            {
//                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
//                {
//                    connection.Open();

//                    string checkQuery = "SELECT COUNT(*) FROM \"Client\" WHERE \"Id\" = @Id";
//                    NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
//                    checkCommand.Parameters.AddWithValue("@Id", client.Id);

//                    int conflictChecker = (int)checkCommand.ExecuteScalar();
//                    if (conflictChecker > 0)
//                    {
//                        return Request.CreateResponse(HttpStatusCode.Conflict);
//                    }

//                    string insertQuery = "INSERT INTO \"Client\" (\"Id\", \"Name\", \"Surname\", \"AcquiredCar\", \"ContractDuration\") " +
//                        "VALUES (@Id, @Name, @Surname, @AcquiredCar, @ContractDuration)";
//                    NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
//                    insertCommand.Parameters.AddWithValue("@Id", client.Id);
//                    insertCommand.Parameters.AddWithValue("@Name", client.Name);
//                    insertCommand.Parameters.AddWithValue("@Surname", client.Surname);
//                    insertCommand.Parameters.AddWithValue("@AcquiredCar", client.AcquiredCar);
//                    insertCommand.Parameters.AddWithValue("@ContractDuration", client.ContractDuration);

//                    insertCommand.ExecuteNonQuery();
//                    return Request.CreateResponse(HttpStatusCode.OK, "Client has been successfully added!");
//                }
//            }
//            catch
//            {
//                return Request.CreateResponse(HttpStatusCode.InternalServerError);
//            }
//        }

//        [HttpPut]
//        public HttpResponseMessage Put(Guid id, [FromBody] Client client)
//        {
//            try
//            {
//                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
//                {
//                    connection.Open();

//                    string checkQuery = "SELECT COUNT(*) FROM \"Client\" WHERE \"Id\" = @Id";
//                    NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
//                    checkCommand.Parameters.AddWithValue("@Id", id);

//                    int IdChecker = (int)checkCommand.ExecuteScalar();

//                    if (IdChecker == 0)
//                    {
//                        return Request.CreateResponse(HttpStatusCode.NotFound, "No such ID in base!");
//                    }

//                    string updateQuery = "UPDATE \"Client\" SET \"Name\" = @Name, \"Surname\" = @Surname, \"AcquiredCar\" = @AcquiredCar, \"ContractDuration\" = @ContractDuration WHERE \"Id\" = @Id";
//                    NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
//                    updateCommand.Parameters.AddWithValue("@Id", id);
//                    updateCommand.Parameters.AddWithValue("@Name", client.Name);
//                    updateCommand.Parameters.AddWithValue("@Surname", client.Surname);
//                    updateCommand.Parameters.AddWithValue("@AcquiredCar", client.AcquiredCar);
//                    updateCommand.Parameters.AddWithValue("@ContractDuration", client.ContractDuration);

//                    return Request.CreateResponse(HttpStatusCode.OK, "Client has been successfully updated!");
//                }
//            }
//            catch
//            {
//                return Request.CreateResponse(HttpStatusCode.InternalServerError);
//            }
//        }

//        [HttpDelete]
//        public HttpResponseMessage Delete(Guid id)
//        {
//            try
//            {
//                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
//                {
//                    connection.Open();

//                    string checkQuery = "SELECT COUNT(*) FROM \"Client\" WHERE \"Id\" = @Id";
//                    NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
//                    checkCommand.Parameters.AddWithValue("@Id", id);

//                    int IdChecker = (int)checkCommand.ExecuteScalar();

//                    if (IdChecker == 0)
//                    {
//                        return Request.CreateResponse(HttpStatusCode.NotFound, "No such ID in base!");
//                    }

//                    string deleteQuery = "DELETE FROM \"Client\" WHERE \"Id\" = @Id";
//                    NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteQuery, connection);
//                    deleteCommand.Parameters.AddWithValue("@Id", id);

//                    return Request.CreateResponse(HttpStatusCode.OK, "Client has been successfully deleted!");
//                }
//            }
//            catch
//            {
//                return Request.CreateResponse(HttpStatusCode.InternalServerError);
//            }
//        }
//    }
//}
