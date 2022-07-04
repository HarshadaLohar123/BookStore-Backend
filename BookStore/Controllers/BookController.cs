using BusinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookModel book)
        {
            try
            {
                BookModel userData = this.bookBL.AddBook(book);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "Book Added Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "Sorry! Book Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        //---------------Update Book--------

        [Authorize(Roles = Role.Admin)]
        [HttpPost("UpdateBook")]
        public IActionResult UpdateBook(UpdateBookModel updatebook)
        {
            try
            {
                UpdateBookModel userData = this.bookBL.UpdateBook(updatebook);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "Book Updeted Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "Sorry! Updated Failed" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        ////-------------------------- Delete Book ----------------
        [Authorize(Roles = Role.Admin)]

        [HttpDelete("DeleteBook/{BookId}")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                if (this.bookBL.DeleteBook(BookId))
                {
                    return this.Ok(new { Success = true, message = "Book Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Soory! please Enter Valid Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]

        [HttpGet("GetBookByBookId/{BookId}")]
        public IActionResult GetBookByBookId(int BookId)
        {
            try
            {
                var book = this.bookBL.GetBookByBookId(BookId);
                if (book != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = book });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Soory! Please Enter Valid Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllBook")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var updatedBookDetail = this.bookBL.GetAllBooks();
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sorry! Wrong credentials" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
