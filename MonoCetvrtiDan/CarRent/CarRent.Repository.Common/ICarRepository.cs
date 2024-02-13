using CarRent.Common;
using CarRent.Common.Filters;
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
        Task<List<ICar>> GetAllCars(Paging paging, Sorting sorting, CarFilter filterr);
        Task<ICar> GetCarById(Guid Id);
        Task<bool> CreateCar(ICar car);
        Task<bool> UpdateCar(Guid Id,ICar car);
        Task<bool> DeleteCar(Guid Id);
    }
}
