using InventoryManagementSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Interfaces
{
    public interface IExternalSyncService
    {
        Task<ResultModel> SyncToSalesForce(string userId, string companyName, string phone);
    }
}
