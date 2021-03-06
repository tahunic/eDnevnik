﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using eDnevnik.API.Models;
using eDnevnik.API.Util;

namespace eDnevnik.API.Controllers
{
    public class MaterijaliController : ApiController
    {
        private eDnevnikEntities db = new eDnevnikEntities();

        // GET: api/Obavijesti
        public IQueryable<MaterijalVM> GetMaterijal()
        {
            return db.Materijal.Select(x => new MaterijalVM
            {
                MaterijalId = x.MaterijalId,
                Naziv = x.Naziv,
                Predmet = x.Predmet.Naziv,
                PredmetId = x.PredmetId,
                Datum = x.Datum,
                Sadrzaj = x.Sadrzaj
            });
        }

        // GET: api/MaterijalVM/5
        [ResponseType(typeof(MaterijalVM))]
        public IHttpActionResult GetMaterijal(int id)
        {
            MaterijalVM materijal = db.Materijal.Where(x=>x.MaterijalId == id).Select(x => new MaterijalVM
            {
                MaterijalId = x.MaterijalId,
                Naziv = x.Naziv,
                Predmet = x.Predmet.Naziv,
                PredmetId = x.PredmetId,
                Datum = x.Datum,
                Sadrzaj = x.Sadrzaj,
                File = x.Fajl,
                FileIme = x.FajlIme,
                FileEkstenzija = x.FajlEkstenzija,
                BrojPreuzimanja = x.BrojPreuzimanja
            }).Single();

            if (materijal == null)
            {
                return NotFound();
            }

            return Ok(materijal);
        }

        // GET: api/Materijali/GetMaterialByPredmet/1
        [Route("api/Materijali/GetMaterialByPredmet/{predmetId}")]
        public IQueryable<MaterijalVM> GetMaterialByPredmet(int predmetId)
        {
            return db.Materijal.Where(y => y.PredmetId == predmetId).Select(x => new MaterijalVM
            {
                MaterijalId = x.MaterijalId,
                Naziv = x.Naziv,
                Predmet = x.Predmet.Naziv,
                PredmetId = x.PredmetId,
                Datum = x.Datum,
                Sadrzaj = x.Sadrzaj
            });
        }

        // GET: api/Materijali/GetMaterialByPredmet/1
        [Route("api/Materijali/GetSlicniMaterijali/{materijalId}")]
        public PreporuceniMaterijaliVM GetSlicniMaterijali(int materijalId)
        {
            Recommender r = new Recommender();
            List<MaterijalVM> slicniMaterijali = r.GetSimilarMaterials(materijalId);
            List<MaterijalVM> najpopularnijiMaterijali = r.GetMostPopularMaterials(slicniMaterijali, db.Materijal.Find(materijalId).PredmetId);

            return new PreporuceniMaterijaliVM()
            {
                SlicniMaterijali = slicniMaterijali,
                PopularniMaterijali = najpopularnijiMaterijali
            };
        }

        // PUT: api/Materijali/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaterijal(int id, Materijal materijal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materijal.MaterijalId)
            {
                return BadRequest();
            }

            db.Entry(materijal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterijalExists(id))
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

        // POST: api/Materijali
        [ResponseType(typeof(Materijal))]
        public IHttpActionResult PostMaterijal(Materijal materijal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Materijal.Add(materijal);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MaterijalExists(materijal.MaterijalId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = materijal.MaterijalId }, materijal);
        }

        // DELETE: api/Obavijesti/5
        [ResponseType(typeof(Obavijest))]
        public IHttpActionResult DeleteMaterijal(int id)
        {
            Materijal materijal = db.Materijal.Find(id);
            if (materijal == null)
            {
                return NotFound();
            }

            db.Materijal.Remove(materijal);
            db.SaveChanges();

            return Ok(materijal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterijalExists(int id)
        {
            return db.Materijal.Count(e => e.MaterijalId == id) > 0;
        }
    }
}