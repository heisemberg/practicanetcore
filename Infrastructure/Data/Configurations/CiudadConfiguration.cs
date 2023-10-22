using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuracion
{
     public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
     {
         public void Configure(EntityTypeBuilder<Ciudad> builder)
         {
             // Aquí puedes configurar las propiedades de la entidad
             // utilizando el objeto builder
             builder.ToTable("Ciudad");

             builder.HasKey(e => e.Id   );
             builder.Property(e => e.Id);

             builder.Property(e => e.NombreCiudad)
                 .IsRequired()
                 .HasMaxLength(50);

             builder.HasOne(p => p.Departamentos)
                  .WithMany(p => p.Ciudades)
                  .HasForeignKey(p => p.DepartamentoId);
             }
         }
     }