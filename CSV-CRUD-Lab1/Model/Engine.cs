using System;
using System.Collections.Generic;
using System.Text;

namespace CSV_CRUD_Lab1.Model
{
    public class Engine
    {
        public string typeOfFuel { get; set; }
        public int volumeSm { get; set; }
        public byte numberOfCylinders { get; set; }

        public byte numberOfValves { get; set; }

        public override string ToString()
        {
            return $"{numberOfCylinders}цил. ({numberOfValves}кл.) {typeOfFuel} {volumeSm}sm3";
        }
    }
}
                    