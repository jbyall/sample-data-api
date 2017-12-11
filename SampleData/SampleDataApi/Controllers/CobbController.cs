using SampleData.DAL.Cobb;
using SampleData.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleDataApi.Controllers
{
    public class CobbController : ApiController
    {
        // GET: api/Cobb
        public IEnumerable<Log> GetLogs()
        {
            var context = new SampleDbContext();
            return context.GetLogInfos();
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
    }
}
