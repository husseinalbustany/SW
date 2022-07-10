namespace SW.Classes
{
    public class ListOfStarship
    {
        public List<Starship>? Starships { get; set; }
    }
    public class Starship
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Numberofstops { get; set; }
        public int? MGLT { get; set; }
        public string? Consumables { get; set; }
        public string? Url { get; set; }

    }
}


