using System;
using System.Collections.Generic;
using System.Text;

namespace CSV_CRUD_Lab1.Model
{
    public class Car
    {
        public Guid id { get; set; }
        public decimal price { get; set; }

        public CarsCondition condition { get; set; }

        public BodyTypes bodyType { get; set; }

        public string brand { get; set; }

        public string model { get; set; }

        public int yearOfProduction { get; set; }

        public Engine engine { get; set; }

        public Car()
        {
            id = Guid.NewGuid();
        }
    }
}
