using DataLayer.Entities;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.IServices;

public interface ITutorService
{
    Task <List<TutorViewModel>> GetTutorList();

    Task<TutorViewModel> GetTutorById(Guid tutorId);

    Task<TutorViewModel> GetTutorByEmail(string email);

    Task<Guid> AddNewTutor(AddTutorViewModel addTutorViewModel);

    Task<UpdateTutorViewModel> UpdateTutorById(Guid tutorId, UpdateTutorViewModel tutorViewModel);

    Tutor GetTutorEntityByUsername(string username);

    LoginViewModel GetTutorByUsername(string username);
}