using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel book);

        public UpdateBookModel UpdateBook(UpdateBookModel updatebook);

        public bool DeleteBook(int BookId);

        public BookModel GetBookByBookId(int BookId);
        public List<BookModel> GetAllBooks();
    }
}
