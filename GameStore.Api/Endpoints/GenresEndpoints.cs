using GameStore.Api.Data;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace asp_game_store;

public static class GenresEndpoints
{
  public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("genres");

    // GET /genres
    group.MapGet("/", async (GameStoreContext dbContext) =>
    {
      return await dbContext.Genres
        .Select(genre => genre.ToDto())
        .AsNoTracking()
        .ToListAsync();
    });

    return group;
  }
}
