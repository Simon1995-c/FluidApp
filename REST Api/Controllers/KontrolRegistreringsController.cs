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
    public class KontrolRegistreringsController : ApiController
    {
        private FluidContext db = new FluidContext();

        // GET: api/KontrolRegistrerings
        public IQueryable<KontrolRegistrering> GetKontrolRegistrering()
        {
            return db.KontrolRegistrering;
        }

        // GET: api/KontrolRegistrerings/5
        [ResponseType(typeof(KontrolRegistrering))]
        public IHttpActionResult GetKontrolRegistrering(int id)
        {
            KontrolRegistrering kontrolRegistrering = db.KontrolRegistrering.Find(id);
            if (kontrolRegistrering == null)
            {
                return NotFound();
            }

            return Ok(kontrolRegistrering);
        }

        // PUT: api/KontrolRegistrerings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKontrolRegistrering(int id, KontrolRegistrering kontrolRegistrering)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kontrolRegistrering.ID)
            {
                return BadRequest();
            }

            db.Entry(kontrolRegistrering).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KontrolRegistreringExists(id))
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

        // POST: api/KontrolRegistrerings
        [ResponseType(typeof(KontrolRegistrering))]
        public IHttpActionResult PostKontrolRegistrering(KontrolRegistrering kontrolRegistrering)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KontrolRegistrering.Add(kontrolRegistrering);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kontrolRegistrering.ID }, kontrolRegistrering);
        }

        // DELETE: api/KontrolRegistrerings/5
        [ResponseType(typeof(KontrolRegistrering))]
        public IHttpActionResult DeleteKontrolRegistrering(int id)
        {
            KontrolRegistrering kontrolRegistrering = db.KontrolRegistrering.Find(id);
            if (kontrolRegistrering == null)
            {
                return NotFound();
            }

            db.KontrolRegistrering.Remove(kontrolRegistrering);
            db.SaveChanges();

            return Ok(kontrolRegistrering);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KontrolRegistreringExists(int id)
        {
            return db.KontrolRegistrering.Count(e => e.ID == id) > 0;
        }
    }
}