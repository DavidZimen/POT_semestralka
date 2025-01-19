-- PostgreSQL truncate script to clear all data before repopulating

-- Disable foreign key constraints temporarily
SET session_replication_role = 'replica';

-- Truncate dependent tables
TRUNCATE TABLE rating CASCADE ;
TRUNCATE TABLE film_genre CASCADE;
TRUNCATE TABLE episode CASCADE;
TRUNCATE TABLE season CASCADE;
TRUNCATE TABLE character CASCADE;

-- Truncate main tables
TRUNCATE TABLE film CASCADE;
TRUNCATE TABLE show CASCADE;
TRUNCATE TABLE genre CASCADE;
TRUNCATE TABLE actor CASCADE;
TRUNCATE TABLE director CASCADE;
TRUNCATE TABLE person CASCADE;
TRUNCATE TABLE "user" CASCADE;

-- Re-enable foreign key constraints
SET session_replication_role = 'origin';