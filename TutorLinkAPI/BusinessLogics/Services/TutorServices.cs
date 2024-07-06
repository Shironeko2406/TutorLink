using AutoMapper;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.Services;

public class TutorServices : ITutorService
{
    private readonly IMapper _mapper;
    private readonly TutorRepository _tutorRepository;
    private readonly QualificationRepository _qualificationRepository;
    private readonly RoleRepository _roleRepository;
    public TutorServices(IMapper mapper, TutorRepository tutorRepository, QualificationRepository qualificationRepository, RoleRepository roleRepository)
    {
        _mapper = mapper;
        _tutorRepository = tutorRepository;
        _qualificationRepository = qualificationRepository;
        _roleRepository = roleRepository;
    }

    #region GetTutorList
    public async Task<List<TutorViewModel>> GetTutorList()
    {
        //try
        //{
        //    var tutorList = await _tutorRepository.GetAllWithIncludeAsync(
        //        t => true, 
        //        t => t.Qualifications
        //    );
        //    var tutorListViewModel = _mapper.Map<List<TutorViewModel>>(tutorList);
        //    return tutorListViewModel;
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception("An error occurred while getting the list of tutors!", ex);
        //}
        try
        {
            var tutors = _tutorRepository.GetAll().ToList();
            var tutorViewModels = _mapper.Map<List<TutorViewModel>>(tutors);

            foreach (var tutorViewModel in tutorViewModels)
            {
                var qualifications = _qualificationRepository
                    .GetAll()
                    .Where(q => q.TutorId == tutorViewModel.TutorId)
                    .ToList();

                tutorViewModel.Qualifications = _mapper.Map<List<QualificationViewModel>>(qualifications);
            }

            return tutorViewModels;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting all tutors.", ex);
        }
    }
    #endregion

    #region GetTutorById
    public async Task<TutorViewModel> GetTutorById(Guid tutorId)
    {
        try
        {
            var tutor = await _tutorRepository.GetSingleWithAsync(t => t.TutorId == tutorId);
            if (tutor == null)
            {
                throw new Exception("No Tutor found with this ID!");
            }

            var tutorViewModel = _mapper.Map<TutorViewModel>(tutor);

            var qualifications = await _qualificationRepository.GetListWithAsync(q => q.TutorId == tutorViewModel.TutorId);

            tutorViewModel.Qualifications = _mapper.Map<List<QualificationViewModel>>(qualifications);

            return tutorViewModel;
        }
        catch (Exception ex)
        {

            throw new Exception("An error while getting the tutor by Id!", ex);
        }
    }
    #endregion

    #region AddNewTutor
    public async Task<Guid> AddNewTutor(AddTutorViewModel addTutorViewModel)
    {
        try
        {
            var newTutor = _mapper.Map<Tutor>(addTutorViewModel);
            newTutor.TutorId= Guid.NewGuid();
            var tutorRole = await _roleRepository.GetSingleWithAsync(r => r.RoleId == 3);
            newTutor.RoleId = tutorRole.RoleId;

            await _tutorRepository.AddSingleWithAsync(newTutor);
            await _tutorRepository.SaveChangesAsync();

            return newTutor.TutorId;
        }
        catch (Exception ex)
        {

            throw new Exception("An error occurred while adding the tutor.", ex);
        }
    }
    #endregion

    #region UpdateTutorById
    public async Task<UpdateTutorViewModel> UpdateTutorById(Guid tutorId, UpdateTutorViewModel tutorViewModel)
    {
        try
        {
            var existingTutor = await _tutorRepository.GetByIdAsync(tutorId);
            if (existingTutor != null)
            {
                _mapper.Map(tutorViewModel, existingTutor);

                await _tutorRepository.UpdateWithAsync(existingTutor);
                await _tutorRepository.SaveChangesAsync();

                return _mapper.Map<UpdateTutorViewModel>(existingTutor);
            }
            throw new Exception("Tutor not found.");
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the tutor.", ex);
        }
    }
    #endregion

    #region GetTutorByUserName
    public LoginViewModel GetTutorByUsername(string username)
    {
        var tutor = GetTutorEntityByUsername(username);
        return _mapper.Map<LoginViewModel>(tutor);
    }
    #endregion

    #region GetTutorByEmail
    public async Task<TutorViewModel> GetTutorByEmail(string email)
    {
        var tutor = await _tutorRepository.GetSingleWithAsync(t => t.Email == email);
        if (tutor == null)
        {
            throw new Exception("No Tutor found with this Email!");
        }
        var tutorViewModel= _mapper.Map<TutorViewModel>(tutor);
        return tutorViewModel;
    }
    #endregion

    #region Using Entity
    public Tutor GetTutorEntityByUsername(string username)
    {
        var tutor = _tutorRepository.Get(t => t.Username == username);
        return tutor;
    }
    #endregion
}