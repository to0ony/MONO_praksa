using CarRent.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarRent.Api.Controllers
{
    public class CarController : ApiController
    {
        private static List<Car> _cars = new List<Car>();  //privatni entiteti se nazivaju _malaSlova

        // GET api/values
        [HttpGet]
        public HttpResponseMessage Get([FromBody] CarFilter filter)
        {
            try
            {
                List<Car> filteredCars = _cars.ToList();
                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Brand))
                        filteredCars = filteredCars.Where(c => c.Brand == filter.Brand).ToList();
                    if (!string.IsNullOrEmpty(filter.Model))
                        filteredCars = filteredCars.Where(c => c.Model == filter.Model).ToList();
                    if (filter.ManafactureDate != 0)
                        filteredCars = filteredCars.Where(c => c.ManafactureDate == filter.ManafactureDate).ToList();
                    if (filter.Mileage != 0)
                        filteredCars = filteredCars.Where(c => c.Mileage == filter.Mileage).ToList();
                    if (filter.InsuranceStatus)
                        filteredCars = filteredCars.Where(c => c.InsuranceStatus == filter.InsuranceStatus).ToList();
                    if (filter.Available)
                        filteredCars = filteredCars.Where(c => c.Available == filter.Available).ToList();
                }
                return Request.CreateResponse(HttpStatusCode.OK, filteredCars.Select(x => new CarView(x)));
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var car = _cars.FirstOrDefault(c => c.Id == id);
                if (car == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                return Request.CreateResponse(HttpStatusCode.OK, car);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        // POST api/values
        public HttpResponseMessage Post([FromBody] Car car)  //nema potrebe za [FromBody]
        {
            try
            {
                if (_cars.Any(c => c.Id == car.Id))
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                _cars.Add(car);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody] Car car)   //nema potrebe za [FromBody]
        {
            try
            {
                if (id < 0 || id > _cars.Count)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "ID out of range");
                Car updatedCar = _cars.FirstOrDefault(c => c.Id == id);
                updatedCar.Brand = car.Brand;
                updatedCar.Model = car.Model;
                updatedCar.ManafactureDate = car.ManafactureDate;
                updatedCar.Mileage = car.Mileage;
                updatedCar.InsuranceStatus = car.InsuranceStatus;
                updatedCar.Available = car.Available;

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (_cars.Any(c => c.Id == id))
                {
                    _cars.Remove(_cars[id]);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.OK, "No such ID in base");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
