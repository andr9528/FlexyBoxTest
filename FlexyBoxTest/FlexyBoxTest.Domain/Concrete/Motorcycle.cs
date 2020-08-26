using FlexyBoxTest.Domain.Core;

namespace FlexyBoxTest.Domain.Concrete
{
    // Task 1.1
    public class Motorcycle : Vehicle
    {
        // Task 1.2
        public Motorcycle()
        {
            MaxSpeed = 200;
            Wheels = 2;
            ABS = true;
            Model = "2019 BMW R 1250 GS";
        }
    }
}