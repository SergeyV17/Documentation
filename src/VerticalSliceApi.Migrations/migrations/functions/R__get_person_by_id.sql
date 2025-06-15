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