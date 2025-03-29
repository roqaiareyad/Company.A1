namespace Company.A1.PL.Services
{
    public interface ISingletonServices
    {
        public Guid Guid { get; set; }
        String GetGuid();
    }
}
