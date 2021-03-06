USE [ModernInfoTechInventory]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12/29/2016 17:29:38 ******/
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
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 12/29/2016 17:29:38 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12/29/2016 17:29:38 ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12/29/2016 17:29:38 ******/
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
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12/29/2016 17:29:38 ******/
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
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12/29/2016 17:29:38 ******/
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
CREATE NONCLUSTERED INDEX [IX_User_Id] ON [dbo].[AspNetUserClaims] 
(
	[User_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tenant]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenant](
	[TenantId] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ActivationDate] [datetime] NOT NULL,
	[InactivationDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[UnitId] [nvarchar](128) NOT NULL,
	[UnitName] [nvarchar](max) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[LocationId] [nvarchar](128) NOT NULL,
	[LocationName] [nvarchar](max) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceInfo]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceInfo](
	[InvoiceInfoId] [nvarchar](128) NOT NULL,
	[EntryId] [nvarchar](128) NOT NULL,
	[EntryType] [int] NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_InvoiceInfo] PRIMARY KEY CLUSTERED 
(
	[InvoiceInfoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Investment]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Investment](
	[InvestmentId] [nvarchar](128) NOT NULL,
	[Amount] [float] NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Investment] PRIMARY KEY CLUSTERED 
(
	[InvestmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expense]    Script Date: 12/29/2016 17:29:38 ******/
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
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED 
(
	[ExpenseId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [nvarchar](128) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cash]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cash](
	[CashId] [nvarchar](128) NOT NULL,
	[Amount] [float] NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Cash] PRIMARY KEY CLUSTERED 
(
	[CashId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requisition]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requisition](
	[RequisitionId] [nvarchar](128) NOT NULL,
	[RequisitionDate] [datetime] NOT NULL,
	[IsApproved] [bit] NULL,
	[Description] [nvarchar](max) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Requisition] PRIMARY KEY CLUSTERED 
(
	[RequisitionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseReturn]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseReturn](
	[PurchaseReturnId] [nvarchar](128) NOT NULL,
	[RefInvoiceId] [nvarchar](128) NOT NULL,
	[ReturnDate] [datetime] NOT NULL,
	[Penalty] [float] NULL,
	[PaidAmount] [float] NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_PurchaseReturn] PRIMARY KEY CLUSTERED 
(
	[PurchaseReturnId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockAdjustment]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockAdjustment](
	[StockAdjustmentId] [nvarchar](128) NOT NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[ReceiveNumber] [nvarchar](max) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_StockAdjustment] PRIMARY KEY CLUSTERED 
(
	[StockAdjustmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleReturn]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleReturn](
	[SaleReturnId] [nvarchar](128) NOT NULL,
	[RefInvoiceId] [nvarchar](128) NOT NULL,
	[Penalty] [float] NULL,
	[PaidAmount] [float] NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_SaleReturn] PRIMARY KEY CLUSTERED 
(
	[SaleReturnId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Investor]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Investor](
	[InvestorId] [nvarchar](128) NOT NULL,
	[InvestorName] [nvarchar](max) NOT NULL,
	[LocationId] [nvarchar](128) NOT NULL,
	[PhoneNumber] [nvarchar](14) NOT NULL,
	[Balance] [float] NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Investor] PRIMARY KEY CLUSTERED 
(
	[InvestorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [nvarchar](128) NOT NULL,
	[SupplierName] [nvarchar](max) NOT NULL,
	[LocationId] [nvarchar](128) NOT NULL,
	[PhoneNumber] [nvarchar](14) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[SubCategoryId] [nvarchar](128) NOT NULL,
	[SubCategoryName] [nvarchar](max) NOT NULL,
	[CategoryId] [nvarchar](128) NOT NULL,
	[UnitId] [nvarchar](128) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_SubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/29/2016 17:29:38 ******/
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
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyInfo]    Script Date: 12/29/2016 17:29:38 ******/
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
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_CompanyInfo] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryOrder]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryOrder](
	[DeliveryOrderId] [nvarchar](128) NOT NULL,
	[RequisitionId] [nvarchar](128) NOT NULL,
	[DeliveryOrderDate] [datetime] NOT NULL,
	[IsReceived] [bit] NULL,
	[Description] [nvarchar](max) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_DeliveryOrder] PRIMARY KEY CLUSTERED 
