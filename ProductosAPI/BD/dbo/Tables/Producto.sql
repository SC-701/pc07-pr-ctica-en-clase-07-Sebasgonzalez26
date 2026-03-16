CREATE TABLE [dbo].[Producto] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdSubCategoria] UNIQUEIDENTIFIER NOT NULL,
    [Nombre]         VARCHAR (MAX)    NOT NULL,
    [Descripcion]    VARCHAR (MAX)    NULL,
    [Precio]         DECIMAL (18, 2)  NOT NULL,
    [Stock]          INT              NOT NULL,
    [CodigoBarras]   VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Producto_SubCategorias] FOREIGN KEY ([IdSubCategoria]) REFERENCES [dbo].[SubCategorias] ([Id])
);

