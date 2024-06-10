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
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<ParentFeedback> ParentFeedbacks { get; set; }
    public DbSet<PostRequest> PostRequests { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletTransaction> WalletTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Server=localhost;Database=TutorLinkDB;User ID=SA;Password=Trongtin1701;TrustServerCertificate=true");
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
        
        modelBuilder.Entity<Appointment>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.AppointmentId);
            
            //Require fields
            entity.Property(p => p.ExpiredDate)
                .IsRequired();
            entity.Property(p => p.AppTime)
                .IsRequired();
            entity.Property(p => p.Address)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(p => p.Status)
                .IsRequired();
            entity.Property(p => p.ParentId)
                .IsRequired();
            entity.Property(p => p.TutorId)
                .IsRequired();
            entity.Property(p => p.PostId)
                .IsRequired();
            
            //Foreign key
            entity.HasOne(o => o.PostRequest)
                .WithMany(m => m.Appointments)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        #endregion
        
        #region ParentFeedback

        modelBuilder.Entity<ParentFeedback>(entity =>
        {
            //Configure PK
            entity.HasKey(a => a.FeedbackId);

            //Require fields
            entity.Property(p => p.Description)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(p => p.IsSucessful)
                .IsRequired();
            entity.Property(p => p.AppointmentId)
                .IsRequired();

            //Foreign key
            entity.HasOne(o => o.Appointment)
                .WithMany(m => m.ParentFeedbacks)
                .HasForeignKey(f => f.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

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
                .HasMaxLength(10)
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
                .OnDelete(DeleteBehavior.Restrict);
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
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(p => p.InstitutionName)
                .HasMaxLength(125)
                .IsRequired();
            entity.Property(p => p.YearObtained)
                .IsRequired();
            entity.Property(p => p.TutorId)
                .IsRequired();
            
            //Foreign key
            entity.HasOne(o => o.Tutor)
                .WithMany(m => m.Qualifications)
                .HasForeignKey(f => f.TutorId)
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
        });
        
        #endregion


        #region Data Seeding

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
    }
}