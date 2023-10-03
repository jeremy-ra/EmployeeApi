using System;

namespace Employee.Entity
{
    public abstract class EntityBase
    {        
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual DateTime? UpdatedDate { get; set; }
    }
}
