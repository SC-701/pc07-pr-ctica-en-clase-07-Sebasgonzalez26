CREATE TABLE [dbo].[SubCategorias] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdCategoria] UNIQUEIDENTIFIER NOT NULL,
    [Nombre]      VARCHAR (MAX)    NOT NULL,
    CONSTRAINT [PK_SubCategorias] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubCategorias_Categorias] FOREIGN KEY ([IdCategoria]) REFERENCES [dbo].[Categorias] ([Id])
);

