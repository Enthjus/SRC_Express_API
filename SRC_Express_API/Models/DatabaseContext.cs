using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ActiveTrip> ActiveTrips { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<MapBlank> MapBlanks { get; set; }
        public virtual DbSet<PercentByAge> PercentByAges { get; set; }
        public virtual DbSet<PricePerKm> PricePerKms { get; set; }
        public virtual DbSet<Refund> Refunds { get; set; }
        public virtual DbSet<RequestRefund> RequestRefunds { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketRequestRefund> TicketRequestRefunds { get; set; }
        public virtual DbSet<TimeStart> TimeStarts { get; set; }
        public virtual DbSet<Total> Totals { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<TypeCar> TypeCars { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(250)
                    .HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(250);

                entity.Property(e => e.ExpCode).HasColumnType("datetime");

                entity.Property(e => e.Idrole).HasColumnName("IDRole");

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Username).HasMaxLength(250);

                entity.HasOne(d => d.IdroleNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.Idrole)
                    .HasConstraintName("FK_Accounts_Roles");
            });

            modelBuilder.Entity<ActiveTrip>(entity =>
            {
                entity.HasKey(e => e.IdtripsTiimeStart)
                    .HasName("PK_TripsTimeStart");

                entity.Property(e => e.IdtripsTiimeStart).HasColumnName("IDTripsTiimeStart");

                entity.Property(e => e.Idcars)
                    .HasMaxLength(250)
                    .HasColumnName("IDCars");

                entity.Property(e => e.IdtimeStart).HasColumnName("IDTimeStart");

                entity.Property(e => e.Idtrips).HasColumnName("IDTrips");

                entity.HasOne(d => d.IdcarsNavigation)
                    .WithMany(p => p.ActiveTrips)
                    .HasForeignKey(d => d.Idcars)
                    .HasConstraintName("FK_ActiveTrips_Cars");

                entity.HasOne(d => d.IdtimeStartNavigation)
                    .WithMany(p => p.ActiveTrips)
                    .HasForeignKey(d => d.IdtimeStart)
                    .HasConstraintName("FK_TripsTimeStart_TimeStart");

                entity.HasOne(d => d.IdtripsNavigation)
                    .WithMany(p => p.ActiveTrips)
                    .HasForeignKey(d => d.Idtrips)
                    .HasConstraintName("FK_TripsTimeStart_Trips");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(250)
                    .HasColumnName("ID");

                entity.Property(e => e.Model).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Photo).HasMaxLength(250);

                entity.Property(e => e.RegistrationDateEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("RegistrationDate_end");

                entity.Property(e => e.RegistrationDateStart)
                    .HasColumnType("datetime")
                    .HasColumnName("RegistrationDate_start");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.Type)
                    .HasConstraintName("FK_Cars_TypeCars");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Dob).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.FullName).HasMaxLength(250);

                entity.Property(e => e.Idaccount)
                    .HasMaxLength(250)
                    .HasColumnName("IDAccount");

                entity.Property(e => e.Photo).HasMaxLength(250);

                entity.HasOne(d => d.IdaccountNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Idaccount)
                    .HasConstraintName("FK_Customers_Accounts1");
            });

            modelBuilder.Entity<MapBlank>(entity =>
            {
                entity.HasKey(e => e.IdmapBlank);

                entity.ToTable("Map_Blank");

                entity.Property(e => e.IdmapBlank).HasColumnName("IDMap_blank");

                entity.Property(e => e.A).HasMaxLength(50);

                entity.Property(e => e.B).HasMaxLength(50);
            });

            modelBuilder.Entity<PercentByAge>(entity =>
            {
                entity.ToTable("PercentByAge");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<PricePerKm>(entity =>
            {
                entity.ToTable("PricePerKm");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Refund>(entity =>
            {
                entity.ToTable("Refund");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<RequestRefund>(entity =>
            {
                entity.HasKey(e => e.Idrequest);

                entity.ToTable("Request_Refund");

                entity.Property(e => e.Idrequest).HasColumnName("IDRequest");

                entity.Property(e => e.DaysConfirmRefund)
                    .HasColumnType("datetime")
                    .HasColumnName("Days_Confirm_Refund");

                entity.Property(e => e.DaysSendRefund)
                    .HasColumnType("datetime")
                    .HasColumnName("Days_Send_Refund");

                entity.Property(e => e.Idrefund).HasColumnName("IDRefund");

                entity.Property(e => e.StatusDone).HasColumnName("Status_done");

                entity.Property(e => e.TotalRefund).HasColumnName("Total_Refund");

                entity.HasOne(d => d.IdrefundNavigation)
                    .WithMany(p => p.RequestRefunds)
                    .HasForeignKey(d => d.Idrefund)
                    .HasConstraintName("FK_Request_Refund_Refund");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Creater).HasMaxLength(250);

                entity.Property(e => e.FromAddress).HasMaxLength(250);

                entity.Property(e => e.IdAccCustomer).HasMaxLength(250);

                entity.Property(e => e.IdactiveTrip).HasColumnName("IDActiveTrip");

                entity.Property(e => e.Seats).HasMaxLength(250);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.ToAddress).HasMaxLength(250);

                entity.HasOne(d => d.IdAccCustomerNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdAccCustomer)
                    .HasConstraintName("FK_Tickets_Accounts");

                entity.HasOne(d => d.IdactiveTripNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdactiveTrip)
                    .HasConstraintName("FK_Tickets_ActiveTrips");
            });

            modelBuilder.Entity<TicketRequestRefund>(entity =>
            {
                entity.ToTable("TicketRequestRefund");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idrequest).HasColumnName("IDRequest");

                entity.Property(e => e.Idticket).HasColumnName("IDTicket");

                entity.HasOne(d => d.IdrequestNavigation)
                    .WithMany(p => p.TicketRequestRefunds)
                    .HasForeignKey(d => d.Idrequest)
                    .HasConstraintName("FK_TicketRequestRefund_Request_Refund");

                entity.HasOne(d => d.IdticketNavigation)
                    .WithMany(p => p.TicketRequestRefunds)
                    .HasForeignKey(d => d.Idticket)
                    .HasConstraintName("FK_TicketRequestRefund_Tickets");
            });

            modelBuilder.Entity<TimeStart>(entity =>
            {
                entity.HasKey(e => e.IdtimeStart);

                entity.ToTable("TimeStart");

                entity.Property(e => e.IdtimeStart).HasColumnName("IDTimeStart");

                entity.Property(e => e.TimeStart1)
                    .HasColumnType("datetime")
                    .HasColumnName("TimeStart");
            });

            modelBuilder.Entity<Total>(entity =>
            {
                entity.HasKey(e => e.Idtotal);

                entity.Property(e => e.Idtotal).HasColumnName("IDTotal");

                entity.Property(e => e.IdPercentByAge).HasColumnName("ID_PercentByAge");

                entity.Property(e => e.IdPricePerKm).HasColumnName("ID_PricePerKm");

                entity.Property(e => e.IdtypeCar).HasColumnName("IDTypeCar");

                entity.Property(e => e.Total1).HasColumnName("Total");

                entity.HasOne(d => d.IdPercentByAgeNavigation)
                    .WithMany(p => p.Totals)
                    .HasForeignKey(d => d.IdPercentByAge)
                    .HasConstraintName("FK_Totals_PercentByAge");

                entity.HasOne(d => d.IdPricePerKmNavigation)
                    .WithMany(p => p.Totals)
                    .HasForeignKey(d => d.IdPricePerKm)
                    .HasConstraintName("FK_Totals_PricePerKm");

                entity.HasOne(d => d.IdtypeCarNavigation)
                    .WithMany(p => p.Totals)
                    .HasForeignKey(d => d.IdtypeCar)
                    .HasConstraintName("FK_Totals_TypeCars");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FromAddress).HasMaxLength(250);

                entity.Property(e => e.NameTrip).HasMaxLength(250);

                entity.Property(e => e.Photo).HasMaxLength(250);

                entity.Property(e => e.ToAddress).HasMaxLength(250);
            });

            modelBuilder.Entity<TypeCar>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeName).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
