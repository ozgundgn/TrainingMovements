using Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Repository.Abstract;
using Repository.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ExceptionLoggingRepository : DapperRepositoryBase<MySqlConnection, ExcepitonLog>, IExceptionLoggingRepository
    {
        public ExceptionLoggingRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
