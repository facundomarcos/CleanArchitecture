using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infraestructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if (!context.Streamers!.Any())
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos insertando nuevos registros a la bd {context}", typeof(StreamerDbContext).Name);
            }
        }

        public static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
                new Streamer{CreatedBy ="facundo", Nombre="Maxi HBP", Url = "http://hbp.com"},
                new Streamer{CreatedBy ="facundo", Nombre="Amazon VIP", Url = "http://amazonvip.com"},
            };
        }
    }
}
