using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BookWebAPIService.Models;

namespace BookWebAPIService.Controllers
{
    public class BooksController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetBooks()
        {
            IList<Book> books = null;
            using (var entities = new BookDBEntities())
            {
                books = entities.Books.ToList();
            }
            if (books.Count == 0)
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpGet]
        [ActionName("GetBySearch")]
        public IHttpActionResult GetBooks(string text)
        {
            IQueryable<Book> books = null;
            using (var entities = new BookDBEntities())
            {
                books = entities.Books.Where(b=>b.book_title.Contains(text)
                || b.author_name.Contains(text)
                || b.isbn.Contains(text));
            }
            if (!books.Any())
            {
                return Content(HttpStatusCode.NotFound, "Book does not exist.");
            }

            return Ok(books);
        }
        [HttpPost]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var entities = new BookDBEntities())
            {
                entities.Books.Add(new Book()
                {
                    book_id = book.book_id,
                    book_title = book.book_title,
                    author_name = book.author_name,
                    isbn = book.isbn,
                    publication_date = book.publication_date
                });
                entities.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete]
        //Delete a book item
        public IHttpActionResult DeleteBook(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Book id");

            using (var entities = new BookDBEntities())
            {
                var entity = entities.Books.FirstOrDefault(b => b.book_id == id);
                entities.Books.Remove(entity);
                entities.SaveChanges();
            }
            return Ok();
        }



        [HttpPut]
        //Update a book item
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid Book");
            using (var entities = new BookDBEntities())
            {
                var entity = entities.Books.FirstOrDefault(b => b.book_id == id);
                if (entity != null)
                {
                    entity.book_id = book.book_id == 0 ? entity.book_id: book.book_id;
                    entity.book_title = book.book_title == null? entity.book_title :book.book_title;
                    entity.author_name = book.author_name == null ? entity.author_name : book.author_name;
                    entity.isbn = book.isbn == null ? entity.isbn : book.isbn;
                    entity.publication_date = book.publication_date.HasValue ? book.publication_date : entity.publication_date;
                    entities.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();

        }
    }
}
