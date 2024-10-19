using System.ComponentModel.DataAnnotations;

namespace GQLDomain.Entities
{
    public class Person
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }
    }
}
