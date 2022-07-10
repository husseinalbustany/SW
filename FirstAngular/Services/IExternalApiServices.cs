namespace SW.Services
{
    public interface IExternalApiServices
    {
        Task<string> SendRequestAsync(RestSendRequest input);
    }
}
