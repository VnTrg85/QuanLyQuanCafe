USE [master]
GO
/****** Object:  Database [QuanLyQuanCafe]    Script Date: 12/24/2022 8:56:12 PM ******/
CREATE DATABASE [QuanLyQuanCafe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyQuanCafe', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\QuanLyQuanCafe.mdf' , SIZE = 8384KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QuanLyQuanCafe_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\QuanLyQuanCafe_log.ldf' , SIZE = 70976KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QuanLyQuanCafe] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyQuanCafe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyQuanCafe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyQuanCafe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DELAYED_DURABILITY = DISABLED 
GO
USE [QuanLyQuanCafe]
GO
/****** Object:  UserDefinedFunction [dbo].[ConvertToUnsign]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ConvertToUnsign] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[DisplayedName] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[PassWord] [nvarchar](1000) NOT NULL CONSTRAINT [Default_Pass]  DEFAULT ('20720532132149213101239102231223249249135100218'),
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bill]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[timeCheckin] [date] NOT NULL,
	[timeCheckout] [date] NULL,
	[idTable] [int] NULL,
	[status] [int] NULL DEFAULT ((0)),
	[disCount] [float] NULL DEFAULT ((0)),
	[totalPrice] [float] NULL DEFAULT ((0)),
	[finalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idBill] [int] NULL,
	[idFood] [int] NULL,
	[count] [int] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Food]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[idCategory] [int] NULL,
	[price] [float] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FoodCategory]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TableFood]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableFood](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[status] [int] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Account] ([DisplayedName], [UserName], [PassWord], [Type]) VALUES (N'b', N'nv', N'20720532132149213101239102231223249249135100218', 1)
INSERT [dbo].[Account] ([DisplayedName], [UserName], [PassWord], [Type]) VALUES (N'abc', N'tr', N'3244185981728979115075721453575112', 1)
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([id], [timeCheckin], [timeCheckout], [idTable], [status], [disCount], [totalPrice], [finalPrice]) VALUES (63, CAST(N'2022-12-24' AS Date), CAST(N'2022-12-24' AS Date), 168659, 1, 0, 25000, 25000)
INSERT [dbo].[Bill] ([id], [timeCheckin], [timeCheckout], [idTable], [status], [disCount], [totalPrice], [finalPrice]) VALUES (64, CAST(N'2022-12-24' AS Date), CAST(N'2022-12-24' AS Date), 168662, 1, 0, 30000, 30000)
INSERT [dbo].[Bill] ([id], [timeCheckin], [timeCheckout], [idTable], [status], [disCount], [totalPrice], [finalPrice]) VALUES (65, CAST(N'2022-12-24' AS Date), CAST(N'2022-12-24' AS Date), 168664, 1, 0, 100000, 100000)
INSERT [dbo].[Bill] ([id], [timeCheckin], [timeCheckout], [idTable], [status], [disCount], [totalPrice], [finalPrice]) VALUES (66, CAST(N'2022-12-24' AS Date), CAST(N'2022-12-24' AS Date), 168659, 1, 0, 35000, 35000)
INSERT [dbo].[Bill] ([id], [timeCheckin], [timeCheckout], [idTable], [status], [disCount], [totalPrice], [finalPrice]) VALUES (67, CAST(N'2022-12-24' AS Date), CAST(N'2022-12-24' AS Date), 168659, 1, 0, 35000, 35000)
INSERT [dbo].[Bill] ([id], [timeCheckin], [timeCheckout], [idTable], [status], [disCount], [totalPrice], [finalPrice]) VALUES (68, CAST(N'2022-12-24' AS Date), CAST(N'2022-12-24' AS Date), 168657, 1, 3500, 35000, 31500)
SET IDENTITY_INSERT [dbo].[Bill] OFF
SET IDENTITY_INSERT [dbo].[BillInfo] ON 

INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (77, 63, 24, 1)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (78, 64, 25, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (79, 65, 25, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (80, 65, 30, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (81, 66, 26, 1)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (82, 67, 30, 1)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (83, 68, 30, 1)
SET IDENTITY_INSERT [dbo].[BillInfo] OFF
SET IDENTITY_INSERT [dbo].[Food] ON 

INSERT [dbo].[Food] ([id], [Name], [idCategory], [price]) VALUES (24, N'Late', 1, 25000)
INSERT [dbo].[Food] ([id], [Name], [idCategory], [price]) VALUES (25, N'Capuchino', 1, 15000)
INSERT [dbo].[Food] ([id], [Name], [idCategory], [price]) VALUES (26, N'Flower Tea', 2, 35000)
INSERT [dbo].[Food] ([id], [Name], [idCategory], [price]) VALUES (30, N'Chicken Bread', 4, 35000)
SET IDENTITY_INSERT [dbo].[Food] OFF
SET IDENTITY_INSERT [dbo].[FoodCategory] ON 

INSERT [dbo].[FoodCategory] ([id], [Name]) VALUES (1, N'Cafe VN')
INSERT [dbo].[FoodCategory] ([id], [Name]) VALUES (2, N'Tea')
INSERT [dbo].[FoodCategory] ([id], [Name]) VALUES (3, N'Cakes ')
INSERT [dbo].[FoodCategory] ([id], [Name]) VALUES (4, N'Bread')
INSERT [dbo].[FoodCategory] ([id], [Name]) VALUES (5, N'Freeze')
INSERT [dbo].[FoodCategory] ([id], [Name]) VALUES (15, N'Other drink')
SET IDENTITY_INSERT [dbo].[FoodCategory] OFF
SET IDENTITY_INSERT [dbo].[TableFood] ON 

INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (168657, N'Ban 1', 0)
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (168658, N'Ban 2', 0)
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (168659, N'Ban 3', 0)
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (168660, N'Ban 4', 0)
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (168661, N'Ban 5', 0)
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (168662, N'Ban 6', 0)
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (168663, N'Ban 7', 0)
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (168664, N'Ban 8', 0)
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (168665, N'Ban 9', 0)
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (168666, N'Ban 10', 0)
SET IDENTITY_INSERT [dbo].[TableFood] OFF
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([idTable])
REFERENCES [dbo].[TableFood] ([id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idBill])
REFERENCES [dbo].[Bill] ([id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idFood])
REFERENCES [dbo].[Food] ([id])
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD FOREIGN KEY([idCategory])
REFERENCES [dbo].[FoodCategory] ([id])
GO
/****** Object:  StoredProcedure [dbo].[USP_CheckDeleteCate]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CheckDeleteCate]
@id varchar(6)
as
begin
	select count(*) from Food where idCategory = @id
end
GO
/****** Object:  StoredProcedure [dbo].[USP_CheckDupUserName]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CheckDupUserName]
@userName varchar(100)
as
begin
	select count(*)  from Account where UserName = @userName
end
GO
/****** Object:  StoredProcedure [dbo].[USP_DeleteFood]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[USP_DeleteFood]
@idFood int
as 
begin
Delete from BillInfo where idFood = @idFood
Delete From Food where id = @idFood
end

GO
/****** Object:  StoredProcedure [dbo].[USP_getAllBill]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[USP_getAllBill]
@timeCheckin date , @timeCheckout date 
as
begin
	select  t.name, B.id AS IDBILL,timeCheckin,timeCheckout,B.disCount,b.totalPrice,b.finalPrice from Bill as B , TableFood as T
	where B.status=1 AND timeCheckin >= @timeCheckin and timeCheckout <= @timeCheckout and B.idTable = t.id
end

GO
/****** Object:  StoredProcedure [dbo].[USP_GetListBillByDateAndPage]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetListBillByDateAndPage]
@time_Checkin Date , @time_Checkout Date , @PageNum int
as
begin
	Declare @default_Row int = 10
	Declare @Select_Row int = @PageNum * @default_Row
	Declare @Except_Row int = (@PageNum-1)*@default_Row
	;With BillTemp as (
	select
	b.id , b.timeCheckin as [Time Checkin] , b.timeCheckout as [Time Checkout] , b.disCount as [Discount] , b.totalPrice as[Total Price] , b.finalPrice as [Final Price]
	from Bill as b , TableFood as t
	where b.idTable = t.id and b.timeCheckin >= @time_Checkin and b.timeCheckout <= @time_Checkout and b.status =1
	)
	select TOP (@Select_Row) * from BillTemp
	except 
	select TOP (@Except_Row) * from BillTemp
end

GO
/****** Object:  StoredProcedure [dbo].[USP_GetNumBill]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetNumBill]
@time_Checkin Date , @time_Checkout Date 
as
begin
	
	select count(*)
	from Bill as b , TableFood as t
	where b.idTable = t.id and b.timeCheckin >= @time_Checkin and b.timeCheckout <= @time_Checkout and b.status =1
end

GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBill]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBill]
@idTable int
as 
begin
Insert into Bill
values(Getdate(),null,@idTable,0,0,0,0)
end

GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBillInfo]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBillInfo]
@idBill int , @idFood int , @count int
as 
begin
	declare @existFood int--Dem so food ton tai
	declare @existFoodCount int --Dem so luong food ton tai do
	select @existFood = idBill , @existFoodCount = count+@count from BillInfo B where B.idBill = @idBill AND B.idFood = @idFood
	if(@existFood > 0)
	begin		
		if(@existFoodCount < 0)
		begin
		delete from BillInfo where idBill = @idBill and idFood= @idFood
		end
		else
		UPDATE BillInfo 
		set count=@existFoodCount where idBill = @idBill and idFood = @idFood
	end
	else 
	begin
		if(@count>0)
		begin
		insert into BillInfo
		values(@idBill,@idFood,@count)
		end
	end
end

GO
/****** Object:  StoredProcedure [dbo].[USP_Numbills]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[USP_Numbills]
@time_Checkin Date , @time_Checkout Date 
as
begin
	select count(*)
	from Bill as b , TableFood as t
	where b.idTable = t.id and b.timeCheckin >= @time_Checkin and b.timeCheckout <= @time_Checkout and b.status =1
end

GO
/****** Object:  StoredProcedure [dbo].[USP_SwitchTable]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SwitchTable]
@idTable1 int, @idTable2 int
as
begin
declare @idBillFirst int
declare @idBillSecond int
select @idBillFirst = id from Bill where idTable =@idTable1 and status = 0
select @idBillSecond = id from Bill  where idTable = @idTable2 and status =0

if(@idBillFirst is null and @idBillSecond is not null)
begin
update Bill set idTable = @idTable1 where idTable = @idTable2
update TableFood set status = 1 where id = @idTable1 
update TableFood set status = 0 where id = @idTable2 
end
else if(@idBillFirst is not null and @idBillSecond is null)
begin
update Bill set idTable = @idTable2 where idTable= @idTable1
update TableFood set status = 1 where id = @idTable2 
update TableFood set status = 0 where id = @idTable1
end
else
begin
declare @idBilltemp int
select  @idBilltemp = id from Bill where idTable =@idTable2 
update Bill set idTable = @idTable2 where idTable = @idTable1
update Bill  set idTable = @idTable1 where id = @idBilltemp
end
end

GO
/****** Object:  StoredProcedure [dbo].[USP_updateAcocunt]    Script Date: 12/24/2022 8:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_updateAcocunt]
@userName nvarchar(100), @displayedName nvarchar(100), @passWord nvarchar(100) , @newPass nvarchar(100)
as
begin
declare @check int =0
select @check=count(*) from Account where UserName = @userName and PassWord = @passWord
if(@check  <> 0 and @check is not null and @newPass <> '')
			begin
				update Account set DisplayedName = @displayedName where UserName =@userName
				update Account set PassWord = @newPass where UserName =@userName
			end
else if(@check  <> 0 and @check is not null and @newPass ='')
			begin
				update Account set DisplayedName = @displayedName where UserName =@userName
			end
end
GO
USE [master]
GO
ALTER DATABASE [QuanLyQuanCafe] SET  READ_WRITE 
GO
