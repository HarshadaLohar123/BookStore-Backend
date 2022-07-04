using BusinessLayer.Interface;
using DatabaseLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BookBL:IBookBL
    {
        private readonly IBookRL BookRL;

        public BookBL(IBookRL bookRL)
        {
            this.BookRL = bookRL;
        }

        public BookModel AddBook(BookModel book)
        {
            try
            {
                return this.BookRL.AddBook(book);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UpdateBookModel UpdateBook(UpdateBookModel updatebook)
        {
            try
            {
                return this.BookRL.UpdateBook(updatebook);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool DeleteBook(int BookId)
        {
            try
            {
                return this.BookRL.DeleteBook(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.BookRL.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                return this.BookRL.GetBookByBookId(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
