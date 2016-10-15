using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static SampleContext _dbcontext = EmployeesContextFactory.Create();

        private DbSet<SampleTable> DbSet
        {
            get { return _dbcontext.Set<SampleTable>(); }
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<SampleTable> Get()
        {
            return DbSet;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public SampleTable Get(int id)
        {
            return DbSet.Where(c => c.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody]SampleTable value)
        {
            try
            {
                DbSet.Add(value);
                _dbcontext.SaveChanges();
                return "OK";
            }
            catch (Exception e)
            {
                return "Erro: " + e.Message;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody]SampleTable value)
        {
            try
            {
                SampleTable t = DbSet.Where(c => c.Id == id).FirstOrDefault();
                t.Name = value.Name;
                _dbcontext.Entry<SampleTable>(t).State = EntityState.Modified;
                _dbcontext.SaveChanges();
                return "OK";
            }
            catch (Exception e)
            {
                return "Erro: " + e.Message;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            SampleTable t = DbSet.Where(c => c.Id == id).FirstOrDefault();
            if (t != null)
            {
                DbSet.Remove(t);
                _dbcontext.SaveChanges();
                return "OK";
            }
            else
            {
                return "Não encontrado";
            }
        }
    }
}
