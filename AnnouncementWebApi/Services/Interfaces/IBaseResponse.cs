namespace AnnouncementWebApi.Services.Interfaces
{
    public interface IBaseResponse<T>
    {
        T Response { get; set; }
    }
}
