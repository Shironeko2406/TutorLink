using DataLayer.Entities;
using TutorLinkAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IApplyService
    {
        Task<IEnumerable<Apply>> GetAllApplies();
        Task<Apply> GetApplyById(Guid applyId);
        Task<Apply> AddNewApply(AddApplyViewModel applyViewModel);
        Task<Apply> UpdateApply(Guid applyId, UpdateApplyViewModel applyViewModel);
        Task<bool> DeleteApply(Guid applyId);
    }
}
