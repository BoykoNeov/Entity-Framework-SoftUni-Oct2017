namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext() {}

        public HospitalContext(DbContextOptions options) : base()
        {}

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<PatientMedicament> PatientMedicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.Property(e => e.FirstName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.Property(e => e.LastName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.Property(e => e.Address)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(250);

                entity.Property(e => e.Email)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(80);

                entity.Property(e => e.HasInsurance)
                .IsRequired(true)
                .HasDefaultValue(true);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.Property(e => e.Name)
                .IsUnicode(true)
                .IsRequired(true)
                .HasMaxLength(100);

                entity.Property(e => e.Specialty)
                .IsUnicode(true)
                .IsRequired(true)
                .HasMaxLength(100);
            });
            
            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.HasKey(e => e.VisitationId);

                entity.Property(e => e.Date)
                .IsRequired(true)
                .HasColumnName("DATETIME2")
                .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Comments)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                .WithMany(p => p.Visitations)
                .HasForeignKey(e => e.PatientId)
                .HasConstraintName("FK_Visitation_Patient");

                entity.Property(e => e.DoctorId)
                .IsRequired(false);

                entity.HasOne(e => e.Doctor)
                .WithMany(d => d.Visitations)
                .HasForeignKey(e => e.DoctorId)
                .HasConstraintName("FK_Visitation_Doctor");
            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(e => e.DiagnoseId);

                entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);

                entity.Property(e => e.Comments)
                .IsRequired(false)
                .IsUnicode(true)
                .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                .WithMany(p => p.Diagnoses)
                .HasForeignKey(e => e.PatientId)
                .HasConstraintName("FK_Diagnoses_Patient");
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.PatientId,
                    e.MedicamentId
                });

                entity.HasOne(e => e.Medicament)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(e => e.MedicamentId);

                entity.HasOne(e => e.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(e => e.PatientId);
            });
        } 
    }
}