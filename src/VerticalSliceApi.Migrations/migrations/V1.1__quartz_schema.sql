DO
$do$
    BEGIN
        IF NOT EXISTS(
            SELECT
            FROM pg_catalog.pg_roles
            WHERE rolname = 'quartz_jobstore_user') THEN
            CREATE ROLE quartz_jobstore_user;
        END IF;
    END
$do$;

CREATE SCHEMA IF NOT EXISTS "quartz";
GRANT USAGE ON SCHEMA "quartz" TO quartz_jobstore_user;
CREATE TABLE IF NOT EXISTS quartz.qrtz_job_details
(
    sched_name        text    NOT NULL,
    job_name          text    NOT NULL,
    job_group         text    NOT NULL,
    description       text,
    job_class_name    text    NOT NULL,
    is_durable        boolean NOT NULL,
    is_nonconcurrent  boolean NOT NULL,
    is_update_data    boolean NOT NULL,
    requests_recovery boolean NOT NULL,
    job_data          bytea
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_job_details TO quartz_jobstore_user;

CREATE TABLE IF NOT EXISTS quartz.qrtz_triggers
(
    sched_name     text   NOT NULL,
    trigger_name   text   NOT NULL,
    trigger_group  text   NOT NULL,
    job_name       text   NOT NULL,
    job_group      text   NOT NULL,
    description    text,
    next_fire_time bigint,
    prev_fire_time bigint,
    priority       integer,
    trigger_state  text   NOT NULL,
    trigger_type   text   NOT NULL,
    start_time     bigint NOT NULL,
    end_time       bigint,
    calendar_name  text,
    misfire_instr  smallint,
    job_data       bytea
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_triggers TO quartz_jobstore_user;

CREATE TABLE IF NOT EXISTS quartz.qrtz_simple_triggers
(
    sched_name      text   NOT NULL,
    trigger_name    text   NOT NULL,
    trigger_group   text   NOT NULL,
    repeat_count    bigint NOT NULL,
    repeat_interval bigint NOT NULL,
    times_triggered bigint NOT NULL
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_simple_triggers TO quartz_jobstore_user;

CREATE TABLE IF NOT EXISTS quartz.qrtz_simprop_triggers
(
    sched_name    text NOT NULL,
    trigger_name  text NOT NULL,
    trigger_group text NOT NULL,
    str_prop_1    text,
    str_prop_2    text,
    str_prop_3    text,
    int_prop_1    integer,
    int_prop_2    integer,
    long_prop_1   bigint,
    long_prop_2   bigint,
    dec_prop_1    numeric,
    dec_prop_2    numeric,
    bool_prop_1   boolean,
    bool_prop_2   boolean,
    time_zone_id  text
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_simprop_triggers TO quartz_jobstore_user;

CREATE TABLE IF NOT EXISTS quartz.qrtz_cron_triggers
(
    sched_name      text NOT NULL,
    trigger_name    text NOT NULL,
    trigger_group   text NOT NULL,
    cron_expression text NOT NULL,
    time_zone_id    text
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_cron_triggers TO quartz_jobstore_user;

CREATE TABLE IF NOT EXISTS quartz.qrtz_blob_triggers
(
    sched_name    text NOT NULL,
    trigger_name  text NOT NULL,
    trigger_group text NOT NULL,
    blob_data     bytea
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_blob_triggers TO quartz_jobstore_user;

CREATE TABLE IF NOT EXISTS quartz.qrtz_calendars
(
    sched_name    text  NOT NULL,
    calendar_name text  NOT NULL,
    calendar      bytea NOT NULL
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_calendars TO quartz_jobstore_user;

CREATE TABLE IF NOT EXISTS quartz.qrtz_paused_trigger_grps
(
    sched_name    text NOT NULL,
    trigger_group text NOT NULL
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_paused_trigger_grps TO quartz_jobstore_user;

CREATE TABLE IF NOT EXISTS quartz.qrtz_fired_triggers
(
    sched_name        text    NOT NULL,
    entry_id          text    NOT NULL,
    trigger_name      text    NOT NULL,
    trigger_group     text    NOT NULL,
    instance_name     text    NOT NULL,
    fired_time        bigint  NOT NULL,
    sched_time        bigint  NOT NULL,
    priority          integer NOT NULL,
    state             text    NOT NULL,
    job_name          text,
    job_group         text,
    is_nonconcurrent  boolean NOT NULL,
    requests_recovery boolean
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_fired_triggers TO quartz_jobstore_user;

CREATE TABLE IF NOT EXISTS quartz.qrtz_scheduler_state
(
    sched_name        text   NOT NULL,
    instance_name     text   NOT NULL,
    last_checkin_time bigint NOT NULL,
    checkin_interval  bigint NOT NULL
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_scheduler_state TO quartz_jobstore_user;

CREATE TABLE IF NOT EXISTS quartz.qrtz_locks
(
    sched_name text NOT NULL,
    lock_name  text NOT NULL
);

GRANT SELECT, INSERT, DELETE, UPDATE ON TABLE quartz.qrtz_locks TO quartz_jobstore_user;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_job_details
                ADD CONSTRAINT qrtz_job_details_pkey PRIMARY KEY (sched_name, job_name, job_group);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_job_details_pkey` for Table `quartz.qrtz_job_details` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_triggers
                ADD CONSTRAINT qrtz_triggers_pkey PRIMARY KEY (sched_name, trigger_name, trigger_group);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_triggers_pkey` for Table `quartz.qrtz_triggers` already exists';
        END;
    END
$$;


DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_triggers
                ADD CONSTRAINT qrtz_triggers_sched_name_job_name_job_group_fkey
                    FOREIGN KEY (sched_name, job_name, job_group)
                        REFERENCES quartz.qrtz_job_details (sched_name, job_name, job_group);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_triggers_sched_name_job_name_job_group_fkey` for Table `quartz.qrtz_triggers` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_simple_triggers
                ADD CONSTRAINT qrtz_simple_triggers_pkey PRIMARY KEY (sched_name, trigger_name, trigger_group);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_simple_triggers_pkey` for Table `quartz.qrtz_simple_triggers` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_simple_triggers
                ADD CONSTRAINT qrtz_simple_triggers_sched_name_trigger_name_trigger_group_fkey
                    FOREIGN KEY (sched_name, trigger_name, trigger_group)
                        REFERENCES quartz.qrtz_triggers (sched_name, trigger_name, trigger_group) ON DELETE CASCADE;
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_simple_triggers_sched_name_trigger_name_trigger_group_fkey` for Table `quartz.qrtz_simple_triggers` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_simprop_triggers
                ADD CONSTRAINT qrtz_simprop_triggers_pkey PRIMARY KEY (sched_name, trigger_name, trigger_group);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_simprop_triggers_pkey` for Table `quartz.qrtz_simprop_triggers` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_simprop_triggers
                ADD CONSTRAINT qrtz_simprop_triggers_sched_name_trigger_name_trigger_grou_fkey
                    FOREIGN KEY (sched_name, trigger_name, trigger_group)
                        REFERENCES quartz.qrtz_triggers (sched_name, trigger_name, trigger_group) ON DELETE CASCADE;
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_simprop_triggers_sched_name_trigger_name_trigger_grou_fkey` for Table `quartz.qrtz_simprop_triggers` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_cron_triggers
                ADD CONSTRAINT qrtz_cron_triggers_pkey PRIMARY KEY (sched_name, trigger_name, trigger_group);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_cron_triggers_pkey` for Table `quartz.qrtz_cron_triggers` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_cron_triggers
                ADD CONSTRAINT qrtz_cron_triggers_sched_name_trigger_name_trigger_group_fkey
                    FOREIGN KEY (sched_name, trigger_name, trigger_group)
                        REFERENCES quartz.qrtz_triggers (sched_name, trigger_name, trigger_group) ON DELETE CASCADE;
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_cron_triggers_sched_name_trigger_name_trigger_group_fkey` for Table `quartz.qrtz_cron_triggers` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_blob_triggers
                ADD CONSTRAINT qrtz_blob_triggers_pkey PRIMARY KEY (sched_name, trigger_name, trigger_group);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_blob_triggers_pkey` for Table `quartz.qrtz_blob_triggers` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_blob_triggers
                ADD CONSTRAINT qrtz_blob_triggers_sched_name_trigger_name_trigger_group_fkey
                    FOREIGN KEY (sched_name, trigger_name, trigger_group)
                        REFERENCES quartz.qrtz_triggers (sched_name, trigger_name, trigger_group) ON DELETE CASCADE;
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_blob_triggers_sched_name_trigger_name_trigger_group_fkey` for Table `quartz.qrtz_blob_triggers` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_calendars
                ADD CONSTRAINT qrtz_calendars_pkey PRIMARY KEY (sched_name, calendar_name);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_calendars_pkey` for Table `quartz.qrtz_calendars` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_paused_trigger_grps
                ADD CONSTRAINT qrtz_paused_trigger_grps_pkey PRIMARY KEY (sched_name, trigger_group);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_paused_trigger_grps_pkey` for Table `quartz.qrtz_paused_trigger_grps` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_fired_triggers
                ADD CONSTRAINT qrtz_fired_triggers_pkey PRIMARY KEY (sched_name, entry_id);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_fired_triggers_pkey` for Table `quartz.qrtz_fired_triggers` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_scheduler_state
                ADD CONSTRAINT qrtz_scheduler_state_pkey PRIMARY KEY (sched_name, instance_name);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_scheduler_state_pkey` for Table `quartz.qrtz_scheduler_state` already exists';
        END;
    END
$$;

DO
$$
    BEGIN
        BEGIN
            ALTER TABLE quartz.qrtz_locks
                ADD CONSTRAINT qrtz_locks_pkey PRIMARY KEY (sched_name, lock_name);
        EXCEPTION
            WHEN duplicate_table THEN
            WHEN duplicate_object THEN
            WHEN SQLSTATE '42P16' THEN
                RAISE NOTICE 'Constraint `qrtz_locks_pkey` for Table `quartz.qrtz_locks` already exists';
        END;
    END
$$;

CREATE INDEX IF NOT EXISTS idx_qrtz_j_req_recovery ON quartz.qrtz_job_details USING btree (requests_recovery);

CREATE INDEX IF NOT EXISTS idx_qrtz_t_next_fire_time ON quartz.qrtz_triggers USING btree (next_fire_time);

CREATE INDEX IF NOT EXISTS idx_qrtz_t_state ON quartz.qrtz_triggers USING btree (trigger_state);

CREATE INDEX IF NOT EXISTS idx_qrtz_t_nft_st ON quartz.qrtz_triggers USING btree (next_fire_time, trigger_state);

CREATE INDEX IF NOT EXISTS idx_qrtz_ft_trig_name ON quartz.qrtz_fired_triggers USING btree (trigger_name);

CREATE INDEX IF NOT EXISTS idx_qrtz_ft_trig_group ON quartz.qrtz_fired_triggers USING btree (trigger_group);

CREATE INDEX IF NOT EXISTS idx_qrtz_ft_trig_nm_gp ON quartz.qrtz_fired_triggers USING btree (sched_name, trigger_name, trigger_group);

CREATE INDEX IF NOT EXISTS idx_qrtz_ft_trig_inst_name ON quartz.qrtz_fired_triggers USING btree (instance_name);

CREATE INDEX IF NOT EXISTS idx_qrtz_ft_job_name ON quartz.qrtz_fired_triggers USING btree (job_name);

CREATE INDEX IF NOT EXISTS idx_qrtz_ft_job_group ON quartz.qrtz_fired_triggers USING btree (job_group);

CREATE INDEX IF NOT EXISTS idx_qrtz_ft_job_req_recovery ON quartz.qrtz_fired_triggers USING btree (requests_recovery);