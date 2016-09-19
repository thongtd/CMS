using Framework.Core.Domain;
using Framework.Core.Repositories;

namespace Framework.Persistence.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(WorkContext context) : base(context)
        {

        }
    }
}
