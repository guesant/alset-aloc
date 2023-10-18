CREATE DATABASE IF NOT EXISTS aloc;


USE aloc;

CREATE TABLE IF NOT EXISTS endereco (
  id_end int primary key not null auto_increment,
  pais_end varchar(45) not null,
  codigo_postal_end varchar(45) not null,
  uf_end varchar(2) not null,
  cidade_end varchar(45) not null,
  rua_end varchar(45) not null,
  numero_end int not null,
  bairro_end varchar(45) not null,
  complemento_end varchar(45)
);

delimiter $$ 

create procedure SalvarEndereco
  (pais varchar(45), codigoPostal varchar(45), uf varchar(20), cidade varchar(45), rua varchar(45), numero int, bairro varchar(45), complemento varchar(45))
begin
  if (pais IS NOT NULL) AND (codigoPostal IS NOT NULL) AND (uf IS NOT NULL) AND (cidade IS NOT NULL) AND (rua IS NOT NULL) AND (numero IS NOT NULL) AND (bairro IS NOT NULL) THEN
    INSERT INTO endereco
      (pais_end, codigo_postal_end, uf_end, cidade_end, rua_end, numero_end, bairro_end, complemento_end)
    VALUES
      (pais, codigoPostal, uf, cidade, rua, numero, bairro, complemento)
    ;

    SELECT "O endereço foi salvo com sucesso!" as Confirmacao;
  else
    SELECT "Os campos a seguir são obrigatórios: PAÍS, CÓDIGO POSTAL, UF, CIDADE, RUA, NÚMERO, BAIRRO" as Erro;
  end if;
end;

$$ delimiter ;

CALL SalvarEndereco('Brasil', '12345-678', 'SP', 'São Paulo', 'Rua A', 123, 'Bairro A', 'Complemento 1');


CALL SalvarEndereco('EUA', '98765', 'CA', 'Los Angeles', 'Main Street', 456, 'Downtown', 'Apartment 2');


CALL SalvarEndereco('Canadá', '56789', 'ON', 'Toronto', 'Maple Avenue', 789, 'Suburb', NULL);

CREATE TABLE IF NOT EXISTS cliente (
  id_cli int primary key not null auto_increment,
  nome_cli varchar(200) not null,
  data_nascimento_cli date not null,
  cpf_cli varchar(11) not null,
  rg_cli varchar(30) not null,
  cnh_cli varchar(30) not null,
  email_cli varchar(45) not null,
  telefone_cli varchar(45) not null,
  genero_cli varchar(45) not null,
  id_end_fk int,
  foreign key (id_end_fk) references endereco(id_end)
);

delimiter $$

CREATE PROCEDURE SalvarCliente 
  (nome VARCHAR(200), dataNascimento DATE, cpf VARCHAR(11), rg VARCHAR(30), cnh VARCHAR(30), email VARCHAR(30), telefone VARCHAR(45), genero VARCHAR(45), idEnd INT)
BEGIN
  IF (idEnd IS NULL) OR (NOT EXISTS(SELECT id_end FROM endereco WHERE id_end = idEnd)) THEN
    SELECT "A chave estrangeira de endereço informada não existe" as Erro;
  ELSEIF (nome IS NULL) OR (dataNascimento IS NULL) OR (cpf IS NULL) OR (rg IS NULL) OR (email IS NULL) OR (telefone IS NULL) OR (genero IS NULL) THEN
    SELECT "Os campos a seguir são obrigatórios: NOME, DATA DE NASCIMENTO, CPF, RG, CNH, EMAIL, TELEFONE E GÊNERO" as Erro;
  ELSE
    INSERT INTO cliente (nome_cli, data_nascimento_cli, cpf_cli, rg_cli, cnh_cli, email_cli, telefone_cli, genero_cli, id_end_fk)
    VALUES (nome, dataNascimento, cpf, rg, cnh, email, telefone, genero, idEnd);
    SELECT "O cliente foi salvo com sucesso!" as Confirmacao;
  END IF;
END 

$$ DELIMITER ;


CALL SalvarCliente('João Silva', '1990-05-15', '12345678901', '1234567', 'AB123456', 'joao@email.com', '123-456-7890', 'Masculino', 1);


