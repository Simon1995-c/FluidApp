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
    public class KontrolSkemasController : ApiController
    {
        private FluidContext db = new FluidContext();

        // GET: api/KontrolSkemas
        public IQueryable<KontrolSkema> GetKontrolSkema()
        {
            return db.KontrolSkema;
        }

        // GET: api/KontrolSkemas/5
        [ResponseType(typeof(KontrolSkema))]
        public IHttpActionResult GetKontrolSkema(int id)
        {
            KontrolSkema kontrolSkema = db.KontrolSkema.Find(id);
            if (kontrolSkema == null)
            {
                return NotFound();
            }

            return Ok(kontrolSkema);
        }

        // PUT: api/KontrolSkemas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKontrolSkema(int id, KontrolSkema kontrolSkema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kontrolSkema.ID)
            {
                return BadRequest();
            }

            db.Entry(kontrolSkema).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KontrolSkemaExists(id))
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

        // POST: api/KontrolSkemas
        [ResponseType(typeof(KontrolSkema))]
        public IHttpActionResult PostKontrolSkema(KontrolSkema kontrolSkema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KontrolSkema.Add(kontrolSkema);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kontrolSkema.ID }, kontrolSkema);
        }

        // DELETE: api/KontrolSkemas/5
        [ResponseType(typeof(KontrolSkema))]
        public IHttpActionResult DeleteKontrolSkema(int id)
        {
            KontrolSkema kontrolSkema = db.KontrolSkema.Find(id);
            if (kontrolSkema == null)
            {
                return NotFound();
            }

            db.KontrolSkema.Remove(kontrolSkema);
            db.SaveChanges();

            return Ok(kontrolSkema);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KontrolSkemaExists(int id)
        {
            return db.KontrolSkema.Count(e => e.ID == id) > 0;
        }
    }
}