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
    public class ForsidesController : ApiController
    {
        private FluidContext db = new FluidContext();

        // GET: api/Forsides
        public IQueryable<Forside> GetForside()
        {
            return db.Forside;
        }

        // GET: api/Forsides/5
        [ResponseType(typeof(Forside))]
        public IHttpActionResult GetForside(int id)
        {
            Forside forside = db.Forside.Find(id);
            if (forside == null)
            {
                return NotFound();
            }

            return Ok(forside);
        }

        // PUT: api/Forsides/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutForside(int id, Forside forside)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != forside.ID)
            {
                return BadRequest();
            }

            db.Entry(forside).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForsideExists(id))
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

        // POST: api/Forsides
        [ResponseType(typeof(Forside))]
        public IHttpActionResult PostForside(Forside forside)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Forside.Add(forside);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = forside.ID }, forside);
        }

        // DELETE: api/Forsides/5
        [ResponseType(typeof(Forside))]
        public IHttpActionResult DeleteForside(int id)
        {
            Forside forside = db.Forside.Find(id);
            if (forside == null)
            {
                return NotFound();
            }

            db.Forside.Remove(forside);
            db.SaveChanges();

            return Ok(forside);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ForsideExists(int id)
        {
            return db.Forside.Count(e => e.ID == id) > 0;
        }
    }
}