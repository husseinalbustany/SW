namespace SW.Helper
{
    public class Helper
    {
        /// <summary>
        /// This function return the number of hour.example 2 months return 2*30*24, 2 days return 2 *24
        /// </summary>
        /// <param name="consumables"></param>
        /// <returns></returns>
        public static int GetNumberOfHour(string consumables)
        {
            List<string> list= consumables.Split(' ').ToList();

            int hours = 0;

            if(list==null || list.Count != 2)
                return hours;

            switch (list[1].ToLower())
            { 
                case "day":
                case "days":
                    hours =  Convert.ToInt32(list[0]) * 24;
                    break;
                case "week":
                case "weeks":
                    hours = Convert.ToInt32(list[0]) * 7 * 24;
                    break;
                case "month":
                case "months":
                    hours = Convert.ToInt32(list[0]) * 30 * 24;
                    break ;
                case "year":
                case "years":
                    hours = Convert.ToInt32(list[0]) * 365 * 24;
                    break;
                default:
                    hours = 0;
                    break;
            }
            return hours;

        }
        
    }
}
