using SW.Classes;

namespace SW.Services
{
    public interface ISwApiServices
    {
        List<Starship> GetStarshipswithNumberofStops(int Distance);
    }
}
