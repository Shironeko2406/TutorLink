using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using TutorLinkAPI.BusinessLogics.IServices;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class ApplyService : IApplyService
    {
        private readonly IGenericRepository<Apply> _applyRepository;

        public ApplyService(IGenericRepository<Apply> applyRepository)
        {
            _applyRepository = applyRepository;
        }

        public List<Apply> GetAllApplies() => _applyRepository.GetAll().ToList();

        public Apply GetApplyById(Guid applyId) => _applyRepository.Get(a => a.ApplyId == applyId);

        public void CreateApply(Apply apply)
        {
            _applyRepository.Add(apply);
            _applyRepository.SaveChanges();
        }

        public void UpdateApply(Apply apply)
        {
            _applyRepository.Update(apply);
            _applyRepository.SaveChanges();
        }

        public void DeleteApply(Guid applyId)
        {
            _applyRepository.Delete(applyId);
            _applyRepository.SaveChanges();
        }
    }
}
