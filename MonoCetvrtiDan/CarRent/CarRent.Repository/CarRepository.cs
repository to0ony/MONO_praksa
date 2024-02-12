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
        public List<ICar> GetAllCars(CarFilter filter)
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
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
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
                }
                catch (NpgsqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
            return cars;
        }

        public ICar GetCarById(Guid id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Connection.ConnectionString);
            ICar car = null;
            string commandText = "SELECT * FROM \"Car\" WHERE \"Id\" = @Id";

            using (connection)
            {
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand();
                npgsqlCommand.CommandText = commandText;
                npgsqlCommand.Connection = connection;

                npgsqlCommand.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    NpgsqlDataReader reader = npgsqlCommand.ExecuteReader();
                    while (reader.Read())
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
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
            return car;
        }

        public void CreateCar(ICar newCar)
        {
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
                    insertCommand.ExecuteNonQuery();
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

        //ASYNC HERE!
        public async Task<bool> UpdateCar(Guid id, ICar updatedCar)
        {
            int rowChanged;

            ICar car = GetCarById(id);
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

                updateCommand.Parameters.AddWithValue("@Id", id);
                if (!string.IsNullOrEmpty(updatedCar.Brand))
                {
                    updateCommand.Parameters.AddWithValue("@Brand", updatedCar.Brand);
                }
                if (!string.IsNullOrEmpty(updatedCar.Model))
                {
                    updateCommand.Parameters.AddWithValue("@Model", updatedCar.Model);
                }
                if (updatedCar.ManafactureDate != null)
                {
                    updateCommand.Parameters.AddWithValue("@ManafactureDate", updatedCar.ManafactureDate);
                }
                if (updatedCar.Mileage != null)
                {
                    updateCommand.Parameters.AddWithValue("@Mileage", updatedCar.Mileage);
                }
                if (updatedCar.InsuranceStatus != null)
                {
                    updateCommand.Parameters.Add("@InsuranceStatus", NpgsqlDbType.Bit).Value = updatedCar.InsuranceStatus;
                }
                if (updatedCar.Available != null)
                {
                    updateCommand.Parameters.Add("@Available", NpgsqlDbType.Bit).Value = updatedCar.Available;
                }

                rowChanged = await updateCommand.ExecuteNonQueryAsync();
            }
            return rowChanged != 0;
        }

        public void DeleteCar(Guid id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Connection.ConnectionString))
            {
                string deleteQuery = "DELETE FROM \"Car\" WHERE \"Id\" = @Id";
                NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    deleteCommand.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
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

    }
}
