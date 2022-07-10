namespace SW.Classes.Responses
{

    public class ListofStarshipApiResponse:ApiResponse
    {
        public int? Total_records { get; set; }
        public int? Total_pages { get; set; }
        public string? Previous { get; set; }
        public string? Next { get; set; }
        public List<StarShip>? Results { get; set; }
    }

    public class StarShip
    {
        public string? Uid { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
    }

}
