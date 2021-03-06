## Central de Erros 
### Projeto Final do curso AceleraDev oferecido pela CodeNation em parceria com a Stone



A documentação da API no Swagger se encontra neste [link](http://central-de-erros-api-anderson.herokuapp.com/swagger/index.html).

O vídeo explicativo do projeto se encontra neste [link](https://www.youtube.com/watch?v=Gyf4lKfvRmA).

### Objetivo

Em projetos modernos é cada vez mais comum o uso de arquiteturas baseadas em serviços ou microsserviços. Nestes ambientes complexos, erros podem surgir em diferentes camadas da aplicação (backend, frontend, mobile, desktop) e mesmo em serviços distintos. Desta forma, é muito importante que os desenvolvedores possam centralizar todos os registros de erros em um local, de onde podem monitorar e tomar decisões mais acertadas. Neste projeto vamos implementar um sistema para centralizar registros de erros de aplicações.

A arquitetura do projeto é formada por:

### Backend - API
- criar endpoints para serem usados pelo frontend da aplicação
- criar um endpoint que será usado para gravar os logs de erro em um banco de dados relacional
- a API deve ser segura, permitindo acesso apenas com um token de autenticação válido

### Ferramentas 
- Visual Studio 2017
- SQL Server 
- Heroku
- Swagger
- Docker

### Linguagens e bibliotecas

- C# com .NET Core 3.1
- Entity Framework Core
- SQL Server 2019
- Identity

### Modelo relacional do banco de dados

- Application layer: ex. Web, Desktop, Mobile
- Environment: ex. Produção, Homologação, Desenvolvimento
- Level: ex. Error, Warning, Info, Debug, Trace
- Language: ex. C#, Python, JavaScript

!["Modelo relacional do banco de dados"](https://github.com/andersonmendrot/central-erros-codenation/blob/master/modelo_relacional_db.png)


### Estrutura do projeto

O projeto foi dividido em camadas de modo a adaptar a Clean Architecture a ele.

!["Estrutura do projeto"](https://github.com/andersonmendrot/central-erros-codenation/blob/master/projeto_diagrama.png)



