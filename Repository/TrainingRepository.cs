using Dapper;
using Entities;
using Entities.Dto;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Repository.Abstract;
using Repository.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TrainingRepository : DapperRepositoryBase<MySqlConnection, Training>, ITrainingRepository
    {
        private readonly string _connectionString;
        public TrainingRepository(IConfiguration configuration) : base(configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<IEnumerable<Training>> GetAllTrainingsWithMovements(Training training)
        {
            var trainingDic = new Dictionary<long, Training>();
            using (MySqlConnection conn = new MySqlConnection())
            {
                
                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var parameters = new DynamicParameters();
                parameters.Add("TrainingName", training.TrainingName, DbType.String, ParameterDirection.Input);
                parameters.Add("Difficulty", training.Difficulty, DbType.Int32, ParameterDirection.Input);
                parameters.Add("Region", training.Region, DbType.String, ParameterDirection.Input);
                parameters.Add("TrainingTime", training.TrainingTime, DbType.Int32, ParameterDirection.Input);

                var results = await conn.QueryAsync<Training, Movement, Training>("GetAllTrainingsWithMovements", (training, movement) =>
                {
                    if (!trainingDic.TryGetValue(training.Id, out var currentTraining))
                    {
                        currentTraining = training;
                        trainingDic.Add(training.Id, currentTraining);
                    }
                    currentTraining.Movements.Add(movement);
                    return currentTraining;
                }, parameters,commandType: CommandType.StoredProcedure);
                return results.DistinctBy(t => t.Id).ToList();
            }
        }

        public async Task<IEnumerable<Training>> GetAllTrainingsWithMovementsByOrFilter(Training training)
        {
            var trainingDic = new Dictionary<long, Training>();
            using (MySqlConnection conn = new MySqlConnection())
            {

                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var parameters = new DynamicParameters();
                parameters.Add("TrainingName", training.TrainingName, DbType.String, ParameterDirection.Input);
                parameters.Add("Difficulty", training.Difficulty, DbType.Int32, ParameterDirection.Input);
                parameters.Add("Region", training.Region, DbType.String, ParameterDirection.Input);
                parameters.Add("TrainingTime", training.TrainingTime, DbType.Int32, ParameterDirection.Input);

                var results = await conn.QueryAsync<Training, Movement, Training>("GetAllTrainingsWithMovementsByOrFilter", (training, movement) =>
                {
                    if (!trainingDic.TryGetValue(training.Id, out var currentTraining))
                    {
                        currentTraining = training;
                        trainingDic.Add(training.Id, currentTraining);
                    }
                    currentTraining.Movements.Add(movement);
                    return currentTraining;
                }, parameters, commandType: CommandType.StoredProcedure);
                return results.DistinctBy(t => t.Id).ToList();
            }
        }
        

        public async Task<Training> GetTrainingByIdWithMovements(int id, string procedure)
        {
            var trainingDic = new Dictionary<long, Training>();

            using (MySqlConnection conn = new MySqlConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Identifier", id, DbType.Int32, ParameterDirection.Input);
                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var results = await conn.QueryAsync<Training, Movement, Training>(procedure, (training, movement) =>
                {
                    if (!trainingDic.TryGetValue(training.Id, out var currentTraining))
                    {
                        currentTraining = training;
                        trainingDic.Add(training.Id, currentTraining);
                    }
                    currentTraining.Movements.Add(movement);
                    return currentTraining;
                }, parameters, commandType: CommandType.StoredProcedure);
                return results.DistinctBy(x => x.Id).ToList().First();
            }
        }
    }
}
