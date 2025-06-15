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
	
CREATE OR REPLACE FUNCTION public.get_persons()
    RETURNS TABLE
	(
		person_id uuid,
    	full_name text
	) 
    LANGUAGE 'plpgsql'
AS $BODY$
BEGIN	
	RETURN QUERY
	SELECT 
		persons.person_id, 
		concat_ws(' ', persons.first_name, persons.last_name)		AS full_name
	FROM public.person												AS persons;
END;
$BODY$;                                                           
GRANT EXECUTE ON FUNCTION public.get_persons() TO vert_slice_templ;

CREATE OR REPLACE FUNCTION public.get_person_by_id(p_person_id uuid)
    RETURNS SETOF public.person
    LANGUAGE 'plpgsql'

AS $BODY$
BEGIN	
	RETURN QUERY
	SELECT *
	FROM public.person 
	WHERE person_id = p_person_id;
END;
$BODY$;                                                                                
GRANT EXECUTE ON FUNCTION public.get_person_by_id(p_person_id uuid) TO vert_slice_templ;

CREATE OR REPLACE PROCEDURE public.create_person(IN p_person person_type)
LANGUAGE 'plpgsql'
AS $BODY$

BEGIN
    INSERT  INTO public.person 
	(
		person_id,
		first_name,
		last_name,
		age,
		gender
	)
    VALUES 
	(
		p_person.person_id,
		p_person.first_name,
		p_person.last_name,
		p_person.age,
		p_person.gender
	);
END;
$BODY$;
GRANT EXECUTE ON PROCEDURE public.create_person(IN p_person person_type) TO vert_slice_templ;

CREATE OR REPLACE PROCEDURE public.update_person(IN p_person person_type)
LANGUAGE 'plpgsql'
AS $BODY$

BEGIN
    UPDATE public.person 
	SET
		first_name = p_person.first_name,
		last_name = p_person.last_name,
		age = p_person.age,
		gender = p_person.gender
	WHERE person_id = p_person.person_id;
END;
$BODY$;
GRANT EXECUTE ON PROCEDURE public.update_person(IN p_person person_type) TO vert_slice_templ;

CREATE OR REPLACE PROCEDURE public.delete_person(p_person_id uuid)
LANGUAGE 'plpgsql'
AS $BODY$

BEGIN
    DELETE FROM public.person 
	WHERE person_id = person.person_id;
END;
$BODY$;
GRANT EXECUTE ON PROCEDURE public.delete_person(p_person_id uuid) TO vert_slice_templ;       