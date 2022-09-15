namespace MusicStore.Dto
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public ICollection<string> ListErrors { get; set; }

        public BaseResponse()
        {
            ListErrors = new List<string>();
        }
    }

    public class BaseResponseGeneric<TClass> : BaseResponse
    {
        public TClass ResponseResult { get; set; }

    }
}
