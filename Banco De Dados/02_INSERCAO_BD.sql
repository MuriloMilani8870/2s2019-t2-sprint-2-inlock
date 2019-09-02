USE M_InLock

INSERT INTO Usuarios (Email, Senha, PermissaoUsuario)
	VALUES	('admin@admin.com' , 'admin', 'ADMINISTRADOR'), 
			('cliente@cliente.com' , 'cliente', 'COMUM')  


INSERT INTO Estudios (NomeEstudio, PaisOrigem , DataCriacao , UsuarioId)
	VALUES	('Blizzard' , 'EUA' , '1991-02-08' , 1),
			('Rockstar Studios' , 'EUA' , '1998-12-01' , 1),
			('Square Enix' , 'Japão' , '2003-04-01' , 1)

			
INSERT INTO Jogos (NomeJogo , Descricao , DataLancamento , Valor, EstudioId)
	VALUES	('Diablo 3' , 'É um jogo que contém bastante ação e é viciante, seja você um novato ou um fã' , '2012-05-15' , 99 , 1),
			('Red Dead Redemption II', 'jogo eletrônico de ação-aventura western' , '2018-10-26' , 120 , 2)
			