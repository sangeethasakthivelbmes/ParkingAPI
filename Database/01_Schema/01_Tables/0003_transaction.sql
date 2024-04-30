DO $$ 
BEGIN
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'transaction') THEN
    CREATE TABLE public.transaction (
        id bigint NOT NULL,
        created_by bigint NOT NULL,
        created_on timestamp without time zone NOT NULL,
        updated_by bigint NOT NULL,
        updated_on timestamp without time zone NOT NULL,
        parking_id bigint NOT NULL,
	    phone varchar(10) NOT NULL,
        otp varchar(10) NOT NULL,
    );

    ALTER TABLE public.transaction OWNER TO postgres;

    ALTER TABLE public.transaction ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
        SEQUENCE NAME public.transaction_id_seq
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    );

    ALTER TABLE ONLY public.transaction ADD CONSTRAINT pk__transaction PRIMARY KEY (id) INCLUDE (id);


    ALTER TABLE ONLY public.transaction
        ADD CONSTRAINT fk__transaction__parking_id__parking_user__id FOREIGN KEY (parking_id) REFERENCES public.parking_user(id);
  END IF;
END $$;