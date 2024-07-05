using AutoMapper;
using DataLayer.DAL;
using DataLayer.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.BusinessLogics.Services;
using System.Text;

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
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter the JWT token obtained from the login endpoint",
                    Name = "Authorization"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                         new OpenApiSecurityScheme
                         {
                            Reference = new OpenApiReference
                            {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                            }
                         },
                         Array.Empty<string>()
                    }
                });
            });
            // Configure JWT authentication
            var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
            var securityKey = jwtSettingsSection["SecurityKey"];
            var issuer = jwtSettingsSection["Issuer"];
            var audience = jwtSettingsSection["Audience"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOrStaff", policy =>
                    policy.RequireAssertion(context =>
                    {
                        var roleClaim = context.User.FindFirst("Role");
                        return roleClaim != null && (roleClaim.Value == "1" || roleClaim.Value == "2");
                    }));
            });

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
            builder.Services.AddScoped<IAccountService, AccountServices>();
            builder.Services.AddScoped<IApplyService, ApplyServices>();
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
            builder.Services.AddScoped<IAuthService, AuthServices>();
            #endregion

            #region CORS handler
            var CORS_CONFIG = "_CORS_CONFIG";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CORS_CONFIG,
                    builder => builder
                        .WithOrigins("http://localhost:5173") 
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()); 
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
