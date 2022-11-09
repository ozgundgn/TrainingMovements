using Core;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IExceptionLoggingRepository : IEntityRepository<ExcepitonLog>
    {
    }
}
