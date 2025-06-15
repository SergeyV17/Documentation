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