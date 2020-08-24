using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Core.CrossCuttingConcerns.Postsharp.Caching
{
    public interface ICacheManagerPostsharp
    {

        T Get<T>(string key);
        void Add(string key, object data, int cacheTime);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
        void Clear();
    }
}
