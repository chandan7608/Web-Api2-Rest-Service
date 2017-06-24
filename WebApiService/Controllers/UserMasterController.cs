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
using WebApiService;
using WebApiService.Models;

namespace WebApiService.Controllers
{
    public class UserMasterController : ApiController
    {
        private WebApiServiceContext db = new WebApiServiceContext();

        // GET api/UserMaster
        public IQueryable<UserMaster> GetUserMasters()
        {
            return db.UserMasters;
        }

        // GET api/UserMaster/5
        [ResponseType(typeof(UserMaster))]
        public IHttpActionResult GetUserMaster(int id)
        {
            UserMaster usermaster = db.UserMasters.Find(id);
            if (usermaster == null)
            {
                return NotFound();
            }

            return Ok(usermaster);
        }

        // PUT api/UserMaster/5
        public IHttpActionResult PutUserMaster(int id, UserMaster usermaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usermaster.Id)
            {
                return BadRequest();
            }

            db.Entry(usermaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserMasterExists(id))
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

        // POST api/UserMaster
        [ResponseType(typeof(UserMaster))]
        public IHttpActionResult PostUserMaster(UserMaster usermaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserMasters.Add(usermaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usermaster.Id }, usermaster);
        }

        // DELETE api/UserMaster/5
        [ResponseType(typeof(UserMaster))]
        public IHttpActionResult DeleteUserMaster(int id)
        {
            UserMaster usermaster = db.UserMasters.Find(id);
            if (usermaster == null)
            {
                return NotFound();
            }

            db.UserMasters.Remove(usermaster);
            db.SaveChanges();

            return Ok(usermaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserMasterExists(int id)
        {
            return db.UserMasters.Count(e => e.Id == id) > 0;
        }
    }
}