using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore1.Models.Repositories  
{ 
    public class AuthorDbRepository : IBookStorRepositore<Author>
    {
        BookStoreDbContext db;
        public AuthorDbRepository(BookStoreDbContext _db)
        {
            db = _db;
        }

        public void Add(Author entity)
        {
            //entity.Id = db.Authors.Max(b => b.Id) + 1;
            db.Authors.Add(entity);
            DbSaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);
            db.Authors.Remove(author);
            DbSaveChanges();
        }

        public Author Find(int id)
        {
            var auther = db.Authors.SingleOrDefault(b => b.Id == id);
            return auther;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList() ;
        }

        public void Update(int id, Author newAuther)
        {
            db.Update(newAuther);
            DbSaveChanges();
        }


        public void DbSaveChanges()
        {
            db.SaveChanges();
        }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(a => a.FullName.Contains(term)).ToList();
        }
       
    }
}
