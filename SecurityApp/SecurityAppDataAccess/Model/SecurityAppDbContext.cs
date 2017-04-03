using System.Data.Entity;
using MySql.Data.Entity;

namespace SecurityAppDataAccess.Model
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class SecurityAppDbContext : DbContext
    {
        public SecurityAppDbContext()
            : base("name=SecurityAppConnectionString")
        {
        }
        static SecurityAppDbContext()
        {
            // ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Application>()
                .HasMany(e => e.roles)
                .WithRequired(e => e.application)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.roles)
                .WithRequired(e => e.group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Token>();

        }
    }
}
