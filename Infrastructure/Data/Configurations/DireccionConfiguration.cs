using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuracion
{
     public class DireccionConfiguration : IEntityTypeConfiguration<Direccion>
     {
         public void Configure(EntityTypeBuilder<Direccion> builder)
         {
             // Aquí puedes configurar las propiedades de la entidad
             // utilizando el objeto builder
             builder.ToTable("Direccion");

             builder.HasKey(e => e.Id);
             builder.Property(e => e.Id);

             builder.Property(e => e.DetalleDireccion)
                 .IsRequired()
                 .HasMaxLength(100);

             builder.HasOne(p => p.Ciudades)
                 .WithMany(p => p.Direcciones)
                 .HasForeignKey(p => p.CiudadId);
             }
         }
     }