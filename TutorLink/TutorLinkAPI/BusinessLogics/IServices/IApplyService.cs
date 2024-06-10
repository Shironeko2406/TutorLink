using System.Collections.Generic;
using DataLayer.Entities;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IApplyService
    {
        List<Apply> GetAllApplies();
        Apply GetApplyById(Guid applyId);
        void CreateApply(Apply apply);
        void UpdateApply(Apply apply);
        void DeleteApply(Guid applyId);
    }
}
