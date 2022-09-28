using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using MusicStore.DataAccess;
using MusicStore.Entities;

namespace MusicStore.UnitTest;

public class DbContextUnitTest : IDisposable
{
    protected readonly MusicStoreDbContext Context;

    protected DbContextUnitTest()
    {
        var options = new DbContextOptionsBuilder<MusicStoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Context = new MusicStoreDbContext(options);

        Context.Database.EnsureCreated();

        Seed();
    }

    private void Seed()
    {
        var random = new Random();
        var list = new List<Concert>();

        for (int i = 0; i < 100; i++)
        {
            var valor = random.Next(20, 400);

            list.Add(new Concert
            {
                Title = $"Concierto Random en {valor}",
                Description = $"Concierto con el id {i}",
                GenreId = 1,
                TicketsQuantity = random.Next(),
                UnitPrice = Convert.ToDecimal(random.Next(10, 500)),
                DateEvent = new DateTime(random.Next(2018, 2022), random.Next(1, 12), random.Next(1, 28)),
                Status = true,
                Finalized = false
            });
        }

        Context.Set<Concert>().AddRange(list);
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context?.Dispose();
    }
}