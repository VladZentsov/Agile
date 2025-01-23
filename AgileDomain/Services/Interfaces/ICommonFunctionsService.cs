using AgileDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services.Interfaces
{
    public interface ICommonFunctionsService
    {
        Task HandleUpdateConflictAsync(int entityId, string entityType, DateTime lastKnownUpdate);
        Task<List<dynamic>> SearchEntitiesAsync(SearchFilterDto filterDto);
        Task ExportDataAsync(ExportDataDto exportDto);
        //Task<List<ChangeLogDto>> GetChangeLogsAsync(string entityType, int entityId);
    }
}
