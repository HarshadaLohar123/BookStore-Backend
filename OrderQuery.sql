create table Orders
(
	OrderId int identity(1,1) not null primary key,
	TotalPrice int not null,
	BookQuantity int not null,
	OrderDate Date not null,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
	BookId INT NOT NULL FOREIGN KEY REFERENCES Books(BookId),
	AddressId int not null FOREIGN KEY REFERENCES Address(AddressId)
);
select * from Orders;

-----Add order SP-----


Create Proc AddOrders
(
	@BookQuantity int,
	@UserId int,
	@BookId int,
	@AddressId int
)
as
Declare @TotalPrice int
BEGIN
	set @TotalPrice = (select DiscountPrice from Books where BookId = @BookId);
	
			If(Exists (Select * from Books where BookId = @BookId))
				BEGIN
					Begin try
						Begin Transaction
						Insert Into Orders(TotalPrice, BookQuantity, OrderDate, UserId, BookId, AddressId)
						Values(@TotalPrice*@BookQuantity, @BookQuantity, GETDATE(), @UserId, @BookId, @AddressId);
						Update Books set BookQuantity= BookQuantity-@BookQuantity where BookId = @BookId;
						Delete from Cart where BookId = @BookId and UserId = @UserId;
						select * from Orders;
						commit Transaction
					End try
					Begin Catch
							rollback;
					End Catch
				END
			
	Else
		Begin
			Select 2;
		End
END;

-------Get All Order SP-----

Create Proc GetAllOrders
(
	@UserId int
)
as
begin
		Select 
		Orders.OrderId, Orders.UserId, Orders.AddressId, Books.BookId,
		Orders.TotalPrice, Orders.BookQuantity, Orders.OrderDate,
		Books.BookName, Books.AuthorName, Books.BookImage
		FROM Books 
		inner join Orders on Orders.BookId = Books.BookId 
		where 
			Orders.UserId = @UserId;
END