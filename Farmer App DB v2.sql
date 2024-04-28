-- Exported from QuickDBD: https://www.quickdatabasediagrams.com/
-- NOTE! If you have used non-SQL datatypes in your design, you will have to change these here.


SET XACT_ABORT ON

BEGIN TRANSACTION QUICKDBD

CREATE TABLE [ADMIN] (
    [userID] int IDENTITY(1,1) NOT NULL ,
    [email] string  NOT NULL ,
    [password] password  NOT NULL ,
    CONSTRAINT [PK_ADMIN] PRIMARY KEY CLUSTERED (
        [userID] ASC
    )
)

CREATE TABLE [PROVINCE] (
    [provinceID] int IDENTITY(1,1) NOT NULL ,
    [Province] string  NOT NULL ,
    CONSTRAINT [PK_PROVINCE] PRIMARY KEY CLUSTERED (
        [provinceID] ASC
    )
)

CREATE TABLE [FARM_TYPE] (
    [farmTypeID] int IDENTITY(1,1) NOT NULL ,
    [FarmType] string  NOT NULL ,
    CONSTRAINT [PK_FARM_TYPE] PRIMARY KEY CLUSTERED (
        [farmTypeID] ASC
    )
)

CREATE TABLE [GENDER] (
    [genderID] int IDENTITY(1,1) NOT NULL ,
    [Gender] string  NOT NULL ,
    CONSTRAINT [PK_GENDER] PRIMARY KEY CLUSTERED (
        [genderID] ASC
    )
)

CREATE TABLE [FARMER] (
    [userID] int IDENTITY(1,1) NOT NULL ,
    [FirstName] string  NOT NULL ,
    [LastName] string  NOT NULL ,
    [VillageName] string  NOT NULL ,
    [provinceID] int  NOT NULL ,
    [genderID] int  NOT NULL ,
    [farmTypeID] int  NOT NULL ,
    CONSTRAINT [PK_FARMER] PRIMARY KEY CLUSTERED (
        [userID] ASC
    )
)

ALTER TABLE [FARMER] WITH CHECK ADD CONSTRAINT [FK_FARMER_provinceID] FOREIGN KEY([provinceID])
REFERENCES [PROVINCE] ([provinceID])

ALTER TABLE [FARMER] CHECK CONSTRAINT [FK_FARMER_provinceID]

ALTER TABLE [FARMER] WITH CHECK ADD CONSTRAINT [FK_FARMER_genderID] FOREIGN KEY([genderID])
REFERENCES [GENDER] ([genderID])

ALTER TABLE [FARMER] CHECK CONSTRAINT [FK_FARMER_genderID]

ALTER TABLE [FARMER] WITH CHECK ADD CONSTRAINT [FK_FARMER_farmTypeID] FOREIGN KEY([farmTypeID])
REFERENCES [FARM_TYPE] ([farmTypeID])

ALTER TABLE [FARMER] CHECK CONSTRAINT [FK_FARMER_farmTypeID]

COMMIT TRANSACTION QUICKDBD