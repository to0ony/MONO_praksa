using CarRent.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Web.Http;
using Npgsql;
using NpgsqlTypes;
using CarRent.Common;
using CarRent.Model.Common;
using System.Drawing.Text;
using CarRent.Service.Common;
using CarRent.Service;
using CarRent.Model;
using System.Threading.Tasks;


namespace CarRent.WebApi.Controllers
{
    [RoutePrefix("api/Car")]
    public class CarController : ApiController
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        [Route("")]
        // GET api/values
        public HttpResponseMessage GetAllCars([FromUri] CarFilter filter)
        {
            List<ICar> cars;
            try
            {
                cars = carService.GetAllCars(filter);
                return Request.CreateResponse(HttpStatusCode.OK,cars);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        // GET api/values/5
        public HttpResponseMessage GetCarById(Guid id)
        {
            ICar car = carService.GetCarById(id);
            try
            {
                if (car == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK,car);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex);
            }
        }

        [HttpPost]
        [Route("")]
        // POST api/values
        public HttpResponseMessage CreateCar([FromUri] Car car)
        {
            if (car == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                carService.CreateCar(car);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //ASYNC HERE!
        [HttpPut]
        [Route("{id:guid}")]
        // PUT api/values/5
        public async Task<HttpResponseMessage> UpdateCar(Guid id, [FromUri] Car updatedCar)
        {
            if(updatedCar == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            ICar carInBase = carService.GetCarById(id);
            if(carInBase == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                bool updated = await carService.UpdateCar(id, new Car()
                {
                    Brand = updatedCar.Brand,
                    Model = updatedCar.Model,
                    Mileage = updatedCar.Mileage,
                    InsuranceStatus = updatedCar.InsuranceStatus,
                    Available = updatedCar.Available,
                    ManafactureDate = updatedCar.ManafactureDate
                }); 
                
                if (updated) return Request.CreateResponse(HttpStatusCode.OK);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        // DELETE api/values/5
        public HttpResponseMessage DeleteCar(Guid id)
        {
            if(id == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            ICar car = carService.GetCarById(id);
            if (car == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                carService.DeleteCar(id);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(InternalServerError(ex));
            }
            return Request.CreateResponse(Ok());
        }
    }
}
