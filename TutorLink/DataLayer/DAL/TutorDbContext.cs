using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataLayer.DAL;

public partial class TutorDbContext : DbContext
{
    public TutorDbContext() {}
    
    public TutorDbContext(DbContextOptions<TutorDbContext> options) : base(options){}
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Apply> Applies { get; set; }
    //public DbSet<Appointment> Appointments { get; set; }
    //public DbSet<ParentFeedback> ParentFeedbacks { get; set; }
    public DbSet<PostRequest> PostRequests { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Proficiency> Proficiencies { get; set; }
    public DbSet<WalletTransaction> WalletTransactions { get; set; }
    public DbSet<AppointmentFeedback> AppointmentFeedbacks { get; set; }
    public DbSet<Deposit> Deposits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Server=tcp:mytutorlink.database.windows.net,1433;Initial Catalog=TutorLinkDB;Persist Security Info=False;User ID=tintruong;Password=Trongtin1701;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}

public  partial class TutorDbContext {
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Account

        modelBuilder.Entity<Account>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.AccountId);
            
            //Require fields
            entity.Property(p => p.Username)
                .HasMaxLength(50)
                .IsRequired();
            entity.Property(p => p.Password)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(p => p.Fullname)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(p => p.Phone)
                .HasMaxLength(12)
                .IsRequired();
            entity.Property(p => p.Address)
                .HasMaxLength(150)
                .IsRequired();
            entity.Property(p => p.Gender)
                .IsRequired();
            
            //Foreign key
            entity.HasOne(o => o.Role)
                .WithMany(m => m.Accounts)
                .HasForeignKey(f => f.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        #endregion
        
        #region Apply
        
        modelBuilder.Entity<Apply>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.ApplyId);
            
            //Require fields
            entity.Property(p => p.PostId)
                .IsRequired();
            entity.Property(p => p.TutorId)
                .IsRequired();
            entity.Property(p => p.Status)
                .IsRequired();
            
            //Foreign key
            entity.HasOne(o => o.PostRequest)
                .WithMany(m => m.Applies)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(o => o.Tutor)
                .WithMany(m => m.Applies)
                .HasForeignKey(f => f.TutorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        #endregion

        #region Appointment

        //modelBuilder.Entity<Appointment>(entity =>
        //{
        //    //Configure PK
        //    entity.HasKey(a => a.AppointmentId);

        //    //Require fields
        //    entity.Property(p => p.ExpiredDate)
        //        .IsRequired();
        //    entity.Property(p => p.AppTime)
        //        .IsRequired();
        //    entity.Property(p => p.Address)
        //        .HasMaxLength(255)
        //        .IsRequired();
        //    entity.Property(p => p.Status)
        //        .IsRequired();
        //    entity.Property(p => p.ParentId)
        //        .IsRequired();
        //    entity.Property(p => p.TutorId)
        //        .IsRequired();
        //    entity.Property(p => p.PostId)
        //        .IsRequired();

        //    //Foreign key
        //    entity.HasOne(o => o.PostRequest)
        //        .WithMany(m => m.Appointments)
        //        .HasForeignKey(f => f.PostId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //});

        #endregion

        #region ParentFeedback

        //modelBuilder.Entity<ParentFeedback>(entity =>
        //{
        //    //Configure PK
        //    entity.HasKey(a => a.FeedbackId);

        //    //Require fields
        //    entity.Property(p => p.Description)
        //        .HasMaxLength(255)
        //        .IsRequired();
        //    entity.Property(p => p.IsSucessful)
        //        .IsRequired();
        //    entity.Property(p => p.AppointmentId)
        //        .IsRequired();

        //    //Foreign key
        //    entity.HasOne(o => o.Appointment)
        //        .WithMany(m => m.ParentFeedbacks)
        //        .HasForeignKey(f => f.AppointmentId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //});

        #endregion

        #region PostRequest

        modelBuilder.Entity<PostRequest>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.PostId);
            
            //Require fields
            entity.Property(p => p.Description)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(p => p.Location)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(p => p.Schedule)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(p => p.PreferredTime)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(p => p.Mode)
                .IsRequired();
            entity.Property(p => p.Gender)
                .IsRequired();
            entity.Property(p => p.Status)
                .IsRequired();
            entity.Property(p => p.RequestSkill)
                .IsRequired();
            entity.Property(p => p.CreatedBy)
                .IsRequired();
            entity.Property(p => p.CreatedDate)
                .IsRequired();
            
