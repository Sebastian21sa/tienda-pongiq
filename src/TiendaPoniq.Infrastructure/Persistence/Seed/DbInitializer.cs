using Microsoft.EntityFrameworkCore;
using TiendaPoniq.Domain.Entities;

namespace TiendaPoniq.Infrastructure.Persistence.Seed
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Categorias.AnyAsync())
            {
                return; // ya hay datos, no volver a sembrar
            }

            // Categorías
            var catPalas = new Categoria { Nombre = "Palas", Descripcion = "Palas completas listas para jugar" };
            var catMaderas = new Categoria { Nombre = "Maderas", Descripcion = "Maderas sin gomas, para armar tu pala personalizada" };
            var catGomas = new Categoria { Nombre = "Gomas", Descripcion = "Gomas / cauchos para pegar a la madera" };
            var catBolas = new Categoria { Nombre = "Bolas", Descripcion = "Bolas de competición y entrenamiento" };
            var catMesas = new Categoria { Nombre = "Mesas", Descripcion = "Mesas de tenis de mesa para interior y exterior" };
            var catAccesorios = new Categoria { Nombre = "Accesorios", Descripcion = "Fundas, redes, pegamento y otros accesorios" };

            // Marcas
            var marcaButterfly = new Marca { Nombre = "Butterfly", PaisOrigen = "Japón" };
            var marcaDhs = new Marca { Nombre = "DHS", PaisOrigen = "China" };
            var marcaDonic = new Marca { Nombre = "Donic", PaisOrigen = "Alemania" };
            var marcaStiga = new Marca { Nombre = "Stiga", PaisOrigen = "Suecia" };
            var marcaJoola = new Marca { Nombre = "Joola", PaisOrigen = "Alemania" };
            var marcaXiom = new Marca { Nombre = "Xiom", PaisOrigen = "Corea del Sur" };
            var marcaTibhar = new Marca { Nombre = "Tibhar", PaisOrigen = "Alemania" };
            var marcaNittaku = new Marca { Nombre = "Nittaku", PaisOrigen = "Japón" };
            var marcaCornilleau = new Marca { Nombre = "Cornilleau", PaisOrigen = "Francia" };
            var marcaYasaka = new Marca { Nombre = "Yasaka", PaisOrigen = "Suecia" };

            var productos = new List<Producto>
            {
                // ---- Palas completas ----
                new Producto
                {
                    Nombre = "Butterfly Timo Boll ALC",
                    Descripcion = "Pala completa ofensiva con madera ALC de fibra de carbono",
                    Categoria = catPalas,
                    Marca = marcaButterfly,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "BTF-TBALC-FL", Atributo = "Mango Flare", Precio = 580000m, Stock = 10 },
                        new VarianteProducto { Sku = "BTF-TBALC-ST", Atributo = "Mango Straight", Precio = 580000m, Stock = 6 }
                    }
                },
                new Producto
                {
                    Nombre = "Butterfly Timo Boll Spirit",
                    Descripcion = "Pala completa todoterreno, ideal para jugadores en formación",
                    Categoria = catPalas,
                    Marca = marcaButterfly,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "BTF-TBSP-FL", Atributo = "Mango Flare", Precio = 320000m, Stock = 15 },
                        new VarianteProducto { Sku = "BTF-TBSP-ST", Atributo = "Mango Straight", Precio = 320000m, Stock = 10 }
                    }
                },
                new Producto
                {
                    Nombre = "DHS Hurricane Long 5",
                    Descripcion = "Pala completa de estilo chino, muy popular en el circuito competitivo",
                    Categoria = catPalas,
                    Marca = marcaDhs,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "DHS-HL5-FL", Atributo = "Mango Flare", Precio = 150000m, Stock = 20 },
                        new VarianteProducto { Sku = "DHS-HL5-ST", Atributo = "Mango Straight", Precio = 150000m, Stock = 18 }
                    }
                },
                new Producto
                {
                    Nombre = "Stiga Pro Carbon",
                    Descripcion = "Pala completa ofensiva con tecnología de carbono",
                    Categoria = catPalas,
                    Marca = marcaStiga,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "STG-PROC-FL", Atributo = "Mango Flare", Precio = 420000m, Stock = 9 },
                        new VarianteProducto { Sku = "STG-PROC-ST", Atributo = "Mango Straight", Precio = 420000m, Stock = 7 }
                    }
                },
                new Producto
                {
                    Nombre = "Joola Rossi",
                    Descripcion = "Pala completa equilibrada entre ataque y control",
                    Categoria = catPalas,
                    Marca = marcaJoola,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "JOL-ROSSI-ST", Atributo = "Estándar", Precio = 260000m, Stock = 14 }
                    }
                },
                new Producto
                {
                    Nombre = "Xiom Muv 9",
                    Descripcion = "Pala completa de gama media-alta para jugadores en progresión",
                    Categoria = catPalas,
                    Marca = marcaXiom,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "XIO-MUV9-FL", Atributo = "Mango Flare", Precio = 310000m, Stock = 11 },
                        new VarianteProducto { Sku = "XIO-MUV9-ST", Atributo = "Mango Straight", Precio = 310000m, Stock = 8 }
                    }
                },
                new Producto
                {
                    Nombre = "Donic Appelgren Allplay",
                    Descripcion = "Pala completa versátil para todo tipo de estilos de juego",
                    Categoria = catPalas,
                    Marca = marcaDonic,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "DON-APAP-ST", Atributo = "Estándar", Precio = 190000m, Stock = 16 }
                    }
                },
                new Producto
                {
                    Nombre = "Tibhar Pro Platin",
                    Descripcion = "Pala completa de control, recomendada para iniciación avanzada",
                    Categoria = catPalas,
                    Marca = marcaTibhar,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "TIB-PROPL-ST", Atributo = "Estándar", Precio = 275000m, Stock = 12 }
                    }
                },

                // ---- Maderas (sin gomas) ----
                new Producto
                {
                    Nombre = "Butterfly Viscaria",
                    Descripcion = "Madera ofensiva de fibra de carbono, usada por jugadores profesionales",
                    Categoria = catMaderas,
                    Marca = marcaButterfly,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "BTF-VISC-FL", Atributo = "Mango Flare", Precio = 650000m, Stock = 6 },
                        new VarianteProducto { Sku = "BTF-VISC-ST", Atributo = "Mango Straight", Precio = 650000m, Stock = 4 }
                    }
                },
                new Producto
                {
                    Nombre = "Butterfly Korbel",
                    Descripcion = "Madera clásica de 5 capas, muy versátil",
                    Categoria = catMaderas,
                    Marca = marcaButterfly,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "BTF-KORB-FL", Atributo = "Mango Flare", Precio = 210000m, Stock = 14 },
                        new VarianteProducto { Sku = "BTF-KORB-ST", Atributo = "Mango Straight", Precio = 210000m, Stock = 10 }
                    }
                },
                new Producto
                {
                    Nombre = "DHS Hurricane Long 5 (madera)",
                    Descripcion = "Madera sola de estilo chino, base de muchas palas de ataque",
                    Categoria = catMaderas,
                    Marca = marcaDhs,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "DHS-HL5M-ST", Atributo = "Estándar", Precio = 95000m, Stock = 20 }
                    }
                },
                new Producto
                {
                    Nombre = "Stiga Clipper Wood",
                    Descripcion = "Madera all-round de 5 capas de madera pura",
                    Categoria = catMaderas,
                    Marca = marcaStiga,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "STG-CLIP-FL", Atributo = "Mango Flare", Precio = 180000m, Stock = 12 },
                        new VarianteProducto { Sku = "STG-CLIP-ST", Atributo = "Mango Straight", Precio = 180000m, Stock = 9 }
                    }
                },
                new Producto
                {
                    Nombre = "Donic Waldner Senso Carbon",
                    Descripcion = "Madera ofensiva con capas de carbono, muy rápida",
                    Categoria = catMaderas,
                    Marca = marcaDonic,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "DON-WSC-ST", Atributo = "Estándar", Precio = 480000m, Stock = 7 }
                    }
                },
                new Producto
                {
                    Nombre = "Nittaku Acoustic Carbon",
                    Descripcion = "Madera premium japonesa de carbono para jugadores avanzados",
                    Categoria = catMaderas,
                    Marca = marcaNittaku,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "NIT-ACAR-ST", Atributo = "Estándar", Precio = 520000m, Stock = 5 }
                    }
                },

                // ---- Gomas ----
                new Producto
                {
                    Nombre = "Butterfly Tenergy 05",
                    Descripcion = "La goma ofensiva más icónica del circuito profesional",
                    Categoria = catGomas,
                    Marca = marcaButterfly,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "BTF-TEN05-19", Atributo = "1.9mm", Precio = 320000m, Stock = 25 },
                        new VarianteProducto { Sku = "BTF-TEN05-21", Atributo = "2.1mm", Precio = 320000m, Stock = 20 },
                        new VarianteProducto { Sku = "BTF-TEN05-MX", Atributo = "Max", Precio = 320000m, Stock = 15 }
                    }
                },
                new Producto
                {
                    Nombre = "Butterfly Tenergy 64",
                    Descripcion = "Goma de alta rotación y velocidad explosiva",
                    Categoria = catGomas,
                    Marca = marcaButterfly,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "BTF-TEN64-19", Atributo = "1.9mm", Precio = 300000m, Stock = 18 },
                        new VarianteProducto { Sku = "BTF-TEN64-21", Atributo = "2.1mm", Precio = 300000m, Stock = 14 }
                    }
                },
                new Producto
                {
                    Nombre = "DHS Hurricane 3 Neo",
                    Descripcion = "Goma de pegamento interno, tacky, para jugadores de ataque",
                    Categoria = catGomas,
                    Marca = marcaDhs,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "DHS-H3N-20", Atributo = "2.0mm", Precio = 110000m, Stock = 22 },
                        new VarianteProducto { Sku = "DHS-H3N-MX", Atributo = "Max", Precio = 110000m, Stock = 16 }
                    }
                },
                new Producto
                {
                    Nombre = "Donic Bluefire M1",
                    Descripcion = "Goma de tensión moderna, buen balance velocidad/control",
                    Categoria = catGomas,
                    Marca = marcaDonic,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "DON-BFM1-20", Atributo = "2.0mm", Precio = 180000m, Stock = 17 },
                        new VarianteProducto { Sku = "DON-BFM1-MX", Atributo = "Max", Precio = 180000m, Stock = 13 }
                    }
                },
                new Producto
                {
                    Nombre = "Xiom Vega Europe",
                    Descripcion = "Goma de entrada a gama de tensión, buena relación precio-rendimiento",
                    Categoria = catGomas,
                    Marca = marcaXiom,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "XIO-VEGE-20", Atributo = "2.0mm", Precio = 160000m, Stock = 20 },
                        new VarianteProducto { Sku = "XIO-VEGE-MX", Atributo = "Max", Precio = 160000m, Stock = 15 }
                    }
                },
                new Producto
                {
                    Nombre = "Tibhar Evolution MX-P",
                    Descripcion = "Goma de tensión para juego moderno de topspin",
                    Categoria = catGomas,
                    Marca = marcaTibhar,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "TIB-EVMXP-20", Atributo = "2.0mm", Precio = 170000m, Stock = 19 },
                        new VarianteProducto { Sku = "TIB-EVMXP-MX", Atributo = "Max", Precio = 170000m, Stock = 12 }
                    }
                },
                new Producto
                {
                    Nombre = "Yasaka Mark V",
                    Descripcion = "La goma all-round más vendida de la historia",
                    Categoria = catGomas,
                    Marca = marcaYasaka,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "YAS-MKV-19", Atributo = "1.9mm", Precio = 140000m, Stock = 30 },
                        new VarianteProducto { Sku = "YAS-MKV-20", Atributo = "2.0mm", Precio = 140000m, Stock = 25 },
                        new VarianteProducto { Sku = "YAS-MKV-MX", Atributo = "Max", Precio = 140000m, Stock = 20 }
                    }
                },

                // ---- Bolas ----
                new Producto
                {
                    Nombre = "Butterfly 3-Star",
                    Descripcion = "Bola de competición certificada ITTF",
                    Categoria = catBolas,
                    Marca = marcaButterfly,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "BTF-B3S-X3", Atributo = "Caja x3", Precio = 35000m, Stock = 40 },
                        new VarianteProducto { Sku = "BTF-B3S-X6", Atributo = "Caja x6", Precio = 65000m, Stock = 25 }
                    }
                },
                new Producto
                {
                    Nombre = "DHS 3-Star ABS",
                    Descripcion = "Bola de plástico ABS de competición",
                    Categoria = catBolas,
                    Marca = marcaDhs,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "DHS-B3S-X6", Atributo = "Caja x6", Precio = 60000m, Stock = 30 },
                        new VarianteProducto { Sku = "DHS-B3S-X10", Atributo = "Caja x10", Precio = 95000m, Stock = 20 }
                    }
                },
                new Producto
                {
                    Nombre = "Nittaku Premium 3-Star",
                    Descripcion = "Bola japonesa de alta calidad para competición",
                    Categoria = catBolas,
                    Marca = marcaNittaku,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "NIT-P3S-X3", Atributo = "Caja x3", Precio = 40000m, Stock = 35 }
                    }
                },
                new Producto
                {
                    Nombre = "Joola Flash 40+",
                    Descripcion = "Bola de entrenamiento de buena durabilidad",
                    Categoria = catBolas,
                    Marca = marcaJoola,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "JOL-FL40-X6", Atributo = "Caja x6", Precio = 55000m, Stock = 28 }
                    }
                },
                new Producto
                {
                    Nombre = "Stiga 3-Star",
                    Descripcion = "Bola de competición de la marca sueca",
                    Categoria = catBolas,
                    Marca = marcaStiga,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "STG-B3S-X6", Atributo = "Caja x6", Precio = 50000m, Stock = 32 }
                    }
                },

                // ---- Mesas ----
                new Producto
                {
                    Nombre = "Cornilleau 500M Crossover",
                    Descripcion = "Mesa para interior y exterior, resistente a la intemperie",
                    Categoria = catMesas,
                    Marca = marcaCornilleau,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "COR-500M-AZ", Atributo = "Azul", Precio = 3200000m, Stock = 5 }
                    }
                },
                new Producto
                {
                    Nombre = "Cornilleau Sport 250S",
                    Descripcion = "Mesa de interior plegable para uso en casa o club",
                    Categoria = catMesas,
                    Marca = marcaCornilleau,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "COR-250S-AZ", Atributo = "Azul", Precio = 2100000m, Stock = 6 }
                    }
                },
                new Producto
                {
                    Nombre = "Stiga Expert Roller",
                    Descripcion = "Mesa de interior con ruedas, fácil de transportar",
                    Categoria = catMesas,
                    Marca = marcaStiga,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "STG-EXPR-VD", Atributo = "Verde", Precio = 2800000m, Stock = 4 }
                    }
                },
                new Producto
                {
                    Nombre = "Joola Transport",
                    Descripcion = "Mesa de interior plegable de gama media",
                    Categoria = catMesas,
                    Marca = marcaJoola,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "JOL-TRANS-AZ", Atributo = "Azul", Precio = 1950000m, Stock = 7 }
                    }
                },

                // ---- Accesorios ----
                new Producto
                {
                    Nombre = "Butterfly Table Tennis Net Set",
                    Descripcion = "Juego de red y postes de competición",
                    Categoria = catAccesorios,
                    Marca = marcaButterfly,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "BTF-NETSET-ST", Atributo = "Estándar", Precio = 85000m, Stock = 20 }
                    }
                },
                new Producto
                {
                    Nombre = "Donic Cleaning Sponge",
                    Descripcion = "Esponja limpiadora para gomas",
                    Categoria = catAccesorios,
                    Marca = marcaDonic,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "DON-CLSP-ST", Atributo = "Estándar", Precio = 25000m, Stock = 40 }
                    }
                },
                new Producto
                {
                    Nombre = "Butterfly Case Bag",
                    Descripcion = "Funda acolchada para pala",
                    Categoria = catAccesorios,
                    Marca = marcaButterfly,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "BTF-CASE-NG", Atributo = "Negro", Precio = 90000m, Stock = 15 },
                        new VarianteProducto { Sku = "BTF-CASE-AZ", Atributo = "Azul", Precio = 90000m, Stock = 12 }
                    }
                },
                new Producto
                {
                    Nombre = "Tibhar Grip Tape",
                    Descripcion = "Cinta de agarre para el mango de la pala",
                    Categoria = catAccesorios,
                    Marca = marcaTibhar,
                    Variantes = new List<VarianteProducto>
                    {
                        new VarianteProducto { Sku = "TIB-GRIP-ST", Atributo = "Estándar", Precio = 18000m, Stock = 50 }
                    }
                }
            };

            await context.Productos.AddRangeAsync(productos);
            await context.SaveChangesAsync();
        }
    }
}