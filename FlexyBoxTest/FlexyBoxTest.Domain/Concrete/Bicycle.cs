using FlexyBoxTest.Domain.Core;

namespace FlexyBoxTest.Domain.Concrete
{
    // Task 1.1
    public class Bicycle : Vehicle
    {
        // Task 1.2
        public Bicycle()
        {
            MaxSpeed = 30;
            Wheels = 2;
            ABS = false;
            Model = "Merida Bicycle";
        }
    }
}