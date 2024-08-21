using ClientSupportSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientSupportSystem.Database.Maps
{
    public class TicketMap : IEntityTypeConfiguration<TicketModel>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketModel> builder)
        {
            builder.HasKey(t => t.Id);

        }
    }
}