CALL SalvarCliente('Maria Souza', '1985-08-20', '98765432101', '7654321', 'CD987654', 'maria@email.com', '987-654-3210', 'Feminino', 2);


CALL SalvarCliente('Pedro Santos', '1998-03-10', '54321678901', '9876543', 'EF543216', 'pedro@email.com', '345-678-9012', 'Masculino', 3);


CREATE TABLE IF NOT EXISTS funcionario (
  id_func int primary key auto_increment not null,
  nome_func varchar(300) not null,
  data_nascimento_func date not null,
  cpf_func varchar(11) not null,
  rg_func varchar(45) not null,
  email_func varchar(45) not null,
  telefone_func varchar(45) not null,
  genero_func varchar(45),
  id_end_fk int,
  foreign key (id_end_fk) references endereco(id_end)
);


DELIMITER $$

CREATE PROCEDURE SalvarFuncionario 
  (nome VARCHAR(200), dataNascimento DATE, cpf VARCHAR(11), rg VARCHAR(30), email VARCHAR(30), telefone VARCHAR(45), genero VARCHAR(45), idEnd INT)
BEGIN
  IF (idEnd IS NULL) OR (NOT EXISTS(SELECT id_end FROM endereco WHERE id_end = idEnd)) THEN
    SELECT "A chave estrangeira de endereço informada não existe" as Erro;
  ELSE
    IF (nome IS NOT NULL) AND (dataNascimento IS NOT NULL) AND (cpf IS NOT NULL) AND (rg IS NOT NULL) AND (email IS NOT NULL) AND (telefone IS NOT NULL) THEN 
      INSERT INTO funcionario
        (nome_func, data_nascimento_func, cpf_func, rg_func, email_func, telefone_func, genero_func, id_end_fk)
      VALUES
        (nome, dataNascimento, cpf, rg, email, telefone, genero, idEnd)
      ;

      SELECT "O funcionário foi salvo com sucesso!" as Confirmacao;
    ELSE
      SELECT  "Os campos a seguir são obrigatórios: NOME, DATA DE NASCIMENTO, CPF, RG, EMAIL, TELEFONE" as Erro;
    END IF;
  END IF;
END$$

DELIMITER ;

-- Chamar o procedimento e inserir o primeiro registro
CALL SalvarFuncionario('Carlos Oliveira', '1990-06-20', '12345678901', '123456789', 'carlos@email.com', '123-456-7890', 'Masculino', 1);

-- Chamar o procedimento e inserir o segundo registro
CALL SalvarFuncionario('Ana Santos', '1985-09-15', '98765432101', '987654321', 'ana@email.com', '987-654-3210', 'Feminino', 2);

-- Chamar o procedimento e inserir o terceiro registro
CALL SalvarFuncionario('Mário Silva', '1998-04-12', '54321678901', '543216789', 'mario@email.com', '345-678-9012', 'Masculino', 3);


CREATE TABLE IF NOT EXISTS usuario (
  id_usua int primary key auto_increment not null,
  usuario_usua varchar(45) not null,
  -- TODO: usar bcrypt
  senha_usua varchar(45) not null,
  id_func_fk int,
  foreign key (id_func_fk) references funcionario(id_func)
);

DELIMITER $$

CREATE PROCEDURE SalvarUsuario (usuario VARCHAR(45), senha VARCHAR(45), idFunc INT)
BEGIN
  IF (NOT EXISTS(SELECT id_usua FROM usuario WHERE usuario_usua = usuario)) THEN
    IF (usuario IS NOT NULL AND senha IS NOT NULL) THEN
      INSERT INTO usuario (usuario_usua, senha_usua, id_func_fk) VALUES (usuario, senha, idFunc);
      SELECT "O usuário foi salvo com sucesso!" as Confirmacao;
    ELSE 
      SELECT "Os seguintes campos são obrigatórios: USUÁRIO E SENHA" as Erro;
    END IF;
  ELSE
    SELECT "A chave estrangeira de usuário informada não existe" as Erro;
  END IF;
END;

$$ DELIMITER ;

-- Chamar o procedimento e inserir o primeiro registro
CALL SalvarUsuario('carlos123', 'senha123', 1);

