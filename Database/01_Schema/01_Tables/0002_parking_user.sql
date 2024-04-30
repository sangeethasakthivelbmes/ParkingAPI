DO $$ 
BEGIN
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'parking_user') THEN
    CREATE TABLE public.parking_user (
        id bigint NOT NULL,
        created_by bigint NOT NULL,
        created_on timestamp without time zone NOT NULL,
        updated_by bigint NOT NULL,
        updated_on timestamp without time zone NOT NULL,
        is_active boolean DEFAULT true NOT NULL,
        role_id bigint NOT NULL,
        email varchar(250) not null,
        name varchar(200) NOT NULL,
        phone varchar(10) NOT NULL,
        note varchar(2000)
    );

    ALTER TABLE public.parking_user OWNER TO postgres;

    ALTER TABLE public.parking_user ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
        SEQUENCE NAME public.parking_user_id_seq
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    );

    ALTER TABLE ONLY public.parking_user ADD CONSTRAINT pk__parking_user PRIMARY KEY (id) INCLUDE (id);
    ALTER TABLE ONLY public.parking_user ADD CONSTRAINT uk__parking_user_phone UNIQUE (phone);

    ALTER TABLE ONLY public.parking_user
        ADD CONSTRAINT fk__parking_user__role_id__role__id FOREIGN KEY (role_id) REFERENCES public.role(id);
  END IF;
END $$;