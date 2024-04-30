DO $$ 
DECLARE 
    roles_to_insert text[] := ARRAY['Admin', 'User'];
    role_name text;
BEGIN
    FOR i IN 1 .. array_length(roles_to_insert, 1)
    LOOP
        role_name := roles_to_insert[i];
        IF NOT EXISTS (
            SELECT 1 FROM public.role WHERE name = role_name
        ) THEN
            INSERT INTO public.role ( 
                created_by, created_on, updated_by, updated_on, is_active, name, note
            )
            VALUES 
                (1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, true, role_name, role_name);
        END IF;
    END LOOP;
END $$;