            //Foreign key
            entity.HasOne(o => o.Account)
                .WithMany(m => m.PostRequests)
                .HasForeignKey(f => f.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        #endregion
        
        #region Qualification
        
        modelBuilder.Entity<Qualification>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.QualificationId);
            
            //Require fields
            entity.Property(p => p.QualificationType)
                .IsRequired();
            entity.Property(p => p.QualificationName)
                .HasMaxLength(255);
            entity.Property(p => p.InstitutionName)
                .HasMaxLength(125);
            entity.Property(p => p.YearObtained)
                .IsRequired();
            entity.Property(p => p.TutorId)
                .IsRequired();
            
            //Foreign key
            entity.HasOne(o => o.Tutor)
                .WithMany(m => m.Qualifications)
                .HasForeignKey(f => f.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Skill)
                .WithMany(m => m.Qualifications)
                .HasForeignKey(f => f.SkillId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Proficiency)
                .WithMany(m => m.Qualifications)
                .HasForeignKey(f => f.ProficiencyId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        #endregion
        
        #region Role

        modelBuilder.Entity<Role>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.RoleId);
            
            //Require fields
            entity.Property(p => p.RoleName)
                .HasMaxLength(10)
                .IsRequired();
                
            //Foreign key -Nothing
        });

        #endregion

        #region Tutor

        modelBuilder.Entity<Tutor>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.TutorId);
            
            //Require fields
            entity.Property(p => p.Username)
                .HasMaxLength(50)
                .IsRequired();
            entity.Property(p => p.Password)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(p => p.Fullname)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(p => p.Phone)
                .HasMaxLength(12)
                .IsRequired();
            entity.Property(p => p.Address)
                .HasMaxLength(150)
                .IsRequired();
            entity.Property(p => p.Gender)
                .IsRequired();
 
            //Foreign key
            entity.HasOne(o => o.Role)
                .WithMany(m => m.Tutors)
                .HasForeignKey(f => f.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        #endregion
        
        #region Wallet
        
        modelBuilder.Entity<Wallet>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.WalletId);
            
            //Require fields
                //Nothing here
                
            //Foreign key
            entity.HasOne(o => o.Tutor)
                .WithOne(m => m.Wallet)
                .HasForeignKey<Wallet>(f => f.TutorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        #endregion

        #region Skill
        modelBuilder.Entity<Skill>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.SkillId);
            entity.Property(p => p.SkillId)
                  .ValueGeneratedOnAdd();

            //Require fields
            entity.Property(p => p.SkillName)
                .HasMaxLength(20)
                .IsRequired();

            //Foreign key -Nothing
        });
        #endregion

        #region Proficiency
        modelBuilder.Entity<Proficiency>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.ProficiencyId);

            //Require fields
            entity.Property(p => p.ProficiencyCode)
                .HasMaxLength(5)
                .IsRequired();
            entity.Property(p => p.Description)
                .HasMaxLength(100)
                .IsRequired();
            //Foreign key -Nothing
        });
        #endregion

        #region WalletTransaction

        modelBuilder.Entity<WalletTransaction>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.TransactionId);
            
            //Require fields
            entity.Property(p => p.Amount)
                .IsRequired();
            entity.Property(p => p.Type)
                .IsRequired();
            entity.Property(p => p.TransactionDate)
                .IsRequired();
            entity.Property(p => p.WalletId)
                .IsRequired();

            //Foreign key
            entity.HasOne(o => o.Wallet)
                .WithMany(m => m.WalletTransactions)
                .HasForeignKey(f => f.WalletId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Deposit)
                .WithMany(m => m.WalletTransactions)
                .HasForeignKey(f => f.DepositId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        #endregion

        #region AppointmentFeedback

        modelBuilder.Entity<AppointmentFeedback>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.AppointmentId);

            //Foreign key
            entity.HasOne(o => o.Account)
                .WithMany(m => m.AppointmentFeedbacks)
                .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(o => o.Tutor)
                .WithMany(m => m.AppointmentFeedbacks)
                .HasForeignKey(f => f.TutorId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(o => o.PostRequest)
                .WithMany(m => m.AppointmentFeedbacks)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        #endregion

        #region Deposit

        modelBuilder.Entity<Deposit>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.DepositId);

            //Require fields
            entity.Property(p => p.Amount)
                .IsRequired();
            entity.Property(p => p.DepositDate)
                .IsRequired();
            entity.Property(p => p.Method)
                .IsRequired();
            entity.Property(p => p.WalletId)
                .IsRequired();

            //Foreign key
            entity.HasOne(o => o.Wallet)
                .WithMany(m => m.Deposits)
                .HasForeignKey(f => f.WalletId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        #endregion

        //Data Seeding
        #region Role Data Seeding

        modelBuilder.Entity<Role>().HasData(new Role()
        {
            RoleId = 1,
            RoleName = "Admin"
        });
        modelBuilder.Entity<Role>().HasData(new Role()
        {
            RoleId = 2,
            RoleName = "Staff"
        });
        modelBuilder.Entity<Role>().HasData(new Role()
        {
            RoleId = 3,
            RoleName = "Tutor"
        });
        modelBuilder.Entity<Role>().HasData(new Role()
        {
            RoleId = 4,
            RoleName = "Parent"
        });

        #endregion

        #region Proficiency Data Seeding

        modelBuilder.Entity<Proficiency>().HasData(new Proficiency()
        {
            ProficiencyId = 1,
            ProficiencyCode= "A1",
            Description= "Basic level of proficiency"
        });

        modelBuilder.Entity<Proficiency>().HasData(new Proficiency()
        {
            ProficiencyId = 2,
            ProficiencyCode = "A2",
            Description = "Elementary level of proficiency"
        });

        modelBuilder.Entity<Proficiency>().HasData(new Proficiency()
        {
            ProficiencyId = 3,
            ProficiencyCode = "B1",
            Description = "Intermediate level of proficiency"
        });

        modelBuilder.Entity<Proficiency>().HasData(new Proficiency()
        {
            ProficiencyId = 4,
            ProficiencyCode = "B2",
            Description = "Upper Intermediate level of proficiency"
        });

        modelBuilder.Entity<Proficiency>().HasData(new Proficiency()
        {
            ProficiencyId = 5,
            ProficiencyCode = "C1",
            Description = "Advanced level of proficiency"
        });

        modelBuilder.Entity<Proficiency>().HasData(new Proficiency()
        {
            ProficiencyId = 6,
            ProficiencyCode = "C2",
            Description = "Proficient/native-like level of proficiency"
        });

        #endregion

        #region Skill Data Seeding

        modelBuilder.Entity<Skill>().HasData(new Skill()
        {
            SkillId = 1,
            SkillName = "English"
        });

        modelBuilder.Entity<Skill>().HasData(new Skill()
        {
            SkillId = 2,
            SkillName = "Japanese"
        });

        modelBuilder.Entity<Skill>().HasData(new Skill()
        {
            SkillId = 3,
            SkillName = "Korean"
        });

        modelBuilder.Entity<Skill>().HasData(new Skill()
        {
            SkillId = 4,
            SkillName = "Chinese"
        });

        modelBuilder.Entity<Skill>().HasData(new Skill()
        {
            SkillId = 5,
            SkillName = "Spanish"
        });
        #endregion
        
        #region Account Data Seeding
        modelBuilder.Entity<Account>().HasData(new Account()
        {
            AccountId = Guid.NewGuid(),
            Username = "admin",
            Password = "admin123",
            Fullname = "ADMIN",
            Email = "admin@gmail.com",
            Phone = "0945677876",
            Address = "Ho Chi Minh, Viet Name",
            Gender = UserGenders.Male,
            RoleId = 1
        });
        modelBuilder.Entity<Account>().HasData(new Account()
        {
            AccountId = Guid.NewGuid(),
            Username = "staff",
            Password = "staff123",
            Fullname = "STAFF",
            Email = "staff@gmail.com",
            Phone = "0912377890",
            Address = "Ho Chi Minh, Viet Name",
            Gender = UserGenders.Female,
            RoleId = 2
        });
        modelBuilder.Entity<Account>().HasData(new Account()
        {
            AccountId = Guid.NewGuid(),
            Username = "parent1",
            Password = "@123",
            Fullname = "Tran Van A",
            Email = "vana@gmail.com",
            Phone = "0978988768",
            Address = "Ho Chi Minh, Viet Name",
            Gender = UserGenders.Female,
            RoleId = 4
        });
        #endregion
    }
}