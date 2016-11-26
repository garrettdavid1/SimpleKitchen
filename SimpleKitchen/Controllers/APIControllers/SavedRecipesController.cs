using SimpleKitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleKitchen.Controllers.APIControllers
{
    public class SavedRecipesController : ApiController
    {

        ApplicationDbContext context = new ApplicationDbContext();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        public IHttpActionResult PostFavorite(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}