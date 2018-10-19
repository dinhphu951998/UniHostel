USE [UniHostelDB]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'Host')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (3, N'Renter')
SET IDENTITY_INSERT [dbo].[Roles] OFF
INSERT [dbo].[Users] ([ID], [Username], [Password], [isActive], [RoleID]) VALUES (N'1', N'dinhphu', N'123', 1, 2)
INSERT [dbo].[Users] ([ID], [Username], [Password], [isActive], [RoleID]) VALUES (N'2', N'cachua', N'123', 1, 3)
INSERT [dbo].[Users] ([ID], [Username], [Password], [isActive], [RoleID]) VALUES (N'3', N'admin', N'123', 1, 1)
INSERT [dbo].[Hosts] ([ID], [FullName], [Address], [Phone], [Mail], [NumOfRoom], [Description], [isActive]) VALUES (N'1', N'Phu Nguyen Dinh', N'Binh Duong', N'12212121', N'dinhphu@gmail.com', 12, NULL, 1)
INSERT [dbo].[Rooms] ([ID], [Name], [image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'Room 1', N'1', NULL, 12, 1E+07, NULL, 0, 1, N'1')
INSERT [dbo].[Renters] ([ID], [FullName], [StartDate], [EndDate], [BirthDate], [Mail], [HomeTown], [Phone], [Description], [RoomID]) VALUES (N'2', N'Ca Chua Nguyen Thi', CAST(N'2018-09-28 00:00:00.000' AS DateTime), NULL, NULL, N'cachua@gmail.com', N'Ha Noi', N'212121', NULL, N'Room 1')
INSERT [dbo].[CompulsoryServices] ([ID], [Name], [Price], [Unit], [Description], [isActive], [HostID]) VALUES (N'201810162134469962', N'Điện', 2500, N'KWh', NULL, 1, N'1')
INSERT [dbo].[CompulsoryServices] ([ID], [Name], [Price], [Unit], [Description], [isActive], [HostID]) VALUES (N'201810162135048355', N'Nước', 13000, N'Khối (m3)', NULL, 1, N'1')
INSERT [dbo].[AdvancedServices] ([ID], [Name], [Price], [Unit], [Description], [isActive], [HostID]) VALUES (N'201810162135442138', N'Wifi', 50000, N'Phòng', NULL, 1, N'1')
INSERT [dbo].[AdvancedServices] ([ID], [Name], [Price], [Unit], [Description], [isActive], [HostID]) VALUES (N'201810162135566053', N'Gửi xe', 60000, N'Chiếc', NULL, 1, N'1')
