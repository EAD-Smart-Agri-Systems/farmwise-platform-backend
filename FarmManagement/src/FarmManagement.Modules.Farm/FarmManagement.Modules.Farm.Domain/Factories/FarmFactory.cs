using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;
using FarmManagement.Modules.Farm.Domain.ValueObjects;

namespace FarmManagement.Modules.Farm.Domain.Factories;

public static class FarmFactory
{
    public static Aggregates.FarmAggregate.Farm Create(
        FarmName name,
        Location location)
    {
        return new Aggregates.FarmAggregate.Farm(
            FarmId.New(),
            name,
            location
        );
    }
}

// using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;
// using FarmManagement.Modules.Farm.Domain.ValueObjects;

// namespace FarmManagement.Modules.Farm.Domain.Factories;

// public static class FarmFactory
// {
//     public static Farm Create(FarmName name, Location location)
//     {
//         return new Farm(
//             FarmId.New(),
//             name,
//             location
//         );
//     }
// }
