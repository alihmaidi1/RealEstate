using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Shared.Abstraction.Entities;

public class TEntity: IEntity
{

    public Guid Id{ get; set; }
    
}
