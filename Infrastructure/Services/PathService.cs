using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Models;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class PathService : IPathService
    {
        private readonly IPathRepository repository;
        private readonly IMapper mapper;

        public PathService(IPathRepository repository,
                           IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public void AddRange(List<PathModel> models)
        {
            foreach(var model in models)
                repository.Add(mapper.Map<Path>(model));
        }

        public List<PathModel> GetAll()
        {
            return mapper.Map<List<PathModel>>(repository.GetAll());
        }
    }
}
