using Educode.Domain.Models.Abstract;

namespace Educode.Domain.Courses
{
    public class Course : Entity<Guid>
    {
        public Course(Guid id) : base(id)
        {
        }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }
    }
}