-- Chamar o procedimento e inserir o segundo registro
CALL SalvarUsuario('ana456', 'senha456', 2);

-- Chamar o procedimento e inserir o terceiro registro
CALL SalvarUsuario('mario789', 'senha789', 3);

CREATE TABLE IF NOT EXISTS veiculo (
  id_vei int primary KEY auto_increment not null,

  modelo_vei varchar(45) not null,
  marca_vei varchar(45) not null,
  ano_vei int not null,
  placa_vei varchar(45) not null,
  numero_chassi_vei varchar(45) not null,
  cor_vei varchar(45) not null,
  data_compra_vei date not null,
  descricao_vei varchar(200) not null
);


DELIMITER $$

CREATE PROCEDURE SalvarVeiculo 
  (modelo varchar(45), marca varchar(45), ano int, placa varchar(45), numero_chassi varchar(45), cor varchar(45), data_compra date, descricao varchar(200))
begin

  if (modelo IS NOT NULL) AND (marca IS NOT NULL) AND (ano IS NOT NULL) AND (placa IS NOT NULL) AND (numero_chassi IS NOT NULL) AND (cor IS NOT NULL) AND (data_compra IS NOT NULL) AND (descricao IS NOT NULL) THEN 
    INSERT INTO veiculo
      (modelo_vei, marca_vei, ano_vei, placa_vei, numero_chassi_vei, cor_vei, data_compra_vei, descricao_vei)
    VALUES
      (modelo, marca, ano, placa, numero_chassi, cor, data_compra, descricao)
    ;
    SELECT "O veículo foi salvo com sucesso!" as Confirmacao;
  else 
    SELECT  "Os campos a seguir são obrigatórios: MODELO, MARCA, ANO, PLACA, NÚMERO DO CHASSI, COR, DATA DE COMPRA, DESCRIÇÃO";
  END IF;

END

$$ DELIMITER ;

-- Chamar o procedimento e inserir o primeiro registro
CALL SalvarVeiculo('Fiat Uno', 'Fiat', 2020, 'ABC123', '1234567890', 'Vermelho', '2023-01-15', 'Veículo compacto em bom estado.');

-- Chamar o procedimento e inserir o segundo registro
CALL SalvarVeiculo('Volkswagen Gol', 'VW', 2022, 'XYZ789', '9876543210', 'Prata', '2023-02-20', 'Veículo econômico e confortável.');

-- Chamar o procedimento e inserir o terceiro registro
CALL SalvarVeiculo('Ford Focus', 'Ford', 2021, 'DEF456', '5678901234', 'Azul', '2023-03-10', 'Veículo com ótimo desempenho.');


CREATE TABLE IF NOT EXISTS locacao (
  id_loc int primary key not null auto_increment,
  data_locacao_loc datetime not null,
  data_devo	sta_loc datetime not null,
  data_devolucao_efetivada_loc date,
  alocado_loc tinyint not null,
  id_vei_fk int,
  id_fun_fk int,
  foreign key (id_vei_fk) references veiculo(id_vei),
  foreign key (id_fun_fk) references funcionario(id_func)
);

DELIMITER $$

CREATE PROCEDURE SalvarLocacao 
  (dataLocacao DATETIME, dataDevolucaoPrevista DATETIME, alocado TINYINT, dataDevolucaoEfetivada DATETIME, idVei INT, idFun INT)
begin

  if (idVei IS NOT NULL) AND (idFun IS NOT NULL) THEN
    if ((dataLocacao IS NOT NULL) AND (dataDevolucaoPrevista IS NOT NULL)) THEN
      INSERT INTO locacao
        (data_locacao_loc, data_devolucao_prevista_loc, data_devolucao_efetivada_loc, alocado_loc, id_vei_fk, id_fun_fk)
      VALUES
        (dataLocacao, dataDevolucaoPrevista, dataDevolucaoEfetivada, alocado, idVei, idFun)
      ;
      SELECT "A locação foi salvo com sucesso!" as Confirmacao;
    else 
      SELECT "Os campos a seguir são obrigatórios: DATA LOCAÇÃO E DATA DEVOLUÇÃO PREVISTA" as Erro;
    END IF;
  else
    SELECT "Os campos a seguir são obrigatórios: deve ser informado o veículo e o funcinário" as Erro ;
  END IF;
