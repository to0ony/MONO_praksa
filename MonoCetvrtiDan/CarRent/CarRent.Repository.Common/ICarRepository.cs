using CarRent.Common;
using CarRent.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Repository.Common
{
    public interface ICarRepository
    {
        List<ICar> GetAllCars(CarFilter filter);
        ICar GetCarById(Guid Id);
        void CreateCar(ICar car);
        Task<bool> UpdateCar(Guid Id,ICar car);
        void DeleteCar(Guid Id);
    }
}
