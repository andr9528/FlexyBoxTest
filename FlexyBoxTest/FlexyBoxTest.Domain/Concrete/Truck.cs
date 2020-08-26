using FlexyBoxTest.Domain.Core;

namespace FlexyBoxTest.Domain.Concrete
{
    // Task 1.1 & 2.2
    public class Truck : Vehicle
    {
        // Task 1.2
        public Truck()
        {
            MaxSpeed = 100;
            Wheels = 8;
            ABS = true;
            Model = "Box truck";
        }
    }
}