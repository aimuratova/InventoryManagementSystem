using Dapper;
using InventoryManagementSystem.DAL.Interfaces;
using InventoryManagementSystem.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
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

        public async Task<ResultDbModel> UpdateInventoryItem(InventoryItemModel inventoryModel, List<string> inventoryUsers, 
            List<InventoryFieldModel> fieldsList)
        {
            var result = new ResultDbModel();
            result.IsUpdated = false;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        using (var command = new SqlCommand("[dbo].[spUpdateInventory]", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@Id", inventoryModel.InventoryItemId));
                            command.Parameters.Add(new SqlParameter("@Title", inventoryModel.InventoryItemTitle));
                            command.Parameters.Add(new SqlParameter("@CategoryId", inventoryModel.CategoryId));
                            command.Parameters.Add(new SqlParameter("@Description", inventoryModel.Description));
                            command.Parameters.Add(new SqlParameter("@IsPublic", inventoryModel.IsPublic));

                            await command.ExecuteNonQueryAsync();
                        }

                        if (inventoryModel.IsPublic)
                        {
                            //delete all users
                            using (var command = new SqlCommand("[dbo].[spDeleteInventoryUsersByInventoryId]", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@InventoryId", inventoryModel.InventoryItemId));

                                await command.ExecuteNonQueryAsync();
                            }
                        }
                        else
                        {
                            var table = new DataTable();
                            table.Columns.Add("UserId", typeof(string));
                            // Populate the DataTable with the user ID and inventory item IDs
                            foreach (var userId in inventoryUsers)
                            {
                                table.Rows.Add(userId);
                            }

                            using (var command = new SqlCommand("dbo.[spInsertInventoryUserInventoryBatch]", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@InventoryId", inventoryModel.InventoryItemId));
                                var parameter = command.Parameters.AddWithValue("@UserIds", table);
                                parameter.SqlDbType = SqlDbType.Structured;
                                parameter.TypeName = "dbo.UserInventoryItemTableType2";

                                await command.ExecuteNonQueryAsync();
                            }
                        }

                        //fields update

                        await transaction.CommitAsync();

                        result.IsUpdated = true;
                        result.ErrorMessage = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        // If any exception occurs, rollback the transaction
                        await transaction.RollbackAsync();
                        result.ErrorMessage = $"Error: {ex.Message}";
                    }
                }
            }            

            return result;
        }
    }
}
