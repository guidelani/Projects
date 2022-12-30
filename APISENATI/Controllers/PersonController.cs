using APISENATI.Models;
using APISENATI.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PersonModel;

namespace APISENATI.Controllers
{
    public class PersonController : ApiController
    {
        [HttpPost]
        [Route("AddPerson")]
        public IHttpActionResult AddPerson(PersonRequest Person)
        {
            using (senatiapiEntities3 dbSenati = new senatiapiEntities3())
            {
                //var oPerson = new Person();
                //oPerson.Nombre = model.Nombre;
                //oPerson.Ciudad = model.Ciudad;
                //db.Person.Add(oPerson);
                //db.SaveChanges();
                if (Person.Id == 0)
                {
                    Person oPerson = new Person();
                    oPerson.Nombre = Person.Nombre;
                    oPerson.Ciudad = Person.Ciudad;
                    dbSenati.Person.Add(oPerson);
                }
                //Update
                else
                {
                    Person oPerson = dbSenati.Person.Where(X => X.Id == Person.Id).FirstOrDefault();
                    oPerson.Nombre = Person.Nombre;
                    oPerson.Ciudad = Person.Ciudad;
                    dbSenati.Entry(oPerson).State = System.Data.Entity.EntityState.Modified;


                    /*var _oPerson = (from p in db.Person
                                    where p.id == Person.id
                                    select new
                                    {
                                        Nombre = p.Nombre,
                                        Ciudad = p.Ciudad,
                                    });*/
                }
                dbSenati.SaveChanges();
            }
            return Ok("exito");
        }
        [HttpPost]
        [Route("getPerson")]
        public IHttpActionResult getPerson(PersonRequestV2 Person)
        {
            List<PersonResponseV1> PersonAll = new List<PersonResponseV1>();
            using (senatiapiEntities3 dbSenati = new senatiapiEntities3())
            {
                if (Person.des != "")
                {
                    PersonAll = (from p in dbSenati.Person
                                 where p.Nombre.ToString().Contains(Person.des)
                                 select new PersonResponseV1
                                 {
                                     Id = p.Id,
                                     Nombre = p.Nombre,
                                     Ciudad = p.Ciudad
                                 }).ToList();
                }
                else
                {
                    PersonAll = (from p in dbSenati.Person
                                 select new PersonResponseV1
                                 {
                                     Id = p.Id,
                                     Nombre = p.Nombre,
                                     Ciudad = p.Ciudad

                                 }).ToList();
                }
            }
            return Ok(PersonAll);
        }
        [HttpGet]
        [Route("deletePerson/{PersonId}")]
        public IHttpActionResult deletePerson(int PersonId)
        {
            using (senatiapiEntities3 dbSenati = new senatiapiEntities3())
            {
                Person oPerson = dbSenati.Person.Where(x => x.Id == PersonId).FirstOrDefault();
                dbSenati.Person.Remove(oPerson);



                dbSenati.SaveChanges();
            }
            return Ok("Se elimino");
        }
        [HttpGet]
        [Route("getPersonById/Id")]
        public IHttpActionResult getPersonById(int Id)
        {
            List<PersonResponseV1> PersonAll = new List<PersonResponseV1>();
            using (senatiapiEntities3 dbSenati = new senatiapiEntities3())
            {
                PersonAll = (from p in dbSenati.Person
                             where p.Id.Equals(Id)
                             select new PersonResponseV1
                             {
                                 Id= p.Id,
                                 Nombre = p.Nombre,
                                 Ciudad = p.Ciudad
                             }).ToList();
            }
             
            return Ok(PersonAll);
        }
    }
}