namespace GQLDomain.Entities
{
    public class Student : Person
    {
        public decimal GPA { get; set; }

        public ICollection<Enrollment> Enrolls { get; set; }
    }
}
