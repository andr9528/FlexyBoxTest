using FlexyBoxTest.Core;

namespace FlexyBoxTest.Domain
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