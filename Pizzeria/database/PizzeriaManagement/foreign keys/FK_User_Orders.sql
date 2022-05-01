ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_User_Orders] FOREIGN KEY ([UserId]) 
	REFERENCES [dbo].[Users] ([Id])
