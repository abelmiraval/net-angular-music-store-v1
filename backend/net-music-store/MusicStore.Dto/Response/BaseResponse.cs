namespace MusicStore.Dto.Response
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
        public TClass Result { get; set; }
    }

    public class BaseCollectionResponse<TClass> : BaseResponseGeneric<TClass>
    {
        public int TotalPages { get; set; }
    }
}
