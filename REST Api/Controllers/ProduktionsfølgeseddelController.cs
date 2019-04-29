using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using REST_Api;

namespace REST_Api.Controllers
{
    public class ProduktionsfølgeseddelController : ApiController
    {
        private FluidContext db = new FluidContext();

        // GET: api/Produktionsfølgeseddel
        public IQueryable<Produktionsfølgeseddel> GetProduktionsfølgeseddel()
        {
            return db.Produktionsfølgeseddel;
        }

        // GET: api/Produktionsfølgeseddel/5
        [ResponseType(typeof(Produktionsfølgeseddel))]
        public IHttpActionResult GetProduktionsfølgeseddel(int id)
        {
            Produktionsfølgeseddel produktionsfølgeseddel = db.Produktionsfølgeseddel.Find(id);
            if (produktionsfølgeseddel == null)
            {
                return NotFound();
            }

            return Ok(produktionsfølgeseddel);
        }

        // PUT: api/Produktionsfølgeseddel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduktionsfølgeseddel(int id, Produktionsfølgeseddel produktionsfølgeseddel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produktionsfølgeseddel.ID)
            {
                return BadRequest();
            }

            db.Entry(produktionsfølgeseddel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduktionsfølgeseddelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Produktionsfølgeseddel
        [ResponseType(typeof(Produktionsfølgeseddel))]
        public IHttpActionResult PostProduktionsfølgeseddel(Produktionsfølgeseddel produktionsfølgeseddel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Produktionsfølgeseddel.Add(produktionsfølgeseddel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = produktionsfølgeseddel.ID }, produktionsfølgeseddel);
        }

        // DELETE: api/Produktionsfølgeseddel/5
        [ResponseType(typeof(Produktionsfølgeseddel))]
        public IHttpActionResult DeleteProduktionsfølgeseddel(int id)
        {
            Produktionsfølgeseddel produktionsfølgeseddel = db.Produktionsfølgeseddel.Find(id);
            if (produktionsfølgeseddel == null)
            {
                return NotFound();
            }

            db.Produktionsfølgeseddel.Remove(produktionsfølgeseddel);
            db.SaveChanges();

            return Ok(produktionsfølgeseddel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProduktionsfølgeseddelExists(int id)
        {
            return db.Produktionsfølgeseddel.Count(e => e.ID == id) > 0;
        }
    }
}