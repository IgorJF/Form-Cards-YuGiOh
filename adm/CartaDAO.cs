using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Tabuleiro.adm
{
    internal class CartaDAO
    {
        internal static string Cadastrar(Carta carta)
        {
            string retorno = "";

            try
            {
                // Usando o contexto do banco de dados para adicionar o usuário e salvar as alterações
                using (var ctx = new DatabaseEntities())
                {
                    ctx.Cartas.Add(carta);
                    ctx.SaveChanges();
                }

                retorno = "Carta cadastrada com sucesso!";
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }

            return retorno;
        }

        internal static List<Carta> ObterTodasAsCartas()
        {
            List<Carta> cartas = new List<Carta>();

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    cartas = ctx.Cartas.ToList();
                }
            }
            catch (Exception ex)
            {
                // Lide com exceções, por exemplo, logue o erro
                Console.WriteLine("Erro ao obter as cartas: " + ex.Message);
            }

            return cartas;
        }

        internal static int ObterProximoID()
        {
            int proximoID = 1; // Valor padrão, caso não haja registros ainda

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    // Consulta para encontrar o próximo ID disponível
                    var maiorID = ctx.Cartas.Max(c => (int?)c.idCarta) ?? 0;
                    proximoID = maiorID + 1;
                }
            }
            catch (Exception ex)
            {
                // Lida com a exceção, por exemplo, logando o erro
                Console.WriteLine("Erro ao obter o próximo ID: " + ex.Message);
            }

            return proximoID;
        }

        internal static int ObterUltimoIDCadastrado()
        {
            int ultimoID = 0;

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    var ultimaCarta = ctx.Cartas.OrderByDescending(c => c.idCarta).FirstOrDefault();

                    if (ultimaCarta != null)
                    {
                        ultimoID = ultimaCarta.idCarta;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter o último ID cadastrado: " + ex.Message);
                // Lidar com a exceção conforme necessário
            }

            return ultimoID;
        }


        internal static List<Carta> ObterCartasDaZona(int zonaId)
        {
            List<Carta> cartasZona = new List<Carta>();

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    // Consulta ao banco de dados para obter as cartas associadas à zona específica
                    cartasZona = ctx.Cartas.Where(c => c.ZonaId == zonaId).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter as cartas da zona: " + ex.Message);
                // Você pode lidar com a exceção conforme necessário, como registrá-la ou tratar de outra forma
            }

            return cartasZona;
        }

        internal static void AssociarCartasAZona(int zonaId, List<int> cartasSelecionadas)
        {
            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    // Obtém todas as cartas que estão na lista de cartas selecionadas
                    var cartas = ctx.Cartas.Where(c => cartasSelecionadas.Contains(c.idCarta)).ToList();

                    // Atualiza o ID da zona para cada carta encontrada
                    foreach (var carta in cartas)
                    {
                        carta.ZonaId = zonaId;
                    }

                    // Salva as alterações no banco de dados
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao associar as cartas à zona: " + ex.Message);
            }
        }

        internal static List<Carta> ObterCartasDaZonaAM(int zonaId)
        {
            List<Carta> cartasZona = new List<Carta>();

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    cartasZona = ctx.Cartas.Where(c => c.ZonaId == zonaId).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter as cartas da Zona de Armadilhas e Magias: " + ex.Message);
                // Trate a exceção conforme necessário
            }

            return cartasZona;
        }

        internal static void AssociarCartasAZonaAM(int zonaId, List<int> cartasSelecionadas)
        {
            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    var cartas = ctx.Cartas.Where(c => cartasSelecionadas.Contains(c.idCarta)).ToList();

                    foreach (var carta in cartas)
                    {
                        carta.ZonaId = zonaId;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao associar as cartas à Zona de Armadilhas e Magias: " + ex.Message);
                // Trate a exceção conforme necessário
            }
        }

        internal static List<Carta> ObterCartasDaZonaDeckPrincipal(int zonaId)
        {
            List<Carta> cartasZona = new List<Carta>();

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    cartasZona = ctx.Cartas.Where(c => c.ZonaId == zonaId).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter as cartas da Zona de Deck Principal: " + ex.Message);
                // Trate a exceção conforme necessário
            }

            return cartasZona;
        }

        internal static void AssociarCartasAZonaDeckPrincipal(int zonaId, List<int> cartasSelecionadas)
        {
            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    var cartas = ctx.Cartas.Where(c => cartasSelecionadas.Contains(c.idCarta)).ToList();

                    foreach (var carta in cartas)
                    {
                        carta.ZonaId = zonaId;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao associar as cartas à Zona de Deck Principal: " + ex.Message);
                // Trate a exceção conforme necessário
            }
        }

        internal static List<Carta> ObterCartasDaZonaCemiterio(int zonaId)
        {
            List<Carta> cartasZona = new List<Carta>();

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    cartasZona = ctx.Cartas.Where(c => c.ZonaId == zonaId).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter as cartas da Zona do Cemitério: " + ex.Message);
                // Trate a exceção conforme necessário
            }

            return cartasZona;
        }

        internal static void AssociarCartasAZonaCemiterio(int zonaId, List<int> cartasSelecionadas)
        {
            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    var cartas = ctx.Cartas.Where(c => cartasSelecionadas.Contains(c.idCarta)).ToList();

                    foreach (var carta in cartas)
                    {
                        carta.ZonaId = zonaId;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao associar as cartas à Zona do Cemitério: " + ex.Message);
                // Trate a exceção conforme necessário
            }
        }

        internal static List<Carta> ObterCartasDaZonaCampo(int zonaId)
        {
            List<Carta> cartasZona = new List<Carta>();

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    cartasZona = ctx.Cartas.Where(c => c.ZonaId == zonaId).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter as cartas da Zona de Campo: " + ex.Message);
                // Trate a exceção conforme necessário
            }

            return cartasZona;
        }

        internal static void AssociarCartasAZonaCampo(int zonaId, List<int> cartasSelecionadas)
        {
            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    var cartas = ctx.Cartas.Where(c => cartasSelecionadas.Contains(c.idCarta)).ToList();

                    foreach (var carta in cartas)
                    {
                        carta.ZonaId = zonaId;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao associar as cartas à Zona de Campo: " + ex.Message);
                // Trate a exceção conforme necessário
            }
        }

        internal static List<Carta> ObterCartasDaZonaDeckAdicional(int zonaId)
        {
            List<Carta> cartasZona = new List<Carta>();

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    cartasZona = ctx.Cartas.Where(c => c.ZonaId == zonaId).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter as cartas da Zona de Deck Adicional: " + ex.Message);
                // Trate a exceção conforme necessário
            }

            return cartasZona;
        }

        internal static void AssociarCartasAZonaDeckAdicional(int zonaId, List<int> cartasSelecionadas)
        {
            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    var cartas = ctx.Cartas.Where(c => cartasSelecionadas.Contains(c.idCarta)).ToList();

                    foreach (var carta in cartas)
                    {
                        carta.ZonaId = zonaId;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao associar as cartas à Zona de Deck Adicional: " + ex.Message);
                // Trate a exceção conforme necessário
            }
        }

        internal static List<Carta> ObterCartasDaZonaPendulo(int zonaId)
        {
            List<Carta> cartasZona = new List<Carta>();

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    cartasZona = ctx.Cartas.Where(c => c.ZonaId == zonaId).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter as cartas da Zona de Pêndulo: " + ex.Message);
                // Trate a exceção conforme necessário
            }

            return cartasZona;
        }

        internal static void AssociarCartasAZonaPendulo(int zonaId, List<int> cartasSelecionadas)
        {
            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    var cartas = ctx.Cartas.Where(c => cartasSelecionadas.Contains(c.idCarta)).ToList();

                    foreach (var carta in cartas)
                    {
                        carta.ZonaId = zonaId;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao associar as cartas à Zona de Pêndulo: " + ex.Message);
                // Trate a exceção conforme necessário
            }
        }

        public static string ObterZonaDaCartaPorID(int idCarta)
        {
            string zona = "Sem Zona"; // Valor padrão caso não encontre a zona

            try
            {
                using (var ctx = new DatabaseEntities())
                {
                    var carta = ctx.Cartas.FirstOrDefault(c => c.idCarta == idCarta);

                    if (carta != null && carta.ZonaId.HasValue)
                    {
                        zona = ObterNomeZonaPorId(carta.ZonaId.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter a zona da carta: " + ex.Message);
                // Lidar com a exceção, como registrar o erro ou tratar de outra forma
            }

            return zona;
        }

        // Método auxiliar para obter o nome da zona com base no ID
        private static string ObterNomeZonaPorId(int zonaId)
        {
            Dictionary<int, string> zonas = new Dictionary<int, string>
        {
            { 1, "Zona de Monstros" },
            { 2, "Zona de Magias e Armadilhas" },
            { 3, "Cemiterio" },
            { 4, "Zona do Deck Principal" },
            { 5, "Zona de Campo" },
            { 6, "Zona o Deck Adicional" },
            { 7, "Zona de Pendulo" }
        };

            return zonas.ContainsKey(zonaId) ? zonas[zonaId] : "Sem Zona";
        }
    }
}