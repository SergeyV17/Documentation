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