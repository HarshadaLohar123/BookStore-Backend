----Create table for wishlist----
create table WishList
(
	WishListId int identity(1,1) not null primary key,
	UserId int foreign key references Users(UserId) on delete no action,
	BookId int foreign key references Books(BookId) on delete no action
);

select * from WishList

---store procedure for AddToWishlist-----

create proc AddWishList
(
@UserId int,
@BookId int
)
as
begin 
       insert into WishList
	   values (@UserId,@BookId);
end;

-----Delete WishList---
create proc DeleteWishList
(
@WishListId int,
@UserId int

)
as
begin
delete WishList where WishListId = @WishListId;
end;

drop proc DeleteWishList


create proc GetWishListByUserId
(
	@UserId int
)
as
begin
	select WishListId,UserId,c.BookId,BookName,AuthorName,
	DiscountPrice,OriginalPrice,BookImage from WishList c join Books b on c.BookId=b.BookId 
	where UserId=@UserId;
end;
Footer