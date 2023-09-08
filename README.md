<a name="readme-top"></a>
<h3 align="center">[B]³ - Projeto de avaliação</h3>

<!-- Tabela de conteúdo -->
<details>
  <summary>Conteúdo</summary>
  <ol>
    <li>
      <a href="#sobre-o-projeto">Sobre o projeto</a>
      <ul><li><a href="#aplicação">Aplicação</a></li></ul>
      <ul><li><a href="#api">API</a></li></ul>
    </li>
    <li><a href="#execução">Execução</a></li>
    <li>Relatório de cobertura de testes</li>
  </ol>
</details>

<!-- Sobre o projeto -->
## Sobre o projeto
![Screenshot da aplicação](imagens/aplicacao.png)

Aplicação para o cálculo do de um investimento a partir de um valor inicial e um prazo em meses para o resgate. <br>
Após o cálculo, o sistema apresenta o resultado bruto e líquido do investimento.

Requisitos:
* O valor inicial deve ser positivo e maior que zero
* O prazo em meses pra o resgate da aplicação deve ser maior que um

<!-- Execução -->
## Execução
1. Clonar o repositório
```sh
git clone https://github.com/danieltostes/B3.git
```

### Aplicação
2. Navegar até o diretório B3.Frontend e instalar as dependências via NPM
```sh
npm install
```

3. Executar o comando para rodar a aplicação
```sh
ng serve -o
```

<p>(<a href="#readme-top">Voltar ao topo</a>)</p>

### API
4. Abrir outro terminal e navegar até o diretório B3.API
5. Compilar o projeto .net
```sh
dotnet build
```

6. Executar o projeto API
```sh
dotnet watch run
```

<p>(<a href="#readme-top">Voltar ao topo</a>)</p>
