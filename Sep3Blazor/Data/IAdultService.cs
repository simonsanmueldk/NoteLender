using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sep3Blazor.Data
{
    public interface IAdultService
    {
        public IList<String> AdultsList { get; }

        public void Save();
        void Remove(int adultId);

        public Task<IList<string>> Connect(String s);
        
    }
}