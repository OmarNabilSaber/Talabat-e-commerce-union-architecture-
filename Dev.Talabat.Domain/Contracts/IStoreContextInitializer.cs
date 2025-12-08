using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Domain.Contracts
{
    public interface IStoreContextInitializer
    {
        public Task InitializeAsync();
        public Task SeedAsync();
    }
}
