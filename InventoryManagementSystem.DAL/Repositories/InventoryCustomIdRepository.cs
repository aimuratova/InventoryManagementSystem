using Dapper;
using InventoryManagementSystem.DAL.Interfaces;
using InventoryManagementSystem.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Repositories
{
    public class InventoryCustomIdRepository : IInventoryCustomIdRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public InventoryCustomIdRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<InventoryCustomIdValueModel>> GetAllByInventoryId(int inventoryId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var resultList = (await connection.QueryAsync<InventoryCustomIdValueModel>(
                    "[dbo].[spGetInventoryCustomIds]",
                    new { InventoryId = inventoryId },
                    commandType: CommandType.StoredProcedure
                )).ToList();
                return resultList;
            }
        }
    }
}
