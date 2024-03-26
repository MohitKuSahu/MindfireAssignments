-- Create user table
CREATE TABLE [user] (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(255),
    email VARCHAR(255) UNIQUE,
    [password] VARCHAR(255),
    [type] VARCHAR(50)
);

-- Create parking_zone table
CREATE TABLE parking_zone (
    parking_zone_id INT PRIMARY KEY IDENTITY(1,1),
    parking_zone_title VARCHAR(255) UNIQUE -- Adding unique constraint
);

-- Create parking_space table
CREATE TABLE parking_space (
    parking_space_id INT PRIMARY KEY IDENTITY(1,1),
    parking_space_title VARCHAR(255) UNIQUE, -- Adding unique constraint
    parking_zone_id INT,
    FOREIGN KEY (parking_zone_id) REFERENCES parking_zone(parking_zone_id)
);

-- Create vehicle_parking table
CREATE TABLE vehicle_parking (
    vehicle_parking_id INT PRIMARY KEY IDENTITY(1,1),
    parking_zone_id INT,
    parking_space_id INT,
    booking_date_time DATETIME,
    release_date_time DATETIME,
    FOREIGN KEY (parking_zone_id) REFERENCES parking_zone(parking_zone_id),
    FOREIGN KEY (parking_space_id) REFERENCES parking_space(parking_space_id)
);
