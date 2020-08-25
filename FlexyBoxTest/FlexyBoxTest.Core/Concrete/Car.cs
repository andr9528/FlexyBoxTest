using FlexyBoxTest.Domain.Core;

namespace FlexyBoxTest.Domain.Concrete
{
    public class Car : Vehicle
    {
        public Car()
        {
            MaxSpeed = 150;
            Wheels = 4;
            ABS = true;
            Model = "BMW Z4";
        }
    }
}