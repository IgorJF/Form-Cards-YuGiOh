using System;
using System.Collections.Generic;
using System.Linq;

namespace Tabuleiro
{
    internal class UsuarioDAO
    {
        // Método para cadastrar um novo usuário
        internal static string Cadastrar(Usuario user)
        {
            string retorno = "";

            try
            {
                // Usando o contexto do banco de dados para adicionar o usuário e salvar as alterações
                using (var ctx = new DatabaseEntities())
                {
                    ctx.Usuarios.Add(user);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
            return retorno;
        }

        // Método para selecionar um usuário com base no nome de usuario
        internal static Usuario SelecionarUsuario(string usuario)
        {
            Usuario user = null;
            try
            {
                // Usando o contexto do banco de dados para selecionar o usuário com base no login
                using (var ctx = new DatabaseEntities())
                {
                    user = ctx.Usuarios.FirstOrDefault(x => x.Usuario1 == usuario);
                }
            }
            catch (Exception ex)
            {
            }

            return user;
        }

        // Método para listar todos os usuários
        internal static List<Usuario> SelecionarUsuario()
        {
            string mensagem = "";
            List<Usuario> users = null;
            try
            {
                // Usando o contexto do banco de dados para obter a lista de usuários, ordenados pelo nome
                using (var ctx = new DatabaseEntities())
                {
                    users = ctx.Usuarios.OrderBy(x => x.Nome).ToList();
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return users;
        }

        // Método para selecionar todos os usuários com um perfil específico
        internal static List<Usuario> SelecionarUsuarioPorPerfil(int idPerfil)
        {
            string mensagem = "";
            List<Usuario> users = null;
            try
            {
                // Usando o contexto do banco de dados para obter a lista de usuários com o perfil especificado, ordenados pelo nome
                using (var ctx = new DatabaseEntities())
                {
                    users = ctx.Usuarios.Where(x => x.PerfilId == idPerfil).OrderBy(x => x.Nome).ToList();
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }
            return users;
        }

        // Método para fazer o registro do último acesso do usuário
        internal static Usuario LogUsuario(string login)
        {
            Usuario user = null;

            try
            {
                // Seleciona o usuário com base no login e faz o registro do último acesso
                user = SelecionarUsuario(login);
                LogAcessoDAO.Cadastrar(user);
            }
            catch (Exception ex)
            {
                // Lida com a exceção, se ocorrer algum erro durante o registro do acesso
            }
            return user;
        }
    }
}