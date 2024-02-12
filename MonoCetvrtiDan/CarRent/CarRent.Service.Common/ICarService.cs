using CarRent.Common;
using CarRent.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Service.Common
{
    public interface ICarService
    {
        List<ICar> GetAllCars(CarFilter filter);
        ICar GetCarById(Guid id);
        void CreateCar(ICar car);
        Task<bool> UpdateCar(Guid Id, ICar car);
        void DeleteCar(Guid Id);
    }
}
