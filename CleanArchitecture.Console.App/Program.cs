using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

StreamerDbContext dbContext = new();

//await AddNewRecords();
//QueryStreaming();
//await QueryFilter();
//await QueryMethods();
await QueryLinq();

Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();

async Task QueryLinq()
{
    Console.WriteLine($"Ingrese el servicio de streaming");
    var streamerNombre = Console.ReadLine();
    var streamers = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"%{streamerNombre}%")
                           select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task QueryMethods()
{
    var streamer = dbContext!.Streamers!;

    //si es nulo dispara un error
    var firstAsync = await streamer.Where(y => y.Nombre.Contains("a")).FirstAsync();

    //si es nulo devuelve nulo
    var firstOrDefault1 = await streamer.Where(y => y.Nombre.Contains("a")).FirstOrDefaultAsync();

    //si es nulo devuelve nulo
    var firstOrDefault2 = await streamer.FirstOrDefaultAsync(y => y.Nombre.Contains("a"));

    //si el resultado tiene mas de un valor dispara una excepcion
    //si no hay valor tambien
    var singleAsync = await streamer.Where(x=> x.Id == 1).SingleAsync();

    //siempre devuelve un valor, si no tiene un valor devuelve nulo
    var singleOrDefaultAsync = await streamer.Where(x => x.Id == 1).SingleOrDefaultAsync();

    //busca por ID
    var resultado = await streamer.FindAsync(1);
}

async Task QueryFilter()
{
    Console.WriteLine($"Ingrese una compañia de streaming:");
    var streamingNombre = Console.ReadLine();

    var streamers = await dbContext!.Streamers!
        .Where(x => x.Nombre.Equals(streamingNombre)).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{ streamer.Id} - {streamer.Nombre}");
    }

    //var streamerPartialResults = await dbContext!.Streamers!
    //     .Where(x => x.Nombre.Contains(streamingNombre)).ToListAsync();

    //busqueda con EF
    var streamerPartialResults = await dbContext!.Streamers!
     .Where(x => EF.Functions.Like(x.Nombre, $"%{streamingNombre}%")).ToListAsync();


    foreach (var streamer in streamerPartialResults)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }

}

void QueryStreaming()
{
    var streamers = dbContext!.Streamers!.ToList();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task AddNewRecords()
{
    Streamer streamer = new()
    {
        Nombre = "disney",
        Url = "https://www.disney.com"
    };

    dbContext!.Streamers!.Add(streamer);

    await dbContext.SaveChangesAsync();

    var movies = new List<Video>
{
    new Video
    {
        Nombre = "La Cenicienta",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "1001 dalmatas",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "El jorobado de Notredame",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "Star Wars",
        StreamerId = streamer.Id
    },
};

    await dbContext.AddRangeAsync(movies);

    await dbContext.SaveChangesAsync();
}