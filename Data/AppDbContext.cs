using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Models.amenities;
using PrePerchaseServer.Models.amenities.enums;
using PrePerchaseServer.Models.hotelgroup;
using PrePerchaseServer.Models.cities;
using PrePerchaseServer.Models.stay_highlight;
using PrePerchaseServer.Models.stay_highlight.enums;
using PrePerchaseServer.Models.hotelgroup.enums;
using PrePerchaseServer.Models.hotel;
using PrePerchaseServer.Models.hotel.enums;
using PrePerchaseServer.Models.roomcategory;
using PrePerchaseServer.Models.roomcategory.enums;
using PrePerchaseServer.Models.mediaBank;

namespace PrePerchaseServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DB TABLES
        public DbSet<City> Cities { get; set; }
        public DbSet<StayHighlight> StayHighlights { get; set; }

        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<HotelGroup> HotelGroups { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<HotelCancellationPolicy> HotelCancellationPolicies { get; set; }
        public DbSet<HotelCancellationPolicySlab> HotelCancellationPolicySlabs { get; set; }
        public DbSet<Mediabank> Mediabank => Set<Mediabank>();

        // 🔥 THIS IS WHERE YOUR CODE GOES
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // CITY CONFIGURATION
            // =========================
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(x => x.Name).HasMaxLength(100);
                entity.Property(x => x.State).HasMaxLength(100);
                entity.Property(x => x.Country).HasMaxLength(100);

                // INDEXES
                entity.HasIndex(x => x.Name);
                entity.HasIndex(x => x.Country);
                entity.HasIndex(x => new { x.Name, x.Country });
            });

            // =========================
            // STAY_HIGHLIGHT CONFIGURATION
            // =========================

            modelBuilder.Entity<StayHighlight>(entity =>
       {
           entity.HasKey(x => x.Id);

           entity.Property(x => x.Name)
                 .HasMaxLength(100)
                 .IsRequired();

           entity.Property(x => x.Slug)
                 .HasMaxLength(120)
                 .IsRequired();

           entity.Property(x => x.Status)
                 .HasMaxLength(20)
                 .HasDefaultValue(StayStatus.ACTIVE);

           entity.HasIndex(x => x.Slug)
                 .IsUnique();
       });

            // =========================
            // AMENITY CONFIGURATION
            // =========================
            modelBuilder.Entity<Amenities>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(x => x.Slug)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.HasIndex(x => x.Slug)
                      .IsUnique();

                entity.Property(x => x.Type)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(x => x.Status)
                      .HasConversion<string>()
                      .HasDefaultValue(AmenitiesStatus.ACTIVE);

                entity.Property(x => x.IconId)
                      .IsRequired(false);

                entity.Property(x => x.CreatedAt)
                      .HasDefaultValueSql("NOW()");

                entity.Property(x => x.UpdatedAt)
                      .IsRequired(false);
            });
            modelBuilder.Entity<HotelGroup>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.HasIndex(x => x.Name)
                    .IsUnique();

                entity.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasDefaultValue(HotelGroupStatus.ACTIVE);

                entity.Property(x => x.LogoId)
                    .IsRequired(false);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(x => x.UpdatedAt)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.HasIndex(x => x.Name).IsUnique();
                entity.HasIndex(x => x.Slug).IsUnique();

                entity.Property(x => x.Address)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(x => x.AboutHotel)
                    .HasMaxLength(3000);

                entity.Property(x => x.CheckInTime).IsRequired();
                entity.Property(x => x.CheckOutTime).IsRequired();

                entity.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasDefaultValue(HotelStatus.ACTIVE);

                entity.Property(x => x.CentralLatitude)
                    .HasPrecision(10, 7);

                entity.Property(x => x.CentralLongitude)
                    .HasPrecision(10, 7);

                entity.Property(x => x.Amenities)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );

                entity.Property(x => x.StayHighlights)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(x => x.UpdatedAt)
                    .IsRequired(false);
                entity.HasMany(h => h.RoomCategories)
                    .WithOne(rc => rc.Hotel)
                    .HasForeignKey(rc => rc.HotelId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(h => h.CancellationPolicies)
                    .WithOne(cp => cp.Hotel)
                    .HasForeignKey(cp => cp.HotelId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<RoomCategory>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.CategoryName)
                            .HasMaxLength(100)
                            .IsRequired();

                entity.Property(x => x.Description)
                            .HasMaxLength(3000);

                entity.Property(x => x.Slug)
                            .HasMaxLength(120);

                entity.Property(x => x.Status)
                            .HasConversion<string>()
                            .HasDefaultValue(RoomCategoryStatus.ACTIVE);

                // 🔥 ARRAY (PostgreSQL text[])
                entity.Property(x => x.RoomAmenities)
                            .HasColumnType("text[]");

                // 🔥 RELATIONSHIP
                entity.HasOne(rc => rc.Hotel)
                            .WithMany(h => h.RoomCategories)
                            .HasForeignKey(rc => rc.HotelId)
                            .OnDelete(DeleteBehavior.Cascade);

                // INDEXES
                entity.HasIndex(x => x.HotelId);
                entity.HasIndex(x => new { x.HotelId, x.CategoryName })
                            .IsUnique();

                entity.ToTable("room_categories");
            });
            modelBuilder.Entity<HotelCancellationPolicy>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.PolicyType)
                .HasConversion<string>();

                entity.HasMany(x => x.Slabs)
                .WithOne(s => s.Policy)
                .HasForeignKey(s => s.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(x => new { x.HotelId, x.PolicyType })
                .IsUnique();

                entity.ToTable("hotel_cancellation_policies");
            });

            modelBuilder.Entity<HotelCancellationPolicySlab>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.TimeRange)
                    .HasMaxLength(100);

                entity.Property(x => x.ChargeType)
                    .HasMaxLength(50);

                entity.Property(x => x.Value)
                    .HasPrecision(10, 2);

                entity.ToTable("hotel_cancellation_policy_slabs");
            });
             
    
            modelBuilder.Entity<Mediabank>(entity =>
                {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Url).IsRequired();

                entity.Property(x => x.Module)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne(x => x.RoomCategory)
                    .WithMany(r => r.RoomImg)
                    .HasForeignKey(x => x.RoomCategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Hotel)
                    .WithMany(h => h.HotelImages)
                    .HasForeignKey(x => x.HotelId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.HotelGroup)
                    .WithMany(g => g.HotelGroupImg)
                    .HasForeignKey(x => x.HotelGroupId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Amenities)
                    .WithMany(a => a.AmenitiesImages)
                    .HasForeignKey(x => x.AmenitiesId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
    
}
