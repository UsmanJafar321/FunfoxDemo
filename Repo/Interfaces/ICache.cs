using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Int
{
   public interface ICache
    {
        int ItemLifeMinutes { get; }
        bool Contains(string key);
        void Add(string key, object value);
        void Remove(string key);
        T Get<T>(string key);
    }
}
