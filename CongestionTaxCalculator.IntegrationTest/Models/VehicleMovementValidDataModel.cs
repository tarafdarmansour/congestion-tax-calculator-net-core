using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.IntegrationTest.Models
{
    public class VehicleMovementValidDataModel : VehicleMovementTestBaseModel
    {
        public int ExpectedTax { get; set; }
    }
}
