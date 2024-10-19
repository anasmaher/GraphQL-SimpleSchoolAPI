namespace GQLAPI.GraphQL.Types
{
    public class InstructorType : PersonType
    {
        public decimal Salary { get; set; }

        public ICollection<CourseType> Courses { get; set; }
    }
}
