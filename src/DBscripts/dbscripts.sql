--database
CREATE DATABASE todo_sampe_db
    WITH 
    OWNER = tempuser
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1;


--schema
CREATE SCHEMA todo
AUTHORIZATION tempuser;


--table
CREATE TABLE IF NOT EXISTS todo."TodoList"
(
    "Id" SERIAL PRIMARY KEY NOT NULL,
    "Title" text NOT NULL,
    "Colour" text NOT NULL,
    "Created" timestamp with time zone NULL,
    "CreatedBy" text NOT NULL,
    "LastModified" timestamp with time zone NULL,
    "LastModifiedBy" text NOT NULL
);

ALTER TABLE todo."TodoList"
    OWNER to tempuser;

--table
CREATE TABLE IF NOT EXISTS todo."TodoItem"
(
    "Id" SERIAL PRIMARY KEY NOT NULL,
    "ListId" integer NOT NULL,
    "Title" text NOT NULL,
    "Note" text NOT NULL,
    "Priority" integer NOT NULL,
    "Reminder" timestamp with time zone NULL,
    "Created" timestamp with time zone NULL,
    "CreatedBy" text NOT NULL,
    "LastModified" timestamp with time zone NULL,
    "LastModifiedBy" text NOT NULL
);

ALTER TABLE todo."TodoItem"
    OWNER to tempuser;






