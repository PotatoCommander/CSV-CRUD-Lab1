using System;
using System.Collections.Generic;
using System.Text;
using CSV_CRUD_Lab1.Model;

namespace CSV_CRUD_Lab1.Repository
{
    interface IRepository
    {
        List<Car> GetCars();
        void AddCar(Car item);

        void UpdateCar(Car item);

        void DeleteCar(Car item);
    }
}
