﻿using CleanArchitecture.Data;
using CleanArchitecture.Domain;

StreamerDbContext dbContext = new();

//await AddNewRecords();

QueryStreaming();

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