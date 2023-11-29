drop database if exists aloc;

CREATE DATABASE aloc;

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
    
    id_end_fk int null,
    foreign key (id_end_fk) references endereco(id_end) on delete set null
);

CREATE TABLE IF NOT EXISTS funcionario (
    id_func int primary key auto_increment not null,

    nome_func varchar(300) not null,
    
    data_nascimento_func date not null,
    
    cpf_func varchar(11) not null,
    rg_func varchar(45) null,

    cnh_func varchar(200),

    email_func varchar(45) not null,
    telefone_func varchar(45) not null,
    genero_func varchar(45),

    cargo_func varchar(200) not null,

    id_end_fk int,
    foreign key (id_end_fk) references endereco(id_end) on delete set null
);

CREATE TABLE IF NOT EXISTS usuario (
    id_usua int primary key auto_increment not null,

    usuario_usua varchar(45) not null,
    senha_usua varchar(45) not null,

    id_func_fk int,
    foreign key (id_func_fk) references funcionario(id_func) on delete cascade
);

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


CREATE TABLE IF NOT EXISTS locacao (
    id_loc int primary key not null auto_increment,

    data_locacao_loc datetime not null,
    data_devolucao_prevista datetime null,
    data_devolucao_efetivada date,
    status_loc tinyint not null,

    valor_diaria_loc double,

    id_vei_fk int,
    foreign key (id_vei_fk) references veiculo(id_vei) on delete cascade,
    
    id_fun_fk int,
    foreign key (id_fun_fk) references funcionario(id_func) on delete set null,

    id_cli_fk int,
    foreign key (id_cli_fk) references cliente(id_cli) on delete set null
);

CREATE TABLE IF NOT EXISTS fornecedor (

    id_forn int primary key not null auto_increment,

    cnpj_forn varchar(20),
    razao_social_forn varchar(45),
    nome_fantasia_forn varchar(45),
    email_forn varchar(60),
    telefone_forn varchar(45),

    id_end_fk int,
    foreign key (id_end_fk) references endereco(id_end) on delete set null

);

select * from fornecedor;

CREATE TABLE IF NOT EXISTS produto (

id_prod int primary key not null auto_increment,
nome_prod varchar(60),
preco_prod double,
estoque_prod double

);

CREATE TABLE IF NOT EXISTS compra (

id_com int primary key not null auto_increment,
data_compra_com date,
numero_nota_com varchar(45),
quantidade_com double,

id_prod_fk int,
id_forn_fk int,

foreign key (id_prod_fk) references produto(id_prod) on delete cascade,
foreign key (id_forn_fk) references fornecedor(id_forn) on delete set null

);

CREATE TABLE IF NOT EXISTS pagamento (

id_pag int primary key not null auto_increment,
descricao_pag varchar(200) not null,
valor_pag double,
data_vencimento_pag date,
data_credenciamento_pag date,
credor_pag varchar(45),
parcelas_pag int,

id_com_fk int,
foreign key (id_com_fk) references compra(id_com) on delete cascade
);

CREATE TABLE IF NOT EXISTS recebimento (

id_rec int primary key not null auto_increment,
descricao_rec varchar(200) not null,
valor_rec double,
data_vencimento_rec date,
data_credenciamento_rec date,
pagador_rec varchar(200) not null,
parcelas_rec int,

id_loc_fk int,
foreign key (id_loc_fk) references locacao(id_loc) on delete cascade

);

CREATE TABLE IF NOT EXISTS parcela (

id_par int primary key not null auto_increment,
data_vencimento_par date not null,
valor_par double not null,
forma_pagamento_par varchar(45),
numero_parcela_par int,
tipo_parcela_par varchar(45),
status_par tinyint,

id_rec_fk int,
id_pag_fk int,

foreign key (id_rec_fk) references recebimento(id_rec) on delete cascade,
foreign key (id_pag_fk) references pagamento(id_pag) on delete cascade

);


select * from funcionario;
INSERT INTO endereco (pais_end, codigo_postal_end, uf_end, cidade_end, rua_end, numero_end, bairro_end, complemento_end)
VALUES ('Brasil', '123456', 'RO', 'Ji-parana', 'Rua das Ruas', 1528, 'Bairro dos bairros', 'Logradouro');

