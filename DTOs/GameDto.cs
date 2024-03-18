namespace asp_game_store.DTOs;

public record class GameDto(
  int Id,
  string Name,
  string Genre,
  decimal Price,
  DateOnly ReleaseDate
)
{

}
