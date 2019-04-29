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
    public class AdministratorsController : ApiController
    {
        private FluidContext db = new FluidContext();

        // GET: api/Administrators
        public IQueryable<Administrator> GetAdministrator()
        {
            return db.Administrator;
        }

        // GET: api/Administrators/5
        [ResponseType(typeof(Administrator))]
        public IHttpActionResult GetAdministrator(string id)
        {
            Administrator administrator = db.Administrator.Find(id);
            if (administrator == null)
            {
                return NotFound();
            }

            return Ok(administrator);
        }

        // PUT: api/Administrators/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdministrator(string id, Administrator administrator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administrator.Brugernavn)
            {
                return BadRequest();
            }

            db.Entry(administrator).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
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

        // POST: api/Administrators
        [ResponseType(typeof(Administrator))]
        public IHttpActionResult PostAdministrator(Administrator administrator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Administrator.Add(administrator);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AdministratorExists(administrator.Brugernavn))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = administrator.Brugernavn }, administrator);
        }

        // DELETE: api/Administrators/5
        [ResponseType(typeof(Administrator))]
        public IHttpActionResult DeleteAdministrator(string id)
        {
            Administrator administrator = db.Administrator.Find(id);
            if (administrator == null)
            {
                return NotFound();
            }

            db.Administrator.Remove(administrator);
            db.SaveChanges();

            return Ok(administrator);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdministratorExists(string id)
        {
            return db.Administrator.Count(e => e.Brugernavn == id) > 0;
        }
    }
}