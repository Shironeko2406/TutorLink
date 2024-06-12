using AutoMapper;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class ApplyServices : IApplyService
    {
        private readonly GenericRepository<Apply> _applyRepository;
        private readonly IMapper _mapper;

        public ApplyServices(GenericRepository<Apply> applyRepository, IMapper mapper)
        {
            _applyRepository = applyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Apply>> GetAllApplies()
        {
            return await _applyRepository.GetAllWithAsync();
        }

        public async Task<Apply> GetApplyById(Guid applyId)
        {
            return await _applyRepository.GetByIdAsync(applyId);
        }

        public async Task<Apply> AddNewApply(AddApplyViewModel applyViewModel)
        {
            var applyEntity = _mapper.Map<Apply>(applyViewModel);
            var result = await _applyRepository.AddSingleWithAsync(applyEntity);
            await _applyRepository.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Apply> UpdateApply(Guid applyId, UpdateApplyViewModel applyViewModel)
        {
            var existingApply = await _applyRepository.GetByIdAsync(applyId);
            if (existingApply == null)
            {
                return null;
            }

            _mapper.Map(applyViewModel, existingApply);
            await _applyRepository.UpdateWithAsync(existingApply);
            return existingApply;
        }

        public async Task<bool> DeleteApply(Guid applyId)
        {
            var existingApply = await _applyRepository.GetByIdAsync(applyId);
            if (existingApply == null)
            {
                return false;
            }

            _applyRepository.Remove(existingApply);
            await _applyRepository.SaveChangesAsync();
            return true;
        }
    }
}
