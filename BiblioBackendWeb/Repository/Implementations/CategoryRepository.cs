using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Implementations;
using System;
using System.Collections;

namespace BiblioBackendWeb.Repository.Interfaces
{
    public class CategoryRepository : Repository<Categorie>, ICategoryRepository
    {
        public CategoryRepository(BibliothequeDbContext context):base(context)
        {

        }
    }
}