(
	[DeliveryOrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vat]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vat](
	[VatId] [nvarchar](128) NOT NULL,
	[VatAmount] [float] NOT NULL,
	[LocationId] [nvarchar](128) NOT NULL,
	[VatRegistrationNumber] [nvarchar](max) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Vat] PRIMARY KEY CLUSTERED 
(
	[VatId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierPayment]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierPayment](
	[SupplierPaymentId] [nvarchar](128) NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[PaidAmount] [float] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[SupplierId] [nvarchar](128) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_SupplierPayment] PRIMARY KEY CLUSTERED 
(
	[SupplierPaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvestorTransaction]    Script Date: 12/29/2016 17:29:38 ******/
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
	[InvestorId] [nvarchar](128) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_InvestorTransaction] PRIMARY KEY CLUSTERED 
(
	[InvestorTransactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerDue]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerDue](
	[CustomerDueId] [nvarchar](128) NOT NULL,
	[ReceiveAmount] [float] NOT NULL,
	[CustomerId] [nvarchar](128) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_CustomerDue] PRIMARY KEY CLUSTERED 
(
	[CustomerDueId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleEntry]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleEntry](
	[SaleEntryId] [nvarchar](128) NOT NULL,
	[CustomerId] [nvarchar](128) NOT NULL,
	[Discount] [float] NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_SaleEntry] PRIMARY KEY CLUSTERED 
(
	[SaleEntryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12/29/2016 17:29:38 ******/
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
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseEntry]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseEntry](
	[PurchaseEntryId] [nvarchar](128) NOT NULL,
	[SupplierId] [nvarchar](128) NOT NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[ReceiveNumber] [nvarchar](max) NOT NULL,
	[PaidAmount] [float] NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_PurchaseEntry] PRIMARY KEY CLUSTERED 
(
	[PurchaseEntryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductQuantity]    Script Date: 12/29/2016 17:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductQuantity](
	[ProductQuantityId] [nvarchar](128) NOT NULL,
	[ProductId] [nvarchar](128) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_ProductQuantity] PRIMARY KEY CLUSTERED 
