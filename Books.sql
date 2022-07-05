---Create Book Table----
create table Books(
BookId int identity (1,1)primary key,
BookName varchar(255),
AuthorName varchar(255),
Rating int,
RatingCount int,
OriginalPrice decimal,
DiscountPrice decimal,
BookDetails varchar(255),
BookImage varchar(255),
BookQuantity int
);

select *from Books
----Add Procedure  for AddBook----
create procedure AddBook
(
@BookName varchar(255),
@authorName varchar(255),
@rating varchar(100),
@RatingCount int,
@originalPrice Decimal,
@discountPrice Decimal,
@BookDetails varchar(255),
@bookImage varchar(255),
@BookQuantity int
)
as
BEGIN
Insert into Books(BookName, authorName, rating, RatingCount, originalPrice, 
discountPrice, BookDetails, bookImage, BookQuantity)
values (@BookName, @authorName, @rating, @RatingCount ,@originalPrice, @discountPrice,
@BookDetails, @bookImage,@BookQuantity);
End;

----Store procedure for update book--

create procedure UpdateBook
(
@BookId int,
@BookName varchar(255),
@authorName varchar(255),
@rating varchar(100),
@RatingCount int,
@originalPrice Decimal,
@discountPrice Decimal,
@BookDetails varchar(255),
@bookImage varchar(255),
@BookQuantity int
)
as
BEGIN
Update Books set BookName = @BookName, 
authorName = @authorName,
rating = @rating,
RatingCount =@RatingCount,
originalPrice= @originalPrice,
discountPrice = @discountPrice,
BookDetails = @BookDetails,
bookImage =@bookImage,
BookQuantity = @BookQuantity
where BookId = @BookId;
End;

-------Procedure for delete----
create procedure DeleteBook
(
@BookId int
)
as
BEGIN
Delete Books 
where BookId = @BookId;
End;

----procedure for get book by id-----

create procedure GetBookByBookId
(
@BookId int
)
as
BEGIN
select * from Books
where BookId = @BookId;
End;

----Procedure for getAllBook----

create procedure GetAllBook
as
BEGIN
	select * from Books;
End;


------Create Admin Table----

create Table Admin
(
	AdminId int Identity(1,1) primary key not null,
	FullName varchar(255) not null,
	Email varchar(255) not null,
	Password varchar(255) not null,
	MobileNumber varchar(50) not null,
);

select * from Admin


INSERT INTO Admin VALUES ('Admin harsha','harshalohar@gmail.com', 'harsha@123', '+91 8805713251');


Create Procedure LoginAdmin
(
	@Email varchar(max),
	@Password varchar(max)
)
as
BEGIN
	If(Exists(select * from Admin where Email= @Email and Password = @Password))
		Begin
			select * from Admin where Email= @Email and Password = @Password;
		end
	Else
		Begin
			select 2;
		End
END;


