namespace Company.A1.PL.Services
{
    public interface ITransientService
    {
        public Guid Guid { get; set; }
        String GetGuid();
    }
}
