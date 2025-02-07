﻿using CarRent.WebApi.Models;
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
using CarRent.Common.Filters;


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
        public async Task<HttpResponseMessage> GetAllCarsAsync(
            int pageNum = 1,
            int pageSize = 10,
            string sortBy = "Model",
            string sortOrder = "ASC",
            string searchQuery = null,
            string brand = null,
            string model = null,
            int? mileage = null,
            bool? insuranceStatus = null,
            bool? available = null,
            int? manufactureDate = null)
        {
            try
            {
                Paging paging = new Paging { PageNum = pageNum, PageSize = pageSize };
                Sorting sorting = new Sorting { SortBy = sortBy, SortOrder = sortOrder };
                CarFilter filter = new CarFilter {
                    SearchQuery = searchQuery, 
                    Brand = brand, 
                    Model = model,
                    Mileage = mileage,
                    InsuranceStatus = insuranceStatus,
                    Available = available,
                    ManafactureDate = manufactureDate
                };

                List<ICar> cars = await carService.GetAllCars(paging, sorting, filter);

                if (cars == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, cars.Select(car => new CarView(car)));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        // GET api/values/5
        public async Task<HttpResponseMessage> GetCarByIdAsync(Guid id)
        {
            try
            {
                ICar car = await carService.GetCarById(id);

                if (car == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, new CarView(car));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("")]
        // POST api/values
        public async Task<HttpResponseMessage> CreateCarAsync([FromBody] Car car)
        {
            if (car == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                bool created = await carService.CreateCar(car);
                if (created) return Request.CreateResponse(HttpStatusCode.OK);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        // PUT api/values/5
        public async Task<HttpResponseMessage> UpdateCarAsync(Guid id, [FromBody] Car updatedCar)
        {
            if(updatedCar == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            Task<ICar> carInBase = carService.GetCarById(id);
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
        public async Task<HttpResponseMessage> DeleteCarAsync(Guid id)
        {
            if(id == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            Task<ICar> car = carService.GetCarById(id);
            if (car == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                bool deleted = await carService.DeleteCar(id);
                if (deleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(InternalServerError(ex));
            }
        }
    }
}
