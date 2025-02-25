using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet для всех сущностей
        public DbSet<User> Users { get; set; }
        public DbSet<PlantFamily> PlantFamilies { get; set; }
        public DbSet<Exposition> Expositions { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<PhenologyRecord> PhenologyRecords { get; set; }
        public DbSet<BiometryRecord> BiometryRecords { get; set; }
        public DbSet<PlantImage> PlantImages { get; set; }
        public DbSet<PlantStatus> PlantStatuses { get; set; }
        public DbSet<PlantMovement> PlantMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация для User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.Username).IsUnique();
            });

            // Конфигурация для PlantFamily
            modelBuilder.Entity<PlantFamily>(entity =>
            {
                entity.HasKey(e => e.PlantFamilyId);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Конфигурация для Exposition
            modelBuilder.Entity<Exposition>(entity =>
            {
                entity.HasKey(e => e.ExpositionId);
                entity.HasIndex(e => e.ExpositionName).IsUnique();
            });

            // Конфигурация для Area
            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.AreaId);
                entity.HasIndex(e => e.Name).IsUnique();
                
                // Связь с Exposition (многие области могут относиться к одной экспозиции)
                entity.HasOne(e => e.Exposition)
                    .WithMany()
                    .HasForeignKey(e => e.ExpositionId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Конфигурация для Plant
            modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasKey(e => e.PlantId);
                entity.HasIndex(e => e.InventoryNumber).IsUnique();
                
                // Связь с PlantFamily
                entity.HasOne(e => e.PlantFamily)
                    .WithMany()
                    .HasForeignKey(e => e.PlantFamilyId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                // Связь с Exposition
                entity.HasOne(e => e.Exposition)
                    .WithMany()
                    .HasForeignKey(e => e.ExpositionId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                // Связь с Area
                entity.HasOne(e => e.Area)
                    .WithMany(a => a.Plants)
                    .HasForeignKey(e => e.AreaId)
                    .OnDelete(DeleteBehavior.SetNull);
                
                // Связь с User (кто создал запись)
                entity.HasOne(e => e.CreatedBy)
                    .WithMany()
                    .HasForeignKey(e => e.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Конфигурация для PhenologyRecord
            modelBuilder.Entity<PhenologyRecord>(entity =>
            {
                entity.HasKey(e => e.PhenologyRecordId);
                
                // Связь с Plant
                entity.HasOne(e => e.Plant)
                    .WithMany(p => p.PhenologyRecords)
                    .HasForeignKey(e => e.PlantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация для BiometryRecord
            modelBuilder.Entity<BiometryRecord>(entity =>
            {
                entity.HasKey(e => e.BiometryRecordId);
                
                // Связь с Plant
                entity.HasOne(e => e.Plant)
                    .WithMany(p => p.BiometryRecords)
                    .HasForeignKey(e => e.PlantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация для PlantImage
            modelBuilder.Entity<PlantImage>(entity =>
            {
                entity.HasKey(e => e.PlantImageId);
                
                // Связь с Plant
                entity.HasOne(e => e.Plant)
                    .WithMany(p => p.PlantImages)
                    .HasForeignKey(e => e.PlantId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Связь с User (кто загрузил изображение)
                entity.HasOne(e => e.UploadedBy)
                    .WithMany()
                    .HasForeignKey(e => e.UploadedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Конфигурация для PlantStatus
            modelBuilder.Entity<PlantStatus>(entity =>
            {
                entity.HasKey(e => e.PlantStatusId);
                
                // Связь с Plant
                entity.HasOne(e => e.Plant)
                    .WithMany(p => p.PlantStatuses)
                    .HasForeignKey(e => e.PlantId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Связь с User (кто установил статус)
                entity.HasOne(e => e.UpdatedBy)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Конфигурация для PlantMovement
            modelBuilder.Entity<PlantMovement>(entity =>
            {
                entity.HasKey(e => e.PlantMovementId);
                
                // Связь с Plant
                entity.HasOne(e => e.Plant)
                    .WithMany(p => p.PlantMovements)
                    .HasForeignKey(e => e.PlantId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Связь с SourceArea
                entity.HasOne(e => e.SourceArea)
                    .WithMany()
                    .HasForeignKey(e => e.SourceAreaId)
                    .OnDelete(DeleteBehavior.SetNull);
                
                // Связь с DestinationArea
                entity.HasOne(e => e.DestinationArea)
                    .WithMany()
                    .HasForeignKey(e => e.DestinationAreaId)
                    .OnDelete(DeleteBehavior.SetNull);
                
                // Связь с User (кто переместил растение)
                entity.HasOne(e => e.MovedBy)
                    .WithMany()
                    .HasForeignKey(e => e.MovedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