INSERT INTO cliente (nome_cli, data_nascimento_cli, cpf_cli, rg_cli, cnh_cli, email_cli, telefone_cli, genero_cli, id_end_fk)
VALUES ('Nome Cliente', '1990-01-01', '12345678901', '01234567890', '12345678901', 'cliente@email.com', '123-456-7890', 'Masculino', 1);

INSERT INTO funcionario (nome_func, data_nascimento_func, cpf_func, rg_func, cnh_func, email_func, telefone_func, genero_func, cargo_func, id_end_fk)
VALUES ('Nome Funcionario', '1980-02-15', '98765432101', '567890123', '123456789012', 'funcionario@email.com', '987-654-3210', 'Feminino', 'Cargo Funcionario', 2);


-- Insert into veiculo table
INSERT INTO veiculo (modelo_vei, marca_vei, ano_vei, placa_vei, numero_chassi_vei, cor_vei, data_compra_vei, descricao_vei)
VALUES ('Modelo Carro', 'Marca Carro', 2022, 'ABC123', '12345678901234567', 'Azul', '2023-01-01', 'Descrição do veículo');

-- Insert into fornecedor table
INSERT INTO fornecedor (cnpj_forn, razao_social_forn, nome_fantasia_forn, email_forn, telefone_forn, id_end_fk)
VALUES ('12345678901234', 'Razão Social', 'Nome Fantasia', 'fornecedor@email.com', '789-456-1230', 3);

INSERT INTO produto (nome_prod, preco_prod, estoque_prod)
VALUES ('Produto A', 49.99, 100);

INSERT INTO pagamento (descricao_pag, valor_pag, data_vencimento_pag, data_credenciamento_pag, credor_pag, parcelas_pag, id_com_fk)
VALUES ('Pagamento 1', 499.90, '2023-03-01', '2023-03-02', 'Credor 1', 1, 1);

-- Insert into recebimento table
INSERT INTO recebimento (descricao_rec, valor_rec, data_vencimento_rec, data_credenciamento_rec, pagador_rec, parcelas_rec, id_loc_fk)
VALUES ('Recebimento 1', 499.90, '2023-03-01', '2023-03-02', 'Pagador 1', 1, 1);

-- Insert into parcela table
INSERT INTO parcela (data_vencimento_par, valor_par, forma_pagamento_par, numero_parcela_par, tipo_parcela_par, status_par, id_rec_fk, id_pag_fk)
VALUES ('2023-03-01', 499.90, 'Boleto', 1, 'Parcela Única', 1, 1, 1);


-- Additional inserts for endereco table
INSERT INTO endereco (pais_end, codigo_postal_end, uf_end, cidade_end, rua_end, numero_end, bairro_end, complemento_end)
VALUES ('Espanha', '567890', 'MD', 'Madrid', 'Calle Principal', 789, 'Barrio Central', 'Apartamento 302');

INSERT INTO endereco (pais_end, codigo_postal_end, uf_end, cidade_end, rua_end, numero_end, bairro_end, complemento_end)
VALUES ('Estados Unidos', '90210', 'CA', 'Beverly Hills', 'Sunset Boulevard', 1234, 'Luxury District', 'Suite 567');

-- Additional inserts for cliente table
INSERT INTO cliente (nome_cli, data_nascimento_cli, cpf_cli, rg_cli, cnh_cli, email_cli, telefone_cli, genero_cli, id_end_fk)
VALUES ('Cliente Adicional 1', '1985-06-20', '98765432102', '5678901234', '98765432102', 'cliente2@email.com', '987-654-3211', 'Feminino', 2);

INSERT INTO cliente (nome_cli, data_nascimento_cli, cpf_cli, rg_cli, cnh_cli, email_cli, telefone_cli, genero_cli, id_end_fk)
VALUES ('Cliente Adicional 2', '1992-03-12', '87654321098', '5432109876', '87654321098', 'cliente3@email.com', '876-543-2109', 'Masculino', 3);

-- Additional inserts for funcionario table
INSERT INTO funcionario (nome_func, data_nascimento_func, cpf_func, rg_func, cnh_func, email_func, telefone_func, genero_func, cargo_func, id_end_fk)
VALUES ('Funcionario Adicional 1', '1982-10-25', '87654321021', '543210987', '87654321021', 'funcionario2@email.com', '876-543-2108', 'Masculino', 'Cargo Funcionario 2', 3);

