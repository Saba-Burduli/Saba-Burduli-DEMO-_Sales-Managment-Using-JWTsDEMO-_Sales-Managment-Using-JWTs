using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.DATA.Configurations;
using SalesManagementSystem.DATA.Entites;

namespace SalesManagementSystem.DATA
{
    public class SalesManagmentSystemDbContext : DbContext
    {
        public DbSet<Category>? Categorys { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<OrderStatus>? OrderStatus { get; set; } // დავამატე Entites. რადგან მეორე OnModelCreating  დააერორა
        public DbSet<PaymentType>? PaymentTypes { get; set; }
        public DbSet<Person>? Persons { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Rating>? Ratings { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<PersonType>? PersonTypes { get; set; }


        public SalesManagmentSystemDbContext()
        {

        }

        public SalesManagmentSystemDbContext(DbContextOptions<SalesManagmentSystemDbContext> contect) : base(contect)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(); //we need this for migration

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding

            modelBuilder.Entity<PaymentType>().HasData(
                   new PaymentType()
                   {
                       PaymentTypeId = 1,
                       PaymentName = "Card"
                   },
                   new PaymentType()
                   {
                       PaymentTypeId = 2,
                       PaymentName = "Cash"
                   },
                   new PaymentType()
                   {
                       PaymentTypeId = 3,
                       PaymentName = "Coupon"
                   });

            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus()
                {
                    OrderStatusId = 1,
                    OrderStatusA = "Pending "
                },

                new OrderStatus()
                {
                    OrderStatusId = 2,
                    OrderStatusA = "Completed "
                },

                new OrderStatus()
                {
                    OrderStatusId = 3,
                    OrderStatusA = "Cancelled "
                });

            modelBuilder.Entity<PersonType>().HasData(
                new PersonType
                {
                    PersontypeId = 1,
                    PersonTypeName = "Customer",
                    PersonTypeDescription = "the Customer is always right"
                },

                new PersonType
                {
                    PersontypeId = 2,
                    PersonTypeName = "Delivere",
                    PersonTypeDescription = ""
                },

                new PersonType
                {
                    PersontypeId = 3,
                    PersonTypeName = "Employer",
                    PersonTypeDescription = ""
                });

            modelBuilder.Entity<Role>().HasData(
              new Role
              {
                  RoleId = 1,
                  RoleName = "Admin",
                  RoleDescription = ""
              },

              new Role
              {
                  RoleId = 2,
                  RoleName = "User",
                  RoleDescription = ""
              });

            modelBuilder.Entity<Person>().HasData(
                new Person()
                {
                    PersonId = 1,
                    FirstName = "Nikoloz",
                    LastName = "Varamashvili",
                    Address = "Some Address",
                    Phone = "+995 12 34 56",
                    PersonTypeId = 3,
                });

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    UserId = 1,
                    Email = "n.varamashvili@gmail.com",
                    RegistrationDate = new DateTime(2025,02,14),
                    UserName = "Admin",
                    PasswordHash = "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=",  // admin123
                    PersonId = 1
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleUser", // This is the auto-generated join table                

                left => left.HasOne<Role>()
                .WithMany()
                .HasForeignKey("RoleId")
                .OnDelete(DeleteBehavior.Cascade),

                right => right.HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade),

                r => r.HasData(new { UserId = 1, RoleId = 1 }));

            modelBuilder.ApplyConfiguration(new CategoryConfiguaration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguation());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration());
        }
    }
}
