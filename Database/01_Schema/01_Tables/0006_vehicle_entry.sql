DO $$ 
BEGIN
    IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'vehicle_entry') THEN
        CREATE TABLE public.vehicle_entry (
            id bigint NOT NULL,
            created_by bigint NOT NULL,
            created_on timestamp without time zone NOT NULL,
            updated_by bigint NOT NULL,
            updated_on timestamp without time zone NOT NULL,
            vehicle_typeId bigint not null,
            owner_full_name varchar(250) not null,
            vehicle_number varchar(250) not null,
            in_date timestamp without time zone NOT NULL,
            out_date timestamp without time zone  NULL,
            in_time time  NULL, 
            out_time time  NULL,
            vehicle_status int NOT NULL,
            vehicle_photo text null,
            total_hours int NULL,
            total_cost int NULl
        );

        ALTER TABLE public.vehicle_entry OWNER TO postgres;

        ALTER TABLE public.vehicle_entry ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
            SEQUENCE NAME public.vehicle_entry_id_seq
            START WITH 1
            INCREMENT BY 1
            NO MINVALUE
            NO MAXVALUE
            CACHE 1
        );

        ALTER TABLE ONLY public.vehicle_entry ADD CONSTRAINT pk__vehicle_entry PRIMARY KEY (id);
        ALTER TABLE ONLY public.vehicle_entry
        ADD CONSTRAINT fk__vehicle_entry__vehicle_typeId__vehicle_types__id FOREIGN KEY (vehicle_typeId) REFERENCES public.vehicle_types(id);
    END IF;
END $$;