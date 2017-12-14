using SampleData.DAL.Cobb;
using SampleData.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Npgsql;
using Newtonsoft.Json;
using System.Configuration;

namespace SampleDataApi.Controllers
{
    public class CobbController : ApiController
    {
        private string connectionString = ConfigurationManager.AppSettings["cobbConnectionString"];

        // GET: api/Cobb
        public IHttpActionResult GetLogs()
        {
            try
            {
                var result = new List<NpgLogInfo>();
                var table = new DataTable();
                using (var da = new NpgsqlDataAdapter("SELECT * FROM \"LogInfos\"", connectionString))
                {
                    da.Fill(table);
                }
                foreach (var item in table.AsEnumerable())
                {
                    result.Add(getInfoFromRow(item));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // GET: api/Cobb/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Cobb
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Cobb/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cobb/5
        public void Delete(int id)
        {
        }

        private NpgLogInfo getInfoFromRow(DataRow row)
        {
            return new NpgLogInfo
            {
                Id = Convert.ToInt16(row[0]),
                LoggerVersionInfo = row[1].ToString(),
                MapInfo = row[2].ToString(),
                VehicleInfo = row[3].ToString()
            };
        }
    }
}
