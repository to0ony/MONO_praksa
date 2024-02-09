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
    [RoutePrefix("api/Car")]
    public class CarController : ApiController
    {
        private string connectionString = "host=localhost ;port=5432 ;Database=CarRent ;User ID=postgres ;Password=postgres";

        [HttpGet]
        [Route("")]
        // GET api/values
        public IHttpActionResult GetAllCars([FromUri] CarFilter filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM \"Car\" WHERE 1=1";

                    if (filter != null)
                    {
                        if (!string.IsNullOrEmpty(filter.Brand))
                        {
                            query += " AND \"Brand\" = @Brand";
                        }

                        if (!string.IsNullOrEmpty(filter.Model))
                        {
                            query += " AND \"Model\" = @Model";
                        }

                        if (filter.ManafactureDate.HasValue)
                        {
                            query += " AND \"ManafactureDate\" = @ManafactureDate";
                        }

                        if (filter.Mileage.HasValue)
                        {
                            query += " AND \"Mileage\" = @Mileage";
                        }

                        if (filter.InsuranceStatus.HasValue)
                        {
                            query += " AND \"InsuranceStatus\" = @InsuranceStatus";
                        }

                        if (filter.Available.HasValue)
                        {
                            query += " AND \"Available\" = @Available";
                        }
                    }

                    NpgsqlCommand command = new NpgsqlCommand(query, connection);

                    if (filter != null)
                    {
                        if (!string.IsNullOrEmpty(filter.Brand))
                        {
                            command.Parameters.AddWithValue("@Brand", filter.Brand);
                        }

                        if (!string.IsNullOrEmpty(filter.Model))
                        {
                            command.Parameters.AddWithValue("@Model", filter.Model);
                        }

                        if (filter.ManafactureDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@ManafactureDate", filter.ManafactureDate.Value);
                        }

                        if (filter.Mileage.HasValue)
                        {
                            command.Parameters.AddWithValue("@Mileage", filter.Mileage.Value);
                        }

                        if (filter.InsuranceStatus.HasValue)
                        {
                            command.Parameters.AddWithValue("@InsuranceStatus", filter.InsuranceStatus.Value);
                        }

                        if (filter.Available.HasValue)
                        {
                            command.Parameters.AddWithValue("@Available", filter.Available.Value);
                        }
                    }

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

                    return Ok(cars.Select(x => new CarView(x)));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        // GET api/values/5
        public IHttpActionResult GetCarById(Guid id)
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
                            return Ok(new CarView(car));
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
        }

        [HttpPost]
        [Route("")]
        // POST api/values
        public IHttpActionResult CreateCar([FromUri] Car car)
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
                    return Ok("Car has been successfully added!");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        // PUT api/values/5
        public IHttpActionResult Put(Guid id, [FromUri] Car car)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Construct the base update query
                    string updateQuery = "UPDATE \"Car\" SET";
                    List<string> updateFields = new List<string>();

                    // Check each field and add it to the update command if it's provided
                    if (!string.IsNullOrEmpty(car.Brand))
                    {
                        updateFields.Add("\"Brand\" = @Brand");
                    }
                    if (!string.IsNullOrEmpty(car.Model))
                    {
                        updateFields.Add("\"Model\" = @Model");
                    }
                    if (car.ManafactureDate != default(int)) // Assuming default(int) means not provided
                    {
                        updateFields.Add("\"ManafactureDate\" = @ManafactureDate");
                    }
                    if (car.Mileage != default(int))
                    {
                        updateFields.Add("\"Mileage\" = @Mileage");
                    }
                    if (car.InsuranceStatus != default(bool))
                    {
                        updateFields.Add("\"InsuranceStatus\" = @InsuranceStatus");
                    }
                    if (car.Available != default(bool))
                    {
                        updateFields.Add("\"Available\" = @Available");
                    }

                    // Join all update fields
                    updateQuery += " " + string.Join(", ", updateFields);
                    updateQuery += " WHERE \"Id\" = @Id";

                    NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);

                    // Add parameters for fields that are provided
                    updateCommand.Parameters.AddWithValue("@Id", id);
                    if (!string.IsNullOrEmpty(car.Brand))
                    {
                        updateCommand.Parameters.AddWithValue("@Brand", car.Brand);
                    }
                    if (!string.IsNullOrEmpty(car.Model))
                    {
                        updateCommand.Parameters.AddWithValue("@Model", car.Model);
                    }
                    if (car.ManafactureDate != default(int))
                    {
                        updateCommand.Parameters.AddWithValue("@ManafactureDate", car.ManafactureDate);
                    }
                    if (car.Mileage != default(int))
                    {
                        updateCommand.Parameters.AddWithValue("@Mileage", car.Mileage);
                    }
                    if (car.InsuranceStatus != default(bool))
                    {
                        updateCommand.Parameters.AddWithValue("@InsuranceStatus", car.InsuranceStatus);
                    }
                    if (car.Available != default(bool))
                    {
                        updateCommand.Parameters.AddWithValue("@Available", car.Available);
                    }

                    updateCommand.ExecuteNonQuery();

                    return Ok("Car has been successfully updated!");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        // DELETE api/values/5
        public IHttpActionResult Delete(Guid id)
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
                        return Ok("Car has been successfully deleted!");
                    }
                    else
                    {
                        return InternalServerError();
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
