namespace MovieShop.Core.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Overview { get; set; }

        public decimal? Budget { get; set; }
    }
}