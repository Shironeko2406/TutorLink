using AutoMapper;
using DataLayer.DAL;
using DataLayer.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.BusinessLogics.Services;

namespace TutorLinkAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TutorDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
            });
            
            #region Repositories
            builder.Services.AddScoped(typeof(GenericRepository<>));
            builder.Services.AddScoped<AccountRepository>();
            builder.Services.AddScoped<ApplyRepository>();
            builder.Services.AddScoped<AppointmentFeedbackRepository>();
            //builder.Services.AddScoped<AppointmentRepository>();
            //builder.Services.AddScoped<ParentFeedbackRepository>();
            builder.Services.AddScoped<PostRequestRepository>();
            builder.Services.AddScoped<QualificationRepository>();
            builder.Services.AddScoped<RoleRepository>();
            builder.Services.AddScoped<TutorRepository>();
            builder.Services.AddScoped<SkillRepository>();
            builder.Services.AddScoped<ProficiencyRepository>();
            builder.Services.AddScoped<WalletRepository>();
            builder.Services.AddScoped<WalletTransactionRepository>();
            builder.Services.AddScoped<DepositRepository>();
            #endregion
            
            #region Interfaces + Services
            builder.Services.AddScoped<IAccountService, AccountServiceServices>();
            builder.Services.AddScoped<IApplyService, ApplyServiceServices>();
            builder.Services.AddScoped<IAppointmentFeedback, AppoitmentFeedbackServices>();
            //builder.Services.AddScoped<IAppointmentService, AppointmentServices>();
            //builder.Services.AddScoped<IParentFeedbackService, ParentFeedbackServices>();
            builder.Services.AddScoped<IPostRequestService, PostRequestServices>();
            builder.Services.AddScoped<IQualificationService, QualificationServices>();
            builder.Services.AddScoped<IRoleService, RoleServices>();
            builder.Services.AddScoped<ITutorService, TutorServices>();
            builder.Services.AddScoped<ISkillService, SkillServices>();
            builder.Services.AddScoped<IProficiencyService, ProficiencyServices>();
            builder.Services.AddScoped<IWalletService, WalletServices>();
            builder.Services.AddScoped<IWalletTransactionService, WalletTransactionServices>();
            builder.Services.AddScoped<IDepositService, DepositServices>();
            #endregion

            #region CORS handler
            var CORS_CONFIG = "_CORS_CONFIG";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CORS_CONFIG,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            #endregion

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(CORS_CONFIG);
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
