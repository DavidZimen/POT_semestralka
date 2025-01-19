-- PostgreSQL script to populate the database with real-world inspired data for Films, Shows, and related entities

-- Users Table
INSERT INTO "user" VALUES ('c4829046-a1a8-40f3-b677-44e404d1ab0d', true);

INSERT INTO "user" (id, enabled)
SELECT
    md5(random()::varchar(36)) AS random_string,
    true
FROM generate_series(1, 50) g;

-- Persons Table (Actors and Directors)
INSERT INTO person (id, first_name, middle_name, last_name, bio, birth_date, country, created_at, modified_at, modified_by)
VALUES
    (gen_random_uuid(), 'Leonardo', NULL, 'DiCaprio', 'Actor known for Titanic, Inception, and The Revenant.', '1974-11-11', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Quentin', NULL, 'Tarantino', 'Director known for Pulp Fiction and Django Unchained.', '1963-03-27', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Emma', NULL, 'Watson', 'Actress known for Harry Potter and Beauty and the Beast.', '1990-04-15', 'GB', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Christopher', NULL, 'Nolan', 'Director known for Inception, Interstellar, and The Dark Knight.', '1970-07-30', 'GB', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Scarlett', NULL, 'Johansson', 'Actress known for Black Widow and Marriage Story.', '1984-11-22', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Robert', NULL, 'Downey Jr.', 'Actor known for Iron Man and Sherlock Holmes.', '1965-04-04', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Chris', NULL, 'Evans', 'Actor known for Captain America and The Avengers.', '1981-06-13', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Chris', NULL, 'Hemsworth', 'Actor known for Thor and The Avengers.', '1983-08-11', 'AU', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Mark', NULL, 'Ruffalo', 'Actor known for The Hulk and The Avengers.', '1967-11-22', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Tom', NULL, 'Holland', 'Actor known for Spider-Man in the MCU.', '1996-06-01', 'GB', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Benedict', NULL, 'Cumberbatch', 'Actor known for Doctor Strange.', '1976-07-19', 'GB', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Brie', NULL, 'Larson', 'Actress known for Captain Marvel.', '1989-10-01', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Taika', NULL, 'Waititi', 'Director known for Thor: Ragnarok.', '1975-08-16', 'NZ', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'James', NULL, 'Gunn', 'Director known for Guardians of the Galaxy.', '1966-08-05', 'US', NOW(), NOW(), 'generated');

-- Directors Table
INSERT INTO director (id, person_id, created_at, modified_at, modified_by)
SELECT gen_random_uuid(), id, NOW(), NOW(), 'generated'
FROM person
WHERE last_name IN ('Tarantino', 'Nolan', 'Waititi', 'Gunn');

-- Actors Table
INSERT INTO actor (id, person_id, created_at, modified_at, modified_by)
SELECT gen_random_uuid(), id, NOW(), NOW(), 'generated'
FROM person
WHERE last_name IN ('DiCaprio', 'Watson', 'Johansson', 'Downey Jr.', 'Evans', 'Hemsworth', 'Ruffalo', 'Holland', 'Cumberbatch', 'Larson');

