using AutoMapper;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.Services;

public class QualificationServices : IQualificationService
{
    private readonly IMapper _mapper;
    private readonly QualificationRepository _qualificationRepository;
    private readonly TutorRepository _tutorRepository;
    private readonly SkillRepository _skillRepository;
    private readonly ProficiencyRepository _proficiencyRepository;

    public QualificationServices(IMapper mapper, QualificationRepository qualificationRepository, TutorRepository tutorRepository, SkillRepository skillRepository, ProficiencyRepository proficiencyRepository)
    {
        _mapper = mapper;
        _qualificationRepository = qualificationRepository;
        _tutorRepository = tutorRepository;
        _skillRepository = skillRepository;
        _proficiencyRepository = proficiencyRepository;
    }

    #region GetAllQualifications
    public async Task<List<QualificationViewModel>> GetAllQualifications()
    {
        try
        {
            var qualificationList = await _qualificationRepository.GetAllWithAsync();
            var qualificationViewModel = _mapper.Map<List<QualificationViewModel>>(qualificationList);
            return qualificationViewModel;
        }
        catch (Exception ex)
        {

            throw new Exception("An error while getting the tutor by Id!", ex);
        }
    }
    #endregion

    #region AddNewQualification
    public async Task<QualificationViewModel> AddNewQualification(QualificationViewModel qualificationViewModel)
    {
        try
        {
            var qualification = _mapper.Map<Qualification>(qualificationViewModel);

            if (qualificationViewModel.QualificationType == Types.Degree)
            {
                qualificationViewModel.SkillId = null;
                qualificationViewModel.ProficiencyId = null;
            }
            else if (qualificationViewModel.QualificationType == Types.Skill)
            {
                
                qualificationViewModel.QualificationName = null;
                qualificationViewModel.InstitutionName = null;
            }
            await _qualificationRepository.AddSingleWithAsync(qualification);
            await _qualificationRepository.SaveChangesAsync();
            var addedQualificationViewModel = _mapper.Map<QualificationViewModel>(qualification);
            return addedQualificationViewModel;
        }
        catch (Exception ex)
        {

            throw new Exception("An error occurred while adding a new qualification.", ex);
        }
    }
    #endregion
}