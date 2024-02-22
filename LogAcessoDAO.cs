using System;
using System.Collections.Generic;
using System.Linq;

namespace Tabuleiro
{
    internal class LogAcessoDAO
    {
        // Método para cadastrar um novo registro de log de acesso para um usuário
        internal static void Cadastrar(Usuario user)
        {
            // Cria um novo objeto LogAcesso
            LogAcesso log = new LogAcesso();

            // Define o ID do usuário e o momento do último acesso
            log.UsuarioId = user.idUsuario;
            log.UltimoAcesso = DateTime.Now;

            // Usa o contexto do banco de dados para adicionar o log e salvar as alterações
            using (var ctx = new DatabaseEntities())
            {
                ctx.LogAcessoes.Add(log);
                ctx.SaveChanges();
            }
        }

        // Método para selecionar registros de log de acesso para um determinado usuário
        internal static List<LogAcesso> SelecionarLogAcessosPorUsuario(int usuarioId)
        {
            // Lista para armazenar os registros de log
            List<LogAcesso> logs = null;
            try
            {
                // Usa o contexto do banco de dados para selecionar os logs, filtrados pelo ID do usuário e ordenados pelo último acesso
                using (var ctx = new DatabaseEntities())
                {
                    logs = ctx.LogAcessoes
                        .Where(x => x.UsuarioId == usuarioId)
                        .OrderByDescending(x => x.UltimoAcesso)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Lida com a exceção, tratamento de erro
            }
            return logs;
        }
    }
}