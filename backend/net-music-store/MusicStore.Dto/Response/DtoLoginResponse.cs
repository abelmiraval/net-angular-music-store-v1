namespace MusicStore.Dto.Response;

public class DtoLoginResponse : BaseResponse
{
    public string Token { get; set; }
    
    public string FullName { get; set; }

    public ICollection<string> Roles { get; set; }
}