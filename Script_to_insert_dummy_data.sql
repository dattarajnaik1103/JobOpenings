-- Inserting dummy data into the Department table
INSERT INTO Department (Title) 
VALUES 
    ('Software Development'),
    ('Human Resources'),
    ('Marketing'),
    ('Sales'),
    ('Finance'),
    ('Operations'),
    ('Customer Support'),
    ('Legal'),
    ('IT Infrastructure'),
    ('Product Management');

-- Inserting dummy data into the Location table
INSERT INTO Location (Id, Title, City, State, Country, Zip) 
VALUES 
    (1, 'Headquarters', 'New York', 'New York', 'USA', 10001),
    (2, 'Regional Office', 'Los Angeles', 'California', 'USA', 90001),
    (3, 'European Office', 'London', 'England', 'UK', 11001),
    (4, 'Asia Office', 'Tokyo', 'Tokyo', 'Japan', 20001),
    (5, 'Development Center', 'Bangalore', 'Karnataka', 'India', 560001),
    (6, 'Customer Support Center', 'Chicago', 'Illinois', 'USA', 60001),
    (7, 'Sales Office', 'Berlin', 'Berlin', 'Germany', 70001),
    (8, 'Marketing Hub', 'Paris', 'Ile-de-France', 'France', 75001),
    (9, 'Finance Office', 'Sydney', 'New South Wales', 'Australia', 2000),
    (10, 'Operations Base', 'Toronto', 'Ontario', 'Canada', 10005);

-- Inserting dummy data into the JobOpenings table
INSERT INTO JobOpenings (Title, Description, LocationId, DepartmentId, ClosingDate)
VALUES
    ('Software Engineer', 'Develop and maintain software applications.', 1, 1, '2024-12-31'),
    ('HR Manager', 'Oversee recruitment and employee relations.', 2, 2, '2024-11-30'),
    ('Marketing Executive', 'Promote brand awareness and campaigns.', 3, 3, '2024-12-15'),
    ('Sales Representative', 'Generate leads and close sales deals.', 4, 4, '2024-12-01'),
    ('Finance Analyst', 'Analyze financial data and prepare reports.', 5, 5, '2024-11-25'),
    ('Operations Manager', 'Manage daily operations and logistics.', 6, 6, '2024-12-20'),
    ('Customer Support Specialist', 'Provide support to customers and resolve issues.', 7, 7, '2024-12-05'),
    ('Legal Counsel', 'Provide legal advice and representation.', 8, 8, '2024-11-30'),
    ('IT Specialist', 'Maintain IT infrastructure and systems.', 9, 9, '2024-12-10'),
    ('Product Manager', 'Manage product lifecycle from concept to launch.', 10, 10, '2024-12-25');
