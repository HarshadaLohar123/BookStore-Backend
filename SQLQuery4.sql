create procedure ForgotPassword
(
@Email varchar(Max)
)
as 
begin
Update Users
set Password='Null'
where Email=@Email;
select * from Users where Email = @Email;
End;

select * from Users

create procedure ResetPassword
(
@Email varchar(250),
@Password varchar(250)
)
AS
BEGIN
UPDATE Users 
SET 
Password = @Password 
WHERE Email = @Email;
End;

