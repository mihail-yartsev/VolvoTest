using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace CongestionTaxCalculator.Grpc.Services
{
    public class TaxCalculatorService : TaxCalculator.TaxCalculatorBase
    {
        private readonly ICongestionTaxCalculator _calculator;

        public TaxCalculatorService(ICongestionTaxCalculator calculator)    
        {
            _calculator = calculator;
        }

        public override Task<CalculateReply> Calculate(CalculateRequest request, ServerCallContext context)
        {
            var calculationResult = _calculator.GetTax(request.VehicleType,
                request.Timestamp.Select(t => t.ToDateTime()).ToArray());

            return Task.FromResult(calculationResult.ToReply());
        }
    }
}
