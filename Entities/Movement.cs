using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Movement : EntityBase
    {
        public string MovementName { get; set; }
        public long TrainingId { get; set; }
    }
}
