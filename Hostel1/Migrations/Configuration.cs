namespace Hostel1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Hostel1.Models.RoomDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Hostel1.Models.RoomDBContext";
        }

        protected override void Seed(Hostel1.Models.RoomDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //  context.Rooms.AddOrUpdate(x => x.Name, new Models.Room { Name = "Tom and Jerry", Square = 1, Price = 10, Description = "a", IsAvailable = true});
            context.Rooms.AddOrUpdate(new Models.Rooms
            {

                Name = "PHÒNG TRỌ CAO CẤP QUẬN GÒ VẤP",
                Description = "Nội thất sang trọng, phòng sạch sẽ, mới 100% Toilet riêng từng phòng",
                IsAvailable = true,
                Price = 7300000,
                Square = 40,
                Image = "/asset/imgRoommanager/room1.jpg"
            });
            //context.Rooms.AddOrUpdate(new Models.Rooms
            //{
            //    Name = "PHÒNG TRỌ CAO CẤP ĐƯỜNG NGUYỄN HUỆ",
            //    Description = "Phòng riêng lối đi riêng biệt có gác nhà vệ sinh riêng,bếp, khu vực trung tâm thành phố",
            //    IsAvailable = true,
            //    Price = 9300000,
            //    Square = 50,
            //    Image = "/asset/imgRoommanager/room2.jpg"
            //});
            //context.Rooms.AddOrUpdate(new Models.Rooms
            //{
            //    Name = "PHÒNG TRỌ CAO CẤP PHAN VĂN TRỊ",
            //    Description = "Phòng đầy đủ tiện nghi: Phòng mới sạch sẽ, có cửa sổ. Có ban công, điều hòa, tủ lạnh, máy giặt, nước nóng, wifi.",
            //    IsAvailable = true,
            //    Price = 6500000,
            //    Square = 35,
            //    Image = "/asset/imgRoommanager/room3.jpg"
            //});
            //context.Rooms.AddOrUpdate(new Models.Rooms
            //{
            //    Name = "PHÒNG TRỌ CAO CẤP PHAN VĂN TRỊ",
            //    Description = "Phòng cực đẹp, cực rộng, full nội thất. Có thể ở từ 5-7 người.",
            //    IsAvailable = true,
            //    Price = 10000000,
            //    Square = 70,
            //    Image = "/asset/imgRoommanager/room4.jpg"
            //});
            //context.Rooms.AddOrUpdate(new Models.Rooms
            //{
            //    Name = "PHÒNG TRỌ CAO CẤP QUẬN LÊ ĐỨC THỌ",
            //    Description = "Phòng thiết kế cực kỳ cao cấp sang chảnh với sàn nhà lát gạch sáng bóng sạch sẽ cùng các trang thiết bị nội thất thông minh cực kỳ thú vị. ",
            //    IsAvailable = true,
            //    Price = 11000000,
            //    Square = 75,
            //    Image = "/asset/imgRoommanager/room5.jpg"
            //});
            //context.Rooms.AddOrUpdate(new Models.Rooms
            //{
            //    Name = "PHÒNG TRỌ CAO CẤP QUẬN GÒ VẤP",
            //    Description = "Tiện đi qua bình thạnh, phú nhuận ở được 4-6 người phù hợp với các bạn sinh viên hoặc gia đình Wifi, cáp quang mạnh Giờ giấc thoài mái. ",
            //    IsAvailable = true,
            //    Price = 5000000,
            //    Square = 30,
            //    Image = "/asset/imgRoommanager/room6.jpg"
            //});
            //context.Rooms.AddOrUpdate(new Models.Rooms
            //{
            //    Name = "PHÒNG TRỌ CAO CẤP QUẬN GÒ VẤP",
            //    Description = "Căn hộ mới xây còn zin, đầu tháng 9 sẽ ở được. Với diện tích 50 m², 1 phòng ngủ và 1 phòng khách riêng biệt, phòng thiết kế đa dạng ",
            //    IsAvailable = true,
            //    Price = 5000000,
            //    Square = 30,
            //    Image = "/asset/imgRoommanager/room7.jpg"
            //});
            //context.Rooms.AddOrUpdate(new Models.Rooms
            //{
            //    Name = "CĂN HỘ CHUNG CƯ TROPIC GARDEN",
            //    Description = "Căn hộ gồm 3 phòng ngủ, 2 nhà vệ sinh, 1 nhà bếp, 1 phòng khách và 1 sân phơi, ban công cửa sổ hướng tòa nhà LANMARK",
            //    IsAvailable = true,
            //    Price = 29000000,
            //    Square = 90,
            //    Image = "/asset/imgRoommanager/room6.jpg"
            //});
            //context.Rooms.AddOrUpdate(new Models.Rooms
            //{
            //    Name = "CĂN HỘ CHUNG CƯ QUẬN PHÚ NHUẬN",
            //    Description = "Tòa nhà gồm 1 trệt và 4 lầu, có 10 căn hộ cho thuê. Thuận tiện di chuyển ra các quận trung tâm Q.1,",
            //    IsAvailable = true,
            //    Price = 9000000,
            //    Square = 70,
            //    Image = "/asset/imgRoommanager/room8.jpg"
            //});
        }
    }
}
