using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Framework> Frameworks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
        public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Github> Githubs { get; set; }

        public BaseDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(p =>
            {
                p.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                p.Property(a => a.Id).HasColumnName("Id");
                p.Property(a => a.Name).HasColumnName("Name");
                p.HasMany(a => a.Frameworks);
            });

            ProgrammingLanguage[] programmingLanguages = { new(1, "C#"), new(2, "Java") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguages);

            modelBuilder.Entity<Framework>(f =>
            {
                f.ToTable("Frameworks").HasKey(k => k.Id);
                f.Property(k => k.Id).HasColumnName("Id");
                f.Property(k => k.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                f.Property(k => k.Name).HasColumnName("Name");
                f.HasOne(k => k.ProgrammingLanguage);
            });

            Framework[] frameworks = { new(1, 6, " ASP.NET"), new(2, 7, "Spring") };
            modelBuilder.Entity<Framework>().HasData(frameworks);

            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users").HasKey(k => k.Id);
                u.Property(k => k.Id).HasColumnName("Id");
                u.Property(k => k.FirstName).HasColumnName("FirstName");
                u.Property(k => k.LastName).HasColumnName("LastName");
                u.Property(k => k.Email).HasColumnName("Email");
                u.Property(k => k.PasswordSalt).HasColumnName("PasswordSalt");
                u.Property(k => k.PasswordHash).HasColumnName("PasswordHash");
                u.Property(k => k.Status).HasColumnName("Status");
                u.Property(k => k.AuthenticatorType).HasColumnName("AuthenticatorType");
                u.HasMany(k => k.UserOperationClaims);
                u.HasMany(k => k.RefreshTokens);
            });

            modelBuilder.Entity<OperationClaim>(o =>
            {
                o.ToTable("OperationClaims").HasKey(x => x.Id);
                o.Property(x => x.Id).HasColumnName("Id");
                o.Property(x => x.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(u =>
            {
                u.ToTable("UserOperationClaims").HasKey(f => f.Id);
                u.Property(f => f.Id).HasColumnName("Id");
                u.Property(f => f.UserId).HasColumnName("UserId");
                u.Property(f => f.OperationClaimId).HasColumnName("OperationClaimId");
                u.HasOne(f => f.User);
                u.HasOne(f => f.OperationClaim);
            });

            modelBuilder.Entity<EmailAuthenticator>(e =>
            {
                e.ToTable("EmailAuthenticators").HasKey(f => f.Id);
                e.Property(f => f.Id).HasColumnName("Id");
                e.Property(f => f.UserId).HasColumnName("UserId");
                e.Property(f => f.ActivationKey).HasColumnName("ActivationKey");
                e.Property(f => f.IsVerified).HasColumnName("IsVerified");
                e.HasOne(f => f.User);
            });

            modelBuilder.Entity<OtpAuthenticator>(o =>
            {
                o.ToTable("OtpAuthenticators").HasKey(f => f.Id);
                o.Property(f => f.Id).HasColumnName("Id");
                o.Property(f => f.UserId).HasColumnName("UserId");
                o.Property(f => f.SecretKey).HasColumnName("SecretKey");
                o.Property(f => f.IsVerified).HasColumnName("IsVerified");
                o.HasOne(f => f.User);
            });

            modelBuilder.Entity<RefreshToken>(r =>
            {
                r.ToTable("RefreshTokens").HasKey(t => t.Id);
                r.Property(t => t.Id).HasColumnName("Id");
                r.Property(t => t.UserId).HasColumnName("UserId");
                r.Property(t => t.Token).HasColumnName("Token");
                r.Property(t => t.Expires).HasColumnName("Expires");
                r.Property(t => t.Created).HasColumnName("Created");
                r.Property(t => t.CreatedByIp).HasColumnName("CreatedByIp");
                r.Property(t => t.Revoked).HasColumnName("Revoked");
                r.Property(t => t.RevokedByIp).HasColumnName("RevokedByIp");
                r.Property(t => t.ReplacedByToken).HasColumnName("ReplacedByToken");
                r.Property(t => t.ReasonRevoked).HasColumnName("ReasonRevoked");
                r.HasOne(t => t.User);
            });

            modelBuilder.Entity<Github>(g =>
            {
                g.ToTable("Githubs").HasKey(x => x.Id);
                g.Property(x => x.Id).HasColumnName("Id");
                g.Property(x => x.UserId).HasColumnName("UserId");
                g.Property(x => x.GithubUrl).HasColumnName("GithubUrl");
                g.HasOne(x => x.User);
            });
        }
    }
}