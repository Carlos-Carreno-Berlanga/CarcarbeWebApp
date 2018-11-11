using Carcarbe.Shared.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Carcarbe.Shared.Repository
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private string connectionString;
        private readonly ILogger<MeasurementRepository> _logger;
        public MeasurementRepository(IConfiguration configuration, ILogger<MeasurementRepository> logger)
        {
            _logger = logger;
            connectionString = Environment.GetEnvironmentVariable("DB_INFO", EnvironmentVariableTarget.Machine);
            _logger.LogInformation($"logger connectionString {connectionString}");
            Console.WriteLine($" console connectionString {connectionString}");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(Measurement item)
        {
            
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                dbConnection.Execute("INSERT INTO measurement (value, measurement_type, time_stamp_timezone ) VALUES(@Value,@MeasurementType::measurementtype,@TimeStamp)",
                    new
                    {
                        Value = Math.Round((double)item.Value, 2),
                        MeasurementType = item.MeasurementType.ToString(),
                        TimeStamp = DateTime.Now
                    });
            }

        }

        public async Task AddAsync(Measurement item)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

                    await dbConnection.ExecuteAsync("INSERT INTO measurement (value, measurement_type, time_stamp_timezone ) VALUES(@Value,@MeasurementType::measurementtype,@TimeStamp)",
                        new
                        {
                            item.Value,
                            MeasurementType = item.MeasurementType.ToString(),
                            TimeStamp = DateTime.Now
                        });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public IEnumerable<Measurement> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Measurement>("SELECT * FROM customer");
            }
        }

        public Measurement FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Measurement>("SELECT * FROM customer WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM customer WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(Measurement item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE customer SET name = @Name,  phone  = @Phone, email= @Email, address= @Address WHERE id = @Id", item);
            }
        }
    }
}
