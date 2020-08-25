using FlexyBoxTest.Core;

namespace FlexyBoxTest.Domain
{
    public class Truck : Vehicle
    {
        public Truck()
        {
            MaxSpeed = 100;
            Wheels = 8;
            ABS = true;
            Model = "Box truck";
        }
    }
}