# Coding Test Dotkon | Projeto de Blog Simples

## Descrição

Agradecemos por estar participando do nosso processo seletivo! Estamos muito felizes em ter você por aqui 😃.
A primeira etapa do processo seletivo é um teste técnico, que está descrito abaixo. Boa sorte! \o/

Este projeto consiste na implementação de um sistema básico de blog onde os usuários podem visualizar, criar, editar e excluir postagens. O objetivo é avaliar as habilidades técnicas em C# e o uso do Entity Framework para manipulação de dados.

## Requisitos para a entrega
    1. Faça um fork deste repositório;
    2. Realize o teste;
    3. Adicione seu currículo em PDF na raiz do repositório;
    4. Envie-nos o PULL-REQUEST para que seja avaliado.
    
    OBS: seus dados não ficarão públicos em nosso repositório.
  
## Funcionalidades

### Requisitos Funcionais

1. **Autenticação**: 
    - Usuários devem ser capazes de se registrar e fazer login.

2. **Gerenciamento de Postagens**: 
    - Usuários autenticados podem criar postagens, editar suas próprias postagens e excluir postagens existentes.

3. **Visualização de Postagens**: 
    - Qualquer visitante do site pode visualizar as postagens existentes.

### Requisitos Técnicos

- **.NET**: Utilize a versão 7, 8 ou 9
- **Entity Framework**: Utilize o Entity Framework para interagir com o banco de dados e armazenar informações sobre usuários e postagens.

### Requisitos Opcionais

- **Arquitetura Monolítica**: Organize as responsabilidades do sistema, como autenticação, gerenciamento de postagens e notificações em tempo real.

- **Princípios SOLID**: Aplique os princípios SOLID, com ênfase no Princípio da Responsabilidade Única (SRP) e no Princípio da Inversão de Dependência (DIP).

- **WebSockets**: Implemente WebSockets para notificações em tempo real, como uma notificação simples na interface do usuário sempre que uma nova postagem for feita.

- **Interface Web Simples**: Crie uma interface web simples para a interação com o sistema.

## Observações Finais
Certifique-se de que seu código está bem documentado e limpo.
Inclua qualquer documentação adicional que possa ajudar a entender sua solução (README.md).

---

Este teste prático é uma oportunidade para demonstrar suas habilidades em desenvolvimento C#, arquitetura de software e boas práticas de programação. 
Divirta-se no processo!

---
Documentação do projeto

- **Api**:
  
```Solução com conjunto de apis para gerenciamento de usuários e postagens de um blog```
   1. Cadastro de usuários;
   2. Autenticação com bearer token de acesso;
   3. Cadastro, edição, deleção e listagem de postagens;
   4. Notificação de novas postagens via websocket;

```Modelo de dados```

![modelo-dados-blog-api drawio](https://github.com/user-attachments/assets/46fed41b-274c-427d-a46f-812560b46990)

No arquivo appsettings está disponível a connectionstring de acesso ao db do postgres via localhost, para subir o banco é preciso executar o seguinte comando via docker:

```docker run --name my-postgres -p 15432:5432 -e POSTGRES_PASSWORD=postgres -d postgres```

Após a criação do container, será necessário iniciar a criação do schema e tabelas, para isso estamos utilizando o Code First do Entity Framework. Execute os seguintes comandos:

```dotnet ef migrations add InitialCreate --project MGM.Blog.Infrastructure --startup-project MGM.Blog.Api```

```dotnet ef database update --project MGM.Blog.Infrastructure --startup-project MGM.Blog.Api```

_Observe que como o projeto está dividido em componentes, é necessário informar os projetos de leitura da connectionstring e dos modelos._

- **App**:

  ```Solução de interface web de blog```
   1. Login de usuários;
   3. Cadastro, edição, deleção e listagem de postagens;
   4. Notificação de nova postagem via websocket;
 
Para executar o projeto Angular, é necessário executar os seguintes comandos:

```npm install```
```npm start```

```dotnet ef database update --project MGM.Blog.Infrastructure --startup-project MGM.Blog.Api```
