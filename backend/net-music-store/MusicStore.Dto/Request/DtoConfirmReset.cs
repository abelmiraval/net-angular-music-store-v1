namespace MusicStore.Dto.Request;

public record DtoConfirmReset(string Email, string Token, string Password);