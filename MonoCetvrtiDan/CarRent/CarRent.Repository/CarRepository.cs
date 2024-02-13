using CarRent.Common;
using CarRent.Model;
using CarRent.Model.Common;
using CarRent.Repository.Common;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Repository
{
    public class CarRepository : ICarRepository
    {
        public async Task<List<ICar>> GetAllCars(CarFilter filter)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Connection.ConnectionString);
            List<ICar> cars = new List<ICar>();

            using (connection)
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                StringBuilder queryBuilder = new StringBuilder("SELECT * FROM \"Car\"");
                if (filter != null)
                {
                    ApplyFilter(command, filter);
                }
                command.CommandText = queryBuilder.ToString();
                try
                {
                    await connection.OpenAsync();
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
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
                }
                catch (NpgsqlException ex)
                {
                    throw ex;
                }
            }
            return cars;
        }

        public async Task<ICar> GetCarById(Guid id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Connection.ConnectionString))
            {
                ICar car = null;
                string commandText = "SELECT * FROM \"Car\" WHERE \"Id\" = @Id";

                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(commandText, connection))
                {
                    npgsqlCommand.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        await connection.OpenAsync();

                        using (NpgsqlDataReader reader = await npgsqlCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                car = new Car()
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Brand = reader.GetString(reader.GetOrdinal("Brand")),
                                    Model = reader.GetString(reader.GetOrdinal("Model")),
                                    ManafactureDate = reader.GetInt32(reader.GetOrdinal("ManafactureDate")),
                                    Mileage = reader.GetInt32(reader.GetOrdinal("Mileage")),
                                    InsuranceStatus = reader.GetBoolean(reader.GetOrdinal("InsuranceStatus")),
                                    Available = reader.GetBoolean(reader.GetOrdinal("Available"))
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception appropriately
                        throw ex;
                    }
                }
                return car;
            }
        }

        public async Task<bool> CreateCar(ICar newCar)
        {
            int rowChanged;
            NpgsqlConnection connection = new NpgsqlConnection(Connection.ConnectionString);
            using (connection)
            {
                string insertQuery = "INSERT INTO \"Car\" (\"Id\", \"Brand\", \"Model\", \"ManafactureDate\", \"Mileage\", \"InsuranceStatus\", \"Available\") " +
                        "VALUES (@Id, @Brand, @Model, @ManafactureDate, @Mileage, @InsuranceStatus, @Available)";
                NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                insertCommand.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = newCar.Id;
                insertCommand.Parameters.Add("@Brand", NpgsqlDbType.Char).Value = newCar.Brand;
                insertCommand.Parameters.Add("@Model", NpgsqlDbType.Char).Value = newCar.Model;
                insertCommand.Parameters.Add("@ManafactureDate", NpgsqlDbType.Integer).Value = newCar.ManafactureDate;
                insertCommand.Parameters.Add("@Mileage", NpgsqlDbType.Integer).Value = newCar.Mileage;
                insertCommand.Parameters.Add("@InsuranceStatus", NpgsqlDbType.Bit).Value = newCar.InsuranceStatus;
                insertCommand.Parameters.Add("@Available", NpgsqlDbType.Bit).Value = newCar.Available;
                try
                {
                    connection.Open();
                    rowChanged = await insertCommand.ExecuteNonQueryAsync();
                    return rowChanged != 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<bool> UpdateCar(Guid id, ICar updatedCar)
        {
            int rowChanged;

            Task<ICar> car = GetCarById(id);
            if (car == null)
            {
                throw new Exception("No car with such ID in base!");
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(Connection.ConnectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE \"Car\" SET";
                List<string> updateFields = new List<string>();

                if (!string.IsNullOrEmpty(updatedCar.Brand))
                {
                    updateFields.Add("\"Brand\" = @Brand");
                }
                if (!string.IsNullOrEmpty(updatedCar.Model))
                {
                    updateFields.Add("\"Model\" = @Model");
                }
                if (updatedCar.ManafactureDate != null)
                {
                    updateFields.Add("\"ManafactureDate\" = @ManafactureDate");
                }
                if (updatedCar.Mileage != null)
                {
                    updateFields.Add("\"Mileage\" = @Mileage");
                }
                if (updatedCar.InsuranceStatus != null)
                {
                    updateFields.Add("\"InsuranceStatus\" = @InsuranceStatus");
                }
                if (updatedCar.Available != null)
                {
                    updateFields.Add("\"Available\" = @Available");
                }

                updateQuery += " " + string.Join(", ", updateFields);
                updateQuery += " WHERE \"Id\" = @Id";

                NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
                AddCarParameters(updateCommand, id, updatedCar);

                rowChanged = await updateCommand.ExecuteNonQueryAsync();
            }
            return rowChanged != 0;
        }

        public async Task<bool> DeleteCar(Guid id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Connection.ConnectionString))
            {
                string deleteQuery = "DELETE FROM \"Car\" WHERE \"Id\" = @Id";
                NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@Id", id);

                try
                {
                    await connection.OpenAsync();

                    int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
                catch (NpgsqlException ex)
                {
                    throw ex;
                }
            }
        }


        private void ApplyFilter(NpgsqlCommand command, CarFilter filter)
        {
            string query = "SELECT * FROM \"CAR\" WHERE 1=1";

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Brand))
                {
                    query += " AND \"Brand\" = @Brand";
                    command.Parameters.AddWithValue("@Brand", filter.Brand);
                }

                if (!string.IsNullOrEmpty(filter.Model))
                {
                    query += " AND \"Model\" = @Model";
                    command.Parameters.AddWithValue("@Model", filter.Model);
                }

                if (filter.ManafactureDate.HasValue)
                {
                    query += " AND \"ManufactureDate\" = @ManufactureDate";
                    command.Parameters.AddWithValue("@ManufactureDate", filter.ManafactureDate.Value);
                }

                if (filter.Mileage.HasValue)
                {
                    query += " AND \"Mileage\" = @Mileage";
                    command.Parameters.AddWithValue("@Mileage", filter.Mileage.Value);
                }

                if (filter.InsuranceStatus.HasValue)
                {
                    query += " AND \"InsuranceStatus\" = @InsuranceStatus";
                    command.Parameters.AddWithValue("@InsuranceStatus", filter.InsuranceStatus.Value);
                }

                if (filter.Available.HasValue)
                {
                    query += " AND \"Available\" = @Available";
                    command.Parameters.AddWithValue("@Available", filter.Available.Value);
                }
            }
            command.CommandText = query;
        }

        private void AddCarParameters(NpgsqlCommand command, Guid id, ICar updatedCar)
        {
            command.Parameters.AddWithValue("@Id", id);
            if (!string.IsNullOrEmpty(updatedCar.Brand))
            {
                command.Parameters.AddWithValue("@Brand", updatedCar.Brand);
            }
            if (!string.IsNullOrEmpty(updatedCar.Model))
            {
                command.Parameters.AddWithValue("@Model", updatedCar.Model);
            }
            if (updatedCar.ManafactureDate != null)
            {
                command.Parameters.AddWithValue("@ManafactureDate", updatedCar.ManafactureDate);
            }
            if (updatedCar.Mileage != null)
            {
                command.Parameters.AddWithValue("@Mileage", updatedCar.Mileage);
            }
            if (updatedCar.InsuranceStatus != null)
            {
                command.Parameters.Add("@InsuranceStatus", NpgsqlDbType.Bit).Value = updatedCar.InsuranceStatus;
            }
            if (updatedCar.Available != null)
            {
                command.Parameters.Add("@Available", NpgsqlDbType.Bit).Value = updatedCar.Available;
            }
        }
    }
}
