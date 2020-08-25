using FlexyBoxTest.Domain.Core;

namespace FlexyBoxTest.Domain.Concrete
{
    public class Bicycle : Vehicle
    {
        public Bicycle()
        {
            MaxSpeed = 30;
            Wheels = 2;
            ABS = false;
            Model = "Merida Bicycle";
        }
    }
}