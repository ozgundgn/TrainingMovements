
using Core.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;
using Core;
using Dapper;

namespace Repository.Dapper
{
    public class DapperRepositoryBase<TDbConnection, TEntity> : IEntityRepository<TEntity> where TDbConnection : class, IDbConnection, new()
       where TEntity : class, IEntity, new()
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperRepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }


        public async Task<int> AddAsync(TEntity entity, string procedure)
        {
            using (TDbConnection conn = new TDbConnection())
            {
              
                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                return await conn.ExecuteAsync(procedure, entity, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> DeleteAsync(int id, string procedure)
        {
            using (TDbConnection conn = new TDbConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Identifier", id,DbType.Int32,ParameterDirection.Input);

                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open(); 

                var results = await conn.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                return results;
            }
        }

        public async Task<TEntity> GetById(int id, string procedure)
        {
            using (TDbConnection conn = new TDbConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Identifier", id, DbType.Int32, ParameterDirection.Input);

                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var result = await conn.QueryFirstOrDefaultAsync<TEntity>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string procedure)
        {
            using (TDbConnection conn = new TDbConnection())
            {
                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var results = await conn.QueryAsync<TEntity>(procedure, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
        }

        public async Task<int> UpdateAsync(TEntity entity, string procedure)
        {

            using (TDbConnection conn = new TDbConnection())
            {
                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                var results = await conn.ExecuteAsync(procedure, entity, commandType: CommandType.StoredProcedure);
                return results;
            }
        }

    }
}
