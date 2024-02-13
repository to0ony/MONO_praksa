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
        Task<List<ICar>> GetAllCars(CarFilter filter);
        Task<ICar> GetCarById(Guid id);
        Task<bool> CreateCar(ICar car);
        Task<bool> UpdateCar(Guid Id, ICar car);
        Task<bool> DeleteCar(Guid Id);
    }
}
