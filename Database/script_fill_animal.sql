-- Create a temporary table to hold the random names and breeds
CREATE TABLE #temp_data (
    name VARCHAR(255),
    breed VARCHAR(255)
);

-- Insert random names and breeds into the temporary table
INSERT INTO #temp_data (name, breed)
SELECT
    CONCAT('Animal', ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
    CASE WHEN ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) % 2 = 0 THEN 'Dog' ELSE 'Cat' END
FROM
    sys.columns;

-- Insert random records into the animal table
INSERT INTO animal (name, breed, birth_date, sex, price, status)
SELECT
    td.name,
    td.breed,
    DATEADD(DAY, -FLOOR(RAND(CHECKSUM(NEWID())) * 3650), GETDATE()), -- Random birth date within the past 10 years
    CAST(RAND(CHECKSUM(NEWID())) * 2 + 1 AS INT), -- Random sex (1 for male, 2 for female)
    ROUND((RAND(CHECKSUM(NEWID())) * 2000) + 500, 2), -- Random price between 500 and 2500
    1 -- Default status as 1 (active)
FROM
    #temp_data td
CROSS JOIN
    (SELECT TOP 500 1 FROM sys.columns) t; -- Generate 500 random records

-- Drop the temporary table
DROP TABLE #temp_data;
