using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IApplyService
    {
        Task<IEnumerable<ApplyViewModel>> GetAllApplies();
        Task<ApplyViewModel> GetApplyById(Guid applyId);
        Task<ApplyViewModel> AddNewApply(AddApplyViewModel applyViewModel);
        Task<ApplyViewModel> UpdateApply(Guid applyId, UpdateApplyViewModel applyViewModel);
        Task<bool> DeleteApply(Guid applyId);
    }
}
