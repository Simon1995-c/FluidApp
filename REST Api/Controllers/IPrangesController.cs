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
    public class IPrangesController : ApiController
    {
        private FluidContext db = new FluidContext();

        // GET: api/IPranges
        public IQueryable<IPrange> GetIPrange()
        {
            return db.IPrange;
        }

        // GET: api/IPranges/5
        [ResponseType(typeof(IPrange))]
        public IHttpActionResult GetIPrange(string id)
        {
            IPrange iPrange = db.IPrange.Find(id);
            if (iPrange == null)
            {
                return NotFound();
            }

            return Ok(iPrange);
        }

        // PUT: api/IPranges/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIPrange(string id, IPrange iPrange)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iPrange.IP)
            {
                return BadRequest();
            }

            db.Entry(iPrange).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IPrangeExists(id))
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

        // POST: api/IPranges
        [ResponseType(typeof(IPrange))]
        public IHttpActionResult PostIPrange(IPrange iPrange)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IPrange.Add(iPrange);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (IPrangeExists(iPrange.IP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = iPrange.IP }, iPrange);
        }

        // DELETE: api/IPranges/5
        [ResponseType(typeof(IPrange))]
        public IHttpActionResult DeleteIPrange(string id)
        {
            IPrange iPrange = db.IPrange.Find(id);
            if (iPrange == null)
            {
                return NotFound();
            }

            db.IPrange.Remove(iPrange);
            db.SaveChanges();

            return Ok(iPrange);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IPrangeExists(string id)
        {
            return db.IPrange.Count(e => e.IP == id) > 0;
        }
    }
}