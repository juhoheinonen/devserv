using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevServ.SharedKernel.Interfaces
{
    public interface IFilter<T>
    {
        bool Filter(T entity);
    }
}
