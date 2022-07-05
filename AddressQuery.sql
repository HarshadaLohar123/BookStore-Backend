----Table for address type----

create Table AddressType
(
	TypeId int identity(1,1) not null primary key,
	TypeName varchar(255) not null
);

Select * from AddressType;

Insert into AddressType
values('Home'),('Office'),('Other');


-----Table for Address-----
create Table Address
(
	AddressId int identity(1,1) primary key,
	Address varchar(max) not null,
	City varchar(100) not null,
	State varchar(100) not null,
	TypeId int not null 
	FOREIGN KEY (TypeId) REFERENCES AddressType(TypeId),
	UserId INT not null
	FOREIGN KEY (UserId) REFERENCES Users(UserId),
);

select *from Address


------Store Procedure for Add Address----


create proc AddAddress
(
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
)
as
BEGIN
	If exists(select * from AddressType where TypeId=@TypeId)
		begin
			Insert into Address
			values(@Address, @City, @State, @TypeId, @UserId);
		end
	Else
		begin
			select 2
		end
end;


------Procedure for Delete-----

create Proc DeleteAddress
(
	@AddressId int,
	@UserId int
)
as
begin
	Delete Address
	where 
		AddressId=@AddressId and UserId=@UserId;
end;


-----procedure for Update----
 create proc spforUpdateAddress
(
	@Address varchar(255),
    @City varchar(255),
    @State varchar(255),
    @TypeId varchar(255),  
	@UserId varchar(255),
	@AddressId varchar(255)
)
as
begin

update Address set Address =@Address,
				City =@City,
				State  =@State,
				TypeId =@TypeId
				where UserId=@UserId and AddressId=@AddressId;
end;