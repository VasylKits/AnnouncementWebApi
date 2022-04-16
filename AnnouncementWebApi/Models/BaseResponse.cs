using AnnouncementWebApi.Services.Interfaces;

namespace AnnouncementWebApi.Models
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T Response { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
