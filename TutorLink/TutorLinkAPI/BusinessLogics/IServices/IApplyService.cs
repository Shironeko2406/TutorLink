using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Entities;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IApplyService
    {
        Task<ApplyViewModel> AddNewApply(Guid tutorId, Guid postId, AddApplyViewModel applyViewModel);
        Apply GetApplyById(Guid applyId);
        void UpdateApplyStatus(Guid applyId, UpdateApplyViewModel model);
        void DeleteApply(Guid applyId);
        IEnumerable<Apply> GetAppliesByTutorId(Guid tutorId);
        IEnumerable<Apply> GetAllApplies();
    }
}
