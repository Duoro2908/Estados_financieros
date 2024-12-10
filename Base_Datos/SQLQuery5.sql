USE EmpresaFinanzas;


CREATE TABLE BalanceGeneral (
    Id INT PRIMARY KEY IDENTITY,
    Categoria NVARCHAR(50) NOT NULL, 
    SubCategoria NVARCHAR(50) NOT NULL,
    NombreCuenta NVARCHAR(100) NOT NULL,
    Monto DECIMAL(18, 2) NOT NULL
);


CREATE TABLE EstadoResultados (
    Id INT PRIMARY KEY IDENTITY,
    Categoria NVARCHAR(50) NOT NULL,
    Monto DECIMAL(18, 2) NOT NULL
);