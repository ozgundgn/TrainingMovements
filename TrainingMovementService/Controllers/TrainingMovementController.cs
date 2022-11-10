
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

        #region Training(Antrenman)

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

                return await _trainingRepository.UpdateAsync(entity, "TrainingUpdate");

            }

            return await Task.FromResult<int>(0);
        }

        [HttpDelete("deletetraining")]
        public async Task<int> DeleteAsync(int id)
        {
            var result = await _movementRepository.DeleteAsync(id, "MovementDeleteRelatedTraining");

            return await _trainingRepository.DeleteAsync(id, "TrainingDelete");
        }

        [HttpGet("getalltraining")]
        [Authorize]
        public async Task<IEnumerable<Training>> GetAllTrainigAsync()
        {
            return await _trainingRepository.GetAllAsync("GetAllTraining");
        }

        [HttpGet("gettrainingbyid/{id}")]
        public async Task<Training> GetTrainingById(int id)
        {
            return await _trainingRepository.GetById(id, "GetTrainingById");
        }

        [HttpGet("gettrainingbyidwithmovements/{id}")]
        public async Task<Training> GetTrainingByIdWithMovements(int id)
        {
            return await _trainingRepository.GetTrainingByIdWithMovements(id, "GetTrainingByIdWithMovements");
        }

        [HttpGet("getalltrainingwithmovements")]
        public async Task<IEnumerable<Training>> GetAllTrainingsWithMovements()
        {
            return await _trainingRepository.GetAllTrainingsWithMovements(new Training());
        }

        [HttpGet("getalltrainingwithmovementsbyandfilter")]
        public async Task<IEnumerable<Training>> GetAllTrainingsWithMovementsByAndFilter(Training training)
        {
            var list = await _trainingRepository.GetAllTrainingsWithMovements(training);
            return list;
        }

        [HttpGet("getalltrainingwithmovementsbyorfilter")]
        public async Task<IEnumerable<Training>> GetAllTrainingsWithMovementsByOrFilter(Training training)
        {
            var list = await _trainingRepository.GetAllTrainingsWithMovementsByOrFilter(training);
            return list.DistinctBy(x => x.Id).ToList();
        }

        #endregion

        #region Movement(Hareket)

        [HttpPost("addmovement")]
        //[Authorize]
        public async Task<int> AddMovement(Movement movement)
        {
            return await _movementRepository.AddAsync(movement, "MovementInsert");
        }

        [HttpPut("updatemovement")]
        public async Task<int> UpdateMovement(Movement movement)
        {
            if (movement.Id > 0)
            {
                var entity = await _movementRepository.GetById(movement.Id, "GetMovementById");
                entity.Identifier = movement.Id;
                if (!string.IsNullOrWhiteSpace(movement.MovementName))
                    entity.MovementName = movement.MovementName;

                if (movement.TrainingId > 0)
                    entity.TrainingId = movement.TrainingId;

                entity.UpdatedBy = 1; //güncelleyen kullanýcý id si
                return await _movementRepository.UpdateAsync(entity, "MovementUpdate");

            }

            return await Task.FromResult<int>(0);
        }

        [HttpDelete("deletemovement")]
        public async Task<int> DeleteMovement(int id)
        {
            return await _movementRepository.DeleteAsync(id, "MovementDelete");
        }

        [HttpGet("getallmovement")]

        public async Task<IEnumerable<Movement>> GetAllMovements()
        {
            return await _movementRepository.GetAllAsync("GetAllMovement");
        }

        [HttpGet("getmovementbyid/{id}")]
        public async Task<Movement> GetMovementById(int id)
        {
            return await _movementRepository.GetById(id, "GetMovementById");
        }

        #endregion

    }
}