using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;

namespace FarmManagement.Modules.Farm.Domain.Services;

public sealed class FarmCapacityDomainService
{
    public bool CanAddField(Aggregates.FarmAggregate.Farm farm)
    {
        return farm.Fields.Count < 100;
    }
}

// using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;

// namespace FarmManagement.Modules.Farm.Domain.Services;

// public sealed class FarmCapacityDomainService
// {
//     public bool CanAddField(Farm farm)
//     {
//         return farm.Fields.Count < 100; // example rule
//     }
// }
