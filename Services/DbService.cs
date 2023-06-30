using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergiaElectrica.Services
{
    public class DbService : IDbService
    {
        EnergiaContext context;
        public DbService(EnergiaContext dbcontext)
        {
            context = dbcontext;
        }
        public void CreateDb()
        {
            context.Database.EnsureCreated();
        }
    }
    public interface IDbService
    {
        void CreateDb();
    }
}