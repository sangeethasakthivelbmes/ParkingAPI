DO $$ 
BEGIN
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'role') THEN
    CREATE TABLE public.role (
        id bigint NOT NULL,
        created_by bigint NOT NULL,
        created_on timestamp without time zone NOT NULL,
        updated_by bigint NOT NULL,
        updated_on timestamp without time zone NOT NULL,
        is_active boolean DEFAULT true NOT NULL,
        name varchar(200) NOT NULL,
        note varchar(2000) NOT NULL
    );

    ALTER TABLE public.role OWNER TO postgres;

    ALTER TABLE public.role
    ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
        SEQUENCE NAME public.role_id_seq
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    );

    ALTER TABLE ONLY public.role ADD CONSTRAINT pk__role PRIMARY KEY (id) INCLUDE (id);
    ALTER TABLE ONLY public.role ADD CONSTRAINT uk__role_name UNIQUE (name);
  END IF;
END $$;