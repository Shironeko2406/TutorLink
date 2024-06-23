namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IAppointmentFeedback
    {
        void BookAppointmentDate(int AppointmentId, DateTime AppointmentTime);
        void NotifyTutorAboutAppointment(Guid? TutorId, DateTime AppointmentTime, string Address);
        void HandleTutorConfirmation(Guid? TutorId, bool canGoToAppointment);
        void SendFeedbackFormToParent(int AppointmentId, Guid? AccountId);
        void UpdatePostStatus(Guid? PostId, AppStatuses Status);
        void SendReviewReminderToParent(int AppointmentId, DateTime AppointmentTime);
    }
}
