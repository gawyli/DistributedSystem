using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Core.Abstractions;
public interface IEntityIdFactory
{
    string NewId();
}
