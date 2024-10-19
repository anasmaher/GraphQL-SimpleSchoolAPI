using GQLAPI.Schema.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GQLDomain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public SubjectEnum Subject { get; set; }

        [ForeignKey(nameof(Instructor))]
        public Guid InstructorId { get; set; }
        
        public Instructor Instructor { get; set; }

        public ICollection<Enrollment> Enrolls { get; set; }
    }
}
