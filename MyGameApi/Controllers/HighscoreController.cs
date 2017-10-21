using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace MyGameApi.Controllers
{
    using CloudAndDlc.Models;

    public class HighscoreController : ApiController
    {
        // GET api/values
        public HighscoreList Get()
        {
            return new HighscoreList
            {
                Items = new []
                {
                    new HighscoreItem
                    {
                        Name = "Max Mustermann",
                        Points = 42
                    }
                }.OrderBy(i => i.Points).ToArray()
            };
        }
        
        // POST api/values
        public HighscoreList Post([FromBody]HighscoreItem value)
        {
            if (Request.Headers.Authorization.Parameter != "S0jieghJHFcddWHE4jP9OsjoP4Y1XTue")
            {
                return null;
            }

            return new HighscoreList
            {
                Items = new[]
                {
                    new HighscoreItem
                    {
                        Name = "Max Mustermann",
                        Points = 42
                    },
                    value
                }.OrderByDescending(i => i.Points).ToArray()
            };
        }

    }
}
