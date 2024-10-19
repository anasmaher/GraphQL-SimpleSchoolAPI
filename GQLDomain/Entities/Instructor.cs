namespace GQLDomain.Entities
{
    public class Instructor : Person
    {
        public decimal Salary { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
