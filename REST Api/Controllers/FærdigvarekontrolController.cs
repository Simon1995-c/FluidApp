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
    public class FærdigvarekontrolController : ApiController
    {
        private FluidContext db = new FluidContext();

        // GET: api/Færdigvarekontrol
        public IQueryable<Færdigvarekontrol> GetFærdigvarekontrol()
        {
            return db.Færdigvarekontrol;
        }

        // GET: api/Færdigvarekontrol/5
        [ResponseType(typeof(Færdigvarekontrol))]
        public IHttpActionResult GetFærdigvarekontrol(int id)
        {
            Færdigvarekontrol færdigvarekontrol = db.Færdigvarekontrol.Find(id);
            if (færdigvarekontrol == null)
            {
                return NotFound();
            }

            return Ok(færdigvarekontrol);
        }

        // PUT: api/Færdigvarekontrol/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFærdigvarekontrol(int id, Færdigvarekontrol færdigvarekontrol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != færdigvarekontrol.ProcessordreNr)
            {
                return BadRequest();
            }

            db.Entry(færdigvarekontrol).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FærdigvarekontrolExists(id))
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

        // POST: api/Færdigvarekontrol
        [ResponseType(typeof(Færdigvarekontrol))]
        public IHttpActionResult PostFærdigvarekontrol(Færdigvarekontrol færdigvarekontrol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Færdigvarekontrol.Add(færdigvarekontrol);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = færdigvarekontrol.ProcessordreNr }, færdigvarekontrol);
        }

        // DELETE: api/Færdigvarekontrol/5
        [ResponseType(typeof(Færdigvarekontrol))]
        public IHttpActionResult DeleteFærdigvarekontrol(int id)
        {
            Færdigvarekontrol færdigvarekontrol = db.Færdigvarekontrol.Find(id);
            if (færdigvarekontrol == null)
            {
                return NotFound();
            }

            db.Færdigvarekontrol.Remove(færdigvarekontrol);
            db.SaveChanges();

            return Ok(færdigvarekontrol);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FærdigvarekontrolExists(int id)
        {
            return db.Færdigvarekontrol.Count(e => e.ProcessordreNr == id) > 0;
        }
    }
}