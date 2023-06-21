CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    animal_id INT, 
    order_date DATE, 
    updated DATE,
    quantity  INT, 
    discount  DECIMAL(10, 2),
    discount_amount DECIMAL(10, 2),
    total_amount DECIMAL(10, 2),
    unit_price DECIMAL(10, 2),
    total_freight  DECIMAL(10, 2),
    status VARCHAR(20)
);