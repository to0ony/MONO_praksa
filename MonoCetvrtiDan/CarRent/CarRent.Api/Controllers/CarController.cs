using CarRent.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Web.Http;
using Npgsql;
using NpgsqlTypes;

namespace CarRent.Api.Controllers
{
    public class CarController : ApiController
    {
        private string connectionString = "host=localhost ;port=5432 ;Database=CarRent ;User ID=postgres ;Password=postgres";

        // GET api/values
        [HttpGet]
        public HttpResponseMessage Get([FromBody] CarFilter filter) //DONE
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM \"Car\"";
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    List<Car> cars = new List<Car>();

                    while (reader.Read())
                    {
                        Car car = new Car
                        {
                            Id = (Guid)reader["Id"],
                            Brand = reader["Brand"].ToString(),
                            Model = reader["Model"].ToString(),
                            ManafactureDate = (int)reader["ManafactureDate"],
                            Mileage = (int)reader["Mileage"],
                            InsuranceStatus = (bool)reader["InsuranceStatus"],
                            Available = (bool)reader["Available"]
                        };
                        cars.Add(car);
                    }

                    List<Car> filteredCarsList = cars;
                    if (filter != null)
                    {
                        if (!string.IsNullOrEmpty(filter.Brand))
                            filteredCarsList = filteredCarsList.Where(c => c.Brand == filter.Brand).ToList();

                        if (!string.IsNullOrEmpty(filter.Model))
                            filteredCarsList = filteredCarsList.Where(c => c.Model == filter.Model).ToList();

                        if (filter.ManafactureDate != 0)
                            filteredCarsList = filteredCarsList.Where(c => c.ManafactureDate == filter.ManafactureDate).ToList();

                        if (filter.Mileage != 0)
                            filteredCarsList = filteredCarsList.Where(c => c.Mileage == filter.Mileage).ToList();

                        if (filter.InsuranceStatus)
                            filteredCarsList = filteredCarsList.Where(c => c.InsuranceStatus == filter.InsuranceStatus).ToList();

                        if (filter.Available)
                            filteredCarsList = filteredCarsList.Where(c => c.Available == filter.Available).ToList();
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, filteredCarsList.Select(x => new CarView(x)));
                }
            }
            catch 
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        // GET api/values/5
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM \"Car\" WHERE \"Id\" = @Id";
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Car car = new Car()
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Brand = reader.GetString(reader.GetOrdinal("Brand")),
                                Model = reader.GetString(reader.GetOrdinal("Model")),
                                ManafactureDate = reader.GetInt32(reader.GetOrdinal("ManafactureDate")),
                                Mileage = reader.GetInt32(reader.GetOrdinal("Mileage")),
                                InsuranceStatus = reader.GetBoolean(reader.GetOrdinal("InsuranceStatus")),
                                Available = reader.GetBoolean(reader.GetOrdinal("Available"))
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, car);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "No car found with the specified ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        // POST api/values
        public HttpResponseMessage Post([FromBody] Car car)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO \"Car\" (\"Id\", \"Brand\", \"Model\", \"ManafactureDate\", \"Mileage\", \"InsuranceStatus\", \"Available\") " +
                        "VALUES (@Id, @Brand, @Model, @ManafactureDate, @Mileage, @InsuranceStatus, @Available)";
                    NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                    insertCommand.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = car.Id;
                    insertCommand.Parameters.Add("@Brand", NpgsqlDbType.Char).Value = car.Brand;
                    insertCommand.Parameters.Add("@Model", NpgsqlDbType.Char).Value = car.Model;
                    insertCommand.Parameters.Add("@ManafactureDate", NpgsqlDbType.Integer).Value = car.ManafactureDate;
                    insertCommand.Parameters.Add("@Mileage", NpgsqlDbType.Integer).Value = car.Mileage;
                    insertCommand.Parameters.Add("@InsuranceStatus", NpgsqlDbType.Bit).Value = car.InsuranceStatus;
                    insertCommand.Parameters.Add("@Available", NpgsqlDbType.Bit).Value = car.Available;


                    insertCommand.ExecuteNonQuery();
                    return Request.CreateResponse(HttpStatusCode.OK, "Car has been successfully added!");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        [HttpPut]
        // PUT api/values/5
        public HttpResponseMessage Put(Guid id, [FromBody] Car car)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM \"Car\" WHERE \"Id\" = @Id";
                    NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@Id", id);

                    //int carCount = (int)checkCommand.ExecuteScalar();
                    //if (carCount == 0)
                    //{
                    //    return Request.CreateResponse(HttpStatusCode.NotFound, "Car with the specified ID not found.");
                    //}

                    string updateQuery = "UPDATE \"Car\" SET \"Brand\" = @Brand, \"Model\" = @Model, \"ManafactureDate\" = @ManafactureDate, \"Mileage\" = @Mileage, \"InsuranceStatus\" = @InsuranceStatus, \"Available\" = @Available WHERE \"Id\" = @Id";
                    NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
                    updateCommand.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = car.Id;
                    updateCommand.Parameters.Add("@Brand", NpgsqlDbType.Char).Value = car.Brand;
                    updateCommand.Parameters.Add("@Model", NpgsqlDbType.Char).Value = car.Model;
                    updateCommand.Parameters.Add("@ManafactureDate", NpgsqlDbType.Integer).Value = car.ManafactureDate;
                    updateCommand.Parameters.Add("@Mileage", NpgsqlDbType.Integer).Value = car.Mileage;
                    updateCommand.Parameters.Add("@InsuranceStatus", NpgsqlDbType.Bit).Value = car.InsuranceStatus;
                    updateCommand.Parameters.Add("@Available", NpgsqlDbType.Bit).Value = car.Available;

                    updateCommand.ExecuteNonQuery();

                    return Request.CreateResponse(HttpStatusCode.OK, "Car has been successfully updated!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpDelete]
        // DELETE api/values/5
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM \"Car\" WHERE \"Id\" = @Id";
                    NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Car has been successfully deleted!");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed to delete car!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
