DO $$ 
DECLARE 
    types_to_insert text[] := ARRAY['TwoWheeler', 'ThreeWheeler','FourWheeler'];
    type_names text;
BEGIN
    FOR i IN 1 .. array_length(types_to_insert, 1)
    LOOP
        type_names := types_to_insert[i];
        IF NOT EXISTS (
            SELECT 1 FROM public.vehicle_types WHERE name = type_names
        ) THEN
            INSERT INTO public.vehicle_types ( 
                created_by, created_on, updated_by, updated_on, is_active, name
            )
            VALUES 
                (1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, true, type_names, type_names);
        END IF;
    END LOOP;
END $$;





