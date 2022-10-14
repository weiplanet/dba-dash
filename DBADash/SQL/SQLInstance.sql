﻿IF OBJECT_ID('sys.dm_os_host_info') IS NOT NULL
BEGIN
	SELECT host_platform,
		host_distribution,
		host_release,
		host_service_pack_level,
		host_sku,os_language_version,
		@@SERVERNAME AS Instance,
		GETUTCDATE() AS SnapshotDateUTC,
		CAST(SERVERPROPERTY('EditionID') AS BIGINT) AS EditionID,
		ISNULL(CAST(SERVERPROPERTY('ComputerNamePhysicalNetBIOS') AS NVARCHAR(128)),'') AS ComputerNamePhysicalNetBIOS,
		DB_NAME() AS DBName,
		SERVERPROPERTY ('ProductVersion') AS ProductVersion,
		CAST(ROUND(DATEDIFF(s,GETDATE(),GETUTCDATE())/60.0,0) AS INT) AS UTCOffset,
		SERVERPROPERTY('IsHadrEnabled') IsHadrEnabled,
		SERVERPROPERTY('EngineEdition') EngineEdition
	FROM sys.dm_os_host_info
END
ELSE IF OBJECT_ID('sys.dm_os_windows_info') IS NOT NULL
BEGIN
	SELECT 'Windows' AS host_platform,
		NULL AS host_distribution,
		windows_release AS host_release,
		windows_service_pack_level AS host_service_pack_level,
		windows_sku AS host_sku,
		os_language_version,
		@@SERVERNAME AS Instance,
		GETUTCDATE() AS SnapshotDateUTC,
		CAST(SERVERPROPERTY('EditionID') AS BIGINT) AS EditionID,
		ISNULL(CAST(SERVERPROPERTY('ComputerNamePhysicalNetBIOS') AS NVARCHAR(128)),'') AS ComputerNamePhysicalNetBIOS,
		DB_NAME() AS DBName,
		SERVERPROPERTY ('ProductVersion') AS ProductVersion,
		CAST(ROUND(DATEDIFF(s,GETDATE(),GETUTCDATE())/60.0,0) AS INT) AS UTCOffset,
		SERVERPROPERTY('IsHadrEnabled') IsHadrEnabled,
		SERVERPROPERTY('EngineEdition') EngineEdition
	FROM sys.dm_os_windows_info
END
ELSE
BEGIN
	SELECT 'Windows' AS host_platform,
		NULL AS host_distribution,
		NULL AS host_release,'' AS host_service_pack_level,
		NULL AS host_sku,
		NULL os_language_version,
		@@SERVERNAME AS Instance,
		GETUTCDATE() AS SnapshotDateUTC,
		CAST(SERVERPROPERTY('EditionID') AS BIGINT) AS EditionID,
		ISNULL(CAST(SERVERPROPERTY('ComputerNamePhysicalNetBIOS') AS NVARCHAR(128)),'') AS ComputerNamePhysicalNetBIOS,
		DB_NAME() AS DBName,
		SERVERPROPERTY ('ProductVersion') AS ProductVersion,
		CAST(ROUND(DATEDIFF(s,GETDATE(),GETUTCDATE())/60.0,0) AS INT) AS UTCOffset,
		SERVERPROPERTY('IsHadrEnabled') IsHadrEnabled,
		SERVERPROPERTY('EngineEdition') EngineEdition
END