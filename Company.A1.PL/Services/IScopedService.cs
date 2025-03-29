namespace Company.A1.PL.Services
{
    public interface IScopedService
    {
        public Guid Guid { get; set; }
        String GetGuid();
    }
}
