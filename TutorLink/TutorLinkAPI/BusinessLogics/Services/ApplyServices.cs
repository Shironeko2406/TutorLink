using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using TutorLinkAPI.BusinessLogics.IServices;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class ApplyServices : IApplyService
    {
        private readonly ApplyRepository _applyRepository;

        public ApplyServices(ApplyRepository applyRepository)
        {
            _applyRepository = applyRepository;
        }

        public List<Apply> GetAllApplies() => _applyRepository.GetAll().ToList();

        public Apply GetApplyById(Guid applyId) => _applyRepository.Get(a => a.ApplyId == applyId);

        public void CreateApply(Apply apply)
        {
            apply.ApplyId = Guid.NewGuid();
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