END

$$ DELIMITER ;

-- Chamar o procedimento e inserir a primeira locação
CALL SalvarLocacao('2023-09-20 10:00:00', '2023-09-25 12:00:00', 1, NULL, 1, 2);

-- Chamar o procedimento e inserir a segunda locação
CALL SalvarLocacao('2023-09-22 14:00:00', '2023-09-27 16:00:00', 1, NULL, 2, 3);

-- Chamar o procedimento e inserir a terceira locação
CALL SalvarLocacao('2023-09-25 09:00:00', '2023-09-30 11:00:00', 1, NULL, 3, 1);

CREATE TABLE IF NOT EXISTS cliente_locacao (
  id_cli_loc int primary key not null auto_increment,

  id_cli_fk int not null,
  foreign key (id_cli_fk) references cliente(id_cli),
  
  id_loc_fk int not null,
  foreign key (id_loc_fk) references locacao(id_loc)
);


DELIMITER $$

CREATE PROCEDURE SalvarClienteLocacao 
  (fkCli int, fkLoc int)
begin
  if (
    (EXISTS (SELECT id_cli FROM cliente WHERE id_cli = fkCli))
    AND
    (EXISTS (SELECT id_loc FROM locacao WHERE id_loc = fkLoc))
  ) then
    IF NOT EXISTS(SELECT id_cli_loc FROM cliente_locacao WHERE id_cli_fk = fkCli and id_loc_fk = fkLoc) THEN
      INSERT INTO cliente_locacao
        (id_cli_fk, id_loc_fk)
      VALUES
        (fkCli, fkLoc);
    END IF;

    SELECT "O cliente foi associado à locação com sucesso!" as Confirmacao;
  else
    SELECT "O cliente e locação devem existir no banco de dados do sistema!" as Erro ;
  end if;
END

$$ DELIMITER ;

-- Chamar o procedimento e associar o primeiro cliente à primeira locação
CALL SalvarClienteLocacao(1, 1);

-- Chamar o procedimento e associar o segundo cliente à segunda locação
CALL SalvarClienteLocacao(2, 2);

-- Chamar o procedimento e associar o terceiro cliente à terceira locação
CALL SalvarClienteLocacao(3, 3);

CREATE TABLE IF NOT EXISTS fornecedor (
  id_forn int primary key not null auto_increment,
  cnpj_forn varchar(20),
  razao_social_forn varchar(45),
  nome_fantasia_forn varchar(45),
  email_forn varchar(60),
  telefone_forn varchar(45),
  id_end_fk int,
  foreign key (id_end_fk) references endereco(id_end)
);

DELIMITER $$

CREATE PROCEDURE SalvarFornecedor 
  (cnpj varchar(20), razaoSocial varchar(45), nomeFantasia varchar(45), email varchar(60), telefone varchar(45), endereco int )
BEGIN
  IF (endereco IS NULL) OR (NOT EXISTS(SELECT id_end FROM endereco WHERE id_end = endereco)) THEN
    SELECT "A chave estrangeira de endereço informada não existe" as Erro;
  ELSE
    INSERT INTO fornecedor
      (cnpj_forn, razao_social_forn, nome_fantasia_forn, email_forn, telefone_forn, id_end_fk)
    VALUES
      (cnpj, razaoSocial, nomeFantasia, email, telefone, endereco);

    SELECT "O fornecedor foi salvo com sucesso!" as Confirmacao;
  END IF;
END

$$ DELIMITER ;

-- Chamar o procedimento e inserir o primeiro fornecedor
CALL SalvarFornecedor('12345678901', 'Fornecedor A', 'Fantasia A', 'fornecedorA@email.com', '123-456-7890', 1);

-- Chamar o procedimento e inserir o segundo fornecedor
CALL SalvarFornecedor('98765432102', 'Fornecedor B', 'Fantasia B', 'fornecedorB@email.com', '987-654-3210', 2);

-- Chamar o procedimento e inserir o terceiro fornecedor
CALL SalvarFornecedor('54321678903', 'Fornecedor C', 'Fantasia C', 'fornecedorC@email.com', '345-678-9012', 3);

