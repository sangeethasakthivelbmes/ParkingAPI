DO $$ 
BEGIN
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'employee') THEN
    CREATE TABLE public.employee (
        id bigint NOT NULL,
        created_by bigint NOT NULL,
        created_on timestamp without time zone NOT NULL,
        updated_by bigint NOT NULL,
        updated_on timestamp without time zone NOT NULL,
        empname varchar(200) NOT NULL,
        phone varchar(10) NOT NULL,
        is_active boolean DEFAULT true NOT NULL
    );

    ALTER TABLE public.employee OWNER TO postgres;

    ALTER TABLE public.employee ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
        SEQUENCE NAME public.employee_id_seq
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    );

    ALTER TABLE ONLY public.employee ADD CONSTRAINT pk__employee PRIMARY KEY (id) INCLUDE (id);
   
  END IF;
END $$;