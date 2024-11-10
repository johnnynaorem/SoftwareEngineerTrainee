select * from Movies
delete from movies
INSERT INTO Movies
VALUES
('The Shawshank Redemption', 'Drama', 199.99, 'https://i2.wp.com/www.themoviedb.org/t/p/original/4azlwV1nBCtJXGtZssKltv5HwjS.jpg', 9.3, 'Two imprisoned men bond over a number of years.', '1994-09-23', 10),
('The Dark Knight', 'Action', 299.99, 'https://m.media-amazon.com/images/S/pv-target-images/e9a43e647b2ca70e75a3c0af046c4dfdcd712380889779cbdc2c57d94ab63902.jpg', 9.0, 'Batman faces the Joker, a criminal mastermind.', '2008-07-18', 5),
('Inception', 'Sci-Fi', 349.99, 'https://static.toiimg.com/photo/msid-6177430/6177430.jpg?57181', 8.8, 'A thief who steals secrets through dreams.', '2010-07-16', 8),
('Forrest Gump', 'Drama', 249.99, 'https://resizing.flixster.com/hqcqFfWf1syt2OrGlbW7LDvfj9Y=/fit-in/352x330/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p15829_v_v13_aa.jpg', 8.8, 'The presidencies of Kennedy and Johnson told through the life of an Alabama man.', '1994-07-06', 12),
('The Matrix', 'Sci-Fi', 299.99, 'https://static.wikia.nocookie.net/matrix/images/b/be/The_Matrix_Revolutions_digital_release_cover.jpg/revision/latest/scale-to-width-down/1200?cb=20210908111503', 8.7, 'A computer hacker learns about the true nature of his reality.', '1999-03-31', 7),
('Interstellar', 'Sci-Fi', 399.99, 'https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p10543523_p_v8_as.jpg', 8.6, 'A group of explorers travel through a wormhole in space to ensure humanity''s survival.', '2014-11-07', 3),
('Gladiator', 'Action', 349.99, 'https://assetscdn1.paytm.com/images/cinema/Gladiator--608x800-4f79cf90-8547-11ef-a9f3-bb0df457e7e9.jpg', 8.5, 'A betrayed Roman general seeks revenge against the corrupt emperor who murdered his family.', '2000-05-05', 6),
('The Godfather', 'Crime', 199.99, 'https://m.media-amazon.com/images/M/MV5BNzc1OWY5MjktZDllMi00ZDEzLWEwMGItYjk1YmRhYjBjNTVlXkEyXkFqcGc@._V1_.jpg', 9.2, 'The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.', '1972-03-24', 9),
('Pulp Fiction', 'Crime', 249.99, 'https://m.media-amazon.com/images/I/91peDnalJQL.jpg', 8.9, 'The lives of two mob hitmen, a boxer, a gangster''s wife, and a pair of diner bandits intertwine in four tales of violence and redemption.', '1994-10-14', 10),
('Avatar', 'Sci-Fi', 499.99, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQZe2PSiA8VZ2_ELcPtn3Q8QQquW3qrMxdHUg&s', 7.8, 'A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.', '2009-12-18', 15);


select * from Reservations
INSERT INTO Reservations
VALUES
(1, 1012, '2024-10-01 14:00:00', 2),  -- Reservation for "The Shawshank Redemption" by Customer 1
(2, 1013, '2024-10-02 10:00:00', 4),   -- Reservation for "The Dark Knight" by Customer 2
(3, 1014, '2024-10-02 12:30:00', 5), -- Reservation for "Inception" by Customer 3
(4, 1015, '2024-10-20 16:45:00', 2), -- Reservation for "Forrest Gump" by Customer 4
(5, 1016, '2024-10-28 09:00:00', 3),   -- Reservation for "The Matrix" by Customer 5 
(1, 1017, '2024-11-02 14:30:00', 2),   -- Reservation for "Interstellar" by Customer 1 
(2, 1018, '2024-11-04 08:15:00', 0),    -- Reservation for "Gladiator" by Customer 2 
(3, 1019, '2024-11-05 13:00:00', 1),  -- Reservation for "The Godfather" by Customer 3 --
--(4, 1020, '2024-11-09 18:00:00', 2),  -- Reservation for "Pulp Fiction" by Customer 4
--(5, 1021, '2024-11-10 11:00:00', 2); -- Reservation for "Avatar" by Customer 5

select * from Rentals
select * from Payments

-- Insert Rentals with all having 'Returned' Status
INSERT INTO Rentals (RentalDate, DueDate, ReturnDate, Status, CustomerId, MovieId, RentalFee)
VALUES
('2024-10-03 00:00:00', '2024-10-10 00:00:00', '2024-10-09 00:00:00', 3, 1, 1012, 199.99),  -- Shawshank Redemption (Returned)
('2024-10-22 00:00:00', '2024-10-29 00:00:00', '2024-10-28 00:00:00', 3, 4, 1015, 249.99),  -- Forrest Gump (Returned)
('2024-11-03 10:00:00', '2024-11-10 10:00:00', '2024-11-10 05:00:00', 3, 1, 1017, 399.99),  -- Interstellar (Returned) 
('2024-11-06 06:00:00', '2024-11-13 06:00:00', NULL, 2, 2, 1018, 349.99),  -- Gladiator (Active)
('2024-11-07 10:00:00', '2024-11-14 10:00:00', NULL, 2, 3, 1019, 199.99),  -- Godfather (Active)
--('2024-10-09 00:00:00', '2024-10-23 00:00:00', '2023-10-21 00:00:00', 'Returned', 9, 1020, 249.99),  -- Pulp Fiction ()
--('2024-10-10 00:00:00', '2024-10-24 00:00:00', '2023-10-22 00:00:00', 'Returned', 10, 1021, 499.99); -- Avatar ()

-- Insert Payments corresponding to the Rentals (RentalFee as Amount)
INSERT INTO Payments (CustomerId, RentalId, Amount, PaymentDate, PaymentType)
VALUES
(1, 1, 199.99, '2024-10-03 00:05:00', 'Credit Card'),  -- Shawshank Redemption
(4, 2, 249.99, '2024-10-22 00:10:00', 'Credit Card'),    -- Forrest Gump
(1, 3, 399.99, '2024-11-03 10:05:00', 'PayPal'),         -- Interstellar
(2, 4, 349.99, '2024-11-06 06:02:00', 'Credit Card'),    -- Gladiator
(3, 5, 199.99, '2024-11-07 10:02:10', 'Debit Card'),     -- Godfather