CREATE TABLE IF NOT EXISTS produto (
  id_prod int primary key not null auto_increment,
  
  nome_prod varchar(60),
  preco_prod double,
  
  estoque_prod double
);

DELIMITER $$


create procedure SalvarProduto
  (nome varchar(60), preco double, estoque double)
begin
  if (nome <> "") AND (nome is not null) then
    INSERT INTO produto
      (nome_prod, preco_prod, estoque_prod)
    VALUES
      (nome, preco, estoque)
    ;

    SELECT "O produto foi salvo com sucesso!" as Confirmacao;
  else
    SELECT "O campo NOME é obrigatório." as Erro;
  end if;
end;

$$ DELIMITER ;

-- Chamar o procedimento e inserir o primeiro produto
CALL SalvarProduto('Produto A', 50.0, 100.0);

-- Chamar o procedimento e inserir o segundo produto
CALL SalvarProduto('Produto B', 30.0, 200.0);

-- Chamar o procedimento e inserir o terceiro produto
CALL SalvarProduto('Produto C', 40.0, 150.0);

CREATE TABLE IF NOT EXISTS compra (
  id_com int primary key not null auto_increment,

  data_compra_com date,
  numero_nota_com varchar(45),

  id_prod_fk int,
  foreign key (id_prod_fk) references produto(id_prod),
  
  id_forn_fk int,
  foreign key (id_forn_fk) references fornecedor(id_forn)
);

DELIMITER $$

CREATE PROCEDURE SalvarCompra 
  (dataCompra date, numeroNota varchar(45), fkProd int, fkForn int)
begin
  if (
    (EXISTS (SELECT id_prod FROM produto WHERE id_prod = fkProd))
    AND
    (EXISTS (SELECT id_forn FROM fornecedor WHERE id_forn = fkForn))
  ) then
    INSERT INTO compra
      (data_compra_com, numero_nota_com, id_prod_fk, id_forn_fk)
    VALUES
      (dataCompra, numeroNota, fkProd, fkForn)
    ;

    SELECT "A compra foi registrada com sucesso!" as Confirmacao;
  else
    SELECT "O produto e fornecedor devem existir no banco de dados do sistema!" as Erro ;
  end if;
END

$$ DELIMITER ;

-- Chamar o procedimento e inserir a primeira compra
CALL SalvarCompra('2023-09-20', 'Nota1', 1, 1);

-- Chamar o procedimento e inserir a segunda compra
CALL SalvarCompra('2023-09-21', 'Nota2', 2, 2);

-- Chamar o procedimento e inserir a terceira compra
CALL SalvarCompra('2023-09-22', 'Nota3', 3, 3);

CREATE TABLE IF NOT EXISTS pagamento (
  id_pag int primary key not null auto_increment,
  
  descricao_pag varchar(200) not null,
  
  valor_pag double,
  
  data_vencimento_pag date,
  data_credenciamento_pag date,
  
  credor_pag varchar(45),
  
  parcelas_pag int,
  
  id_com_fk int,
  foreign key (id_com_fk) references compra(id_com)
);

DELIMITER $$

CREATE PROCEDURE SalvarPagamento 
  (descricao varchar(200), valor double, dataVencimento date, dataCredenciamento date, credor varchar(45), parcelas int, idCompra int)
BEGIN
  DECLARE compraExistente INT;
  
  SELECT COUNT(*) INTO compraExistente FROM compra WHERE id_com = idCompra;
  
  IF compraExistente = 1 THEN
    INSERT INTO pagamento
      (descricao_pag, valor_pag, data_vencimento_pag, data_credenciamento_pag, credor_pag, parcelas_pag, id_com_fk)
    VALUES
      (descricao, valor, dataVencimento, dataCredenciamento, credor, parcelas, idCompra);
    
    SELECT "O pagamento foi registrado com sucesso!" as Confirmacao;
  ELSE
    SELECT "A compra referenciada não existe no banco de dados!" as Erro;
  END IF;
END
$$ DELIMITER ;

-- Chamar o procedimento e inserir o primeiro pagamento
CALL SalvarPagamento('Pagamento 1', 100.0, '2023-09-20', '2023-09-25', 'Credor A', 1, 1);

