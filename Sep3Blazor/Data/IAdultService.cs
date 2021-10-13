using System;
using System.Collections.Generic;

namespace Assigment_1.Data
{
    public interface IAdultService
    {
        public IList<String> AdultsList { get; }

        public void Save();
        void Remove(int adultId);
    }
}