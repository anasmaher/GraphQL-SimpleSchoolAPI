using GQLAPI.Schema.Enums;

namespace GQLAPI.GraphQL.Types
{

    public class CourseType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public SubjectEnum Subject { get; set; }

        [GraphQLNonNullType]
        public InstructorType Instructor { get; set; }

        public ICollection<StudentType> Students { get; set; }
    }
}
