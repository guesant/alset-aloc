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

CREATE TABLE IF NOT EXISTS funcionario (

id_func int primary key not null auto_increment,
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

CREATE TABLE IF NOT EXISTS usuario (

id_usua int primary key not null auto_increment,
usuario_usua varchar(45) not null,
senha_usua varchar(45) not null,

id_func_fk int,
foreign key (id_func_fk) references funcionario(id_func)

);

CREATE TABLE IF NOT EXISTS veiculo (

id_vei int primary key not null auto_increment,
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
data_devolucao_prevista datetime not null,
data_devolucao_efetivada date,
status_loc tinyint not null,

id_vei_fk int,
id_fun_fk int,

foreign key (id_vei_fk) references veiculo(id_vei),
foreign key (id_fun_fk) references funcionario(id_func)

);

CREATE TABLE IF NOT EXISTS cliente_locacao (

id_cli_loc int primary key not null auto_increment,

id_cli_fk int,
id_loc_fk int,

foreign key (id_cli_fk) references cliente(id_cli),
foreign key (id_loc_fk) references locacao(id_loc)

);

CREATE TABLE IF NOT EXISTS fornecedor (

id_forn int primary key not null,
cnpj_forn varchar(20),
razao_social_forn varchar(45),
nome_fantasia_forn varchar(45),
email_forn varchar(60),
telefone_forn varchar(45),

id_end_fk int,
foreign key (id_end_fk) references endereco(id_end)

);

CREATE TABLE IF NOT EXISTS produto (

id_prod int primary key not null,
nome_prod varchar(60),
preco_prod double,
estoque_prod double

);

CREATE TABLE IF NOT EXISTS compra (

id_com int primary key not null,
data_compra_com date,
numero_nota_com varchar(45),

id_prod_fk int,
id_forn_fk int,

foreign key (id_prod_fk) references produto(id_prod),
foreign key (id_forn_fk) references fornecedor(id_forn)

);

CREATE TABLE IF NOT EXISTS pagamento (

id_pag int primary key not null auto_increment,
descricao_ag varchar(200) not null,
valor_pag double,
data_vencimento_pag date,
data_credenciamento_pag date,
credor_pag varchar(45),
parcelar_pag int,

id_com_fk int,
foreign key (id_com_fk) references compra(id_com)
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
foreign key (id_loc_fk) references locacao(id_loc)

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

foreign key (id_rec_fk) references recebimento(id_rec),
foreign key (id_pag_fk) references pagamento(id_pag)

);