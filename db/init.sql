create table categories (
id serial primary key,
name varchar(50) not null unique,
description text not null
);

create table ingredients (
id serial primary key,
name varchar(50) not null unique,
description text not null
);

create table orders (
id serial primary key,
created_at timestamp with time zone not null default now(),
total_price decimal(10, 2) not null check(total_price >= 0)
);

create table dishes (
id serial primary key,
name varchar(50) not null unique,
description text not null,
price decimal(10, 2) not null check(price >= 0),
stock decimal(10, 2) not null check(stock >= 0),
category_id integer not null,
constraint fk_dish_category
foreign key (category_id) references categories(id)
);

create table order_items (
id serial primary key,
order_id integer not null,
dish_id integer not null,
quantity smallint not null,
unit_price decimal(10, 2) not null,
constraint fk_order_items_order
foreign key (order_id) references orders(id)
on delete cascade,
constraint fk_order_items_dish
foreign key (dish_id) references dishes(id)
on delete cascade
);

create table dish_ingredients (
dish_id integer not null,
ingredient_id integer not null,
primary key (dish_id, ingredient_id),
constraint fk_dish_ingredients_dish
foreign key (dish_id) references dishes(id),
constraint fk_dish_ingredients_ingredient
foreign key (ingredient_id) references ingredients(id)
);

-- =========================
-- FORCE UTF-8 SESSION
-- =========================
SET client_encoding = 'UTF8';

-- =========================
-- CATEGORIES
-- =========================

INSERT INTO categories (name, description) VALUES
('Салати', 'Холодні страви з овочів та зелені'),
('Супи', 'Перші страви на основі бульйонів'),
('Закуски', 'Легкі холодні та гарячі закуски'),
('Основні страви', 'М’ясні, рибні та овочеві гарячі страви'),
('Гарніри', 'Додаткові страви до основних'),
('Паста', 'Італійські страви з макаронів'),
('Піца', 'Страви на основі тіста з начинкою'),
('Бургери', 'Бургери з м’ясом та соусами'),
('Десерти', 'Солодкі страви'),
('Напої', 'Безалкогольні напої');

-- =========================
-- INGREDIENTS
-- =========================

INSERT INTO ingredients (name, description) VALUES
('Куряче філе', 'Ніжне куряче м’ясо'),
('Яловичина', 'Якісна яловичина'),
('Сир моцарела', 'Італійський сир для піци'),
('Помідори', 'Свіжі томати'),
('Огірки', 'Свіжі огірки'),
('Листя салату', 'Салатна зелень'),
('Рис', 'Білий або пропарений рис'),
('Паста', 'Макаронні вироби'),
('Картопля', 'Бульбова культура'),
('Цибуля', 'Ріпчаста цибуля');

-- =========================
-- DISHES
-- =========================

INSERT INTO dishes (name, description, price, stock, category_id) VALUES
('Цезар з куркою', 'Салат з курячим філе та соусом цезар', 189.00, 10, 1),
('Борщ', 'Український традиційний суп', 120.00, 15, 2),
('Паста Карбонара', 'Італійська паста з соусом', 210.00, 8, 6),
('Піца Маргарита', 'Класична піца з томатами та сиром', 199.00, 12, 7),
('Чізбургер', 'Бургер з сиром та котлетою', 160.00, 20, 8);

-- =========================
-- DISH INGREDIENTS (MAPPING)
-- =========================

INSERT INTO dish_ingredients (dish_id, ingredient_id) VALUES
(1, 1), -- Цезар -> Куряче філе
(1, 6), -- Цезар -> Листя салату
(2, 4), -- Борщ -> Помідори
(2, 10), -- Борщ -> Цибуля
(3, 8), -- Паста -> Паста
(3, 3), -- Паста -> Сир моцарела
(4, 3), -- Піца -> Сир моцарела
(4, 4), -- Піца -> Помідори
(5, 2); -- Чізбургер -> Яловичина
