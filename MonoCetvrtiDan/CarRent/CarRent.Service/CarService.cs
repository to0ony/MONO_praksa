using CarRent.Common;
using CarRent.Model.Common;
using CarRent.Repository;
using CarRent.Repository.Common;
using CarRent.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;

        public CarService(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        public CarService()
        {
            carRepository = new CarRepository();
        }

        public Task<List<ICar>> GetAllCars(CarFilter filter)
        {
            try
            {
                return carRepository.GetAllCars(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ICar> GetCarById(Guid id)
        {
            try
            {
                return carRepository.GetCarById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> CreateCar(ICar car)
        {
            try
            {
                return carRepository.CreateCar(car);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> UpdateCar(Guid id, ICar car)
        {
            try
            {
                return carRepository.UpdateCar(id, car);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> DeleteCar(Guid Id)
        {
            try
            {
                return carRepository.DeleteCar(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
