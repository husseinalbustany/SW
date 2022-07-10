using Newtonsoft.Json;
using SW.Classes;
using SW.Classes.Responses;

namespace SW.Services
{
    public class SwApiServices : ISwApiServices
    {
        private IExternalApiServices _apiServices;
        private IConfiguration _config { get; }

        public SwApiServices(IConfiguration configuration, IExternalApiServices apiServices)
        {
            _apiServices = apiServices;
            _config = configuration;
        }
        public List<Starship> GetStarshipswithNumberofStops(int Distance)
        {
            List<Starship> listOfStarships = new List<Starship>();
            try
            {

                #region Get List of all star ship from api
                listOfStarships = GetListOfStarShipId();
                #endregion

                #region Fill the details in formation for each star ship
                if (listOfStarships != null && listOfStarships.Count > 0)
                {
                    foreach (var starship in listOfStarships)
                    {
                        #region Fill star ship details from api
                        StarshipDetailsApiResponse apiResponse = new StarshipDetailsApiResponse();
                        var restRequest = new RestSendRequest()
                        {
                            FullUrl = starship.Url,
                            RequestType = RequestTypeEnum.Get
                        }; // Rest Request
                        var response = _apiServices.SendRequestAsync(restRequest)?.Result;
                        if (!string.IsNullOrEmpty(response))
                        {
                            apiResponse = JsonConvert.DeserializeObject<StarshipDetailsApiResponse>(response);

                            if (apiResponse != null && !string.IsNullOrEmpty(apiResponse.Message) && apiResponse.Message.ToLower() == "ok")
                            {
                                starship.Consumables = apiResponse.Result?.Properties?.Consumables;
                                if (apiResponse.Result?.Properties?.MGLT != "unknown" && apiResponse.Result?.Properties?.Consumables != "unknown")
                                {
                                    starship.MGLT = Convert.ToInt32(apiResponse.Result?.Properties?.MGLT);
                                    starship.Numberofstops = CalculateNumberOfStops(starship.Consumables, (int)starship.MGLT, Distance).ToString();
                                }
                                else
                                    starship.Numberofstops = "Unknown";
                            }
                        }
                        #endregion Fill star ship details from api

                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                //log the error
            }

            return listOfStarships;
        }


        /// <summary>
        /// This function call the api to get list of all star ship 
        /// </summary>
        /// <returns></returns>
        public List<Starship> GetListOfStarShipId()
        {
            List<Starship> list = new List<Starship>();//response for this function
            ListofStarshipApiResponse apiResponse = new ListofStarshipApiResponse();//api call response
            var restRequest = new RestSendRequest()
            {
                BaseUrl = _config["BaseUrl"],
                RequestUrl = "starships",
                InputObject = "",
                RequestType = RequestTypeEnum.Get
            }; // Rest Request
            var response = _apiServices.SendRequestAsync(restRequest)?.Result;
            if (!string.IsNullOrEmpty(response))
            {
                apiResponse = JsonConvert.DeserializeObject<ListofStarshipApiResponse>(response);

                if (apiResponse != null && !string.IsNullOrEmpty(apiResponse.Message) && apiResponse.Message.ToLower() == "ok")
                {
                    FillStarshipFromApiResponse(ref list, apiResponse);
                    #region fill all other ship from all pages
                    while (!string.IsNullOrEmpty( apiResponse.Next ))
                    {
                        restRequest.FullUrl = apiResponse.Next;
                        response = _apiServices.SendRequestAsync(restRequest)?.Result;
                        apiResponse = JsonConvert.DeserializeObject<ListofStarshipApiResponse>(response);
                        FillStarshipFromApiResponse(ref list, apiResponse);
                    }
                    #endregion
                }
            }
            return list;
        }


        /// <summary>
        /// this function used to fill a local list of starship from the api response
        /// </summary>
        /// <param name="starships"></param>
        /// <param name="apiResponse"></param>
        private void FillStarshipFromApiResponse(ref List<Starship> starships, ListofStarshipApiResponse apiResponse)
        {
            if (apiResponse != null && !string.IsNullOrEmpty(apiResponse.Message) && apiResponse.Message.ToLower() == "ok")
            {
                if (apiResponse.Results != null && apiResponse.Results.Count > 0)
                {
                    foreach (var item in apiResponse.Results)
                    {
                        starships.Add(new Starship()
                        {
                            Id = item.Uid,
                            Name = item.Name,
                            Url = item.Url
                        });
                    }
                }
            }


        }

        /// <summary>
        /// this function calculate the number of stops based on consumable , MGLT , Distance,ROund down for the result Distance /(MGLT*time)
        /// </summary>
        /// <param name="Consumable"></param>
        /// <param name="MGLT"></param>
        /// <param name="Distance"></param>
        /// <returns></returns>
        private int CalculateNumberOfStops(string Consumable, int MGLT, int Distance)
        { 
            int totalStops = 0;

            try
            {
                totalStops = (int)Math.Floor((decimal) (Distance / (Helper.Helper.GetNumberOfHour(Consumable) * MGLT)));

            }
            catch {
                totalStops = 0;
            }

            return totalStops;
        }




    }
}
