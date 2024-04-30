DO $$ 
BEGIN
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'hour_entry') THEN
    CREATE TABLE public.hour_entry (
        id bigint NOT NULL,
        vehicle_typeId bigint NOT NULL,
        created_by bigint NOT NULL,
        created_on timestamp without time zone NOT NULL,
        updated_by bigint NOT NULL,
        updated_on timestamp without time zone NOT NULL,
        min_hours decimal NOT NULL,
        min_rate decimal NOT NULL,
        additional_hour decimal NOT NULL
    );

    ALTER TABLE public.hour_entry OWNER TO postgres;

    ALTER TABLE public.hour_entry ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
        SEQUENCE NAME public.hour_entry_id_seq
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    );

    ALTER TABLE ONLY public.hour_entry ADD CONSTRAINT pk__hour_entry PRIMARY KEY (id) INCLUDE (id);
    ALTER TABLE ONLY public.hour_entry
        ADD CONSTRAINT fk__hour_entry__vehicle_typeId__vehicle_types__id FOREIGN KEY (vehicle_typeId) REFERENCES public.vehicle_types(id);
  END IF;
END $$;