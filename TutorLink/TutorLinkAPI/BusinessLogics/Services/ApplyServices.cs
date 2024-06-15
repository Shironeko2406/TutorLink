using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class ApplyService : IApplyService
    {
        private readonly IGenericRepository<Apply> _applyRepository;
        private readonly IMapper _mapper;

        public ApplyService(IGenericRepository<Apply> applyRepository, IMapper mapper)
        {
            _applyRepository = applyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplyViewModel>> GetAllApplies()
        {
            var applies = await _applyRepository.GetAllWithAsync();
            return _mapper.Map<IEnumerable<ApplyViewModel>>(applies); // Map applies to ApplyViewModel
        }

        public async Task<ApplyViewModel> GetApplyById(Guid applyId)
        {
            var apply = await _applyRepository.GetByIdAsync(applyId);
            if (apply == null) return null;
            return _mapper.Map<ApplyViewModel>(apply); // Map apply to ApplyViewModel
        }

        public async Task<ApplyViewModel> AddNewApply(AddApplyViewModel applyViewModel)
        {
            var apply = _mapper.Map<Apply>(applyViewModel);
            apply.ApplyId = Guid.NewGuid();

            await _applyRepository.AddSingleWithAsync(apply);
            await _applyRepository.SaveChangesAsync();

            return _mapper.Map<ApplyViewModel>(apply); // Map apply to ApplyViewModel
        }

        public async Task<ApplyViewModel> UpdateApply(Guid applyId, UpdateApplyViewModel applyViewModel)
        {
            var apply = await _applyRepository.GetByIdAsync(applyId);
            if (apply == null) return null;

            _mapper.Map(applyViewModel, apply);

            await _applyRepository.UpdateWithAsync(apply);
            await _applyRepository.SaveChangesAsync();

            return _mapper.Map<ApplyViewModel>(apply); // Map apply to ApplyViewModel
        }

        public async Task<bool> DeleteApply(Guid applyId)
        {
            var apply = await _applyRepository.GetByIdAsync(applyId);
            if (apply == null) return false;

            _applyRepository.Remove(apply);
            await _applyRepository.SaveChangesAsync();

            return true;
        }
    }
}
