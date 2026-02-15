using Microsoft.EntityFrameworkCore;
using MiniSistemaEstacionamentoAPI.Models;

namespace MiniSistemaEstacionamentoAPI.Data
{
    /// <summary>
    /// 
    /// Minha classe ParkingDbContext herda os metodos e atributos da DbContext do proprio framework
    /// dessa forma consigo mapear as entidades para tabela do BD
    /// 
    /// Observação:
    /// - Vehicle possui regra de unicidade na placa.
    /// - ParkingSession possui relacionamento com Vehicle.
    /// - Invoice é uma entidade "Owned" (não possui tabela própria).
    /// </summary>
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext(DbContextOptions<ParkingDbContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleModel> Vehicles { get; set; }
        public DbSet<ParkingSessionModel> ParkingSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleModel>(vehicle =>
            {
                vehicle.HasKey(vehicleEntity => vehicleEntity.Id);

                // implmeentando Regra Placa deve ser única
                vehicle.HasIndex(vehicleEntity => vehicleEntity.Plate)
                       .IsUnique();

                vehicle.Property(vehicleEntity => vehicleEntity.Type)
                       .IsRequired();
            });

            modelBuilder.Entity<ParkingSessionModel>(parkingSession =>
            {
                parkingSession.HasKey(session => session.Id);

                // Relacionamento:
                // Uma sessão pertence a um veículo
                parkingSession.HasOne(session => session.Vehicle)
                              .WithMany()
                              .HasForeignKey(session => session.VehicleId)
                              .OnDelete(DeleteBehavior.Cascade);

                // Persisti a invoice na mesma tabela que parkingSession porque so existe fatura se tiver parkingSession
                parkingSession.OwnsOne(session => session.Invoice, inv =>
                {
                    inv.Property(i => i.BasicPayment).IsRequired(false);
                    inv.Property(i => i.Tax).IsRequired(false);
                });
            });
        }
    }
}
