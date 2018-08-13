using System;

namespace MvvmJonasTest.Models
{
    public class ModelBase : IdBase
    {
        public string Name { get; set; }
    }

    public class IdBase
    {
        public Guid Id { get; set; }

        public IdBase()
        {
            Id = Guid.NewGuid();
        }
    }
}