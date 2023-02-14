using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

StreamerDbContext dbContext = new();

//await AddNewRecords();
//QueryStreaming();
//await QueryFilter();
//await QueryMethods();
//await QueryLinq();
//await TrackingAndNotTracking();
//await AddNewStreamerWithVideoId();
//await AddNewActorWithVideo();
//await AddNewDirectorWithVideo();
await MultipleEntitiesQuery();


Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();


async Task MultipleEntitiesQuery()
{
    //var videoWithActores = await dbContext!.Videos!.Include(x => x.Actores).FirstOrDefaultAsync(x => x.Id == 1);

    //var actor = await dbContext!.Actores!.Select(x=>x.Nombre).ToListAsync();

    var videoWithDirector = await dbContext!.Videos!
        .Where(q=> q.Director != null)
        .Include(q => q.Director)
        .Select(q =>
            new
            {
                Director_Nombre_Completo = $"{q.Director.Nombre} {q.Director.Apellido}",
                Movie = q.Nombre
            }
        )
        .ToListAsync();

    foreach (var pelicula in videoWithDirector)
    {
        Console.WriteLine($"{pelicula.Movie} - {pelicula.Director_Nombre_Completo}");
    }
}

async Task AddNewDirectorWithVideo()
{
    var director = new Director
    {
        Nombre = "Lorenzo",
        Apellido = "Basteri",
        VideoId = 1
    };

    await dbContext.AddAsync(director);
    await dbContext.SaveChangesAsync();

}

async Task AddNewActorWithVideo()
{
    var actor = new Actor()
    {
        Nombre = "Brad",
        Apellido = "Pitt"
    };

    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();

    var videoActor = new VideoActor()
    {
        ActorId = actor.Id,
        VideoId = 1
    };

    await dbContext.AddAsync(videoActor);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVideoId()
{
    var batmanForever = new Video
    {
        Nombre = "Batman Forever",
        StreamerId = 4
    };
    await dbContext.AddAsync(batmanForever);
    await dbContext.SaveChangesAsync();

}

async Task AddNewStreamerWithVideo()
{
    var pantaya = new Streamer
    {
        Nombre = "Pantaya"

    };

    var hungerGames = new Video
    {
        Nombre = "Hunger Games",
        Streamer = pantaya
    };
    await dbContext.AddAsync(hungerGames);
    await dbContext.SaveChangesAsync(); 

}

async Task TrackingAndNotTracking() 
    {
        var streamerWithTracking = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);
        //NO tracking libera el objeto de la memoria
        var streamerWithNoTracking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

        streamerWithTracking.Nombre = "Netflix Super";

        //no actualiza porque el objeto fue liberado de la memoria
        streamerWithNoTracking.Nombre = "Amazon Plus";

        await dbContext!.SaveChangesAsync();

    }

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