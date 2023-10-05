namespace WebApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required decimal Price { get; set; }
        public Address Location { get; set; }
        public int PressId { get; set; }
        public Press? Press { get; set; }
    }
}