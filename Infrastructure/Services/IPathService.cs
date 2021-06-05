using Infrastructure.Services.Models;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public interface IPathService
    {
        void AddRange(List<PathModel> models);
        List<PathModel> GetAll();
    }
}
