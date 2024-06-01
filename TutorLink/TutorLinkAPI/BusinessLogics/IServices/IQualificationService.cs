using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.IServices;

public interface IQualificationService
{
    Task<List<QualificationViewModel>> GetAllQualifications();

    Task<QualificationViewModel> AddNewQualification(QualificationViewModel qualificationViewModel);
}