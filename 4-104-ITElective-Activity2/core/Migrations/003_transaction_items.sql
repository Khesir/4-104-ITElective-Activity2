CREATE TABLE IF NOT EXISTS transaction_items (
    id             INT AUTO_INCREMENT PRIMARY KEY,
    transaction_id INT            NOT NULL,
    product_id     INT            NOT NULL,
    name           VARCHAR(255)   NOT NULL,
    cup_size       ENUM('Regular', 'Grande', 'Venti') NOT NULL,
    price          DECIMAL(10, 2) NOT NULL,
    quantity       INT            NOT NULL DEFAULT 1,
    total_price    DECIMAL(10, 2) NOT NULL,

    CONSTRAINT fk_ti_transaction FOREIGN KEY (transaction_id)
        REFERENCES transactions(id) ON DELETE CASCADE,

    CONSTRAINT fk_ti_product FOREIGN KEY (product_id)
        REFERENCES products(id) ON DELETE RESTRICT
);