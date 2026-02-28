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
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DictionaryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Dictionary<int, string>> GetCategoriesAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync(
                    "[dbo].[spGetCategories]",
                    commandType: CommandType.StoredProcedure
                );

                // Предполагается, что результат содержит поля "Id" и "Name"
                var resultList = result.ToDictionary(
                    x => (int)x.Id,
                    x => (string)x.Title
                );
                return resultList;
            }
        }

        public async Task<Dictionary<int, string>> GetInventoriesAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync(
                    "[dbo].[spGetInventories]",
                    commandType: CommandType.StoredProcedure
                );

                // Предполагается, что результат содержит поля "Id" и "Name"
                var resultList = result.ToDictionary(
                    x => (int)x.Id,
                    x => (string)x.Title
                );
                return resultList;
            }
        }

        public async Task<Dictionary<int, string>> GetRolesAsync(string filter)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync(
                    "[dbo].[spGetRoles]",
                    new { Name = filter },
                    commandType: CommandType.StoredProcedure
                );

                // Предполагается, что результат содержит поля "Id" и "Name"
                var resultList = result.ToDictionary(
                    x => (int)x.Id,
                    x => (string)x.Name
                );
                return resultList;
            }
        }
    }
}
