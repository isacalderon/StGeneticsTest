--  list animals older than 2 years and female, sorted by name.
SELECT * 
FROM animal a
INNER JOIN sex_catalog s ON a.sex = s.id
WHERE YEAR(a.birth_date) <= dateadd(year, -2, getdate()) and s.name = 'MALE'
ORDER BY a.name ASC;

-- list the quantity of animals by sex.
SELECT COUNT(*) as total_by_sex, s.name
FROM animal a
INNER JOIN sex_catalog s ON a.sex = s.id
GROUP BY s.name
