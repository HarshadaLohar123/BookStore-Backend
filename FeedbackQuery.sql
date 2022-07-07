create Table Feedback
(
	FeedbackId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Comment varchar(max) not null,
	Rating decimal not null,
	BookId int not null FOREIGN KEY (BookId) REFERENCES Books(BookId),
	UserId INT NOT NULL FOREIGN KEY (UserId) REFERENCES Users(UserId),
);


select * from Feedback;

-----Add Feedback in Stored Processure----

create Proc AddFeedback
(
	@Comment varchar(max),
	@Rating decimal,
	@BookId int,
	@UserId int
)
as
Declare @AverageRating int;
BEGIN
	IF (EXISTS(SELECT * FROM Feedback WHERE BookId = @BookId and UserId=@UserId))
		select 1;
	Else
	Begin
		IF (EXISTS(SELECT * FROM Books WHERE BookId = @BookId))
		Begin  select * from Feedback
			Begin try
				Begin transaction
					Insert into Feedback(Comment, Rating, BookId, UserId) values(@Comment, @Rating, @BookId, @UserId);		
					set @AverageRating = (Select AVG(Rating) from Feedback where BookId = @BookId);
					Update Books set rating = @AverageRating
								 where  BookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
END

-------------------------------------------------------

Create Proc GetAllFeedback
(
	@BookId int
)
as
BEGIN
	Select Feedback.FeedbackId, Feedback.UserId, Feedback.BookId,Feedback.Comment,Feedback.Rating, Users.FullName
	From Users
	Inner Join Feedback
	on Feedback.UserId = Users.UserId
	where
	 BookId = @BookId;
END;

