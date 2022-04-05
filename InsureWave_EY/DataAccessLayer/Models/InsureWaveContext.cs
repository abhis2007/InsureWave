using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class InsureWaveContext : DbContext
    {
        public InsureWaveContext()
        {
        }

        public InsureWaveContext(DbContextOptions<InsureWaveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Broker> Brokers { get; set; }
        public virtual DbSet<BrokerBuyer> BrokerBuyers { get; set; }
        public virtual DbSet<BuyerAssetPolicy> BuyerAssetPolicies { get; set; }
        public virtual DbSet<BuyerAssetVessel> BuyerAssetVessels { get; set; }
        public virtual DbSet<CountryCurrExchange> CountryCurrExchanges { get; set; }
        public virtual DbSet<Demmo> Demmos { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Insurer> Insurers { get; set; }
        public virtual DbSet<InsurerBroker> InsurerBrokers { get; set; }
        public virtual DbSet<PolicyDetail> PolicyDetails { get; set; }
        public virtual DbSet<PremiumAmountDetail> PremiumAmountDetails { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-ASJU298;Database=InsureWave;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("Asset");

                entity.HasIndex(e => e.Name, "UQ__Asset__737584F67B787D2A")
                    .IsUnique();

                entity.Property(e => e.AssetId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Broker>(entity =>
            {
                entity.ToTable("Broker");

                entity.HasIndex(e => e.LicenseId, "LIDUNN")
                    .IsUnique();

                entity.Property(e => e.BrokerId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LicenseId).ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Brokers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("BUIDFK");
            });

            modelBuilder.Entity<BrokerBuyer>(entity =>
            {
                entity.HasKey(e => e.Bbid)
                    .HasName("BBIDPK");

                entity.ToTable("Broker.Buyer");

                entity.Property(e => e.Bbid).HasColumnName("BBId");

                entity.Property(e => e.BrokerId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.BrokerBuyers)
                    .HasForeignKey(d => d.BrokerId)
                    .HasConstraintName("BIdFK");

                entity.HasOne(d => d.PolicyStatusNavigation)
                    .WithMany(p => p.BrokerBuyers)
                    .HasForeignKey(d => d.PolicyStatus)
                    .HasConstraintName("FK__Broker.Bu__Polic__5D95E53A");
            });

            modelBuilder.Entity<BuyerAssetPolicy>(entity =>
            {
                entity.HasKey(e => e.BuyerAssetId)
                    .HasName("PK__BuyerAss__B8DC5AB254FFE1E9");

                entity.ToTable("BuyerAssetPolicy");

                entity.HasIndex(e => e.PolicyId, "UQ__BuyerAss__2E1339A5610209F5")
                    .IsUnique();

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.BuyerAssetPolicies)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK__BuyerAsse__Asset__6166761E");
            });

            modelBuilder.Entity<BuyerAssetVessel>(entity =>
            {
                entity.HasKey(e => e.BuyerId)
                    .HasName("BIDPK");

                entity.ToTable("BuyerAssetVessel");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.BuyerAssetVessels)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("AIDFK");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.BuyerAssetVessels)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("CIDFK");

                entity.HasOne(d => d.RequestStatusNavigation)
                    .WithMany(p => p.BuyerAssetVessels)
                    .HasForeignKey(d => d.RequestStatus)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("RSFK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BuyerAssetVessels)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UIDFK");
            });

            modelBuilder.Entity<CountryCurrExchange>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("CIDPK");

                entity.ToTable("CountryCurrExchange");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Demmo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("demmo");

                entity.Property(e => e.Namr)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("namr");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GenderName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Insurer>(entity =>
            {
                entity.ToTable("Insurer");

                entity.Property(e => e.InsurerId)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LicenseId).ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Insurers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Insurer__UserId__6442E2C9");
            });

            modelBuilder.Entity<InsurerBroker>(entity =>
            {
                entity.HasKey(e => e.Ibid)
                    .HasName("PK__Insurer.__82350FD1B839B641");

                entity.ToTable("Insurer.Broker");

                entity.Property(e => e.Ibid).HasColumnName("IBID");

                entity.Property(e => e.BrokerId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsurerId)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.InsurerBrokers)
                    .HasForeignKey(d => d.BrokerId)
                    .HasConstraintName("FK__Insurer.B__Broke__69FBBC1F");

                entity.HasOne(d => d.Insurer)
                    .WithMany(p => p.InsurerBrokers)
                    .HasForeignKey(d => d.InsurerId)
                    .HasConstraintName("FK__Insurer.B__Insur__6AEFE058");
            });

            modelBuilder.Entity<PolicyDetail>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK__PolicyDe__C577554056B441C2");

                entity.Property(e => e.Pid).HasColumnName("PId");

                entity.Property(e => e.Ibid).HasColumnName("IBID");

                entity.Property(e => e.PolicyStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Ib)
                    .WithMany(p => p.PolicyDetails)
                    .HasForeignKey(d => d.Ibid)
                    .HasConstraintName("FK__PolicyDeta__IBID__6DCC4D03");
            });

            modelBuilder.Entity<PremiumAmountDetail>(entity =>
            {
                entity.HasKey(e => e.PremAmtId)
                    .HasName("PK__PremiumA__38FAB2BDDA0314B6");

                entity.Property(e => e.PremAmtId).HasColumnName("PremAMtId");

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.IntervalOfEmi)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("IntervalOfEMI");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.PreType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PremAmt).HasColumnName("PremAMt");

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.PremiumAmountDetails)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK__PremiumAmou__PID__70A8B9AE");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.RoleName, "UQ__Role__8A2B6160191818E6")
                    .IsUnique();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.EmailId, "EID")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.GenderId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("GIDFK");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__User__RoleId__151B244E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
