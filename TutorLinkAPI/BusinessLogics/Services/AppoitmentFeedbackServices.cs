using TutorLinkAPI.BusinessLogics.IServices;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using DataLayer.Entities;
using Microsoft.Identity.Client;
using Microsoft.Data.SqlClient;
using Amazon;
using Amazon.Runtime;
using System.Numerics;
namespace TutorLinkAPI.BusinessLogics.Services
{
    public class AppoitmentFeedbackServices : IAppointmentFeedback
    {
        private readonly IConfiguration _configuration;
        private readonly AmazonSimpleNotificationServiceClient _snsClient;
        public AppoitmentFeedbackServices(IConfiguration configuration)
        {
            /*_configuration = configuration;
            string awsAccessKeyId = "";
            string awsSecretAccessKey = "";
            BasicAWSCredentials awsCredentials = new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
            _snsClient = new AmazonSimpleNotificationServiceClient(RegionEndpoint.);*/
            _configuration = configuration;

            var awsAccessKeyId = _configuration.GetValue<string>("AKIAQ3EGRT57GWN3HYWF");
            var awsSecretAccessKey = _configuration.GetValue<string>("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");
            var awsRegion = _configuration.GetValue<string>("ap-southeast-2");

            var awsCredentials = new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
            _snsClient = new AmazonSimpleNotificationServiceClient(awsCredentials, Amazon.RegionEndpoint.GetBySystemName("ap-southeast-2"));
        }
        /*--------------------------------------------------------------------------*/
        public void BookAppointmentDate(int AppointmentId, DateTime AppointmentTime)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("MyAzureDatabaseConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Appointments (AppointmentId, AppointmentTime) VALUES (@AppointmentId, @AppointmentTime)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentId", AppointmentId);
                        command.Parameters.AddWithValue("@AppointmentTime", AppointmentTime);

                        int rowAffected = command.ExecuteNonQuery();
                        if (rowAffected > 0)
                        {
                            Console.WriteLine($"Appointment booked successfully. AppointmentId: {AppointmentId}, AppointmentTime: {AppointmentTime}");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to book appointment for AppointmentId: {AppointmentId}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error booking appointment for AppointmentId: {AppointmentId}. Error: {ex.Message}");
            }
        }
        public void NotifyTutorAboutAppointment(Guid? TutorId, DateTime AppointmentTime, string Address)
        {
            var message = $"Appointment scheduled on {AppointmentTime}. Address: {Address}";
            PublishRequest publishRequest = new PublishRequest
            {
                TargetArn = $"arn:aws:sns:us-east-1:123456789012:TutorTopic_{TutorId}",
                Message = message
            };
            _snsClient.PublishAsync(publishRequest);
        }
        /*--------------------------------------------------------------------------*/
        public void HandleTutorConfirmation(Guid? TutorId, bool canGoToAppointment)
        {
            if (canGoToAppointment)
            {
                var confirmationMessage = $"Tutor can attend the appointment.";
                PublishRequest confirmationRequest = new PublishRequest
                {
                    TargetArn = $"arn:aws:sns:us-east-1:123456789012:ParentTopic",
                    Message = confirmationMessage
                };
                _snsClient.PublishAsync(confirmationRequest);
            }
            else
            {
                var rescheduleMessage = $"Tutor unavailable. Please reschedule the appointment.";
                PublishRequest rescheduleRequest = new PublishRequest
                {
                    TargetArn = $"arn:aws:sns:us-east-1:123456789012:ParentTopic",
                    Message = rescheduleMessage
                };
                _snsClient.PublishAsync(rescheduleRequest);
            }
        }
        public void SendFeedbackFormToParent(int AppointmentId, Guid? AccountId)
        {
            if (AccountId.HasValue)
            {
                string Phone = RetrieveParentPhoneNumberFromDatabase(AccountId.Value);
                var feedbackLink = "https://forms.office.com/r/La0LUdhKRM";
                PublishRequest feedbackRequest = new PublishRequest
                {
                    Message = $"Please provide feedback for the tutor: {feedbackLink}",
                    PhoneNumber = Phone
                };
                _snsClient.PublishAsync(feedbackRequest);
            }
            else
            {
                Console.WriteLine("Invalid accountId provided.");
            }
        }
        private string RetrieveParentPhoneNumberFromDatabase(Guid? AccountId)
        {
            if (AccountId.HasValue)
            {
                string connectionString = _configuration.GetConnectionString("MyAzureDatabaseConnection");
                string query = "SELECT Phone FROM ParentTable WHERE AccountId = @AccountId";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", AccountId);
                        string Phone = command.ExecuteScalar() as string;
                        if (!string.IsNullOrEmpty(Phone))
                        {
                            return Phone;
                        }
                        else
                        {
                            Console.WriteLine("Phone number not found for the provided accountId.");
                            return "";
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid accountId provided.");
                return "";
            }
        }
        /*--------------------------------------------------------------------------*/
        public void UpdatePostStatus(Guid? PostId, AppStatuses Status)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("MyDatabaseConnection");
                string query = "UPDATE Posts SET Status = @Status WHERE PostId = @PostId";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", (int)Status);
                        command.Parameters.AddWithValue("@PostId", PostId);
                        int rowsAffect = command.ExecuteNonQuery();
                        if (rowsAffect > 0){
                            Console.WriteLine($"Post status updated successfully. PostId: {PostId}, New Status: {Status}");
                        }
                        else
                        {
                            Console.WriteLine($"No posts were updated for PostId: {PostId}");
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Error updating post status for PostId: {PostId}. Error: {ex.Message}"); }
        }
        public void SendReviewReminderToParent(int AppointmentId, DateTime AppointmentTime)
        {
            var ExpiredDate = AppointmentTime.AddDays(1);
            if (DateTime.Now.Date >= ExpiredDate.Date)
            {
                Guid AccountId = GetParentAccountIdForAppointment(AppointmentId);
                if (AccountId != Guid.Empty)
                {
                    string Phone = RetrieveParentPhoneNumberFromDatabase(AccountId);
                    if (!string.IsNullOrEmpty(Phone))
                    {
                        var feedbackLink = "https://forms.office.com/r/La0LUdhKRM";
                        PublishRequest reviewReminderRequest = new PublishRequest
                        {
                            Message = $"Please review the tutor by filling out this form: {feedbackLink}",
                            PhoneNumber = Phone
                        };
                        _snsClient.PublishAsync(reviewReminderRequest);
                    }
                }
                else
                {
                    Console.WriteLine("Phone number not found for the parent accountId.");
                }

            }
            else
            {
                Console.WriteLine("Parent account ID not found for the given appointment");
            }

        }
        private Guid GetParentAccountIdForAppointment(int AppointmentId)
        {
            Guid AccountId = Guid.Empty;
            try
            {
                string connectionString = _configuration.GetConnectionString("MyDatabaseConnection");
                string query = "SELECT AccountId FROM Appointments WHERE AppointmentId = @AppointmentId";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentId", AppointmentId);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            AccountId = (Guid)result;
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            return AccountId;
        }
    }
}
