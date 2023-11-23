IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'RedBrowTest')
BEGIN
    CREATE DATABASE RedBrowTest;
END
