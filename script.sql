USE [UniHostelDB]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'Host')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (3, N'Renter')
SET IDENTITY_INSERT [dbo].[Roles] OFF
INSERT [dbo].[Users] ([ID], [Username], [Password], [RoleID]) VALUES (N'1', N'dinhphu', N'123', 2)
INSERT [dbo].[Users] ([ID], [Username], [Password], [RoleID]) VALUES (N'2', N'cachua', N'123', 3)
INSERT [dbo].[Users] ([ID], [Username], [Password], [RoleID]) VALUES (N'3', N'admin', N'123', 1)
INSERT [dbo].[Hosts] ([ID], [FullName], [Address], [Phone], [Mail], [NumOfRoom], [Description], [isActive]) VALUES (N'1', N'Phu Nguyen Dinh', N'Binh Duong', N'12212121', N'dinhphu@gmail.com', 12, NULL, 1)
INSERT [dbo].[Rooms] ([ID], [Name], [Square], [Price], [Description], [isActive], [HostID]) VALUES (N'Room 1', N'1', 12, 1E+07, NULL, 1, N'1')
INSERT [dbo].[Renters] ([ID], [FullName], [StartDate], [EndDate], [BirthDate], [Mail], [HomeTown], [Phone], [Description], [RoomID]) VALUES (N'2', N'Ca Chua Nguyen Thi', CAST(N'2018-09-28' AS Date), NULL, NULL, N'cachua@gmail.com', N'Ha Noi', N'212121', NULL, N'Room 1')

/*
ALTER TABLE [dbo].[AdvancedServices] ADD  CONSTRAINT [DF__AdvancedS__isAct__7B264821]  DEFAULT (1) FOR [isActive]
GO
ALTER TABLE [dbo].[BillAdvancedServiceDetails] ADD  DEFAULT (getdate()) FOR [Time]
GO
ALTER TABLE [dbo].[BillAdvancedServiceDetails] ADD  DEFAULT (1) FOR [Quantity]
GO
ALTER TABLE [dbo].[BillCompulsoryServiceDetails] ADD  DEFAULT (getdate()) FOR [Time]
GO
ALTER TABLE [dbo].[Bills] ADD  DEFAULT (getdate()) FOR [Time]
GO
ALTER TABLE [dbo].[Bills] ADD  DEFAULT (0) FOR [OtherFee]
GO
ALTER TABLE [dbo].[Bills] ADD  CONSTRAINT [DF__Bills__isPaid__725BF7F6]  DEFAULT (0) FOR [isPaid]
GO
ALTER TABLE [dbo].[CompulsoryServices] ADD  CONSTRAINT [DF__Compulsor__isAct__753864A1]  DEFAULT (1) FOR [isActive]
GO

*/