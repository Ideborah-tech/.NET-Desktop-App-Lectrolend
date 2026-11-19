-- LectroLend schema, derived from the ERD (Assignment 2).
-- USER / STUDENT / LECTURER / ADMIN implement class inheritance as joined tables.

CREATE TABLE users (
    user_id        BIGSERIAL PRIMARY KEY,
    full_name      VARCHAR(100) NOT NULL,
    email          VARCHAR(100) NOT NULL UNIQUE,
    password_hash  VARCHAR(255) NOT NULL,
    phone          VARCHAR(20),
    role           VARCHAR(10)  NOT NULL CHECK (role IN ('STUDENT', 'LECTURER', 'ADMIN')),
    is_active      BOOLEAN      NOT NULL DEFAULT TRUE
);

CREATE TABLE student (
    user_id          BIGINT PRIMARY KEY REFERENCES users (user_id) ON DELETE CASCADE,
    registration_no  VARCHAR(20) NOT NULL UNIQUE,
    faculty          VARCHAR(80),
    program          VARCHAR(80)
);

CREATE TABLE lecturer (
    user_id     BIGINT PRIMARY KEY REFERENCES users (user_id) ON DELETE CASCADE,
    staff_no    VARCHAR(20) NOT NULL UNIQUE,
    department  VARCHAR(80)
);

CREATE TABLE admin (
    user_id   BIGINT PRIMARY KEY REFERENCES users (user_id) ON DELETE CASCADE,
    staff_no  VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE room (
    room_id    BIGSERIAL PRIMARY KEY,
    room_code  VARCHAR(20) NOT NULL UNIQUE,
    building   VARCHAR(60) NOT NULL,
    capacity   INT         NOT NULL CHECK (capacity > 0)
);

CREATE TABLE equipment (
    equipment_id  BIGSERIAL PRIMARY KEY,
    added_by      BIGINT      NOT NULL REFERENCES admin (user_id),
    name          VARCHAR(80) NOT NULL,
    type          VARCHAR(20) NOT NULL CHECK (type IN ('PROJECTOR', 'HDMI_CABLE', 'REMOTE_CONTROL', 'OTHER')),
    asset_tag     VARCHAR(30) NOT NULL UNIQUE,
    status        VARCHAR(20) NOT NULL DEFAULT 'AVAILABLE'
                  CHECK (status IN ('AVAILABLE', 'IN_USE', 'UNDER_MAINTENANCE', 'RETIRED')),
    description   TEXT
);

CREATE TABLE reservation (
    reservation_id  BIGSERIAL PRIMARY KEY,
    user_id         BIGINT      NOT NULL REFERENCES users (user_id),
    equipment_id    BIGINT      NOT NULL REFERENCES equipment (equipment_id),
    room_id         BIGINT      REFERENCES room (room_id),
    created_at      TIMESTAMP   NOT NULL DEFAULT now(),
    start_time      TIMESTAMP   NOT NULL,
    end_time        TIMESTAMP   NOT NULL,
    status          VARCHAR(20) NOT NULL
                    CHECK (status IN ('BOOKED', 'ISSUED', 'OVERDUE', 'RETURNED', 'CANCELLED', 'EXPIRED')),
    issued_at       TIMESTAMP,
    CHECK (end_time > start_time)
);

CREATE INDEX idx_reservation_equipment_time ON reservation (equipment_id, start_time, end_time);
CREATE INDEX idx_reservation_user ON reservation (user_id);
CREATE INDEX idx_reservation_status ON reservation (status);

CREATE TABLE return_record (
    return_id          BIGSERIAL PRIMARY KEY,
    reservation_id     BIGINT    NOT NULL UNIQUE REFERENCES reservation (reservation_id) ON DELETE CASCADE,
    confirmed_by       BIGINT    NOT NULL REFERENCES admin (user_id),
    returned_at        TIMESTAMP NOT NULL,
    condition_on_return VARCHAR(100) NOT NULL,
    late_return        BOOLEAN   NOT NULL DEFAULT FALSE,
    remarks            TEXT
);
