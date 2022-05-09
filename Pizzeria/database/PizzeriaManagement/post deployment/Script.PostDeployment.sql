
INSERT INTO [dbo].[Roles] ([Name])
VALUES ('User'),
       ('Administrator')

INSERT INTO [dbo].[Users]
VALUES ('James A.A.', 'Jamesss', '0B14D501A594442A01C6859541BCB3E8164D183D32937B851835442F69D5C94E', 2),
       ('Lia G.S.', 'Liaaa', '0B14D501A594442A01C6859541BCB3E8164D183D32937B851835442F69D5C94E', 1),
       ('Tim C.Q.', 'Timmm', '0B14D501A594442A01C6859541BCB3E8164D183D32937B851835442F69D5C94E', 1)

INSERT INTO [dbo].[Orders]
VALUES (3, 'Street Wood 15', cast('4/8/2022 11:12:12 PM +03:00' as datetimeoffset), 30, 'Addition information', 'Tim C.Q.'),
       (3, 'Street Wood 15', cast('5/8/2022 11:34:58 PM +03:00' as datetimeoffset), 171, 'Addition information', 'Tim C.Q.'),
       (2, 'Street Wood 13', cast('5/8/2022 11:48:10 PM +03:00' as datetimeoffset), 104, 'Addition information', 'Lia G.S.'),
       (2, 'Street Wood 12', cast('5/8/2022 11:49:19 PM +03:00' as datetimeoffset), 136, 'Addition information', 'Lia G.S.'),
       (3, 'Street Wood 15', cast('5/9/2022 12:31:11 AM +03:00' as datetimeoffset), 91, 'Addition information', 'Tim C.Q.')

INSERT INTO [dbo].[Pizzas] ([Name], [Description], [ImagePath], [TotalPrice])
VALUES ('Cheese Pizza', 'Topping1, Topping2, Topping3, Topping4', 'Assets\Images\Cheese_Pizza.jpg', 15),
       ('Veggie Pizza', 'Topping1, Topping2, Topping3, Topping4', 'Assets\Images\Veggie_Pizza.jpg', 20),
       ('Pepperoni Pizza', 'Topping1, Topping2, Topping3, Topping4', 'Assets\Images\Pepperoni_Pizza.jpeg', 19),
       ('Meat Pizza', 'Topping1, Topping2, Topping3, Topping4', 'Assets\Images\Meat_Pizza.jpg', 21),
       ('Margherita Pizza', 'Topping1, Topping2, Topping3, Topping4', 'Assets\Images\Margherita_Pizza.jpeg', 20),
       ('BBQ Chicken Pizza', 'Topping1, Topping2, Topping3, Topping4', 'Assets\Images\BBQ_Pizza.jpeg', 24),
       ('Hawaiian Pizza', 'Topping1, Topping2, Topping3, Topping4', 'Assets\Images\Hawaiian_Pizza.jpeg', 23),
       ('Buffalo Pizza', 'Topping1, Topping2, Topping3, Topping4', 'Assets\Images\Buffalo_Pizza.jpeg', 18),
       ('Supreme Pizza', 'Topping1, Topping2, Topping3, Topping4', 'Assets\Images\Supreme_Pizza.jpg', 30)

       
INSERT INTO [dbo].[OrderItems]
VALUES (1, 9, 30, 1, 'Supreme Pizza'),
       (2, 5, 60, 3, 'Margherita Pizza'),
       (2, 6, 24, 1, 'BBQ Chicken Pizza'),
       (2, 7, 69, 3, 'Hawaiian Pizza'),
       (2, 8, 18, 1, 'Buffalo Pizza'),
       (3, 7, 23, 1, 'Hawaiian Pizza'),
       (3, 8, 36, 2, 'Buffalo Pizza'),
       (3, 6, 24, 1, 'BBQ Chicken Pizza'),
       (3, 4, 21, 1, 'Meat Pizza'),
       (4, 7, 46, 2, 'Hawaiian Pizza'),
       (4, 8, 36, 2, 'Buffalo Pizza'),
       (4, 6, 24, 1, 'BBQ Chicken Pizza'),
       (4, 9, 30, 1, 'Supreme Pizza'),
       (5, 5, 20, 1, 'Margherita Pizza'),
       (5, 8, 18, 1, 'Buffalo Pizza'),
       (5, 7, 23, 1, 'Hawaiian Pizza'),
       (5, 9, 30, 1, 'Supreme Pizza')