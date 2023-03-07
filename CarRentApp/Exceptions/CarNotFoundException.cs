namespace CarRentApp.Exceptions
{
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException(string message) : base(message)
        { }
    }
}
