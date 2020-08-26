using FlexyBoxTest.Domain.Core;

namespace FlexyBoxTest.Domain.Concrete
{
    // Task 1.1
    public class Car : Vehicle
    {
        // Task 1.2
        public Car()
        {
            MaxSpeed = 150;
            Wheels = 4;
            ABS = true;
            Model = "BMW Z4";
        }
    }
}