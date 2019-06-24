using System;
using System.Collections.Generic;
using System.Text;

namespace Cj.Entities.BaseEntity
{
    public interface IEntity:IEntity<string>
    {
    }
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        TPrimaryKey Id { get; set; }

        bool IsTransient();
    }
}
