ALTER TABLE users
    ADD COLUMN first_name        VARCHAR(100) NULL,
    ADD COLUMN middle_name       VARCHAR(100) NULL,
    ADD COLUMN last_name         VARCHAR(100) NULL,
    ADD COLUMN permanent_address VARCHAR(500) NULL,
    ADD COLUMN current_address   VARCHAR(500) NULL,
    ADD COLUMN contact           VARCHAR(20)  NULL,
    ADD COLUMN gender            ENUM('Male', 'Female', 'Other') NULL;

ALTER TABLE transactions
    ADD COLUMN user_id INT NULL,
    ADD CONSTRAINT fk_transactions_user
        FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE SET NULL;
