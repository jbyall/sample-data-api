using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SampleData.DAL.Cobb
{
    public class Log
    {
        public Log(string apInfoString)
        {
            var parts = apInfoString.Split('[');
            string replacePattern = @"[\[\]]";
            this.LoggerVersionInfo = Regex.Replace(parts[1], replacePattern, string.Empty);
            this.VehicleInfo = Regex.Replace(parts[2], replacePattern, string.Empty);
            this.MapInfo = Regex.Replace(parts[3], replacePattern, string.Empty);
            this.Logs = new List<LogData>();
        }
        public Log()
        {
            this.Logs = new List<LogData>();
        }
        public ObjectId Id { get; set; }
        public int OldId { get; set; }
        public List<LogData> Logs { get; set; }
        public string LoggerVersionInfo { get; set; }
        public string VehicleInfo { get; set; }
        public string MapInfo { get; set; }
        [NotMapped]
        public int LogCount { get; set; }
    }

    public class LogData
    {
        public double? Time { get; set; }
        public double? AccelPosition { get; set; }
        public double? AmbientAirTemp { get; set; }
        public double? BaroPressure { get; set; }
        public double? Boost { get; set; }
        public int? GearPosition { get; set; }
        public double? IgnitionTiming { get; set; }
        public int? IntakeTemp { get; set; }
        public int? IntakeTempManifold { get; set; }
        public double? ManAbsPress { get; set; }
        public int? RPM { get; set; }
        public double? TDBoostError { get; set; }
        public double? TDIntegral { get; set; }
        public double? TDProportional { get; set; }
        public double? TargetBoost { get; set; }
        public double? TargetBoostAbs { get; set; }
        public double? TargetThrottle { get; set; }
        public int? ThrottlePos { get; set; }
        public int? VehicleSpeed { get; set; }
        public double? WastegateDuty { get; set; }
        public double? WategateMax { get; set; }
    }

    public class NpgLog
    {
        public int Id { get; set; }

        public int LogInfoId { get; set; }
        public NpgLogInfo LogInfo { get; set; }

        public double? Time { get; set; }
        public double? AccelPosition { get; set; }
        public double? AmbientAirTemp { get; set; }
        public double? BaroPressure { get; set; }
        public double? Boost { get; set; }
        public int? GearPosition { get; set; }
        public double? IgnitionTiming { get; set; }
        public int? IntakeTemp { get; set; }
        public int? IntakeTempManifold { get; set; }
        public double? ManAbsPress { get; set; }
        public int? RPM { get; set; }
        public double? TDBoostError { get; set; }
        public double? TDIntegral { get; set; }
        public double? TDProportional { get; set; }
        public double? TargetBoost { get; set; }
        public double? TargetBoostAbs { get; set; }
        public double? TargetThrottle { get; set; }
        public int? ThrottlePos { get; set; }
        public int? VehicleSpeed { get; set; }
        public double? WastegateDuty { get; set; }
        public double? WategateMax { get; set; }
    }

    public class NpgLogInfo
    {
        public NpgLogInfo(string apInfoString)
        {
            var parts = apInfoString.Split('[');
            string replacePattern = @"[\[\]]";
            this.LoggerVersionInfo = Regex.Replace(parts[1], replacePattern, string.Empty);
            this.VehicleInfo = Regex.Replace(parts[2], replacePattern, string.Empty);
            this.MapInfo = Regex.Replace(parts[3], replacePattern, string.Empty);
            this.Logs = new List<Log>();
        }
        public NpgLogInfo()
        {
            this.Logs = new List<Log>();
        }
        public int Id { get; set; }

        public List<Log> Logs { get; set; }

        public string LoggerVersionInfo { get; set; }
        public string VehicleInfo { get; set; }
        public string MapInfo { get; set; }
        [NotMapped]
        public int LogCount { get; set; }
    }
}
