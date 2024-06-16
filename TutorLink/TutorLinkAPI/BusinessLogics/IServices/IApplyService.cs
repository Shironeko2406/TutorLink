using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IApplyService
    {
        Task AddNewApply(Guid tutorId, Guid postRequestId, ApplyStatuses status);
        Task<IEnumerable<Apply>> GetAllApplies();
        Task<Apply> GetApplyById(Guid id);
        Task UpdateApply(Guid id, ApplyStatuses status);
        Task DeleteApply(Guid id);
    }
}
