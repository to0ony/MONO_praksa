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

        public List<ICar> GetAllCars(CarFilter filter)
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

        public ICar GetCarById(Guid id)
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

        public void CreateCar(ICar car)
        {
            try
            {
                carRepository.CreateCar(car);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //ASYNC HERE!
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

        public void DeleteCar(Guid Id)
        {
            try
            {
                carRepository.DeleteCar(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
