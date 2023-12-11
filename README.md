# Health API - Projeto NeoApp 

Uma API desenvolvida para uma aplicação dentro do contexto da área da saúde

## Indíce
- <a href="#funcionalidades-do-projeto">Funcionalidades do projeto</a>
- <a href="#processo-criativo">Processo criativo</a>
- <a href="#tecnologias-utilizadas">Tecnologias utilizadas</a>
- <a href="#instruções-para-uso-da-aplicação">Instruções para uso da aplicação</a>
- <a href="#considerações-sobre-o-projeto">Considerações sobre o projeto</a>
- <a href="#onde-me-encontrar">Minhas redes "Onde me encontrar"</a>


## Funcionalidades do projeto
- [x] Cadastrar Usuários, Pacientes, Médicos e Consultas Médicas
- [x] Consultar Usuários, Pacientes, Médicos e Consultas Médicas
- [x] Atualizar dados de Pacientes, Médicos e Consultas Médicas
- [x] Alteração de status de Pacientes e Médicos
- [x] Deleção de Pacientes, Médicos e Consultas
- [x] Login de Usuários
- [x] Autenticação de Usuários

## Processo criativo
O desenvolvimento dessa aplicação foi algo que me deixou muito animado logo de inicio. Poder trabalhar com novas tecnologias e pensar em como essas tecnologias seriam melhor utilizadas para aumentar o desempenho da aplicação, foram pontos que contribuiram muito para o meu desenvolvimento como desenvolvedor de códigos limpos e funcionais. Foram usados alguns dos conceitos de SOLID como por exemplo o conceito de inversão de dependência.

O projeto como um todo, desde a escolha das tecnologias e padrões arquiteturais, foi pensado para ser algo simples de ser manipulado e poderosamente funcional, além de ter alguns pontos pensados até mesmo na performance da memória da aplicação. Como exemplo disso, temos uma das tomadas de decisão que tive onde foram usadas "structs" em alguns cenários para substituir "classes" propriamente ditas.

O uso de structs nesses cenários otimizam e economizam a memória heap já que são do tipo valor e ficam alocadas na memória stack, sendo nesse caso mais performáticas que as classes em si.

Foi utilizado na aplicação o conceito de DDD, onde não se trata diretamente de uma arquitetura, porém todas as camadas são pensadas para ter uma única responsabilidade facilitando a manutenção do código se necessário e facilitando o compreensão e leitura do mesmo.

Por se tratar de um projeto de teste, não foram usadas variáveis de ambiente, portanto algumas informações como ConnectionStrings e Chave de token se encontram diretamente dentro da aplicação. Estou ciente de que essa não é a maneira mais segura de se usar dados sensíveis, porém, para agilizar e facilitar o funcionamento da aplicação em outra máquina foi usada essa abordagem

## Tecnologias utilizadas
1. [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)
2. [Identity Framework](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio)
1. [AutoMapper](https://docs.automapper.org/en/stable/)
1. [Dapper](https://github.com/DapperLib/Dapper)
1. [XUnit](https://xunit.net)
1. [JWT Bearer](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/interactive)

## Instruções para uso da aplicação

```
Clone o repositório em sua máquina
- git clone https://github.com/KIQxl/Health_Api_NeoApp.git

Acesse a pasta do repositório em sua máquina e abra a solução do projeto
- Health_Api.sln

- Antes de rodar o projeto é necessário configurar a string de conexão de acordo com sua base de dados, nesse projeto é utilizado MySQL. Acesse a pasta APIs dentro do projeto e dentro dela existe um projeto chamado HealthWebApi, identifique o arquivo "appsettings.json" e dentro desse arquivo localize o item "ConnectionStrings > HealthApi" e altere o campo "password" para a senha da sua base de dados 
- "server=localhost; database=HealthApi;user=root;password=InsiraAquiSuaSenha"


Abra o "Console do Gerenciador de Pacotes"
- Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes

Com o Console do Gerenciador de Pacotes aberto, selecione como projeto padrão "Infrastructure" e execute a migração que contém todo o script de geração do banco de dados referente ao contexto de pacientes,médicos e consultas médicas atráves do comando:
- update-database -Context HealthApiContext

Após isso, faça o mesmo com a migração do contexto que contém o acesso e controle de usuários:
- update-database -Context UserContext
(escritos exatamente dessa forma)

Após isso, é só verificar se o projeto de inicialização é "Health_Web_Api" que está dentro da pasta APIs, caso já esteja definido, basta rodar a aplicação (caso sua IDE seja Visual studio basta apertar F5). A aplicação será iniciada na porta 7018.
https://localhost:7018

Caso seja sua primeira inicialização do projeto, o código irá inserir na base de dados um paciente Default na tabela de pacientes, um médico Default na tabela de médicos e um usuário padrão que poderá ser usado para realizar o login e obter um token de autenticação que será necessário para realizar todas as outras ações e operações dentro da aplicação.

As credenciais de login do usuário padrão são
Login: DefaultUser
Password: Default@123

Utilizando a rota de Login com essas credenciais, será retornado uma view do usuário contendo os dados do mesmo, e o token que deverá ser inserido na sessão de autenticação que está localizada na parte de cima das rotas na pagina da documentação da API feita com o swagger. 

Esse token deve ser inserido da seguinte forma:
Bearer <token obtido como resposta do login>

Esse token token é necessário para realizar todas as ações e operações dentro da aplicação. 

Algumas rotas só podem ser acessadas por perfis específicos, porém todas podem ser acessadas pelos usuários que tem contem o perfil (role) "Admin".

As roles que podem ser utilizadas na aplicação são:
Admin, Patient, Doctor.

Para execução dos teste, o a obtenção do token é feita por meio de uma função GetToken, as credenciais para obter esse token também são referentes ao usuário padrão, caso seja excluído da base de dados, as credenciais devem ser alteradas
```
## Considerações sobre o projeto
Ao final do desenvolvimento desse código, posso afirmar que foi um processo muito divertido e de muito aprendizado, dizem que a prática leva a perfeição, e esse projeto com certeza contribuiu ainda mais para meu desenvolvimento. 
Ficarei feliz em responder quaisquer dúvidas e conversar sobre o desenvolvimento e possíveis melhorias para essa aplicação.

## Onde me encontrar
[Linkedin](https://www.linkedin.com/in/kaique-alves-38058a19b/)

[Meu portfólio de pessoal](https://portfolio-kaiquedev.netlify.app)
