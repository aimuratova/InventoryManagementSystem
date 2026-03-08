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
    public class InventoryValueRepository : IInventoryValueRepository
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public InventoryValueRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddValues(InventoryItemValueModel value)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("dbo.[spInsertInventoryValue]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@InventoryId", value.InventoryId));
                    command.Parameters.Add(new SqlParameter("@RowNum",      value.RowNum));
                    command.Parameters.Add(new SqlParameter("@CreatedBy",   value.CreatedBy));
                    command.Parameters.Add(new SqlParameter("@CreatedAt",   value.CreatedAt));
                    command.Parameters.Add(new SqlParameter("@CustomId",    value.CustomId));
                    command.Parameters.Add(new SqlParameter("@Singleline1", value.Singleline1));
                    command.Parameters.Add(new SqlParameter("@Singleline2", value.Singleline2));
                    command.Parameters.Add(new SqlParameter("@Singleline3", value.Singleline3));
                    command.Parameters.Add(new SqlParameter("@Multiline1",  value.Multiline1));
                    command.Parameters.Add(new SqlParameter("@Multiline2",  value.Multiline2));
                    command.Parameters.Add(new SqlParameter("@Multiline3",  value.Multiline3));
                    command.Parameters.Add(new SqlParameter("@Num1",        value.Num1));
                    command.Parameters.Add(new SqlParameter("@Num2",        value.Num2));
                    command.Parameters.Add(new SqlParameter("@Num3",        value.Num3));
                    command.Parameters.Add(new SqlParameter("@Check1",      value.Check1));
                    command.Parameters.Add(new SqlParameter("@Check2",      value.Check2));
                    command.Parameters.Add(new SqlParameter("@Check3",      value.Check3));
                    command.Parameters.Add(new SqlParameter("@Datetime1",   value.Datetime1));
                    command.Parameters.Add(new SqlParameter("@Datetime2",   value.Datetime2));
                    command.Parameters.Add(new SqlParameter("@Datetime3",   value.Datetime3));

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task Delete(int valueId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[dbo].[spDeleteInventoryItemValue]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", valueId));

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<InventoryItemValueModel> GetInventoryValueById(int valueId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var result = await connection.QuerySingleOrDefaultAsync<InventoryItemValueModel>(
                    "[dbo].[spGetInventoryValueById]",
                    new { Id = valueId },
                    commandType: CommandType.StoredProcedure
                );

                if (result == null)
                {
                    throw new InvalidOperationException($"Inventory value with Id {valueId} not found.");
                }

                return result;
            }
        }

        public async Task<DataTable> GetInventoryValuesById(int inventoryId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[dbo].[spGetInventoryValuesById]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@InventoryItemId", SqlDbType.Int).Value = inventoryId;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        public async Task<ResultDbModel> Update(InventoryItemValueModel valueModel)
        {
            var updateResult = new ResultDbModel();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("dbo.[spUpdateInventoryValue]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("Id", valueModel.Id));
                        command.Parameters.Add(new SqlParameter("@RowNum", valueModel.RowNum));
                        
                        command.Parameters.Add(new SqlParameter("@CustomId", valueModel.CustomId));
                        command.Parameters.Add(new SqlParameter("@Singleline1", valueModel.Singleline1));
                        command.Parameters.Add(new SqlParameter("@Singleline2", valueModel.Singleline2));
                        command.Parameters.Add(new SqlParameter("@Singleline3", valueModel.Singleline3));
                        command.Parameters.Add(new SqlParameter("@Multiline1", valueModel.Multiline1));
                        command.Parameters.Add(new SqlParameter("@Multiline2", valueModel.Multiline2));
                        command.Parameters.Add(new SqlParameter("@Multiline3", valueModel.Multiline3));
                        command.Parameters.Add(new SqlParameter("@Num1", valueModel.Num1));
                        command.Parameters.Add(new SqlParameter("@Num2", valueModel.Num2));
                        command.Parameters.Add(new SqlParameter("@Num3", valueModel.Num3));
                        command.Parameters.Add(new SqlParameter("@Check1", valueModel.Check1));
                        command.Parameters.Add(new SqlParameter("@Check2", valueModel.Check2));
                        command.Parameters.Add(new SqlParameter("@Check3", valueModel.Check3));
                        command.Parameters.Add(new SqlParameter("@Datetime1", valueModel.Datetime1));
                        command.Parameters.Add(new SqlParameter("@Datetime2", valueModel.Datetime2));
                        command.Parameters.Add(new SqlParameter("@Datetime3", valueModel.Datetime3));

                        await command.ExecuteNonQueryAsync();
                    }
                }

                updateResult.IsUpdated = true;
            }
            catch (Exception ex) 
            {
                updateResult.IsUpdated = false;
                updateResult.ErrorMessage = ex.Message;
            }

            return updateResult;
        }
    }
}
