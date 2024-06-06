using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.IServices;

public interface IQualificationService
{
    Task<List<QualificationViewModel>> GetAllQualifications();

    Task<AddQualificationViewModel> AddNewQualification(Guid tutorId, AddQualificationViewModel qualificationViewModel);

    Task<UpdateQualificationViewModel> UpdateQualification(Guid tutorId, Guid qualificationId, UpdateQualificationViewModel qualificationViewModel);
}