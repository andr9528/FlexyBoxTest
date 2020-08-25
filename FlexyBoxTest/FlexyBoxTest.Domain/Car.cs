using FlexyBoxTest.Core;

namespace FlexyBoxTest.Domain
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