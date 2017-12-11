using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using SampleData.DAL.Cobb;
using SampleData.DAL;


namespace SampleData.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongo = new SampleDbContext();
            var connString = "Server = localhost; Database = Cobb; User Id=postgres; Password=blue1234;Database=Cobb";
            var mongoLogs = new List<Log>();

            using (var connection = new NpgsqlConnection(connString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM \"LogInfos\"", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var npgId = reader.GetString(0);
                            var mongoLog = new Log();
                            mongoLog.OldId = Convert.ToInt16(reader.GetString(0));
                            mongoLog.LoggerVersionInfo = reader.GetString(1);
                            mongoLog.MapInfo = reader.GetString(2);
                            mongoLog.VehicleInfo = reader.GetString(3);
                            //using (var logCommand = new NpgsqlCommand())
                            //{
                            
                            //}
                            mongoLogs.Add(mongoLog);
                        }
                    }
                }
                foreach (var item in mongoLogs)
                {
                    using (var command = new NpgsqlCommand($"SELECT * FROM \"Logs\" WHERE \"LogInfoId\"={item.OldId.ToString()}", connection))
                    {
                        using (var logReader = command.ExecuteReader())
                        {
                            while (logReader.Read())
                            {
                                item.Logs.Add(new LogData
                                {
                                    AccelPosition = logReader.GetDouble(1),
                                    AmbientAirTemp = logReader.GetDouble(2),
                                    BaroPressure = logReader.GetDouble(3),
                                    Boost = logReader.GetDouble(4),
                                    GearPosition = logReader.GetInt16(5),
                                    IgnitionTiming = logReader.GetDouble(6),
                                    IntakeTemp = logReader.GetInt16(7),
                                    IntakeTempManifold = logReader.GetInt16(8),
                                    RPM = logReader.GetInt16(11),
                                    TDBoostError = logReader.GetDouble(12),
                                    TargetBoost = logReader.GetDouble(15),
                                    TargetBoostAbs = logReader.GetDouble(16),
                                    TargetThrottle = logReader.GetDouble(17),
                                    ThrottlePos = logReader.GetInt16(18),
                                    Time = logReader.GetDouble(19),
                                    VehicleSpeed = logReader.GetInt16(20),
                                    WastegateDuty = logReader.GetDouble(21),

                                });
                            }
                        }
                    }
                    mongo.AddLog(item);
                }

                
            }
        }
    }
}
