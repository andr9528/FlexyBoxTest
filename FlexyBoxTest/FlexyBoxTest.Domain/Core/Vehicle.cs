using System;
using System.Text;

namespace FlexyBoxTest.Domain.Core
{
    public abstract class Vehicle : IVehicle
    {
        public int MaxSpeed { get; set; }
        public string Model { get; set; }
        public int Wheels { get; set; }
        public bool ABS { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            var type = GetType();

            builder.Append($"{nameof(Type)} {nameof(type.Name)}: {type.Name} \t");
            builder.Append($"{nameof(Model)}: {Model} \t");
            builder.Append($"{nameof(MaxSpeed)}: {MaxSpeed} \t");
            builder.Append($"{nameof(Wheels)}: {Wheels} \t");
            builder.Append($"{nameof(ABS)}: {ABS} \t");

            return builder.ToString();
        }
    }
}