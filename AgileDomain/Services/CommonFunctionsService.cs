using AgileDataAccess.Entities;
using AgileDataAccess.UoW;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services
{
    public class CommonFunctionsService : ICommonFunctionsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommonFunctionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleUpdateConflictAsync(int entityId, string entityType, DateTime lastKnownUpdate)
        {
            var entity = entityType switch
            {
                "Project" => (IEntity)await _unitOfWork.ProjectRepository.GetByIdAsync(entityId),
                "Task" => (IEntity)await _unitOfWork.TaskRepository.GetByIdAsync(entityId),
                "Sprint" => (IEntity)await _unitOfWork.SprintRepository.GetByIdAsync(entityId),
                _ => throw new ArgumentException("Invalid entity type")
            };

            if (entity == null)
                throw new KeyNotFoundException("Entity not found");

            if (entity.Updated > lastKnownUpdate)
                throw new InvalidOperationException("Update conflict detected");
        }

        public async Task<List<dynamic>> SearchEntitiesAsync(SearchFilterDto filterDto)
        {
            var projects = await _unitOfWork.ProjectRepository.FindAsync(p =>
                (string.IsNullOrEmpty(filterDto.Keyword) || p.Name.Contains(filterDto.Keyword)) &&
                (string.IsNullOrEmpty(filterDto.Status) || p.Status.ToString() == filterDto.Status));

            var tasks = await _unitOfWork.TaskRepository.FindAsync(t =>
                (string.IsNullOrEmpty(filterDto.Keyword) || t.Title.Contains(filterDto.Keyword)) &&
                (string.IsNullOrEmpty(filterDto.Status) || t.Status.ToString() == filterDto.Status) &&
                (string.IsNullOrEmpty(filterDto.Priority) || t.Priority.ToString() == filterDto.Priority));

            var sprints = await _unitOfWork.SprintRepository.FindAsync(s =>
                (string.IsNullOrEmpty(filterDto.Keyword) || s.Title.Contains(filterDto.Keyword)) &&
                (!filterDto.StartDate.HasValue || s.StartDate >= filterDto.StartDate) &&
                (!filterDto.EndDate.HasValue || s.EndDate <= filterDto.EndDate));

            return new List<dynamic> { projects, tasks, sprints };
        }

        public async Task ExportDataAsync(ExportDataDto exportDto)
        {
            var data = exportDto.EntityType switch
            {
                "Project" => (IEnumerable<IEntity>)await _unitOfWork.ProjectRepository.GetAllAsync(),
                "Task" => (IEnumerable<IEntity>)await _unitOfWork.TaskRepository.GetAllAsync(),
                "Sprint" => (IEnumerable<IEntity>)await _unitOfWork.SprintRepository.GetAllAsync(),
                "Comment" => (IEnumerable<IEntity>)await _unitOfWork.CommentRepository.FindAsync(c => c.EntityID == exportDto.EntityId),
                _ => throw new ArgumentException("Invalid entity type")
            };

            var filePath = $"{exportDto.EntityType}_{DateTime.UtcNow:yyyyMMddHHmmss}.{exportDto.FileType.ToLower()}";

            using var writer = new StreamWriter(filePath);

            if (exportDto.FileType.Equals("CSV", StringComparison.OrdinalIgnoreCase))
            {
                using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
                await csv.WriteRecordsAsync(data);
            }
            else if (exportDto.FileType.Equals("Excel", StringComparison.OrdinalIgnoreCase))
            {
                // Add Excel export logic (using libraries like EPPlus or ClosedXML)
            }
        }

        //public async Task<List<ChangeLogDto>> GetChangeLogsAsync(string entityType, int entityId)
        //{
        //    //var logs = await _unitOfWork.ChangeLogs.FindAsync(cl => cl.EntityType == entityType && cl.EntityID == entityId);

        //    //return logs.Select(log => new ChangeLogDto
        //    //{
        //    //    Id = log.ChangeLogID,
        //    //    EntityType = log.EntityType,
        //    //    EntityId = log.EntityID,
        //    //    PropertyChanged = log.PropertyChanged,
        //    //    OldValue = log.OldValue,
        //    //    NewValue = log.NewValue,
        //    //    Timestamp = log.Timestamp
        //    //}).ToList();
        //}
    }
}
