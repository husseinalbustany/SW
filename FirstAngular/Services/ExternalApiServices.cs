using System.Text;

namespace SW.Services
{
    public class ExternalApiServices:IExternalApiServices
    {
        public async Task<string> SendRequestAsync(RestSendRequest input)
        {
            string apiResponse = "";
            string FullUrlRequest = "";
            if (!string.IsNullOrEmpty(input.FullUrl))
            {
                FullUrlRequest = input.FullUrl;
            }
            else
            {
                FullUrlRequest = input.BaseUrl + input.RequestUrl;
            }

            try
            {
                var requestContent = new StringContent(input.InputObject, input.EncodingType, input.ContentType);
                using (var httpClient = new HttpClient())
                {
                    if (input.HeaderParameters != null)
                    {
                        if (input.HeaderParameters.Any())
                        {
                            foreach (var item in input.HeaderParameters)
                            {
                                httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                            }
                        }
                    }
                    var baseUrl = input.BaseUrl;

                    switch (input.RequestType)
                    {
                        case RequestTypeEnum.Post:
                            using (var response = httpClient.PostAsync(FullUrlRequest, requestContent))
                            {
                                apiResponse = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        case RequestTypeEnum.Get:
                            using (var response = httpClient.GetAsync(FullUrlRequest))
                            {
                                apiResponse = await response.Result.Content.ReadAsStringAsync();
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                //Log exception
                apiResponse = "";
            }
            return apiResponse;
        }
    }

    public class RestSendRequest
    {

        public Encoding EncodingType { get; set; } = Encoding.UTF8;
        public string ContentType { get; set; } = "application/json";
        public string? InputObject { get; set; } = "";
        public string? RequestUrl { get; set; }
        public RequestTypeEnum? RequestType { get; set; }
        public string? BaseUrl { get; set; }
        public Dictionary<string, string>? HeaderParameters { get; set; }
        public string? FullUrl { get; set; }
    }

    public enum RequestTypeEnum
    {
        Post,
        Get
    }

}
