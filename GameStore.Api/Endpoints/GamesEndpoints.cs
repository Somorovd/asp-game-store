﻿using GameStore.Api.Data;
using GameStore.Api.DTOs;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
  const string GetGameEndpoint = "GetGame";

  private static readonly List<GameSummaryDto> games = [
    new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new (2, "Final Fantasy XIV", "Roleplaying", 59.99M, new DateOnly(2010, 9, 30)),
    new (3, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9, 27)),
  ];

  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("games")
                  .WithParameterValidation();

    // GET /games
    group.MapGet("/", () => games);

    // GET /games/1
    group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
    {
      Game? game = dbContext.Games.Find(id);

      return (game is null)
        ? Results.NotFound()
        : Results.Ok(game.ToGameDetailsDto());

    }).WithName(GetGameEndpoint);

    // POST /games
    group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
    {
      Game game = newGame.ToEntity();

      dbContext.Games.Add(game);
      dbContext.SaveChanges();

      return Results.CreatedAtRoute(
        GetGameEndpoint,
        new { id = game.Id },
        game.ToGameDetailsDto()
      );
    });

    // PUT /games/1
    group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
    {
      var existingGame = dbContext.Games.Find(id);

      if (existingGame is null)
      {
        return Results.NotFound();
      }

      dbContext.Entry(existingGame)
        .CurrentValues
        .SetValues(updatedGame.ToEntity(id));
      dbContext.SaveChanges();

      return Results.NoContent();
    });

    //DELETE /games/1
    group.MapDelete("/{id}", (int id) =>
    {
      games.RemoveAll(game => game.Id == id);
      return Results.NoContent();
    });

    return group;
  }
}