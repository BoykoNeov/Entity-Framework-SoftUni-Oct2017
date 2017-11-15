namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext() { }

        public StudentSystemContext(DbContextOptions options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(s =>
            {
                s.HasKey(p => p.StudentId);

                s.Property(p => p.Name)
                .IsUnicode(true)
                .HasMaxLength(100);

                s.Property(p => p.PhoneNumber)
                .IsRequired(false)
                .HasColumnName("CHAR(10)")
                .IsUnicode(false);

                s.Property(p => p.Birthday)
                .IsRequired(false);
            });

            modelBuilder.Entity<Course>(c =>
            {
                c.HasKey(p => p.CourseId);

                c.Property(p => p.Name)
                .IsUnicode(true)
                .HasMaxLength(80)
                .IsRequired(true);

                c.Property(p => p.Description)
                .IsRequired(false)
                .IsUnicode(true);

                c.Property(p => p.StartDate)
                .IsRequired(true)
                .HasColumnName("DATETIME2");

                c.Property(p => p.EndDate)
                .IsRequired(true)
                .HasColumnName("DATETIME2");

                c.Property(p => p.Price)
                .IsRequired(true);
            });

            modelBuilder.Entity<Resource>(r =>
            {
                r.HasKey(p => p.ResourceId);

                r.Property(p => p.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                r.Property(p => p.Url)
                .IsRequired(true)
                .IsUnicode(false);

                r.Property(p => p.ResourceType)
                .IsRequired(true);

                r.HasOne(e => e.Course)
                .WithMany(res => res.Resources)
                .HasForeignKey(e => e.CourseId);
            });

            modelBuilder.Entity<Homework>(hs =>
            {
                hs.HasKey(p => p.HomeworkId);

                hs.Property(p => p.Content)
                .IsUnicode(false)
                .IsRequired(true);

                hs.Property(p => p.ContentType)
                .IsRequired(true);

                hs.Property(p => p.SubmissionTime)
                .HasColumnName("DATETIME2");

                hs.HasOne(e => e.Course)
                .WithMany(h => h.HomeworkSubmissions)
                .HasForeignKey(e => e.CourseId);

                hs.HasOne(e => e.Student)
                .WithMany(h => h.HomeworkSubmissions)
                .HasForeignKey(e => e.StudentId);

            });

            modelBuilder.Entity<StudentCourse>(sc =>
            {
                sc.HasKey(e => new
                {
                    e.StudentId,
                    e.CourseId
                });

                sc.HasOne(e => e.Student)
                .WithMany(s => s.CourseEnrollments)
                .HasForeignKey(e => e.StudentId);

                sc.HasOne(e => e.Course)
                .WithMany(s => s.StudentsEnrolled)
                .HasForeignKey(e => e.CourseId);
            });
        }
    }
}