INSERT INTO funcionario (nome_func, data_nascimento_func, cpf_func, rg_func, cnh_func, email_func, telefone_func, genero_func, cargo_func, id_end_fk)
VALUES ('Funcionario Adicional 2', '1990-07-15', '76543210987', '4321098765', '76543210987', 'funcionario3@email.com', '765-432-1098', 'Feminino', 'Cargo Funcionario 3', 1);



-- Additional inserts for veiculo table
INSERT INTO veiculo (modelo_vei, marca_vei, ano_vei, placa_vei, numero_chassi_vei, cor_vei, data_compra_vei, descricao_vei)
VALUES ('Modelo Carro 2', 'Marca Carro 2', 2021, 'XYZ789', '98765432109876543', 'Preto', '2023-02-15', 'Descrição do veículo 2');

INSERT INTO veiculo (modelo_vei, marca_vei, ano_vei, placa_vei, numero_chassi_vei, cor_vei, data_compra_vei, descricao_vei)
VALUES ('Modelo Carro 3', 'Marca Carro 3', 2023, 'JKL012', '54321098765432109', 'Branco', '2023-04-01', 'Descrição do veículo 3');

-- Additional inserts for fornecedor table
INSERT INTO fornecedor (cnpj_forn, razao_social_forn, nome_fantasia_forn, email_forn, telefone_forn, id_end_fk)
VALUES ('98765432109876', 'Razão Social 2', 'Nome Fantasia 2', 'fornecedor2@email.com', '987-654-3211', 1);

INSERT INTO fornecedor (cnpj_forn, razao_social_forn, nome_fantasia_forn, email_forn, telefone_forn, id_end_fk)
VALUES ('87654321098765', 'Razão Social 3', 'Nome Fantasia 3', 'fornecedor3@email.com', '876-543-2109', 2);

-- Additional inserts for produto table
INSERT INTO produto (nome_prod, preco_prod, estoque_prod)
VALUES ('Produto B', 29.99, 50);

INSERT INTO produto (nome_prod, preco_prod, estoque_prod)
VALUES ('Produto C', 39.99, 75);

-- Additional inserts for compra table
-- Additional inserts for pagamento table
INSERT INTO pagamento (descricao_pag, valor_pag, data_vencimento_pag, data_credenciamento_pag, credor_pag, parcelas_pag, id_com_fk)
VALUES ('Pagamento 2', 299.90, '2023-04-01', '2023-04-02', 'Credor 2', 1, 2);

INSERT INTO pagamento (descricao_pag, valor_pag, data_vencimento_pag, data_credenciamento_pag, credor_pag, parcelas_pag, id_com_fk)
VALUES ('Pagamento 3', 399.90, '2023-05-01', '2023-05-02', 'Credor 3', 1, 3);

-- Additional inserts for recebimento table
INSERT INTO recebimento (descricao_rec, valor_rec, data_vencimento_rec, data_credenciamento_rec, pagador_rec, parcelas_rec, id_loc_fk)
VALUES ('Recebimento 2', 299.90, '2023-04-01', '2023-04-02', 'Pagador 2', 1, 2);

INSERT INTO recebimento (descricao_rec, valor_rec, data_vencimento_rec, data_credenciamento_rec, pagador_rec, parcelas_rec, id_loc_fk)
VALUES ('Recebimento 3', 399.90, '2023-05-01', '2023-05-02', 'Pagador 3', 1, 3);

-- Additional inserts for parcela table
INSERT INTO parcela (data_vencimento_par, valor_par, forma_pagamento_par, numero_parcela_par, tipo_parcela_par, status_par, id_rec_fk, id_pag_fk)
VALUES ('2023-04-01', 
    

-- -- Inserir um funcionário
-- INSERT INTO funcionario (nome_func, data_nascimento_func, cpf_func, rg_func, email_func, telefone_func, genero_func, id_end_fk)
-- VALUES ('João da Silva', '1990-05-15', '12345678901', '789012345', 'joao.silva@email.com', '123-456-7890', 'Masculino', 1);

-- -- Inserir outro funcionário
-- INSERT INTO funcionario (nome_func, data_nascimento_func, cpf_func, rg_func, email_func, telefone_func, genero_func, id_end_fk)
-- VALUES ('Maria Souza', '1985-03-20', '98765432101', '567890123', 'maria.souza@email.com', '987-654-3210', 'Feminino', 2);
