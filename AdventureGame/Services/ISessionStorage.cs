using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureGame.Services
{
    public interface ISessionStorage<T>
    {
        public T LoadOrCreate(string key);
        public void Save(string key, T data);
    }
}
