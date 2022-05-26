using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using DataLayer;
using DataLayer.DataAccess;

namespace Category.Controllers
{
    [ApiController]
    [Route("Category")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IDbRepository _db;

        public CategoryController(ILogger<CategoryController> logger, IDbRepository db)
        {
            _logger = logger;
            _db = db;

            _db.InitializeConnections();
        }

        [HttpGet]
        public IEnumerable<Entities.Category> ReadCategories()
        {
            SqLiteConnector conn = new();
            var items = conn.GetAll<Entities.Category>("Category").ToList();
            return items;
            
        }

        [HttpGet("{id}")]
        public ActionResult<Entities.Category> ReadCategory(int id)
        {
            SqLiteConnector conn = new();
            Entities.Category item = conn.GetById<Entities.Category>(id, "category");
            if(item == null)
            {
                return NotFound();  
            } else
            {
                return Ok(item);
            }
        }

        [HttpPost]
        public ActionResult<Entities.Category> CreateCategory(Entities.Category param)
        {
            Dictionary<string, string> parameters = new()
            {
                { "name", $"{param.Name}" },
                { "description", $"{param.Description}" }
            };
            SqLiteConnector conn = new();
            var result = conn.Create<Entities.Category>("Category", parameters);
            if (result == 0)
            {
                return BadRequest();
            } else
            {
                return Ok(result);
            }
            
        }


    }
}
