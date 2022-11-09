using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ExcepitonLog: IEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Message { get; set; }
    }
}
