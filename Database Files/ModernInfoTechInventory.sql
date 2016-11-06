USE [ModernInfoTechInventory]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[LocationId] [nvarchar](128) NOT NULL,
	[LocationName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Expense]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expense](
	[ExpenseId] [nvarchar](128) NOT NULL,
	[ExpenseDate] [datetime] NOT NULL,
	[Purpose] [nvarchar](max) NOT NULL,
	[Amount] [float] NOT NULL,
	[ExpensedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED 
(
	[ExpenseId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [nvarchar](128) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cash]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cash](
	[CashId] [nvarchar](128) NOT NULL,
	[Amount] [float] NOT NULL,
 CONSTRAINT [PK_Cash] PRIMARY KEY CLUSTERED 
(
	[CashId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vat]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vat](
	[VatId] [nvarchar](128) NOT NULL,
	[VatAmount] [float] NOT NULL,
	[VatArea] [nvarchar](max) NOT NULL,
	[VatRegistrationNumber] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Vat] PRIMARY KEY CLUSTERED 
(
	[VatId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [nvarchar](128) NOT NULL,
	[SupplierName] [nvarchar](max) NOT NULL,
	[LocationId] [nvarchar](128) NOT NULL,
	[PhoneNumber] [nvarchar](14) NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[SubCategoryId] [nvarchar](128) NOT NULL,
	[SubCategoryName] [nvarchar](max) NOT NULL,
	[CategoryId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_SubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[UserId] [nvarchar](128) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[User_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [nvarchar](128) NOT NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](14) NOT NULL,
	[LocationId] [nvarchar](128) NOT NULL,
	[CurrentDue] [float] NOT NULL,
	[DueLimit] [float] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyInfo]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyInfo](
	[CompanyId] [nvarchar](128) NOT NULL,
	[CompanyName] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](14) NOT NULL,
	[LocationId] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_CompanyInfo] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Investor]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Investor](
	[InvestorId] [nvarchar](128) NOT NULL,
	[LocationId] [nvarchar](128) NOT NULL,
	[PhoneNumber] [nvarchar](14) NOT NULL,
	[Balance] [float] NOT NULL,
 CONSTRAINT [PK_Investor] PRIMARY KEY CLUSTERED 
(
	[InvestorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [nvarchar](128) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[Barcode] [nvarchar](max) NOT NULL,
	[CostPrice] [float] NOT NULL,
	[SalePrice] [float] NOT NULL,
	[ReorderLevel] [int] NOT NULL,
	[SubCategoryId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvestorTransaction]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvestorTransaction](
	[InvestorTransactionId] [nvarchar](128) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Amount] [float] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[TransactionType] [bit] NOT NULL,
 CONSTRAINT [PK_InvestorTransaction] PRIMARY KEY CLUSTERED 
(
	[InvestorTransactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerDue]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerDue](
	[CustomerDueId] [nvarchar](128) NOT NULL,
	[ReceiveAmount] [float] NOT NULL,
 CONSTRAINT [PK_CustomerDue] PRIMARY KEY CLUSTERED 
(
	[CustomerDueId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Investment]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Investment](
	[InvestmentId] [nvarchar](128) NOT NULL,
	[Amount] [float] NOT NULL,
 CONSTRAINT [PK_Investment] PRIMARY KEY CLUSTERED 
(
	[InvestmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierPayment]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierPayment](
	[SupplierPaymentId] [nvarchar](128) NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[PaidAmount] [float] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_SupplierPayment] PRIMARY KEY CLUSTERED 
(
	[SupplierPaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductQuantity]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductQuantity](
	[ProductQuantityId] [nvarchar](128) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_ProductQuantity] PRIMARY KEY CLUSTERED 
(
	[ProductQuantityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillEntry]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillEntry](
	[BillEntryId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL,
	[CustomerId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_BillEntry] PRIMARY KEY CLUSTERED 
(
	[BillEntryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[InventoryId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[ReceiveNumber] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DamageStockEntry]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DamageStockEntry](
	[DamageStockEntryId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL,
	[Remark] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DamageStockEntry] PRIMARY KEY CLUSTERED 
(
	[DamageStockEntryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseEntry]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseEntry](
	[PurchaseEntryId] [nvarchar](128) NOT NULL,
	[SupplierId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[ReceiveNumber] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PurchaseEntry] PRIMARY KEY CLUSTERED 
(
	[PurchaseEntryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockAdjustment]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockAdjustment](
	[StockAdjustmentId] [nvarchar](128) NOT NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[ReceiveNumber] [nvarchar](max) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_StockAdjustment] PRIMARY KEY CLUSTERED 
(
	[StockAdjustmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleReturnQuantity]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleReturnQuantity](
	[SaleReturnQuantityId] [nvarchar](128) NOT NULL,
	[ReturnQuantity] [int] NOT NULL,
 CONSTRAINT [PK_SaleReturnQuantity] PRIMARY KEY CLUSTERED 
(
	[SaleReturnQuantityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseReturn]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseReturn](
	[PurchaseReturnId] [nvarchar](128) NOT NULL,
	[ReturnDate] [datetime] NOT NULL,
	[ReturnNumber] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PurchaseReturn] PRIMARY KEY CLUSTERED 
(
	[PurchaseReturnId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceInfo]    Script Date: 11/06/2016 21:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceInfo](
	[InvoiceInfoId] [nvarchar](128) NOT NULL,
	[BillEntryId] [nvarchar](128) NULL,
	[PurchaseEntryId] [nvarchar](128) NULL,
 CONSTRAINT [PK_InvoiceInfo] PRIMARY KEY CLUSTERED 
(
	[InvoiceInfoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_BillEntry_Customer]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[BillEntry]  WITH CHECK ADD  CONSTRAINT [FK_BillEntry_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[BillEntry] CHECK CONSTRAINT [FK_BillEntry_Customer]
GO
/****** Object:  ForeignKey [FK_BillEntry_ProductQuantity]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[BillEntry]  WITH CHECK ADD  CONSTRAINT [FK_BillEntry_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[BillEntry] CHECK CONSTRAINT [FK_BillEntry_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_CompanyInfo_AspNetUsers]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[CompanyInfo]  WITH CHECK ADD  CONSTRAINT [FK_CompanyInfo_AspNetUsers] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[CompanyInfo] CHECK CONSTRAINT [FK_CompanyInfo_AspNetUsers]
GO
/****** Object:  ForeignKey [FK_CompanyInfo_Location]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[CompanyInfo]  WITH CHECK ADD  CONSTRAINT [FK_CompanyInfo_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[CompanyInfo] CHECK CONSTRAINT [FK_CompanyInfo_Location]
GO
/****** Object:  ForeignKey [FK_Customer_Location]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Location]
GO
/****** Object:  ForeignKey [FK_CustomerDue_Customer]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[CustomerDue]  WITH CHECK ADD  CONSTRAINT [FK_CustomerDue_Customer] FOREIGN KEY([CustomerDueId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerDue] CHECK CONSTRAINT [FK_CustomerDue_Customer]
GO
/****** Object:  ForeignKey [FK_DamageStockEntry_ProductQuantity]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[DamageStockEntry]  WITH CHECK ADD  CONSTRAINT [FK_DamageStockEntry_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[DamageStockEntry] CHECK CONSTRAINT [FK_DamageStockEntry_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_Inventory_ProductQuantity]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_Investment_CompanyInfo]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[Investment]  WITH CHECK ADD  CONSTRAINT [FK_Investment_CompanyInfo] FOREIGN KEY([InvestmentId])
REFERENCES [dbo].[CompanyInfo] ([CompanyId])
GO
ALTER TABLE [dbo].[Investment] CHECK CONSTRAINT [FK_Investment_CompanyInfo]
GO
/****** Object:  ForeignKey [FK_Investor_Location]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[Investor]  WITH CHECK ADD  CONSTRAINT [FK_Investor_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Investor] CHECK CONSTRAINT [FK_Investor_Location]
GO
/****** Object:  ForeignKey [FK_InvestorTransaction_Investor1]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[InvestorTransaction]  WITH CHECK ADD  CONSTRAINT [FK_InvestorTransaction_Investor1] FOREIGN KEY([InvestorTransactionId])
REFERENCES [dbo].[Investor] ([InvestorId])
GO
ALTER TABLE [dbo].[InvestorTransaction] CHECK CONSTRAINT [FK_InvestorTransaction_Investor1]
GO
/****** Object:  ForeignKey [FK_InvoiceInfo_BillEntry]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[InvoiceInfo]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceInfo_BillEntry] FOREIGN KEY([BillEntryId])
REFERENCES [dbo].[BillEntry] ([BillEntryId])
GO
ALTER TABLE [dbo].[InvoiceInfo] CHECK CONSTRAINT [FK_InvoiceInfo_BillEntry]
GO
/****** Object:  ForeignKey [FK_InvoiceInfo_PurchaseEntry]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[InvoiceInfo]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceInfo_PurchaseEntry] FOREIGN KEY([PurchaseEntryId])
REFERENCES [dbo].[PurchaseEntry] ([PurchaseEntryId])
GO
ALTER TABLE [dbo].[InvoiceInfo] CHECK CONSTRAINT [FK_InvoiceInfo_PurchaseEntry]
GO
/****** Object:  ForeignKey [FK_Product_SubCategory]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_SubCategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[SubCategory] ([SubCategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_SubCategory]
GO
/****** Object:  ForeignKey [FK_ProductQuantity_Product]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[ProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_ProductQuantity_Product] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductQuantity] CHECK CONSTRAINT [FK_ProductQuantity_Product]
GO
/****** Object:  ForeignKey [FK_PurchaseEntry_ProductQuantity]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[PurchaseEntry]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseEntry_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[PurchaseEntry] CHECK CONSTRAINT [FK_PurchaseEntry_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_PurchaseEntry_Supplier]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[PurchaseEntry]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseEntry_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[PurchaseEntry] CHECK CONSTRAINT [FK_PurchaseEntry_Supplier]
GO
/****** Object:  ForeignKey [FK_PurchaseReturn_PurchaseEntry]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[PurchaseReturn]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseReturn_PurchaseEntry] FOREIGN KEY([PurchaseReturnId])
REFERENCES [dbo].[PurchaseEntry] ([PurchaseEntryId])
GO
ALTER TABLE [dbo].[PurchaseReturn] CHECK CONSTRAINT [FK_PurchaseReturn_PurchaseEntry]
GO
/****** Object:  ForeignKey [FK_SaleReturnQuantity_BillEntry]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[SaleReturnQuantity]  WITH CHECK ADD  CONSTRAINT [FK_SaleReturnQuantity_BillEntry] FOREIGN KEY([SaleReturnQuantityId])
REFERENCES [dbo].[BillEntry] ([BillEntryId])
GO
ALTER TABLE [dbo].[SaleReturnQuantity] CHECK CONSTRAINT [FK_SaleReturnQuantity_BillEntry]
GO
/****** Object:  ForeignKey [FK_StockAdjustment_ProductQuantity]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[StockAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_StockAdjustment_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[StockAdjustment] CHECK CONSTRAINT [FK_StockAdjustment_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_SubCategory_Category]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Category]
GO
/****** Object:  ForeignKey [FK_Supplier_Location]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Location]
GO
/****** Object:  ForeignKey [FK_SupplierPayment_Supplier]    Script Date: 11/06/2016 21:07:08 ******/
ALTER TABLE [dbo].[SupplierPayment]  WITH CHECK ADD  CONSTRAINT [FK_SupplierPayment_Supplier] FOREIGN KEY([SupplierPaymentId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[SupplierPayment] CHECK CONSTRAINT [FK_SupplierPayment_Supplier]
GO
