namespace WebApi.Models
{
    public class Press
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Category Category { get; set; }
        public List<Book> Books { get; set; }
    }
}