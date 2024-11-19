using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.Data.Common.Interfaces
{
    public interface IModelKey<TId>
    {
        public TId PrimaryKeyId { get; } 
    }
}
