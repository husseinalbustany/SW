namespace SW.Classes.Responses
{

    public class StarshipDetailsApiResponse:ApiResponse
    {
        public Result? Result { get; set; }
    }

    public class Result
    {
        public Properties? Properties { get; set; }
    }

    public class Properties
    {
        public string? MGLT { get; set; }
        public string? Consumables { get; set; }

    }

}
