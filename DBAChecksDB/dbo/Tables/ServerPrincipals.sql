﻿CREATE TABLE [dbo].[ServerPrincipals] (
    [InstanceID]            INT            NOT NULL,
    [name]                  NVARCHAR (128) NOT NULL,
    [principal_id]          INT            NOT NULL,
    [sid]                   VARBINARY (85) NULL,
    [type]                  CHAR (1)       NOT NULL,
    [type_desc]             NVARCHAR (60)  NULL,
    [is_disabled]           BIT            NULL,
    [create_date]           DATETIME       NOT NULL,
    [modify_date]           DATETIME       NOT NULL,
    [default_database_name] NVARCHAR (128) NULL,
    [default_language_name] NVARCHAR (128) NULL,
    [credential_id]         INT            NULL,
    [owning_principal_id]   INT            NULL,
    [is_fixed_role]         BIT            NOT NULL,
    CONSTRAINT [PK_ServerPrincipals] PRIMARY KEY CLUSTERED ([InstanceID] ASC, [principal_id] ASC),
    CONSTRAINT [FK_ServerPrincipals_Instances] FOREIGN KEY ([InstanceID]) REFERENCES [dbo].[Instances] ([InstanceID])
);

