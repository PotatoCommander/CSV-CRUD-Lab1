using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CSV_CRUD_Lab1.Model;
using CsvHelper;
using CsvHelper.Configuration;

namespace CSV_CRUD_Lab1.Repository
{
    public class CSVRepository : IRepository
    {
        public string FilePath { get; set; }

        private readonly CsvConfiguration _config = new CsvConfiguration()
        {
            HasHeaderRecord = false,
            CultureInfo = CultureInfo.InvariantCulture
        };


        public List<Car> GetCars()
        {
            using var reader = new StreamReader(FilePath);
            using var csv = new CsvReader(reader, _config);
            var list = csv.GetRecords<Car>().ToList();
            return list;
        }

        public void AddCar(Car item)
        {
            using var stream = File.Open(FilePath, FileMode.Append);
            using var writer = new StreamWriter(stream);
            using var csv = new CsvWriter(writer, _config);
            csv.WriteRecord(item);
        }

        public void UpdateCar(Car item)
        {
            var list = GetCars();
            list[list.FindIndex(x => x.id == item.id)] = item;

            using var writer = new StreamWriter(FilePath);
            using var csvWriter = new CsvWriter(writer, _config);
            csvWriter.WriteRecords(list);
        }

        public void DeleteCar(Car item)
        {
            var list = GetCars();
            var index = list.FindIndex(x => x.id == item.id);
            list.RemoveAt(index);

            using var writer = new StreamWriter(FilePath);
            using var csvWriter = new CsvWriter(writer, _config);
            csvWriter.WriteRecords(list);
        }
    }
}