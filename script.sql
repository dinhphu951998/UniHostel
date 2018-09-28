USE [UniHostelDB]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'Host')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (3, N'Renter')
SET IDENTITY_INSERT [dbo].[Roles] OFF
INSERT [dbo].[Users] ([ID], [Username], [Password], [RoleID]) VALUES (N'1', N'dinhphu', N'123', 2)
INSERT [dbo].[Users] ([ID], [Username], [Password], [RoleID]) VALUES (N'2', N'cachua', N'123', 3)
INSERT [dbo].[Hosts] ([ID], [FullName], [Address], [Phone], [Mail], [NumOfRoom], [Description], [isActive]) VALUES (N'1', N'Phu Nguyen Dinh', N'Binh Duong', N'12212121', N'dinhphu@gmail.com', 12, NULL, 1)
INSERT [dbo].[Rooms] ([ID], [Name], [Square], [Price], [Description], [isAvailable], [HostID]) VALUES (N'Room 1', N'1', 12, 1E+07, NULL, 1, N'1')
INSERT [dbo].[Renters] ([ID], [FullName], [StartDate], [EndDate], [BirthDate], [Mail], [HomeTown], [Phone], [Description], [RoomID]) VALUES (N'2', N'Ca Chua Nguyen Thi', CAST(N'2018-09-28' AS Date), NULL, NULL, N'cachua@gmail.com', N'Ha Noi', N'212121', NULL, N'Room 1')
