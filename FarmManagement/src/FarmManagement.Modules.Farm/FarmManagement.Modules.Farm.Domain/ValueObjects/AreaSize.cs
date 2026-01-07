public record AreaSize(decimal Value)
{
    public decimal Value { get; init; } = Value > 0 
        ? Value 
        : throw new ArgumentException("Area size must be positive.");
}
// namespace FarmManagement.Modules.Farm.Domain.ValueObjects;

// public record AreaSize(decimal Value)
// {
//     public AreaSize
//     {
//         if (Value <= 0)
//             throw new ArgumentException("Area size must be positive.");
//     }
// }
