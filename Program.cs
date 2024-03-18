using asp_game_store.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
  new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
  new (2, "Final Fantasy XIV", "Roleplaying", 59.99M, new DateOnly(2010, 9, 30)),
  new (1, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9, 27)),
];

// GET /games
app.MapGet("games", () => games);

// GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id));

app.Run();
