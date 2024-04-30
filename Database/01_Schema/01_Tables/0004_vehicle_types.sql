DO $$ 
BEGIN
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'vehicle_types') THEN
    CREATE TABLE public.vehicle_types (
        id bigint NOT NULL,
        created_by bigint NOT NULL,
        created_on timestamp without time zone NOT NULL,
        updated_by bigint NOT NULL,
        updated_on timestamp without time zone NOT NULL,
        name varchar(200) NOT NULL,
        is_active boolean DEFAULT true NOT NULL
    );

    ALTER TABLE public.vehicle_types OWNER TO postgres;

    ALTER TABLE public.vehicle_types ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
        SEQUENCE NAME public.vehicle_types_id_seq
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    );

    ALTER TABLE ONLY public.vehicle_types ADD CONSTRAINT pk__vehicle_types PRIMARY KEY (id) INCLUDE (id);
  END IF;
END $$;