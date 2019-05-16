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
using REST_Api.Models;

namespace REST_Api.Controllers
{
    public class Kolonne2Controller : ApiController
    {
        private FluidContext db = new FluidContext();

        // GET: api/Kolonne2
        public IQueryable<Kolonne2> GetKolonne2()
        {
            return db.Kolonne2;
        }

        // GET: api/Kolonne2/5
        [ResponseType(typeof(Kolonne2))]
        public IHttpActionResult GetKolonne2(int id)
        {
            Kolonne2 kolonne2 = db.Kolonne2.Find(id);
            if (kolonne2 == null)
            {
                return NotFound();
            }

            return Ok(kolonne2);
        }

        // PUT: api/Kolonne2/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKolonne2(int id, Kolonne2 kolonne2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kolonne2.ID)
            {
                return BadRequest();
            }

            db.Entry(kolonne2).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Kolonne2Exists(id))
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

        // POST: api/Kolonne2
        [ResponseType(typeof(Kolonne2))]
        public IHttpActionResult PostKolonne2(Kolonne2 kolonne2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kolonne2.Add(kolonne2);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kolonne2.ID }, kolonne2);
        }

        // DELETE: api/Kolonne2/5
        [ResponseType(typeof(Kolonne2))]
        public IHttpActionResult DeleteKolonne2(int id)
        {
            Kolonne2 kolonne2 = db.Kolonne2.Find(id);
            if (kolonne2 == null)
            {
                return NotFound();
            }

            db.Kolonne2.Remove(kolonne2);
            db.SaveChanges();

            return Ok(kolonne2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Kolonne2Exists(int id)
        {
            return db.Kolonne2.Count(e => e.ID == id) > 0;
        }
    }
}