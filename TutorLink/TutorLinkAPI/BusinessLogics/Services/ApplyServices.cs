using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TutorLinkAPI.BusinessLogics.IServices;
using Microsoft.Extensions.Logging;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class ApplyServices : IApplyService
    {
        private readonly IGenericRepository<Apply> _applyRepository;
        private readonly ILogger<ApplyServices> _logger;

        public ApplyServices(IGenericRepository<Apply> applyRepository, ILogger<ApplyServices> logger)
        {
            _applyRepository = applyRepository;
            _logger = logger;
        }

        public async Task AddNewApply(Guid tutorId, Guid postRequestId, ApplyStatuses status)
        {
            try
            {
                var apply = new Apply
                {
                    ApplyId = Guid.NewGuid(),
                    TutorId = tutorId,
                    PostId = postRequestId,
                    Status = status
                };
                await _applyRepository.AddSingleWithAsync(apply);
                await _applyRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new apply");
                throw;
            }
        }

        public async Task<IEnumerable<Apply>> GetAllApplies()
        {
            return await _applyRepository.GetAllWithAsync();
        }

        public async Task<Apply> GetApplyById(Guid id)
        {
            return await _applyRepository.GetByIdAsync(id);
        }

        public async Task UpdateApply(Guid id, ApplyStatuses status)
        {
            var apply = await _applyRepository.GetByIdAsync(id);
            if (apply != null)
            {
                apply.Status = status;
                await _applyRepository.UpdateWithAsync(apply);
            }
        }

        public async Task DeleteApply(Guid id)
        {
            var apply = await _applyRepository.GetByIdAsync(id);
            if (apply != null)
            {
                _applyRepository.Remove(apply);
                await _applyRepository.SaveChangesAsync();
            }
        }
    }
}
