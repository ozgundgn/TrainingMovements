using Core;
using Entities;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface ITrainingRepository : IEntityRepository<Training>
    {
        Task<IEnumerable<Training>> GetAllTrainingsWithMovements();
        Task<Training> GetTrainingByIdWithMovements(int id, string procedure);


    }
}
