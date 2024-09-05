[Preguntas](https://www.cosmopolitan.com/es/consejos-planes/familia-amigos/a37021139/trivial-mejores-preguntas-jugar/)

#BD
create procedure cargarpregunta
@categoria varchar(20),
@dificultad varchar(20),
@enunciado varchar(250),
@foto varchar(500)
as begin
declare @IdCategoria int;
declare @IdDificultad int;

select @IdCategoria=IdCategoria from Categorias where Nombre=@categoria;
select @IdDificultad=@IdDificultad from Dificultades where Nombre=@dificultad;
insert into Preguntas(IdCategoria, IdDificultad, Enunciado, Foto) values(@IdCategoria, @IdDificultad, @enunciado, @foto);
end;

create procedure cargarrespuestas
@idpregunta int,
@contenido1 varchar(50),
@contenido2 varchar(50),
@contenido3 varchar(50),
@contenido4 varchar(50),
@correcta int
as begin
insert into Respuestas(IdPregunta, Opcion, Contenido, Correcta) values
(@idpregunta, 1, @contenido1, 0),
(@idpregunta, 2, @contenido2, 0),
(@idpregunta, 3, @contenido3, 0),
(@idpregunta, 4, @contenido4, 0);

update Respuestas set Correcta=1 where IdPregunta=@idpregunta and Opcion=@correcta;

end;