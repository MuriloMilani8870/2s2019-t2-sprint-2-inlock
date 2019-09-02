USE M_InLock

SELECT * FROM Usuarios

SELECT * FROM Jogos

SELECT * FROM Estudios

SELECT J.JogoId , J.NomeJogo , J.Descricao , J.DataLancamento , J.Valor , E.EstudioId , E.NomeEstudio , E.PaisOrigem , E.DataCriacao , E.UsuarioId FROM Jogos J
INNER JOIN Estudios E
ON J.EstudioId = E.EstudioId

SELECT * FROM Jogos 
FULL JOIN Estudios
ON Jogos.EstudioId = Estudios.EstudioId

SELECT * FROM Usuarios
WHERE Usuarios.Email = 'cliente@cliente.com' and Usuarios.Senha = 'cliente'

SELECT * FROM Jogos
WHERE Jogos.JogoId = 1

SELECT * FROM Estudios
WHERE Estudios.EstudioId = 1