-- Chamar o procedimento e inserir o segundo pagamento
CALL SalvarPagamento('Pagamento 2', 200.0, '2023-09-21', '2023-09-26', 'Credor B', 2, 2);

-- Chamar o procedimento e inserir o terceiro pagamento
CALL SalvarPagamento('Pagamento 3', 150.0, '2023-09-22', '2023-09-27', 'Credor C', 3, 3);


CREATE TABLE IF NOT EXISTS recebimento (
  id_rec int primary key not null auto_increment,
  
  descricao_rec varchar(200) not null,
  
  valor_rec double,
  
  data_vencimento_rec date,
  data_credenciamento_rec date,
  
  pagador_rec varchar(200) not null,
  parcelas_rec int,
  
  id_loc_fk int,
  foreign key (id_loc_fk) references locacao(id_loc)
);

DELIMITER $$

CREATE PROCEDURE SalvarRecebimento 
  (descricao varchar(200), valor double, dataVencimento date, dataCredenciamento date, pagador varchar(200), parcelas int, idLoc int)
BEGIN
  IF (idLoc IS NULL) OR (NOT EXISTS(SELECT id_loc FROM locacao WHERE id_loc = idLoc)) THEN
    SELECT "A chave estrangeira de locação informada não existe" as Erro;
  ELSE
    INSERT INTO recebimento
      (descricao_rec, valor_rec, data_vencimento_rec, data_credenciamento_rec, pagador_rec, parcelas_rec, id_loc_fk)
    VALUES
      (descricao, valor, dataVencimento, dataCredenciamento, pagador, parcelas, idLoc);

    SELECT "O recebimento foi salvo com sucesso!" as Confirmacao;
  END IF;
END$$

DELIMITER ;

-- Chamar o procedimento e inserir o primeiro recebimento
CALL SalvarRecebimento('Recebimento 1', 100.0, '2023-09-20', '2023-09-25', 'Pagador A', 1, 1);

-- Chamar o procedimento e inserir o segundo recebimento
CALL SalvarRecebimento('Recebimento 2', 200.0, '2023-09-21', '2023-09-26', 'Pagador B', 2, 2);

-- Chamar o procedimento e inserir o terceiro recebimento
CALL SalvarRecebimento('Recebimento 3', 150.0, '2023-09-22', '2023-09-27', 'Pagador C', 3, 3);


CREATE TABLE IF NOT EXISTS parcela (
  id_par int primary key not null auto_increment,
  
  data_vencimento_par date not null,
  
  valor_par double not null,
  forma_pagamento_par varchar(45),
  numero_parcela_par int,
  tipo_parcela_par varchar(45), -- pagamento / recebimento
  status_par tinyint,
  
  id_rec_fk int,
  foreign key (id_rec_fk) references recebimento(id_rec),
  
  id_pag_fk int,
  foreign key (id_pag_fk) references pagamento(id_pag)
);

DELIMITER $$

CREATE PROCEDURE SalvarParcela 
  (dataVencimento date, valor double, formaPagamento varchar(45), numeroParcela int, tipoParcela varchar(45), status tinyint, idRec int, idPag int)
BEGIN
  IF (idRec IS NULL OR idPag IS NULL) THEN
    SELECT "As chaves estrangeiras de recebimento e pagamento são obrigatórias" as Erro;
  ELSE
    INSERT INTO parcela
      (data_vencimento_par, valor_par, forma_pagamento_par, numero_parcela_par, tipo_parcela_par, status_par, id_rec_fk, id_pag_fk)
    VALUES
      (dataVencimento, valor, formaPagamento, numeroParcela, tipoParcela, status, idRec, idPag);

    SELECT "A parcela foi salva com sucesso!" as Confirmacao;
  END IF;
END$$

DELIMITER ;

-- Inserir a primeira parcela
CALL SalvarParcela('2023-09-20', 100.0, 'Cartão de Crédito', 1, 'Recebimento', 1, 1, 1);

-- Inserir a segunda parcela
CALL SalvarParcela('2023-09-21', 200.0, 'Dinheiro', 2, 'Pagamento', 0, 2, 2);

-- Inserir a terceira parcela
CALL SalvarParcela('2023-09-22', 150.0, 'Boleto Bancário', 3, 'Recebimento', 1, 3, 1);
