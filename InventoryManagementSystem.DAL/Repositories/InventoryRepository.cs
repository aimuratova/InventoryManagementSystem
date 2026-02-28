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
    public class InventoryRepository : IInventoryRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public InventoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<InventoryItemModel?> GetInventoryItemById(int inventoryId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<InventoryItemModel>(
                    "[dbo].[spGetInventoryItemById]",
                    new { InventoryItemId = inventoryId },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<List<InventoryItemModel>> GetInventoryItems()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var resultList = (await connection.QueryAsync<InventoryItemModel>(
                    "[dbo].[spGetInventoryItems]",
                    commandType: CommandType.StoredProcedure
                )).ToList();
                return resultList;
            }
        }
        
        public async Task<DataSet> GetInventoryValues()
        {
            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("[dbo].[spGetInventoryValues]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                    }
                }
            }

            return dataSet;
        }

    }
}
