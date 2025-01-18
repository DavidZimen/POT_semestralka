-- PostgreSQL script to populate the database with real-world inspired data for Films, Shows, and related entities

-- Users Table
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
    (gen_random_uuid(), 'Natalie', NULL, 'Portman', 'Actress known for Black Swan and Thor.', '1981-06-09', 'IL', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'James', NULL, 'Cameron', 'Director known for Titanic and Avatar.', '1954-08-16', 'CA', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Tom', NULL, 'Hanks', 'Actor known for Forrest Gump and Cast Away.', '1956-07-09', 'US', NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Meryl', NULL, 'Streep', 'Actress known for The Devil Wears Prada and Mamma Mia.', '1949-06-22', 'US', NOW(), NOW(), 'generated');

-- Directors Table
INSERT INTO director (id, person_id, created_at, modified_at, modified_by)
SELECT gen_random_uuid(), id, NOW(), NOW(), 'generated'
FROM person
WHERE last_name IN ('Tarantino', 'Nolan', 'Cameron');

-- Actors Table
INSERT INTO actor (id, person_id, created_at, modified_at, modified_by)
SELECT gen_random_uuid(), id, NOW(), NOW(), 'generated'
FROM person
WHERE last_name IN ('DiCaprio', 'Watson', 'Johansson', 'Downey Jr.', 'Portman', 'Hanks', 'Streep');

-- Films Table
INSERT INTO film (id, title, description, release_date, duration, director_id, created_at, modified_at, modified_by)
VALUES
    (gen_random_uuid(), 'Inception', 'A skilled thief enters dreams to steal secrets.', '2010-07-16', 148, (SELECT id FROM director WHERE person_id = (SELECT id FROM person WHERE last_name = 'Nolan')), NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Titanic', 'A love story on the ill-fated RMS Titanic.', '1997-12-19', 195, (SELECT id FROM director WHERE person_id = (SELECT id FROM person WHERE last_name = 'Cameron')), NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Pulp Fiction', 'The lives of criminals intertwine in Los Angeles.', '1994-10-14', 154, (SELECT id FROM director WHERE person_id = (SELECT id FROM person WHERE last_name = 'Tarantino')), NOW(), NOW(), 'generated');

-- Shows Table
INSERT INTO show (id, title, description, release_date, end_date, created_at, modified_at, modified_by)
VALUES
    (gen_random_uuid(), 'Stranger Things', 'Kids uncover government secrets and supernatural entities.', '2016-07-15', NULL, NOW(), NOW(), 'generated'),
    (gen_random_uuid(), 'Game of Thrones', 'Noble families vie for control of Westeros.', '2011-04-17', '2019-05-19', NOW(), NOW(), 'generated');

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

COMMIT;
