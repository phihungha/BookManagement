using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    [Owned]
    public class Address
    {
        public required string City { get; set; }
        public required string Street { get; set; }
    }
}