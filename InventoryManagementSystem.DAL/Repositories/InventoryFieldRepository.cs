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
    public class InventoryFieldRepository : IInventoryFieldRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public InventoryFieldRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<InventoryFieldModel>> GetFieldsByItemIdAsync(int itemId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var resultList = (await connection.QueryAsync<InventoryFieldModel>(
                    "[dbo].[spGetInventoryItemFieldsById]",
                    new { InventoryItemId = itemId },
                    commandType: CommandType.StoredProcedure
                )).ToList();
                return resultList;
            }
        }
    }
}
