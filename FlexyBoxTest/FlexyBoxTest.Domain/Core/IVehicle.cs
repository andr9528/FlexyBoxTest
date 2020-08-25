namespace FlexyBoxTest.Domain.Core
{
    public interface IVehicle
    {
        int MaxSpeed { get; set; }
        string Model { get; set; }
        int Wheels { get; set; }
        bool ABS { get; set; }
    }
}