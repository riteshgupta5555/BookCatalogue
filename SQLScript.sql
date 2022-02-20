CREATE TABLE Books (
    book_id int PRIMARY KEY,
    book_title varchar(255),
	author_name varchar(255),
    isbn varchar(13),
    publication_date date,
);


INSERT INTO Books values(100,'Learn with Mosh','Mosh','1234567890987','2022-02-20'),
(101,'Introduction to WEB API','Ritesh','1234567890988','2022-02-19'),(102,'SQL Server','Naveen','1234567890989','2022-02-18');



SELECT * from Books




