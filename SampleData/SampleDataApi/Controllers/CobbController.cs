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
    [RoutePrefix("api/Logs")]
    public class CobbController : ApiController
    {
        private string connectionString = ConfigurationManager.AppSettings["cobbConnectionString"];

        // GET: api/Cobb
        [HttpGet]
        [Route("")]
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
                foreach (var row in table.AsEnumerable())
                {
                    result.Add(getItemFromDataRow<NpgLogInfo>(row));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = new List<NpgLog>();
                var table = new DataTable();
                using (var da = new NpgsqlDataAdapter($"SELECT * FROM \"Logs\" WHERE \"LogInfoId\" = {id.ToString()}", connectionString))
                {
                    da.Fill(table);
                }
                foreach (var row in table.AsEnumerable())
                {
                    var logToAdd = getItemFromDataRow<NpgLog>(row);
                    result.Add(logToAdd);
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
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

        private T getItemFromDataRow<T>(DataRow row) where T: new()
        {
            T item = new T();
            foreach (DataColumn c in row.Table.Columns)
            {
                var p = item.GetType().GetProperty(c.ColumnName);
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
            return item;
        }
    }
}
