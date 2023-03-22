/*
Plantilla de script posterior a la implementación							
--------------------------------------------------------------------------------------
 Este archivo contiene instrucciones de SQL que se anexarán al script de compilación.		
 Use la sintaxis de SQLCMD para incluir un archivo en el script posterior a la implementación.			
 Ejemplo:      :r .\miArchivo.sql								
 Use la sintaxis de SQLCMD para hacer referencia a una variable en el script posterior a la implementación.		
 Ejemplo:      :setvar TableName miTabla							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists (select 1 from dbo.RoomTypes)
begin
    INSERT INTO dbo.RoomTypes (Title, Description, Price) VALUES
    ('King Size Bed','This is a room with one king-size bed and a window.', 100),
    ('Two Queen Size Beds','This is a room with two queen-size beds and one window.', 115),
    ('Exective Suite', 'Two rooms, each with a king-size bed and a window.', 205);
end

if not exists (select 1 from dbo.Rooms)
begin
    declare @roomId1 int;
    declare @roomId2 int;
    declare @roomId3 int;

    select @roomId1 = Id from dbo.RoomTypes WHERE Title = 'King Size Bed'
    select @roomId2 = Id from dbo.RoomTypes WHERE Title = 'Two Queen Size Beds'
    select @roomId3 = Id from dbo.RoomTypes WHERE Title = 'Exective Suite'

    INSERT INTO dbo.Rooms (RoomNumber, RoomTypeId) VALUES 
    ('101', @roomId1),
    ('102', @roomId1),
    ('103', @roomId1),
    ('201', @roomId2),
    ('202', @roomId2),
    ('301', @roomId3);
end