 -- script to insert 
    -- animal data into the database
    use STGenetics;

INSERT INTO animal (name, breed, birth_date, sex, price, status)
VALUES ('Max', 'Labrador Retriever', '2019-05-12', 1, 1500.00, 1),
       ('Bella', 'Persian Cat', '2020-02-28', 2, 800.00, 1),
       ('Charlie', 'Golden Retriever', '2018-08-04', 1, 1200.00, 1),
       ('Luna', 'Siamese Cat', '2019-11-15', 2, 650.00, 1),
       ('Cooper', 'German Shepherd', '2017-06-20', 1, 1800.00, 1);

DElETE FROM animal 
Where name = 'Max';

UPDATE animal
SET price = 2000.00
WHERE name = 'Charlie';

INSERT INTO sex_catalog (name)
VALUES ('FEMALE'),
       ('MALE');

