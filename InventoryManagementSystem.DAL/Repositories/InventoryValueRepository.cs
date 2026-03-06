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
    }
}
