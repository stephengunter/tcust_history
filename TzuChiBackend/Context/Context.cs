namespace TzuChiBackend.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;


	public partial class MyContext : DbContext
    {
        public MyContext()
            : base("name=MyConnection")
        {
			
		}

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<ContentType> ContentTypes { get; set; }
        public virtual DbSet<FileUplaod> FileUplaods { get; set; }
        public virtual DbSet<ImageShow> ImageShows { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ClassBook> ClassBooks { get; set; }
        public virtual DbSet<Coordinate> Coordinates { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Educated> Educateds { get; set; }
        public virtual DbSet<PlanInside> PlanInsides { get; set; }
        public virtual DbSet<PlanOutside> PlanOutsides { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
			

            modelBuilder.Entity<Admin>()
                .Property(e => e.AdminID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.LastLogonIP)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Creator)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Updater)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.ContentID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.TypeID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.RelatedLink)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.ContentCreator)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.ContentUpdater)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .HasOptional(e => e.Educated)
                .WithRequired(e => e.Content);

            modelBuilder.Entity<Content>()
                .HasOptional(e => e.PlanInside)
                .WithRequired(e => e.Content);

            modelBuilder.Entity<Content>()
                .HasOptional(e => e.PlanOutside)
                .WithRequired(e => e.Content);

            modelBuilder.Entity<ContentType>()
                .Property(e => e.Id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ContentType>()
                .Property(e => e.BackendLink)
                .IsUnicode(false);

            modelBuilder.Entity<ContentType>()
                .Property(e => e.FrontendLink)
                .IsUnicode(false);

            modelBuilder.Entity<ContentType>()
                .HasMany(e => e.Contents)
                .WithRequired(e => e.ContentType)
                .HasForeignKey(e => e.TypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FileUplaod>()
                .Property(e => e.ItemOID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FileUplaod>()
                .Property(e => e.ContentID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FileUplaod>()
                .Property(e => e.FunctionType)
                .IsUnicode(false);

            modelBuilder.Entity<FileUplaod>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<FileUplaod>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<FileUplaod>()
                .Property(e => e.Bit)
                .IsFixedLength();

            modelBuilder.Entity<FileUplaod>()
                .Property(e => e.Creator)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FileUplaod>()
                .Property(e => e.PreviewPath)
                .IsUnicode(false);

            modelBuilder.Entity<ImageShow>()
                .Property(e => e.TypeID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ImageShow>()
                .Property(e => e.ImageShowID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ImageShow>()
                .Property(e => e.ContentID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ImageShow>()
                .Property(e => e.Creator)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ImageShow>()
                .Property(e => e.Updater)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ClassBook>()
                .Property(e => e.ClassBookID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ClassBook>()
                .Property(e => e.StudentId)
                .IsUnicode(false);

            modelBuilder.Entity<Coordinate>()
                .Property(e => e.PointID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Coordinate>()
                .Property(e => e.TypeID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Director>()
                .Property(e => e.StartYear)
                .IsUnicode(false);

            modelBuilder.Entity<Director>()
                .Property(e => e.EndYear)
                .IsUnicode(false);

            modelBuilder.Entity<Educated>()
                .Property(e => e.ContentID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Educated>()
                .Property(e => e.CategoryYearID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Educated>()
                .Property(e => e.CategoryTermID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Educated>()
                .Property(e => e.CategoryEducatedID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Educated>()
                .Property(e => e.CategoryDoingID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanInside>()
                .Property(e => e.ContentID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanInside>()
                .Property(e => e.CategoryPrepID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanInside>()
                .Property(e => e.CategoryYearID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanInside>()
                .Property(e => e.CategoryPartitionID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanInside>()
                .Property(e => e.CategorySiteID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanInside>()
                .Property(e => e.ImageXY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanOutside>()
                .Property(e => e.ContentID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanOutside>()
                .Property(e => e.CategoryOutsideID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanOutside>()
                .Property(e => e.CategoryCountryID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanOutside>()
                .Property(e => e.CategoryDepartmentID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PlanOutside>()
                .Property(e => e.ImageXY)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
