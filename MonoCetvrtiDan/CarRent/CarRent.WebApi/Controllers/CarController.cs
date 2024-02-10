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


namespace CarRent.WebApi.Controllers
{
    [RoutePrefix("api/Car")]
    public class CarController : ApiController
    {
        private readonly ICarService carService = new CarService();

        [HttpGet]
        [Route("")]
        // GET api/values
        public IHttpActionResult GetAllCars([FromUri] CarFilter filter)
        {
            List<ICar> cars;
            try
            {
                cars = carService.GetAllCars(filter);
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        // GET api/values/5
        public IHttpActionResult GetCarById(Guid id)
        {
            ICar car = carService.GetCarById(id);
            try
            {
                if (car == null)
                {
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("")]
        // POST api/values
        public IHttpActionResult CreateCar([FromUri] Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }
            try
            {
                carService.CreateCar(car);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();
        }

        [HttpPut]
        [Route("{id:guid}")]
        // PUT api/values/5
        public IHttpActionResult UpdateCar(Guid id, [FromUri] Car updatedCar)
        {
            if(updatedCar == null)
            {
                return BadRequest();
            }
            ICar carInBase = carService.GetCarById(id);
            if(carInBase == null)
            {
                return NotFound();
            }

            try
            {
                carService.UpdateCar(id, new Car()
                {
                    Brand = updatedCar.Brand,
                    Model = updatedCar.Model,
                    Mileage = updatedCar.Mileage,
                    InsuranceStatus = updatedCar.InsuranceStatus,
                    Available = updatedCar.Available,
                    ManafactureDate = updatedCar.ManafactureDate
                }); ;
                var car = carService.GetCarById(id);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        // DELETE api/values/5
        public IHttpActionResult DeleteCar(Guid id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            ICar car = carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }

            try
            {
                carService.DeleteCar(id);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();
        }
    }
}
