using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using apiBeer.Models;

// For more information on enabling Web API for empty projects
// visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiBeer.Controllers
{
    [Route("api/[controller]")]
    public class BeerController : Controller
    {

        private readonly BeerContext _context;

        public BeerController (BeerContext context)
        {
            _context = context;
            if (_context.BeerItems.Count()==0)
            {
                _context.BeerItems.Add(new BeerItem { Name = "Punk IPA", Style = "IPA" });
                _context.SaveChanges();
            }
        }

        // GET: api/beer
        [HttpGet]
        public IEnumerable<BeerItem> GetAll()
        {
            return _context.BeerItems.ToList();
        }

        // GET api/beer/5
        [HttpGet("{id}", Name ="GetBeer")]
        public IActionResult GetById(int id)
        {
            var item = _context.BeerItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/beer
        [HttpPost]
        public IActionResult Create([FromBody]BeerItem item)
        {
            if (item==null)
            {
                return BadRequest();
            }
            _context.BeerItems.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetBeer", new {id = item.Id},item);
        }

        // PUT api/beer/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]BeerItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var beer = _context.BeerItems.FirstOrDefault(t => t.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            beer.Style = item.Style;
            beer.Name = item.Name;
            beer.Brand = item.Brand;
            beer.Rate = item.Rate;

            _context.BeerItems.Update(beer);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/beer/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var beer = _context.BeerItems.FirstOrDefault(t => t.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            _context.BeerItems.Remove(beer);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
