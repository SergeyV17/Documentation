DO
$do$
BEGIN
IF NOT EXISTS (
      SELECT FROM pg_catalog.pg_roles
      WHERE  rolname = 'vert_slice_templ') THEN
                        

      CREATE ROLE vert_slice_templ  WITH
		  LOGIN
		  NOSUPERUSER
		  NOINHERIT
		  NOCREATEDB
		  NOCREATEROLE
		  NOREPLICATION
		  PASSWORD '$1mlvFL90!pfl';
END IF;
END
$do$;                                                                            

CREATE TABLE IF NOT EXISTS public.person
(
    person_id uuid NOT NULL,
    first_name character varying(20) NOT NULL,
	last_name character varying(20) NOT NULL,
    age integer NOT NULL,
	gender integer NOT NULL,
    CONSTRAINT person_pk PRIMARY KEY (person_id)
);                                             
GRANT UPDATE, INSERT, SELECT, DELETE ON TABLE public.person TO vert_slice_templ;

DROP TYPE IF EXISTS public.person_type;
CREATE TYPE public.person_type AS
(
    person_id uuid,
    first_name character varying(20),
	last_name character varying(20),
    age integer,
	gender integer
);
