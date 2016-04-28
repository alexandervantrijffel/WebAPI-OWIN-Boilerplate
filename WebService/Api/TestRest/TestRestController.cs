using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Structura.WebApiOwinBoilerPlate.WebService.JsonConfiguration;

namespace Structura.WebApiOwinBoilerPlate.WebService.Api.TestRest
{
    /// <summary>
    /// Example RESTful ApiController 
    /// </summary>
    [RoutePrefix("api/testrest")]
    // [EnableCors("*", "*", "GET,POST,PUT,DELETE")]
    public class TestRestController : ApiController
    { 
        private static SimpleTestRestValuesCollection _values = new SimpleTestRestValuesCollection(new Dictionary<int, string>
            {
                {1, "value1"},
                {2, "value2"}
            });

        [Route("")]
        public HttpResponseMessage GetAll()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _values);
            return Request.CreateResponse(HttpStatusCode.NotModified, Enumerable.Empty<string>());
        }

        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            if (!_values.ContainsKey(id))
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError($"Value with id {id} not found."));

            return Request.CreateResponse(HttpStatusCode.OK, _values[id]);
        }

        [Route("")]
        public HttpResponseMessage Post([FromBody] AddOrUpdateTestRestValueDto args)
        {
            var newValue = _values.Create(args.Value);
            return Request.CreateResponse(HttpStatusCode.Created,
                new { Url = JsonConfigAccessor.Config.FullHostUrl + $"/api/testrest/{newValue.Key}" });
        }

        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, [FromBody] AddOrUpdateTestRestValueDto args)
        {
            if (!_values.ContainsKey(id))
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError($"Value with id {id} not found"));
            _values[id] = args.Value;
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

        /// <summary>
        /// Test URL with invalid date such as http://URL:PORT/api/testrest/badargtest/123 
        /// instead of a valid date such as http://URL:PORT/api/testrest/badargtest/2016-07-18T21:00:00.000Z
        /// </summary>
        [Route("badargtest/{time}")]
        [HttpGet]
        public HttpResponseMessage BadArgTest(DateTime time)
        {
            return Request.CreateResponse(HttpStatusCode.OK, 
                new { ReceivedDateTime = $"{time.ToLongDateString()} {time.ToShortTimeString()}" });
        }

        [Route("exceptiontest")]
        [HttpGet]
        public HttpResponseMessage ExceptionTest()
        {
            throw new Exception("Ugh, that was a bad idea Joe.");
        }
    }
}
