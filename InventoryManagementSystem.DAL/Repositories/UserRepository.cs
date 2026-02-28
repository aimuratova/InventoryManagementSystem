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
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Add(UsersModel user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("[dbo].[spInsertUser]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Id", user.Id));
                    command.Parameters.Add(new SqlParameter("@Email", user.Email));
                    command.Parameters.Add(new SqlParameter("@EmailConfirmed", false));
                    command.Parameters.Add(new SqlParameter("@PasswordHash", user.PasswordHash));
                    command.Parameters.Add(new SqlParameter("@UserName", user.UserName));

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<UsersModel?> GetByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {                
                return await connection.QuerySingleOrDefaultAsync<UsersModel>(
                    "[dbo].[spGetByEmail]",
                    new { Email = email },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<UsersModel?> GetUserById(string userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<UsersModel>(
                    "[dbo].[spGetById]",
                    new { Id = userId },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<List<UsersModel>> GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var resultList = (await connection.QueryAsync<UsersModel>(
                    "[dbo].[spGetAll]",
                    commandType: CommandType.StoredProcedure
                )).ToList();
                return resultList;
            }
        }

        public async Task UpdateAsync(UsersModel user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("[dbo].[spUpdateUser]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", user.Id));
                    //command.Parameters.Add(new SqlParameter("@EmailConfirmed", user.EmailConfirmed));                    
                    command.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
