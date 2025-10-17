\c "LibraryServiceDb";

-- Заполнение таблицы Libraries
INSERT INTO "Libraries" (id, library_uid, name, city, address)
VALUES (1, '83575e12-7ce0-48ee-9931-51919ff3c9ee'::uuid, 'Библиотека имени 7 Непьющих', 'Москва', '2-я Бауманская ул., д.5, стр.1');

-- Заполнение таблицы Books
INSERT INTO "Books" (id, book_uid, name, author, genre, condition)
VALUES (1, 'f7cdc58f-2caf-4b15-9727-f89dcc629b27'::uuid, 'Краткий курс C++ в 7 томах', 'Бьерн Страуструп', 'Научная фантастика', 0);

-- Заполнение таблицы library_books
INSERT INTO library_books (book_id, library_id, available_count)
VALUES (1, 1, 1);

\c "RatingServiceDb";

INSERT INTO rating (id, username, stars) VALUES (1, 'Test Max', 75);