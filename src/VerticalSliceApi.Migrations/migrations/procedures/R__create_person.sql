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