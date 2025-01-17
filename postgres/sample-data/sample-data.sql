-- PostgreSQL script to populate the database with real-world inspired data for Films, Shows, and related entities

-- Users Table
INSERT INTO Users (id, username, email, password_hash, created_at, updated_at)
SELECT
    gen_random_uuid(),
    CONCAT('user', g) AS username,
    CONCAT('user', g, '@example.com') AS email,
    md5(random()::text) AS password_hash,
    NOW() AS created_at,
    NOW() AS updated_at
FROM generate_series(1, 50) g;

-- Persons Table (Actors and Directors)
INSERT INTO Persons (id, first_name, middle_name, last_name, bio, birth_date, country, created_at, updated_at, last_modified_by)
VALUES
    (gen_random_uuid(), 'Leonardo', NULL, 'DiCaprio', 'Actor known for Titanic, Inception, and The Revenant.', '1974-11-11', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Quentin', NULL, 'Tarantino', 'Director known for Pulp Fiction and Django Unchained.', '1963-03-27', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Emma', NULL, 'Watson', 'Actress known for Harry Potter and Beauty and the Beast.', '1990-04-15', 'GB', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Christopher', NULL, 'Nolan', 'Director known for Inception, Interstellar, and The Dark Knight.', '1970-07-30', 'GB', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Scarlett', NULL, 'Johansson', 'Actress known for Black Widow and Marriage Story.', '1984-11-22', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Robert', NULL, 'Downey Jr.', 'Actor known for Iron Man and Sherlock Holmes.', '1965-04-04', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Natalie', NULL, 'Portman', 'Actress known for Black Swan and Thor.', '1981-06-09', 'IL', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'James', NULL, 'Cameron', 'Director known for Titanic and Avatar.', '1954-08-16', 'CA', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Tom', NULL, 'Hanks', 'Actor known for Forrest Gump and Cast Away.', '1956-07-09', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Meryl', NULL, 'Streep', 'Actress known for The Devil Wears Prada and Mamma Mia.', '1949-06-22', 'US', NOW(), NOW(), 'generated');

-- Directors Table
INSERT INTO Directors (id, person_id, created_at, updated_at, last_modified_by)
SELECT gen_random_uuid(), id, NOW(), NOW(), 'generated'
FROM Persons
WHERE last_name IN ('Tarantino', 'Nolan', 'Cameron');

-- Actors Table
INSERT INTO Actors (id, person_id, created_at, updated_at, last_modified_by)
SELECT gen_random_uuid(), id, NOW(), NOW(), 'generated'
FROM Persons
WHERE last_name IN ('DiCaprio', 'Watson', 'Johansson', 'Downey Jr.', 'Portman', 'Hanks', 'Streep');

-- Films Table
INSERT INTO Films (id, title, description, release_date, duration, director_id, created_at, updated_at, last_modified_by)
VALUES
    (gen_random_uuid(), 'Inception', 'A skilled thief enters dreams to steal secrets.', '2010-07-16', 148, (SELECT id FROM Directors WHERE person_id = (SELECT id FROM Persons WHERE last_name = 'Nolan')), NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Titanic', 'A love story on the ill-fated RMS Titanic.', '1997-12-19', 195, (SELECT id FROM Directors WHERE person_id = (SELECT id FROM Persons WHERE last_name = 'Cameron')), NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Pulp Fiction', 'The lives of criminals intertwine in Los Angeles.', '1994-10-14', 154, (SELECT id FROM Directors WHERE person_id = (SELECT id FROM Persons WHERE last_name = 'Tarantino')), NOW(), NOW(), 'generated');

-- Shows Table
INSERT INTO Shows (id, title, description, release_date, end_date, created_at, updated_at, last_modified_by)
VALUES
    (gen_random_uuid(), 'Stranger Things', 'Kids uncover government secrets and supernatural entities.', '2016-07-15', NULL, NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Game of Thrones', 'Noble families vie for control of Westeros.', '2011-04-17', '2019-05-19', NOW(), NOW(), 'generated');

-- Seasons and Episodes for Shows
DO $$
DECLARE
show_rec RECORD;
    season_counter INT := 1;
BEGIN
FOR show_rec IN SELECT id FROM Shows LOOP
    FOR i IN 1..5 LOOP
                INSERT INTO Seasons (id, title, show_id, created_at, updated_at, last_modified_by)
                VALUES (
                    gen_random_uuid(),
                    CONCAT('Season ', i),
                    show_rec.id,
                    NOW(),
                    NOW(),
                    'generated'
                    );

FOR j IN 1..10 LOOP
                INSERT INTO Episodes (id, title, season_id, director_id, created_at, updated_at, last_modified_by)
                VALUES (
                    gen_random_uuid(),
                    CONCAT('Episode ', j),
                    (SELECT id FROM Seasons WHERE show_id = show_rec.id AND title = CONCAT('Season ', i) LIMIT 1),
                    (SELECT id FROM Directors ORDER BY random() LIMIT 1),
                    NOW(),
                    NOW(),
                    'generated'
                );
END LOOP;
END LOOP;
END LOOP;
END $$;
