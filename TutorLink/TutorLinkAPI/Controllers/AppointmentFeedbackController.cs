using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;

namespace TutorLinkAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentFeedbackController : Controller
    {
        private readonly IAppointmentFeedback _appointmentFeedbackService;
        public AppointmentFeedbackController(IAppointmentFeedback appointmentFeedbackService)
        {
            _appointmentFeedbackService = appointmentFeedbackService;
        }

        [HttpPost("book-appointment")]
        public IActionResult BookAppointment(int AppointmentId, DateTime AppointmentTime)
        {
            _appointmentFeedbackService.BookAppointmentDate(AppointmentId, AppointmentTime);
            return Ok("Appointment booked successfully.");
        }

        [HttpPost("notify-tutor")]
        public IActionResult NotifyTutor(Guid? TutorId, DateTime AppointmentTime, string Address)
        {
            _appointmentFeedbackService.NotifyTutorAboutAppointment(TutorId, AppointmentTime, Address);
            return Ok("Tutor notified about the appointment.");
        }

        [HttpPost("handle-tutor-confirmation")]
        public IActionResult HandleTutorConfirmation(Guid? TutorId, bool canGoToAppointment)
        {
            _appointmentFeedbackService.HandleTutorConfirmation(TutorId, canGoToAppointment);
            return Ok("Tutor confirmation handled successfully.");
        }

        [HttpGet("send-feedback-form")]
        public IActionResult SendFeedbackForm(Guid AccountId, int AppointmentId)
        {
            _appointmentFeedbackService.SendFeedbackFormToParent(AppointmentId, AccountId);
            return Ok("Feedback form sent to parent.");
        }

        [HttpPost("update-post-status")]
        public IActionResult UpdatePostStatus(Guid? PostId, AppStatuses Status)
        {
            _appointmentFeedbackService.UpdatePostStatus(PostId, (AppStatuses)Status);
            return Ok("Post status updated successfully.");
        }

        [HttpPost("send-review-reminder")]
        public IActionResult SendReviewReminder(int AppointmentId, DateTime AppointmentTime)
        {
            _appointmentFeedbackService.SendReviewReminderToParent(AppointmentId, AppointmentTime);
            return Ok("Review reminder sent to parent.");
        }
    }
}
