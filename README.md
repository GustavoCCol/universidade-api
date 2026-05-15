# Primeira API
Experiência com criação de API RESTful no modelo MVC no .net 10.
## Avisos
O banco de dados foi inicialmente feito diretamente no editor e as tabelas foram criadas em SQL utilizando o script de criação ***DDLs***, no entanto já estou fazendo a transição para Migrations.

É importante ressaltar que o ***CONNECTION*** é necessário para que não tenha limitações de escrita e leitura, porque o erro do db evitava escrever sequer um objeto na base de dados.

O banco utilizado foi o Oracle, mas é possível alterar a Program para utilizar um banco diferente nas linhas referentes a conexão.

## Pacotes
Para rodar o projeto é preciso ter os pacotes para executar o código corretamente.

**Entre eles estão:** AspNetCore.OpenApi, Microsoft.EntitiyFrameWorkCore[.Design, .InMemory], Oracle.EntityFrameworkCore, 
Oracle.ManagedDataAccess[.Core, .EntityFramework], Serilog.AspNetCore, Serilog.Setting.Configuration, Serilog.Sinks.File, 
Swashbuckle.AspNetCore[.Swagger, .SwaggerUI].

Caso seja utilizado outro banco ou se não houver necessidade de logs e Swagger, basta retirar os pacotes e as suas configurações e referências tanto no json requerido quanto na Program e fazer as alterações necessárias.
