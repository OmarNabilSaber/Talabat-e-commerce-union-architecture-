using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Domain.Common
{
    public abstract class BaseEntity<Tkey> where Tkey : IEquatable<Tkey>
    {
        public required Tkey Id { get; set; }
    }
}
