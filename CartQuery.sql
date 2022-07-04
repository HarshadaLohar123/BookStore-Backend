-----create table for cart----
select  *  From Cart

Create Table Cart
(
CartId int identity(1,1) primary key,
BooksQuantity int default 1,
UserId int not null foreign key (UserId) references Users(UserId),
BookId int not null Foreign key (BookId) references Books(BookId)
)



-----procedure for AddCart----
Create procedure Addcart
( @BooksQuantity int,
@UserId int,
@BookId int
)
As
Begin
	insert into cart(BooksQuantity,UserId, BookId)
	values ( @BooksQuantity,@UserId, @BookId);
End

------procedure remove------

create procedure RemoveFromCart
(
@CartId int
)
As
Begin
	delete from Cart where CartId = @CartId;
end

--------procedure for getcart by userId-----
create proc GetCartByUserId
(
	@UserId int
)
as
begin
	select CartId,BooksQuantity,UserId,c.BookId,BookName,AuthorName,
	DiscountPrice,OriginalPrice,BookImage from Cart c join Books b on c.BookId=b.BookId 
	where UserId=@UserId;
end;

------Update Procedure-----

create proc UpdateCart
(
	@BooksQuantity int,
	@BookId int,
	@UserId int,
	@CartId int
)
as
begin
update Cart set BookId=@BookId,
				UserId=@UserId,
				@BooksQuantity=@BooksQuantity
				where CartId=@CartId;
end;



