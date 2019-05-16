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
    public class RengøringsKolonneController : ApiController
    {
        private FluidContext db = new FluidContext();

        // GET: api/RengøringsKolonne
        public IQueryable<RengøringsKolonne> GetRengøringsKolonne()
        {
            return db.RengøringsKolonne;
        }

        // GET: api/RengøringsKolonne/5
        [ResponseType(typeof(RengøringsKolonne))]
        public IHttpActionResult GetRengøringsKolonne(int id)
        {
            RengøringsKolonne rengøringsKolonne = db.RengøringsKolonne.Find(id);
            if (rengøringsKolonne == null)
            {
                return NotFound();
            }

            return Ok(rengøringsKolonne);
        }

        // PUT: api/RengøringsKolonne/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRengøringsKolonne(int id, RengøringsKolonne rengøringsKolonne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rengøringsKolonne.ID)
            {
                return BadRequest();
            }

            db.Entry(rengøringsKolonne).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RengøringsKolonneExists(id))
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

        // POST: api/RengøringsKolonne
        [ResponseType(typeof(RengøringsKolonne))]
        public IHttpActionResult PostRengøringsKolonne(RengøringsKolonne rengøringsKolonne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RengøringsKolonne.Add(rengøringsKolonne);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rengøringsKolonne.ID }, rengøringsKolonne);
        }

        // DELETE: api/RengøringsKolonne/5
        [ResponseType(typeof(RengøringsKolonne))]
        public IHttpActionResult DeleteRengøringsKolonne(int id)
        {
            RengøringsKolonne rengøringsKolonne = db.RengøringsKolonne.Find(id);
            if (rengøringsKolonne == null)
            {
                return NotFound();
            }

            db.RengøringsKolonne.Remove(rengøringsKolonne);
            db.SaveChanges();

            return Ok(rengøringsKolonne);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RengøringsKolonneExists(int id)
        {
            return db.RengøringsKolonne.Count(e => e.ID == id) > 0;
        }
    }
}