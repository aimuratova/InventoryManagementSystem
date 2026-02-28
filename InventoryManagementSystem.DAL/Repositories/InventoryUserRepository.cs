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
    public class InventoryUserRepository : IInventoryUserRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public InventoryUserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddInventoryItemUserId(string userId, List<int> inventoryItems)
        {
            // Create a DataTable to hold the inventory item IDs
            var table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            // Populate the DataTable with the user ID and inventory item IDs
            foreach (var itemId in inventoryItems)
            {
                table.Rows.Add(itemId);
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("dbo.[spInsertInventoryItemUserBatch]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserId", userId));
                    var parameter = command.Parameters.AddWithValue("@InventIds", table);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.UserInventoryItemTableType";

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<InventoryItemUserModel>> GetInventoryItemsUserModels(string? userId = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var resultList = (await connection.QueryAsync<InventoryItemUserModel>(
                    "[dbo].[spGetInventoryItemsUsers]",
                    new { UserId = userId },
                    commandType: CommandType.StoredProcedure
                )).ToList();
                return resultList;
            }
        }

        public async Task<List<string>> GetInventoryUsersByInventoryId(int inventoryId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var resultList = (await connection.QueryAsync<string>(
                    "[dbo].[spGetInventoryUsersByInventoryId]",
                    new { InventoryId = inventoryId },
                    commandType: CommandType.StoredProcedure
                )).ToList();
                return resultList;
            }
        }

        public async Task RemoveInventoryItemUserId(string userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("[dbo].[spDeleteInventoryItemUsers]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserId", userId));

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
