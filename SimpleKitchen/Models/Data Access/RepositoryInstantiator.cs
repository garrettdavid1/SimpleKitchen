using SimpleKitchen.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SimpleKitchen.Models.Data_Access
{
    public class RepositoryInstantiator<T> where T : class
    {
        protected Repository<T> repository;
        public RepositoryInstantiator()
        {
            repository = new Repository<T>();
        }
    }
}