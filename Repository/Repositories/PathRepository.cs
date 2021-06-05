using Domain.Models;
using Infrastructure.Repositories;
using Repository.Base;

namespace Repository.Repositories
{
    public class PathRepository : GenericRepository<Path>, IPathRepository
    {
        public PathRepository(UniversityMapContext context) : base(context) { }

        public override void Add(Path obj)
        {
            context.Set<Path>().Add(obj);
            context.SaveChanges();
        }
    }
}
