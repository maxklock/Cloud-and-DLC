using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace MyGameApi.Controllers
{
    using System.Configuration;

    using CloudAndDlc.Models;

    using HighscoreItemDb = MyGameApi.HighscoreItem;

    public class HighscoreController : ApiController
    {
        private static readonly Lazy<CloudDataContainer> DbContextFaktory = new Lazy<CloudDataContainer>(() => new CloudDataContainer());

        public CloudDataContainer DbContext => DbContextFaktory.Value;

        // GET api/values
        public HighscoreList Get()
        {
            return new HighscoreList
            {
                Items = DbContext.HighscoreItems.Select(i => new HighscoreItem
                {
                    Points = i.Points,
                    Name = i.Name
                }).OrderByDescending(i => i.Points).ToArray(),
            };
        }
        
        // POST api/values
        public HighscoreList Post([FromBody]HighscoreItem value)
        {
            if (Request.Headers.Authorization.Parameter != "S0jieghJHFcddWHE4jP9OsjoP4Y1XTue")
            {
                return null;
            }

            DbContext.HighscoreItems.Add(new HighscoreItemDb
            {
                Points = value.Points,
                Name = value.Name
            });
            DbContext.SaveChanges();

            return new HighscoreList
            {
                Items = DbContext.HighscoreItems.Select(i => new HighscoreItem
                {
                    Points = i.Points,
                    Name = i.Name
                }).OrderByDescending(i => i.Points).ToArray(),
            };
        }

    }
}
