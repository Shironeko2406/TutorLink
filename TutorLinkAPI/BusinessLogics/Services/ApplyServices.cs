using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;
using DataLayer.DAL;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class ApplyServices : IApplyService
    {
        private readonly ApplyRepository _applyRepository;
        private readonly IMapper _mapper;
        private readonly TutorDbContext _context;
        private readonly TutorRepository _tutorRepository;
        private readonly PostRequestRepository _postRequestRepository;

        public ApplyServices(
            ApplyRepository applyRepository,
            IMapper mapper,
            TutorDbContext context,
            TutorRepository tutorRepository,
            PostRequestRepository postRequestRepository)
        {
            _applyRepository = applyRepository;
            _mapper = mapper;
            _context = context;
            _tutorRepository = tutorRepository;
            _postRequestRepository = postRequestRepository;
        }

        public async Task<ApplyViewModel> AddNewApply(Guid tutorId, Guid postId, AddApplyViewModel applyViewModel)
        {
            var tutor = _tutorRepository.GetByIdAsync(tutorId);
            if (tutor == null)
                throw new Exception("Tutor not found.");

            var postRequest = _postRequestRepository.GetByIdAsync(postId);
            if (postRequest == null)
                throw new Exception("Post request not found.");

            var newApply = _mapper.Map<Apply>(applyViewModel);
            newApply.ApplyId = Guid.NewGuid();
            newApply.TutorId = tutorId;
            newApply.PostId = postId;

            await _applyRepository.AddSingleWithAsync(newApply);
            await _applyRepository.SaveChangesAsync();

            return _mapper.Map<ApplyViewModel>(newApply);
        }

        public Apply GetApplyById(Guid applyId)
        {
            var apply = _applyRepository.GetById(applyId);
            return apply;
        }

        public void UpdateApplyStatus(Guid applyId, UpdateApplyViewModel model)
        {
            var apply = _applyRepository.GetById(applyId);
            if (apply == null)
                throw new Exception("Apply not found.");

            _mapper.Map(model, apply);

            _applyRepository.Update(apply);
            _applyRepository.SaveChanges();
        }

        public void DeleteApply(Guid applyId)
        {
            var apply = _applyRepository.GetById(applyId);
            if (apply != null)
            {
                _applyRepository.Delete(apply.ApplyId);
                _applyRepository.SaveChanges();
            }
            else
            {
                throw new Exception("Apply not found.");
            }
        }

        public IEnumerable<Apply> GetAppliesByTutorId(Guid tutorId)
        {
            return _applyRepository.GetList(a => a.TutorId == tutorId);
        }

        public IEnumerable<Apply> GetAllApplies()
        {
            return _applyRepository.GetAll();
        }
    }
}
