USE [CongestionTaxCalculatorDB]
GO

INSERT INTO [dbo].[Cities]([Name],[DayMaxTax])VALUES ('Gothenburg',60,60)
GO

declare @cityId as int
select @cityId=Id from [dbo].[Cities] where [Name] = 'Gothenburg'

INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('0:0:0.0','5:59:59.999',0,@cityId)
INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('6:0:0:0','6:29:59:999',8,@cityId)
INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('6:30:0:0','6:59:59:999',13,@cityId)
INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('7:0:0:0','7:59:59:999',18,@cityId)
INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('8:0:0:0','8:29:59:999',13,@cityId)
INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('8:30:0:0','14:59:59:999',8,@cityId)
INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('15:0:0:0','15:29:59:999',13,@cityId)
INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('15:30:0:0','16:59:59:999',18,@cityId)
INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('17:0:0:0','17:59:59:999',13,@cityId)
INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('18:0:0:0','18:29:59:999',8,@cityId)
INSERT INTO [dbo].[DayPeriodTax]([StartTime],[EndTime],[TaxFee],[CityId]) VALUES ('18:30:0:0','23:59:59:999',0,@cityId)

INSERT INTO [dbo].[FreeChargeDayOfWeek] ([DayOfWeek] ,[CityId]) VALUES (0 , @cityId)
INSERT INTO [dbo].[FreeChargeDayOfWeek] ([DayOfWeek] ,[CityId]) VALUES (6 , @cityId)

INSERT INTO [dbo].[FreeChargeMonth]([Month],[CityId])  VALUES (7,@cityId)

INSERT INTO [dbo].[FreeChargeDate]([FreeOfChargeDate],[CityId]) VALUES ('2013-01-01',@cityId)
INSERT INTO [dbo].[FreeChargeDate]([FreeOfChargeDate],[CityId]) VALUES ('2013-03-29',@cityId)
INSERT INTO [dbo].[FreeChargeDate]([FreeOfChargeDate],[CityId]) VALUES ('2013-04-01',@cityId)
INSERT INTO [dbo].[FreeChargeDate]([FreeOfChargeDate],[CityId]) VALUES ('2013-05-01',@cityId)
INSERT INTO [dbo].[FreeChargeDate]([FreeOfChargeDate],[CityId]) VALUES ('2013-05-09',@cityId)
INSERT INTO [dbo].[FreeChargeDate]([FreeOfChargeDate],[CityId]) VALUES ('2013-05-20',@cityId)
INSERT INTO [dbo].[FreeChargeDate]([FreeOfChargeDate],[CityId]) VALUES ('2013-10-03',@cityId)
INSERT INTO [dbo].[FreeChargeDate]([FreeOfChargeDate],[CityId]) VALUES ('2013-12-25',@cityId)
INSERT INTO [dbo].[FreeChargeDate]([FreeOfChargeDate],[CityId]) VALUES ('2013-12-26',@cityId)

INSERT INTO [dbo].[FreeChargeVehicle]([Name],[CityId]) VALUES ('Motorcycle',@cityId)
INSERT INTO [dbo].[FreeChargeVehicle]([Name],[CityId]) VALUES ('Bus',@cityId)
INSERT INTO [dbo].[FreeChargeVehicle]([Name],[CityId]) VALUES ('Emergency',@cityId)
INSERT INTO [dbo].[FreeChargeVehicle]([Name],[CityId]) VALUES ('Diplomat',@cityId)
INSERT INTO [dbo].[FreeChargeVehicle]([Name],[CityId]) VALUES ('Foreign',@cityId)
INSERT INTO [dbo].[FreeChargeVehicle]([Name],[CityId]) VALUES ('Military',@cityId)

INSERT INTO [dbo].[AcceptableYear]([Year],[CityId])VALUES(2013,@cityId)
GO

