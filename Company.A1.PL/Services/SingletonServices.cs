﻿namespace Company.A1.PL.Services
{
    public class SingletonServices : ISingletonServices
    {
        public SingletonServices()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
