// FarmManagement.Modules.Farm/FarmManagement.Modules.Farm.Domain/ValueObjects/FieldId.cs
using System;

namespace FarmManagement.SharedKernel.Domain
{
    public sealed class FieldId : IEquatable<FieldId>
    {
        public Guid Value { get; private set; }

        // Parameterless constructor for EF Core
        private FieldId()
        {
            Value = Guid.Empty;
        }

        private FieldId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("FieldId cannot be empty.", nameof(value));

            Value = value;
        }

        // ✅ Added this missing method
        public static FieldId New()
            => new(Guid.NewGuid());

        public static FieldId From(Guid value)
            => new(value);

        public bool Equals(FieldId? other)
            => other is not null && Value.Equals(other.Value);

        public override bool Equals(object? obj)
            => Equals(obj as FieldId);

        public override int GetHashCode()
            => Value.GetHashCode();

        public override string ToString()
            => Value.ToString();

        // Optional: Add implicit/explicit operators for convenience
        public static implicit operator Guid(FieldId id) => id.Value;
        public static explicit operator FieldId(Guid value) => From(value);
    }
}