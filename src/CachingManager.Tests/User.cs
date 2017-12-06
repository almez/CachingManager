using System;
using System.Collections.Generic;
using System.Text;
using CachingManager.Abstraction;

namespace CachingManager.Tests
{
    public class User : IMesurable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long GetSizeInBytes()
        {
            return sizeof(int) + (sizeof(char) * this.Name?.Length ?? 0);
        }
    }
}
