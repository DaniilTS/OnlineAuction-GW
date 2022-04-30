using Microsoft.EntityFrameworkCore;
using DBAL.Models;

#nullable disable

namespace DBAL.Context
{
    public partial class OnlineAuctionContext : DbContext
    {
        public OnlineAuctionContext()
        {
        }

        public OnlineAuctionContext(DbContextOptions<OnlineAuctionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<AuctionType> AuctionTypes { get; set; }
        public virtual DbSet<BalanceOperationType> BalanceOperationTypes { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CurrencyPair> CurrencyPairs { get; set; }
        public virtual DbSet<CurrencyPairRate> CurrencyPairRates { get; set; }
        public virtual DbSet<FinanceOperation> FinanceOperations { get; set; }
        public virtual DbSet<FinanceOperationStatus> FinanceOperationStatuses { get; set; }
        public virtual DbSet<FinanceOperationType> FinanceOperationTypes { get; set; }
        public virtual DbSet<FullName> FullNames { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<LotCategory> LotCategories { get; set; }
        public virtual DbSet<LotImage> LotImages { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<OfferStatus> OfferStatuses { get; set; }
        public virtual DbSet<Pocket> Pockets { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserImage> UserImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-MD63H11;Database=OnlineAuction;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Auction>(entity =>
            {
                entity.ToTable("Auction");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.EndPrice).HasColumnType("money");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.HasOne(d => d.AuctionType)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.AuctionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Auction__Auction__6A30C649");

                entity.HasOne(d => d.FinanceOperation)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.FinanceOperationId)
                    .HasConstraintName("FK__Auction__Finance__6D0D32F4");

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.LotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Auction__LotId__6B24EA82");

                entity.HasOne(d => d.Winner)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.WinnerId)
                    .HasConstraintName("FK__Auction__WinnerI__6C190EBB");
            });

            modelBuilder.Entity<AuctionType>(entity =>
            {
                entity.ToTable("AuctionType");

                entity.HasIndex(e => e.Name, "UQ__AuctionT__737584F6ED97D4B0")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<BalanceOperationType>(entity =>
            {
                entity.ToTable("BalanceOperationType");

                entity.HasIndex(e => e.Name, "UQ__BalanceO__737584F616C33EB5")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.HasIndex(e => e.Name, "UQ__Currency__737584F61B520B73")
                    .IsUnique();

                entity.HasIndex(e => e.Code, "UQ__Currency__A25C5AA7D048F2CE")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<CurrencyPair>(entity =>
            {
                entity.ToTable("CurrencyPair");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.From)
                    .WithMany(p => p.CurrencyPairFroms)
                    .HasForeignKey(d => d.FromId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CurrencyP__FromI__3E52440B");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.CurrencyPairTos)
                    .HasForeignKey(d => d.ToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CurrencyPa__ToId__3F466844");
            });

            modelBuilder.Entity<CurrencyPairRate>(entity =>
            {
                entity.ToTable("CurrencyPairRate");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Rate).HasColumnType("money");

                entity.Property(e => e.RateTime).HasColumnType("datetime");

                entity.HasOne(d => d.CurrencyPairRateNavigation)
                    .WithMany(p => p.CurrencyPairRates)
                    .HasForeignKey(d => d.CurrencyPairRateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CurrencyP__Curre__4222D4EF");
            });

            modelBuilder.Entity<FinanceOperation>(entity =>
            {
                entity.ToTable("FinanceOperation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.FinanceOperationStatus)
                    .WithMany(p => p.FinanceOperations)
                    .HasForeignKey(d => d.FinanceOperationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FinanceOp__Finan__5441852A");

                entity.HasOne(d => d.FinanceOperationType)
                    .WithMany(p => p.FinanceOperations)
                    .HasForeignKey(d => d.FinanceOperationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FinanceOp__Finan__534D60F1");

                entity.HasOne(d => d.Pocket)
                    .WithMany(p => p.FinanceOperations)
                    .HasForeignKey(d => d.PocketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FinanceOp__Pocke__52593CB8");
            });

            modelBuilder.Entity<FinanceOperationStatus>(entity =>
            {
                entity.ToTable("FinanceOperationStatus");

                entity.HasIndex(e => e.Name, "UQ__FinanceO__737584F6959CB82B")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<FinanceOperationType>(entity =>
            {
                entity.ToTable("FinanceOperationType");

                entity.HasIndex(e => e.Name, "UQ__FinanceO__737584F6EE71DC59")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.BalanceOperationType)
                    .WithMany(p => p.FinanceOperationTypes)
                    .HasForeignKey(d => d.BalanceOperationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FinanceOp__Balan__4CA06362");
            });

            modelBuilder.Entity<FullName>(entity =>
            {
                entity.ToTable("FullName");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ThirdName).HasMaxLength(50);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.HasIndex(e => e.Name, "UQ__Gender__737584F682D7749B")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Lot>(entity =>
            {
                entity.ToTable("Lot");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.StartPrice).HasColumnType("money");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Lots)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lot__CreatorId__5DCAEF64");

                entity.HasOne(d => d.LotCategory)
                    .WithMany(p => p.Lots)
                    .HasForeignKey(d => d.LotCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lot__LotCategory__5CD6CB2B");
            });

            modelBuilder.Entity<LotCategory>(entity =>
            {
                entity.ToTable("LotCategory");

                entity.HasIndex(e => e.Name, "UQ__LotCateg__737584F668E72E8B")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LotImage>(entity =>
            {
                entity.ToTable("LotImage");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.LotImages)
                    .HasForeignKey(d => d.LotId)
                    .HasConstraintName("FK__LotImage__LotId__619B8048");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("Offer");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Offer__CreatorId__74AE54BC");

                entity.HasOne(d => d.FinanceOperation)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.FinanceOperationId)
                    .HasConstraintName("FK__Offer__FinanceOp__76969D2E");

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.LotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Offer__LotId__75A278F5");
            });

            modelBuilder.Entity<OfferStatus>(entity =>
            {
                entity.ToTable("OfferStatus");

                entity.HasIndex(e => e.Name, "UQ__OfferSta__737584F62DBF3E9E")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Pocket>(entity =>
            {
                entity.ToTable("Pocket");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.HasOne(d => d.Holder)
                    .WithMany(p => p.Pockets)
                    .HasForeignKey(d => d.HolderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pocket__HolderId__44FF419A");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.Name, "UQ__Role__737584F61F54BE1A")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsFixedLength(true);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(72);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.FullName)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FullNameId)
                    .HasConstraintName("FK__User__FullNameId__2E1BDC42");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK__User__GenderId__2D27B809");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__RoleId__2C3393D0");
            });

            modelBuilder.Entity<UserImage>(entity =>
            {
                entity.ToTable("UserImage");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserImages)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserImage__UserI__34C8D9D1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
