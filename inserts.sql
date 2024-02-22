--Inserts de Zonas e Cartas, caso seja necessário

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
