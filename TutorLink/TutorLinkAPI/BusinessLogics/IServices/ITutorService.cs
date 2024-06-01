using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.IServices;

public interface ITutorService
{
    Task <List<TutorViewModel>> GetTutorList();

    Task<TutorViewModel> GetTutorById(Guid tutorId);

    Task<Guid> AddNewTutor(AddTutorViewModel addTutorViewModel);

    Task<TutorViewModel> UpdateTutorById(TutorViewModel tutorViewModel);
}