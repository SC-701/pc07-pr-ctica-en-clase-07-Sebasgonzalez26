
CREATE PROCEDURE ObtenerProducto
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        p.Id,
        p.Nombre,
        p.Descripcion,
        p.Precio,
        p.Stock,
        p.CodigoBarras,
        s.Id AS IdSubCategoria,
        s.Nombre AS SubCategoria,
        c.Id AS IdCategoria,
        c.Nombre AS Categoria
    FROM Producto p
    INNER JOIN SubCategorias s 
        ON p.IdSubCategoria = s.Id
    INNER JOIN Categorias c 
        ON s.IdCategoria = c.Id
    WHERE p.Id = @Id;
END