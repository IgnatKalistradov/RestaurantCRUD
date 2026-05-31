using Microsoft.EntityFrameworkCore;
using BackendAPI.Models.DbModels;

namespace BackendAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductIngredient>().HasKey(pi => new { pi.ProductId, pi.IngredientId });
            builder.Entity<ProductIngredient>().HasOne(pi => pi.Product).WithMany(p => p.ProductIngredients).HasForeignKey(pi => pi.ProductId);
            builder.Entity<ProductIngredient>().HasOne(pi => pi.Ingredient).WithMany(i => i.ProductIngredients).HasForeignKey(pi => pi.IngredientId);

            builder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Напої", Description = "Безалкогольні та алкогольні напої різних видів" },
                new Category { CategoryId = 2, Name = "Випічка", Description = "Хлібобулочні та кондитерські вироби" },
                new Category { CategoryId = 3, Name = "Молочні продукти", Description = "Молоко, сири, йогурти та інші молочні вироби" },
                new Category { CategoryId = 4, Name = "М’ясні продукти", Description = "Свіже м’ясо, ковбаси та м’ясні напівфабрикати" },
                new Category { CategoryId = 5, Name = "Рибні продукти", Description = "Свіжа риба, морепродукти та рибні делікатеси" },
                new Category { CategoryId = 6, Name = "Овочі", Description = "Свіжі сезонні овочі" },
                new Category { CategoryId = 7, Name = "Фрукти", Description = "Свіжі фрукти та ягоди" },
                new Category { CategoryId = 8, Name = "Крупи та макарони", Description = "Крупи, макаронні вироби та бобові" },
                new Category { CategoryId = 9, Name = "Солодощі", Description = "Цукерки, шоколад, печиво та десерти" },
                new Category { CategoryId = 10, Name = "Заморожені продукти", Description = "Заморожені овочі, напівфабрикати та морозиво" },
                new Category { CategoryId = 11, Name = "Соуси та спеції", Description = "Соуси, приправи та спеції" },
                new Category { CategoryId = 12, Name = "Консерви", Description = "Овочеві, м’ясні та рибні консерви" }
            );

            builder.Entity<Ingredient>().HasData(
                new Ingredient { IngredientId = 1, Name = "Кава арабіка", Description = "Мелена кава з зерен арабіки" },
                new Ingredient { IngredientId = 2, Name = "Молоко", Description = "Коров’яче молоко для кавових напоїв" },
                new Ingredient { IngredientId = 3, Name = "Цукор", Description = "Білий кристалічний цукор" },

                new Ingredient { IngredientId = 4, Name = "Борошно", Description = "Пшеничне борошно" },
                new Ingredient { IngredientId = 5, Name = "Вершкове масло", Description = "Натуральне вершкове масло" },
                new Ingredient { IngredientId = 6, Name = "Дріжджі", Description = "Пекарські дріжджі" },

                new Ingredient { IngredientId = 7, Name = "Яловичина", Description = "М’ясо яловичини" },
                new Ingredient { IngredientId = 8, Name = "Куряче філе", Description = "Філе курячої грудки" },
                new Ingredient { IngredientId = 9, Name = "Лосось", Description = "Філе лосося" },

                new Ingredient { IngredientId = 10, Name = "Паста", Description = "Паста з твердих сортів пшениці" },
                new Ingredient { IngredientId = 11, Name = "Бекон", Description = "Копчений бекон" },
                new Ingredient { IngredientId = 12, Name = "Вершки", Description = "Вершки для соусів" },

                new Ingredient { IngredientId = 13, Name = "Яйця", Description = "Курячі яйця" },
                new Ingredient { IngredientId = 14, Name = "Шоколад", Description = "Темний шоколад" },
                new Ingredient { IngredientId = 15, Name = "Фрукти", Description = "Асорті свіжих фруктів" },

                new Ingredient { IngredientId = 16, Name = "Овочі", Description = "Сезонні овочі" },
                new Ingredient { IngredientId = 17, Name = "Оливкова олія", Description = "Оливкова олія першого віджиму" },
                new Ingredient { IngredientId = 18, Name = "Сіль", Description = "Кухонна харчова сіль" }
            );

            builder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Еспресо", Description = "Класичний еспресо", Price = 45.00m, Stock = 100, CategoryId = 1 },
                new Product { ProductId = 2, Name = "Капучино", Description = "Кава з молочною пінкою", Price = 65.00m, Stock = 80, CategoryId = 1 },

                new Product { ProductId = 3, Name = "Чіабата", Description = "Свіжа італійська чіабата", Price = 35.00m, Stock = 40, CategoryId = 2 },
                new Product { ProductId = 4, Name = "Круасан з маслом", Description = "Теплий круасан з вершковим маслом", Price = 55.00m, Stock = 30, CategoryId = 2 },

                new Product { ProductId = 5, Name = "Сирна тарілка", Description = "Асорті твердих і м’яких сирів", Price = 180.00m, Stock = 20, CategoryId = 3 },

                new Product { ProductId = 6, Name = "Стейк з яловичини", Description = "Стейк середньої прожарки", Price = 420.00m, Stock = 15, CategoryId = 4 },
                new Product { ProductId = 7, Name = "Куряче філе гриль", Description = "Філе курки на грилі", Price = 220.00m, Stock = 25, CategoryId = 4 },

                new Product { ProductId = 8, Name = "Лосось на грилі", Description = "Філе лосося з лимоном", Price = 380.00m, Stock = 12, CategoryId = 5 },

                new Product { ProductId = 9, Name = "Овочі гриль", Description = "Асорті сезонних овочів", Price = 160.00m, Stock = 30, CategoryId = 6 },

                new Product { ProductId = 10, Name = "Фруктова тарілка", Description = "Асорті свіжих фруктів", Price = 140.00m, Stock = 18, CategoryId = 7 },

                new Product { ProductId = 11, Name = "Паста Карбонара", Description = "Паста з беконом і вершковим соусом", Price = 260.00m, Stock = 22, CategoryId = 8 },

                new Product { ProductId = 12, Name = "Шоколадний фондан", Description = "Десерт з рідкою шоколадною начинкою", Price = 150.00m, Stock = 20, CategoryId = 9 },

                new Product { ProductId = 13, Name = "Соус BBQ", Description = "Домашній соус барбекю", Price = 40.00m, Stock = 50, CategoryId = 11 }
            );

            builder.Entity<ProductIngredient>().HasData(
                // Еспресо
                new ProductIngredient { ProductId = 1, IngredientId = 1 }, // Кава арабіка
                new ProductIngredient { ProductId = 1, IngredientId = 3 }, // Цукор (опційно)

                // Капучино
                new ProductIngredient { ProductId = 2, IngredientId = 1 }, // Кава арабіка
                new ProductIngredient { ProductId = 2, IngredientId = 2 }, // Молоко
                new ProductIngredient { ProductId = 2, IngredientId = 3 }, // Цукор (опційно)

                // Чіабата
                new ProductIngredient { ProductId = 3, IngredientId = 4 }, // Борошно
                new ProductIngredient { ProductId = 3, IngredientId = 5 }, // Вершкове масло
                new ProductIngredient { ProductId = 3, IngredientId = 6 }, // Дріжджі
                new ProductIngredient { ProductId = 3, IngredientId = 18 }, // Сіль

                // Круасан з маслом
                new ProductIngredient { ProductId = 4, IngredientId = 4 }, // Борошно
                new ProductIngredient { ProductId = 4, IngredientId = 5 }, // Вершкове масло
                new ProductIngredient { ProductId = 4, IngredientId = 6 }, // Дріжджі
                new ProductIngredient { ProductId = 4, IngredientId = 18 }, // Сіль

                // Сирна тарілка
                new ProductIngredient { ProductId = 5, IngredientId = 2 }, // Молоко
                new ProductIngredient { ProductId = 5, IngredientId = 13 }, // Яйця (для десертів, якщо є)

                // Стейк з яловичини
                new ProductIngredient { ProductId = 6, IngredientId = 7 }, // Яловичина
                new ProductIngredient { ProductId = 6, IngredientId = 18 }, // Сіль

                // Куряче філе гриль
                new ProductIngredient { ProductId = 7, IngredientId = 8 }, // Куряче філе
                new ProductIngredient { ProductId = 7, IngredientId = 18 }, // Сіль

                // Лосось на грилі
                new ProductIngredient { ProductId = 8, IngredientId = 9 }, // Лосось
                new ProductIngredient { ProductId = 8, IngredientId = 18 }, // Сіль

                // Овочі гриль
                new ProductIngredient { ProductId = 9, IngredientId = 16 }, // Овочі
                new ProductIngredient { ProductId = 9, IngredientId = 17 }, // Оливкова олія
                new ProductIngredient { ProductId = 9, IngredientId = 18 }, // Сіль

                // Фруктова тарілка
                new ProductIngredient { ProductId = 10, IngredientId = 15 }, // Фрукти
                new ProductIngredient { ProductId = 10, IngredientId = 3 },  // Цукор (опційно)

                // Паста Карбонара
                new ProductIngredient { ProductId = 11, IngredientId = 10 }, // Паста
                new ProductIngredient { ProductId = 11, IngredientId = 11 }, // Бекон
                new ProductIngredient { ProductId = 11, IngredientId = 12 }, // Вершки
                new ProductIngredient { ProductId = 11, IngredientId = 13 }, // Яйця
                new ProductIngredient { ProductId = 11, IngredientId = 18 }, // Сіль

                // Шоколадний фондан
                new ProductIngredient { ProductId = 12, IngredientId = 14 }, // Шоколад
                new ProductIngredient { ProductId = 12, IngredientId = 13 }, // Яйця
                new ProductIngredient { ProductId = 12, IngredientId = 5 },  // Вершкове масло
                new ProductIngredient { ProductId = 12, IngredientId = 3 },  // Цукор

                // Соус BBQ
                new ProductIngredient { ProductId = 13, IngredientId = 17 }, // Оливкова олія
                new ProductIngredient { ProductId = 13, IngredientId = 18 }  // Сіль
            );
        }
    }
}
