using System;
using System.Collections.Generic;
using System.Linq;

namespace Tabuleiro
{
    internal class PerfilDAO
    {
        // Método para listar todos os perfis disponíveis
        internal static List<Perfil> ListarPerfis()
        {
            List<Perfil> perfis = null;
            try
            {
                // Usando o contexto do banco de dados para obter a lista de perfis, ordenados pelo nome
                using (var ctx = new DatabaseEntities())
                {
                    perfis = ctx.Perfils.OrderBy(x => x.Nome).ToList();
                }
            }
            catch (Exception ex)
            {
                // Lida com a exceção, se ocorrer algum erro durante a obtenção dos perfis
            }
            return perfis;
        }

        // Método para selecionar um perfil com base no ID do perfil
        internal static Perfil SelecionarPerfil(int perfilId)
        {
            Perfil perfil = null;
            try
            {
                // Usando o contexto do banco de dados para selecionar o perfil com base no ID
                using (var ctx = new DatabaseEntities())
                {
                    perfil = ctx.Perfils.FirstOrDefault(x => x.idPerfil == perfilId);
                }
            }
            catch (Exception ex)
            {
                // Lida com a exceção, se ocorrer algum erro durante a seleção do perfil
            }
            return perfil;
        }
    }
}