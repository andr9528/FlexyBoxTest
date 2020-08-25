using FlexyBoxTest.Core;

namespace FlexyBoxTest.Domain
{
    public class Motorcycle : Vehicle
    {
        public Motorcycle()
        {
            MaxSpeed = 200;
            Wheels = 2;
            ABS = true;
            Model = "2019 BMW R 1250 GS";
        }
    }
}