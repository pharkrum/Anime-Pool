CREATE DATABASE AnimePool;
USE AnimePool;

CREATE TABLE Endereco (
	endereco_id INT PRIMARY KEY NOT NULL,
    cep VARCHAR(64),
    pais VARCHAR(64),
    estado VARCHAR(64),
    cidade VARCHAR(64),
    bairro VARCHAR(64),
    rua VARCHAR(64),
    numero INT
); 

CREATE TABLE Autor (
	autor_id INT PRIMARY KEY NOT NULL,
    nome VARCHAR(64),
    sexo VARCHAR(64),
    produto VARCHAR(64)
);

CREATE TABLE Estudio (
	estudio_id INT PRIMARY KEY NOT NULL,
    nome VARCHAR(64),
    CNPJ VARCHAR(64),
    endereco INT NOT NULL,
    FOREIGN KEY (endereco) REFERENCES Endereco(endereco_id)
); 

CREATE TABLE Editora (
	editora_id INT PRIMARY KEY NOT NULL,
    nome VARCHAR(64),
    CNPJ VARCHAR(64),
    endereco INT NOT NULL,
    FOREIGN KEY (endereco) REFERENCES Endereco(endereco_id)
); 

CREATE TABLE Anime (
	anime_id INT PRIMARY KEY NOT NULL,
    nome VARCHAR(64),
    genero VARCHAR(64),
    episodios INT,
    estado VARCHAR(64),
    ano VARCHAR(64),
    estudio INT NOT NULL,
    FOREIGN KEY (estudio) REFERENCES Estudio(estudio_id)
); 

CREATE TABLE Manga (
	manga_id INT PRIMARY KEY NOT NULL,
    nome VARCHAR(64),
    tipo VARCHAR(64),
    genero INT,
    volumes INT, 
    estado VARCHAR(64),
    ano VARCHAR(64),
    autor INT NOT NULL,
    editora INT NOT NULL,
    FOREIGN KEY (autor) REFERENCES Autor(autor_id),
    FOREIGN KEY (editora) REFERENCES Editora(editora_id)
); 

CREATE TABLE Filme (
	filme_id INT PRIMARY KEY NOT NULL,
    nome VARCHAR(64),
    genero INT,
    duracao INT, 
    estado VARCHAR(64),
    ano VARCHAR(64),
    estudio INT NOT NULL,
    FOREIGN KEY (estudio) REFERENCES Estudio(estudio_id)
); 

