CREATE TABLE [dbo].[Perfil] (
    [idPerfil] INT          IDENTITY (1, 1) NOT NULL,
    [Nome]     VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([idPerfil] ASC)
);

CREATE TABLE [dbo].[Usuario] (
    [idUsuario]      INT          IDENTITY (1, 1) NOT NULL,
    [Nome]           VARCHAR (50) NOT NULL,
    [dataNascimento] DATETIME     NULL,
    [Usuario]        VARCHAR (50) NOT NULL,
    [Senha]          VARCHAR (50) NOT NULL,
    [PerfilId]       INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([idUsuario] ASC),
    CONSTRAINT [FK_Usuario_Perfil] FOREIGN KEY ([PerfilId]) REFERENCES [dbo].[Perfil] ([idPerfil])
);

CREATE TABLE [dbo].[LogAcesso] (
    [IdLogAcesso]  INT      IDENTITY (1, 1) NOT NULL,
    [UsuarioId]    INT      NOT NULL,
    [UltimoAcesso] DATETIME NOT NULL,
    [TempoAcesso]  TIME (7) NULL,
    PRIMARY KEY CLUSTERED ([IdLogAcesso] ASC),
    CONSTRAINT [FK_LogAcesso_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuario] ([idUsuario])
);

CREATE TABLE [dbo].[Zona] (
    [idZona] INT          IDENTITY (1, 1) NOT NULL,
    [Nome]   VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([idZona] ASC)
);

CREATE TABLE [dbo].[Carta] (
    [idCarta] INT          IDENTITY (1, 1) NOT NULL,
    [Nome]    VARCHAR (50) NOT NULL,
    [Tipo]    VARCHAR (50) NOT NULL,
    [ZonaId]  INT          NULL,
    PRIMARY KEY CLUSTERED ([idCarta] ASC),
    CONSTRAINT [FK_Cartas_Zonas] FOREIGN KEY ([ZonaId]) REFERENCES [dbo].[Zona] ([idZona])
);



INSERT INTO Perfil (Nome) VALUES
('Administrador');


-- Inserir zonas na tabela Zona
INSERT INTO Zona (Nome) VALUES 
('Monstros'),
('Magias e Armadilhas'),
('Cemiterio'),
('Deck'),
('Campo'),
('Deck Adicional'),
('Pendulo');


-- Inserir cartas na tabela Carta
INSERT INTO Carta (Nome, Tipo, ZonaId) VALUES 
('Dragao Branco de Olhos Azuis', 'Monstro', NULL),
('Rapaz de Baseball Verdadeiro', 'Monstro', NULL),
('Kuriboh', 'Monstro', NULL),
('Exodia Necross', 'Monstro', NULL),
('Jovem Touro Guerreiro', 'Monstro', NULL),
('Demonio Selvagem', 'Monstro', NULL),
('Besta de Natura', 'Monstro', NULL),
('Mirror Force', 'Armadilha', NULL),
('Dispositivo de Platina', 'Monstro', NULL),
('Rose Papillon', 'Monstro', NULL),
('Buraco Armadilha Ácido', 'Armadilha', NULL);
