using System;
using System.Collections.Generic;
using TutorLinkAPI.ViewModel;
using DataLayer.Entities;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IApplyService
    {
        Apply GetApplyById(Guid applyId);
        Apply AddNewApply(Guid postId, Guid tutorId);
        void UpdateApplyStatus(Guid applyId, UpdateApplyViewModel model);
        void DeleteApply(Guid applyId);
        List<Apply> GetAppliesByTutorId(Guid tutorId);
        List<Apply> GetAllApplies();
    }
}
