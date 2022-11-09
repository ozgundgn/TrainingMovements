
using Entities;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Abstract;
using TrainingMovementService.Authorization;

namespace TrainingMovementService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TrainingMovementController : ControllerBase
    {

        private readonly ILogger<TrainingMovementController> _logger;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMovementRepository _movementRepository;

        public TrainingMovementController(ILogger<TrainingMovementController> logger, ITrainingRepository trainingMovementRepository, IMovementRepository movementRepository)
        {
            _trainingRepository = trainingMovementRepository;
            _movementRepository = movementRepository;
            _logger = logger;
        }

        [HttpGet("getalltraining")]
        [Authorize]
        public async Task<IEnumerable<Training>> GetAllTrainigAsync()
        {
            return await _trainingRepository.GetAllAsync("GetAllTraining");
        }
        [HttpPost("addtraining")]

        public async Task<int> AddTraning(Training training)
        {
            return await _trainingRepository.AddAsync(training, "TrainingInsert");
        }

        [HttpPut("updatetraining")]
        public async Task<int> UpdateTraining(Training training)
        {
            if (training.Id > 0)
            {
                var entity = await _trainingRepository.GetById(training.Id, "GetTrainingById");
                if (!string.IsNullOrWhiteSpace(training.TrainingName))
                    entity.TrainingName = training.TrainingName;

                if (!string.IsNullOrWhiteSpace(training.Region))
                    entity.Region = training.Region;

                if (training.Difficulty > 0)
                    entity.Difficulty = training.Difficulty;

                if (training.TrainingTime > 0)
                    entity.TrainingTime = training.TrainingTime;

                entity.UpdatedBy = 1; //ekleyen kullanýcý id si
                return await _trainingRepository.UpdateAsync(training, "TrainingUpdate");

            }

            return await Task.FromResult<int>(0);
        }

        [HttpDelete("trainingdelete")]
        public async Task<int> DeleteAsync(long id)
        {
            var result = await _movementRepository.DeleteAsync(id, "MovementDelete");

            return await _trainingRepository.DeleteAsync(id, "TrainingDelete");
        }
        [HttpGet("getalltrainingwithmovements")]

        public async Task<IEnumerable<Training>> GetAllTrainingsWithMovements()
        {
            return await _trainingRepository.GetAllTrainingsWithMovements();
        }
        [HttpGet("getalltrainingwithmovementsbyandfilter")]
        public async Task<IEnumerable<Training>> GetAllTrainingsWithMovementsByAndFilter(Training training)
        {
            var list = await _trainingRepository.GetAllTrainingsWithMovements();

            if (training != null)
            {
                if (training.Difficulty > 0)
                    list = list.Where(t => t.Difficulty == training.Difficulty).ToList();

                if (training.TrainingTime > 0)
                    list = list.Where((t) => t.TrainingTime == training.TrainingTime).ToList();

                if (!string.IsNullOrEmpty(training.Region))
                    list = list.Where((t) => t.Region == training.Region).ToList();

                if (!string.IsNullOrEmpty(training.TrainingName))
                    list = list.Where((t) => t.TrainingName == training.TrainingName).ToList();
            }
            return list;
        }
        [HttpGet("getalltrainingwithmovementsbyorfilter")]

        public async Task<IEnumerable<Training>> GetAllTrainingsWithMovementsByOrFilter(Training training)
        {
            var list = await _trainingRepository.GetAllTrainingsWithMovements();
            var result = new List<Training>();
            if (training != null)
            {
                if (training.Difficulty > 0)
                {
                    var difficultyList = list.Where(t => t.Difficulty == training.Difficulty).ToList();
                    result.AddRange(difficultyList);
                }

                if (training.TrainingTime > 0)
                {
                    var trainingTimeList = list.Where((t) => t.TrainingTime == training.TrainingTime).ToList();
                    result.AddRange(trainingTimeList);
                }

                if (!string.IsNullOrEmpty(training.Region))
                {
                    var regionList = list.Where((t) => t.Region == training.Region).ToList();
                    result.AddRange(regionList);
                }

                if (!string.IsNullOrEmpty(training.TrainingName))
                {
                    var regionList = list.Where((t) => t.TrainingName == training.TrainingName).ToList();
                    result.AddRange(regionList);
                }
            }
            return result;
        }

        [HttpGet("gettrainingbyid/{id}")]
        public async Task<Training> GetTrainingById(long id)
        {
            return await _trainingRepository.GetById(id, "GetTrainingById");
        }

    }
}