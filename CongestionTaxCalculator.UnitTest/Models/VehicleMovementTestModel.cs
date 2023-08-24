using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.UnitTest.Models
{
    public class VehicleMovementTestModel
    {
        public string VehicleType { get; set; }
        public DateTime[] Movements { get; set; }
        public int ExpectedTax { get; set; }
    }
}