(
	[ProductQuantityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[getInvestorTransactionReport]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getInvestorTransactionReport]
(
	@FromDate datetime,
	@ToDate datetime
)
as
select Amount, Description, TransactionType from ModernInfoTechInventory.dbo.InvestorTransaction
where TransactionDate between @FromDate and @ToDate
order by TransactionDate asc
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[InventoryId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[ReceiveNumber] [nvarchar](max) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryOrderProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryOrderProductQuantity](
	[DeliveryOrderId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DamageStockEntry]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DamageStockEntry](
	[DamageStockEntryId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL,
	[Remark] [nvarchar](max) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_DamageStockEntry] PRIMARY KEY CLUSTERED 
(
	[DamageStockEntryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductReturnQuantity]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductReturnQuantity](
	[ProductReturnQuantityId] [nvarchar](128) NOT NULL,
	[ReturnQuantity] [int] NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL,
	[TenantId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_ProductReturnQuantity] PRIMARY KEY CLUSTERED 
(
	[ProductReturnQuantityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequisitionProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequisitionProductQuantity](
	[RequisitionId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseEntryProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseEntryProductQuantity](
	[PurchaseEntryId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockAdjustmentProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockAdjustmentProductQuantity](
	[StockAdjustmentId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleEntryProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleEntryProductQuantity](
	[SaleEntryId] [nvarchar](128) NOT NULL,
	[ProductQuantityId] [nvarchar](128) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleReturnQuantity]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleReturnQuantity](
	[SaleReturnId] [nvarchar](128) NOT NULL,
	[ProductReturnQuantityId] [nvarchar](128) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseReturnQuantity]    Script Date: 12/29/2016 17:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseReturnQuantity](
	[PurchaseReturnId] [nvarchar](128) NOT NULL,
	[ProductReturnQuantityId] [nvarchar](128) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id]
GO
/****** Object:  ForeignKey [FK_Tenant_AspNetUsers]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Tenant]  WITH CHECK ADD  CONSTRAINT [FK_Tenant_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Tenant] CHECK CONSTRAINT [FK_Tenant_AspNetUsers]
GO
/****** Object:  ForeignKey [FK_Unit_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Unit]  WITH CHECK ADD  CONSTRAINT [FK_Unit_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Unit] CHECK CONSTRAINT [FK_Unit_Tenant]
GO
/****** Object:  ForeignKey [FK_Location_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Tenant]
GO
/****** Object:  ForeignKey [FK_InvoiceInfo_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[InvoiceInfo]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceInfo_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[InvoiceInfo] CHECK CONSTRAINT [FK_InvoiceInfo_Tenant]
GO
/****** Object:  ForeignKey [FK_Investment_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Investment]  WITH CHECK ADD  CONSTRAINT [FK_Investment_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Investment] CHECK CONSTRAINT [FK_Investment_Tenant]
GO
/****** Object:  ForeignKey [FK_Expense_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_Tenant]
GO
/****** Object:  ForeignKey [FK_Category_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Tenant]
GO
/****** Object:  ForeignKey [FK_Cash_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Cash]  WITH CHECK ADD  CONSTRAINT [FK_Cash_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Cash] CHECK CONSTRAINT [FK_Cash_Tenant]
GO
/****** Object:  ForeignKey [FK_Requisition_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Requisition]  WITH CHECK ADD  CONSTRAINT [FK_Requisition_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Requisition] CHECK CONSTRAINT [FK_Requisition_Tenant]
GO
/****** Object:  ForeignKey [FK_PurchaseReturn_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[PurchaseReturn]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseReturn_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[PurchaseReturn] CHECK CONSTRAINT [FK_PurchaseReturn_Tenant]
GO
/****** Object:  ForeignKey [FK_StockAdjustment_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[StockAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_StockAdjustment_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[StockAdjustment] CHECK CONSTRAINT [FK_StockAdjustment_Tenant]
GO
/****** Object:  ForeignKey [FK_SaleReturn_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[SaleReturn]  WITH CHECK ADD  CONSTRAINT [FK_SaleReturn_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[SaleReturn] CHECK CONSTRAINT [FK_SaleReturn_Tenant]
GO
/****** Object:  ForeignKey [FK_Investor_Location]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Investor]  WITH CHECK ADD  CONSTRAINT [FK_Investor_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Investor] CHECK CONSTRAINT [FK_Investor_Location]
GO
/****** Object:  ForeignKey [FK_Investor_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Investor]  WITH CHECK ADD  CONSTRAINT [FK_Investor_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Investor] CHECK CONSTRAINT [FK_Investor_Tenant]
GO
/****** Object:  ForeignKey [FK_Supplier_Location]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Location]
GO
/****** Object:  ForeignKey [FK_Supplier_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Tenant]
GO
/****** Object:  ForeignKey [FK_SubCategory_Category]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Category]
GO
/****** Object:  ForeignKey [FK_SubCategory_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Tenant]
GO
/****** Object:  ForeignKey [FK_SubCategory_Unit]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Unit]
GO
/****** Object:  ForeignKey [FK_Customer_Location]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Location]
GO
/****** Object:  ForeignKey [FK_Customer_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Tenant]
GO
/****** Object:  ForeignKey [FK_CompanyInfo_AspNetUsers]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[CompanyInfo]  WITH CHECK ADD  CONSTRAINT [FK_CompanyInfo_AspNetUsers] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[CompanyInfo] CHECK CONSTRAINT [FK_CompanyInfo_AspNetUsers]
GO
/****** Object:  ForeignKey [FK_CompanyInfo_Location]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[CompanyInfo]  WITH CHECK ADD  CONSTRAINT [FK_CompanyInfo_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[CompanyInfo] CHECK CONSTRAINT [FK_CompanyInfo_Location]
GO
/****** Object:  ForeignKey [FK_CompanyInfo_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[CompanyInfo]  WITH CHECK ADD  CONSTRAINT [FK_CompanyInfo_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[CompanyInfo] CHECK CONSTRAINT [FK_CompanyInfo_Tenant]
GO
/****** Object:  ForeignKey [FK_DeliveryOrder_Requisition]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[DeliveryOrder]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrder_Requisition] FOREIGN KEY([RequisitionId])
REFERENCES [dbo].[Requisition] ([RequisitionId])
GO
ALTER TABLE [dbo].[DeliveryOrder] CHECK CONSTRAINT [FK_DeliveryOrder_Requisition]
GO
/****** Object:  ForeignKey [FK_DeliveryOrder_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[DeliveryOrder]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrder_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[DeliveryOrder] CHECK CONSTRAINT [FK_DeliveryOrder_Tenant]
GO
/****** Object:  ForeignKey [FK_Vat_Location]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Vat]  WITH CHECK ADD  CONSTRAINT [FK_Vat_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Vat] CHECK CONSTRAINT [FK_Vat_Location]
GO
/****** Object:  ForeignKey [FK_Vat_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Vat]  WITH CHECK ADD  CONSTRAINT [FK_Vat_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Vat] CHECK CONSTRAINT [FK_Vat_Tenant]
GO
/****** Object:  ForeignKey [FK_SupplierPayment_Supplier]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[SupplierPayment]  WITH CHECK ADD  CONSTRAINT [FK_SupplierPayment_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[SupplierPayment] CHECK CONSTRAINT [FK_SupplierPayment_Supplier]
GO
/****** Object:  ForeignKey [FK_SupplierPayment_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[SupplierPayment]  WITH CHECK ADD  CONSTRAINT [FK_SupplierPayment_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[SupplierPayment] CHECK CONSTRAINT [FK_SupplierPayment_Tenant]
GO
/****** Object:  ForeignKey [FK_InvestorTransaction_Investor]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[InvestorTransaction]  WITH CHECK ADD  CONSTRAINT [FK_InvestorTransaction_Investor] FOREIGN KEY([InvestorId])
REFERENCES [dbo].[Investor] ([InvestorId])
GO
ALTER TABLE [dbo].[InvestorTransaction] CHECK CONSTRAINT [FK_InvestorTransaction_Investor]
GO
/****** Object:  ForeignKey [FK_InvestorTransaction_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[InvestorTransaction]  WITH CHECK ADD  CONSTRAINT [FK_InvestorTransaction_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[InvestorTransaction] CHECK CONSTRAINT [FK_InvestorTransaction_Tenant]
GO
/****** Object:  ForeignKey [FK_CustomerDue_Customer]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[CustomerDue]  WITH CHECK ADD  CONSTRAINT [FK_CustomerDue_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerDue] CHECK CONSTRAINT [FK_CustomerDue_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerDue_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[CustomerDue]  WITH CHECK ADD  CONSTRAINT [FK_CustomerDue_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[CustomerDue] CHECK CONSTRAINT [FK_CustomerDue_Tenant]
GO
/****** Object:  ForeignKey [FK_SaleEntry_Customer]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[SaleEntry]  WITH CHECK ADD  CONSTRAINT [FK_SaleEntry_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[SaleEntry] CHECK CONSTRAINT [FK_SaleEntry_Customer]
GO
/****** Object:  ForeignKey [FK_SaleEntry_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[SaleEntry]  WITH CHECK ADD  CONSTRAINT [FK_SaleEntry_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[SaleEntry] CHECK CONSTRAINT [FK_SaleEntry_Tenant]
GO
/****** Object:  ForeignKey [FK_Product_SubCategory]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_SubCategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[SubCategory] ([SubCategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_SubCategory]
GO
/****** Object:  ForeignKey [FK_Product_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Tenant]
GO
/****** Object:  ForeignKey [FK_PurchaseEntry_Supplier]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[PurchaseEntry]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseEntry_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[PurchaseEntry] CHECK CONSTRAINT [FK_PurchaseEntry_Supplier]
GO
/****** Object:  ForeignKey [FK_PurchaseEntry_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[PurchaseEntry]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseEntry_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[PurchaseEntry] CHECK CONSTRAINT [FK_PurchaseEntry_Tenant]
GO
/****** Object:  ForeignKey [FK_ProductQuantity_Product]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[ProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_ProductQuantity_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductQuantity] CHECK CONSTRAINT [FK_ProductQuantity_Product]
GO
/****** Object:  ForeignKey [FK_ProductQuantity_Tenant]    Script Date: 12/29/2016 17:29:38 ******/
ALTER TABLE [dbo].[ProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_ProductQuantity_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[ProductQuantity] CHECK CONSTRAINT [FK_ProductQuantity_Tenant]
GO
/****** Object:  ForeignKey [FK_Inventory_ProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_Inventory_Tenant]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Tenant]
GO
/****** Object:  ForeignKey [FK_DeliveryOrderProductQuantity_DeliveryOrder]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[DeliveryOrderProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrderProductQuantity_DeliveryOrder] FOREIGN KEY([DeliveryOrderId])
REFERENCES [dbo].[DeliveryOrder] ([DeliveryOrderId])
GO
ALTER TABLE [dbo].[DeliveryOrderProductQuantity] CHECK CONSTRAINT [FK_DeliveryOrderProductQuantity_DeliveryOrder]
GO
/****** Object:  ForeignKey [FK_DeliveryOrderProductQuantity_ProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[DeliveryOrderProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrderProductQuantity_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[DeliveryOrderProductQuantity] CHECK CONSTRAINT [FK_DeliveryOrderProductQuantity_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_DamageStockEntry_ProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[DamageStockEntry]  WITH CHECK ADD  CONSTRAINT [FK_DamageStockEntry_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[DamageStockEntry] CHECK CONSTRAINT [FK_DamageStockEntry_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_DamageStockEntry_Tenant]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[DamageStockEntry]  WITH CHECK ADD  CONSTRAINT [FK_DamageStockEntry_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[DamageStockEntry] CHECK CONSTRAINT [FK_DamageStockEntry_Tenant]
GO
/****** Object:  ForeignKey [FK_ProductReturnQuantity_ProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[ProductReturnQuantity]  WITH CHECK ADD  CONSTRAINT [FK_ProductReturnQuantity_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[ProductReturnQuantity] CHECK CONSTRAINT [FK_ProductReturnQuantity_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_ProductReturnQuantity_Tenant]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[ProductReturnQuantity]  WITH CHECK ADD  CONSTRAINT [FK_ProductReturnQuantity_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO
ALTER TABLE [dbo].[ProductReturnQuantity] CHECK CONSTRAINT [FK_ProductReturnQuantity_Tenant]
GO
/****** Object:  ForeignKey [FK_RequisitionProductQuantity_ProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[RequisitionProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_RequisitionProductQuantity_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[RequisitionProductQuantity] CHECK CONSTRAINT [FK_RequisitionProductQuantity_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_RequisitionProductQuantity_Requisition]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[RequisitionProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_RequisitionProductQuantity_Requisition] FOREIGN KEY([RequisitionId])
REFERENCES [dbo].[Requisition] ([RequisitionId])
GO
ALTER TABLE [dbo].[RequisitionProductQuantity] CHECK CONSTRAINT [FK_RequisitionProductQuantity_Requisition]
GO
/****** Object:  ForeignKey [FK_PurchaseEntryProductQuantity_ProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[PurchaseEntryProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseEntryProductQuantity_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[PurchaseEntryProductQuantity] CHECK CONSTRAINT [FK_PurchaseEntryProductQuantity_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_PurchaseEntryProductQuantity_PurchaseEntry]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[PurchaseEntryProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseEntryProductQuantity_PurchaseEntry] FOREIGN KEY([PurchaseEntryId])
REFERENCES [dbo].[PurchaseEntry] ([PurchaseEntryId])
GO
ALTER TABLE [dbo].[PurchaseEntryProductQuantity] CHECK CONSTRAINT [FK_PurchaseEntryProductQuantity_PurchaseEntry]
GO
/****** Object:  ForeignKey [FK_StockAdjustmentProductQuantity_ProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[StockAdjustmentProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_StockAdjustmentProductQuantity_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[StockAdjustmentProductQuantity] CHECK CONSTRAINT [FK_StockAdjustmentProductQuantity_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_StockAdjustmentProductQuantity_StockAdjustment]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[StockAdjustmentProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_StockAdjustmentProductQuantity_StockAdjustment] FOREIGN KEY([StockAdjustmentId])
REFERENCES [dbo].[StockAdjustment] ([StockAdjustmentId])
GO
ALTER TABLE [dbo].[StockAdjustmentProductQuantity] CHECK CONSTRAINT [FK_StockAdjustmentProductQuantity_StockAdjustment]
GO
/****** Object:  ForeignKey [FK_SaleEntryProductQuantity_ProductQuantity]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[SaleEntryProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_SaleEntryProductQuantity_ProductQuantity] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductQuantity] ([ProductQuantityId])
GO
ALTER TABLE [dbo].[SaleEntryProductQuantity] CHECK CONSTRAINT [FK_SaleEntryProductQuantity_ProductQuantity]
GO
/****** Object:  ForeignKey [FK_SaleEntryProductQuantity_SaleEntry]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[SaleEntryProductQuantity]  WITH CHECK ADD  CONSTRAINT [FK_SaleEntryProductQuantity_SaleEntry] FOREIGN KEY([SaleEntryId])
REFERENCES [dbo].[SaleEntry] ([SaleEntryId])
GO
ALTER TABLE [dbo].[SaleEntryProductQuantity] CHECK CONSTRAINT [FK_SaleEntryProductQuantity_SaleEntry]
GO
/****** Object:  ForeignKey [FK_SaleReturnQuantity_ProductReturnQuantity]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[SaleReturnQuantity]  WITH CHECK ADD  CONSTRAINT [FK_SaleReturnQuantity_ProductReturnQuantity] FOREIGN KEY([ProductReturnQuantityId])
REFERENCES [dbo].[ProductReturnQuantity] ([ProductReturnQuantityId])
GO
ALTER TABLE [dbo].[SaleReturnQuantity] CHECK CONSTRAINT [FK_SaleReturnQuantity_ProductReturnQuantity]
GO
/****** Object:  ForeignKey [FK_SaleReturnQuantity_SaleReturn]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[SaleReturnQuantity]  WITH CHECK ADD  CONSTRAINT [FK_SaleReturnQuantity_SaleReturn] FOREIGN KEY([SaleReturnId])
REFERENCES [dbo].[SaleReturn] ([SaleReturnId])
GO
ALTER TABLE [dbo].[SaleReturnQuantity] CHECK CONSTRAINT [FK_SaleReturnQuantity_SaleReturn]
GO
/****** Object:  ForeignKey [FK_PurchaseReturnQuantity_ProductReturnQuantity]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[PurchaseReturnQuantity]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseReturnQuantity_ProductReturnQuantity] FOREIGN KEY([ProductReturnQuantityId])
REFERENCES [dbo].[ProductReturnQuantity] ([ProductReturnQuantityId])
GO
ALTER TABLE [dbo].[PurchaseReturnQuantity] CHECK CONSTRAINT [FK_PurchaseReturnQuantity_ProductReturnQuantity]
GO
/****** Object:  ForeignKey [FK_PurchaseReturnQuantity_PurchaseReturn]    Script Date: 12/29/2016 17:29:39 ******/
ALTER TABLE [dbo].[PurchaseReturnQuantity]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseReturnQuantity_PurchaseReturn] FOREIGN KEY([PurchaseReturnId])
REFERENCES [dbo].[PurchaseReturn] ([PurchaseReturnId])
GO
ALTER TABLE [dbo].[PurchaseReturnQuantity] CHECK CONSTRAINT [FK_PurchaseReturnQuantity_PurchaseReturn]
GO
