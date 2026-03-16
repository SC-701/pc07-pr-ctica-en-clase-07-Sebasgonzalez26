
CREATE PROCEDURE AgregarProducto
    @Id UNIQUEIDENTIFIER,
    @IdSubCategoria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(MAX),
    @Descripcion VARCHAR(MAX),
    @Precio DECIMAL(18,2),
    @Stock INT,
    @CodigoBarras VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    Begin transaction
    INSERT INTO Producto (
        Id,
        IdSubCategoria,
        Nombre,
        Descripcion,
        Precio,
        Stock,
        CodigoBarras
    )
    VALUES (
        @Id,
        @IdSubCategoria,
        @Nombre,
        @Descripcion,
        @Precio,
        @Stock,
        @CodigoBarras
    );
    Commit Transaction

    SELECT @Id
END