-- Films Table
INSERT INTO film (id, title, description, release_date, duration, director_id, created_at, modified_at, modified_by)
VALUES
    (gen_random_uuid(), 'Iron Man', 'The origin story of Tony Stark becoming Iron Man.', '2008-05-02', 126, (SELECT id FROM director WHERE person_id = (SELECT id FROM person WHERE last_name = 'Nolan')), NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Thor: Ragnarok', 'Thor must escape the planet Sakaar to save Asgard.', '2017-11-03', 130, (SELECT id FROM director WHERE person_id = (SELECT id FROM person WHERE last_name = 'Waititi')), NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'The Avengers', 'Earths mightiest heroes must team up to stop Loki.', '2012-05-04', 143, (SELECT id FROM director WHERE person_id = (SELECT id FROM person WHERE last_name = 'Tarantino')), NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Guardians of the Galaxy', 'A group of intergalactic criminals band together to save the universe.', '2014-08-01', 121, (SELECT id FROM director WHERE person_id = (SELECT id FROM person WHERE last_name = 'Gunn')), NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Doctor Strange', 'A neurosurgeon learns the mystic arts to protect Earth.', '2016-11-04', 115, (SELECT id FROM director WHERE person_id = (SELECT id FROM person WHERE last_name = 'Waititi')), NOW(), NOW(), 'generated');

-- Characters for Films
DO $$
    DECLARE
        film_rec RECORD;
    BEGIN
        FOR film_rec IN SELECT id FROM film LOOP
                INSERT INTO character (id, name, film_id, actor_id, created_at, modified_at, modified_by)
                SELECT
                    gen_random_uuid(),
                    CONCAT('Character ', row_number() OVER()),
                    film_rec.id,
                    actor.id,
                    NOW(),
                    NOW(),
                    'generated'
                FROM (SELECT id FROM actor ORDER BY random() LIMIT 3) actor;
            END LOOP;
    END $$;

-- Shows Table
INSERT INTO show (id, title, description, release_date, end_date, created_at, modified_at, modified_by)
VALUES
    (gen_random_uuid(), 'Stranger Things', 'Kids uncover government secrets and supernatural entities.', '2016-07-15', NULL, NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Game of Thrones', 'Noble families vie for control of Westeros.', '2011-04-17', '2019-05-19', NOW(), NOW(), 'generated');

-- Characters for Shows
DO $$
    DECLARE
        show_rec RECORD;
    BEGIN
        FOR show_rec IN SELECT id FROM show LOOP
                INSERT INTO character (id, name, show_id, actor_id, created_at, modified_at, modified_by)
                SELECT
                    gen_random_uuid(),
                    CONCAT('Character ', row_number() OVER()),
                    show_rec.id,
                    actor.id,
                    NOW(),
                    NOW(),
                    'generated'
                FROM (SELECT id FROM actor ORDER BY random() LIMIT 3) actor;
            END LOOP;
    END $$;

-- Seasons and Episodes for Shows
DO $$
    DECLARE
        show_rec RECORD;
        season_counter INT := 1;
    BEGIN
        FOR show_rec IN SELECT id FROM show LOOP
            FOR i IN 1..5 LOOP
                        INSERT INTO season (id, title, description, show_id, created_at, modified_at, modified_by)
                        VALUES (
                            gen_random_uuid(),
                            CONCAT('Season ', i),
                            md5(random()::text),   
                            show_rec.id,
                            NOW(),
                            NOW(),
                            'generated'
                            );
    
                FOR j IN 1..10 LOOP
                                INSERT INTO episode (id, title, description, release_data, duration, season_id, director_id, created_at, modified_at, modified_by)
                                VALUES (
                                    gen_random_uuid(),
                                    CONCAT('Episode ', j),
                                    md5(random()::text),
                                    CURRENT_DATE - (random() * (365 * 10))::int,
                                    20 + FLOOR(random() * 51),
                                    (SELECT id FROM season WHERE show_id = show_rec.id AND title = CONCAT('Season ', i) LIMIT 1),
                                    (SELECT id FROM director ORDER BY random() LIMIT 1),
                                    NOW(),
                                    NOW(),
                                    'generated'
                                );
                END LOOP;
            END LOOP;
        END LOOP;
    END $$;

-- Ratings for Films, Shows, and Episodes
DO $$
    DECLARE
        film_rec RECORD;
        show_rec RECORD;
        episode_rec RECORD;
        user_rec RECORD;
        rating_score INT;
    BEGIN
        -- Generate ratings for Films
        FOR film_rec IN SELECT id FROM film LOOP
                FOR user_rec IN SELECT id FROM "user" LIMIT 50 LOOP
                        rating_score := floor(random() * 10 + 1)::int;
                        INSERT INTO rating (id, user_id, film_id, value, description)
                        VALUES (
                                   gen_random_uuid(),
                                   user_rec.id,
                                   film_rec.id,
                                   rating_score,
                                   md5(random()::text)
                               );
                    END LOOP;
            END LOOP;
    
        -- Generate ratings for Shows
        FOR show_rec IN SELECT id FROM show LOOP
                FOR user_rec IN SELECT id FROM "user" LIMIT 50 LOOP
                        rating_score := floor(random() * 10 + 1)::int;
                        INSERT INTO rating (id, user_id, show_id, value, description)
                        VALUES (
                                   gen_random_uuid(),
                                   user_rec.id,
                                   show_rec.id,
                                   rating_score,
                                   md5(random()::text)
                               );
                    END LOOP;
            END LOOP;
    
        -- Generate ratings for Episodes
        FOR episode_rec IN SELECT id FROM episode LOOP
                FOR user_rec IN SELECT id FROM "user" LIMIT 50 LOOP
                        rating_score := floor(random() * 10 + 1)::int;
                        INSERT INTO rating (id, user_id, episode_id, value, description)
                        VALUES (
                                   gen_random_uuid(),
                                   user_rec.id,
                                   episode_rec.id,
                                   rating_score,
                                   md5(random()::text)
                               );
                    END LOOP;
            END LOOP;
    END $$;

-- Genres Table
INSERT INTO genre (id, name)
VALUES
    (gen_random_uuid(), 'Action'),
    (gen_random_uuid(), 'Drama'),
    (gen_random_uuid(), 'Thriller'),
    (gen_random_uuid(), 'Sci-Fi'),
    (gen_random_uuid(), 'Romance'),
    (gen_random_uuid(), 'Comedy'),
    (gen_random_uuid(), 'Adventure');

-- Assign Genres to Films
DO $$
    DECLARE
        film_rec RECORD;
    BEGIN
        FOR film_rec IN SELECT id FROM film LOOP
                INSERT INTO film_genre (film_id, genre_id)
                SELECT film_rec.id, genre.id
                FROM (SELECT id FROM genre ORDER BY random() LIMIT 2) genre;
            END LOOP;
    END $$;

COMMIT;
