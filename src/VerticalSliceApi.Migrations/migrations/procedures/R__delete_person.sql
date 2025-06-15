CREATE OR REPLACE PROCEDURE public.delete_person(p_person_id uuid)
    LANGUAGE 'plpgsql'
AS $BODY$

BEGIN
    DELETE FROM public.person
    WHERE person_id = person.person_id;
END;
$BODY$;
GRANT EXECUTE ON PROCEDURE public.delete_person(p_person_id uuid) TO vert_slice_templ;   