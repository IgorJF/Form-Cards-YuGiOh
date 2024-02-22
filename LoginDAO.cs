using System;

namespace Tabuleiro
{
    internal class LoginDAO
    {
        // Método para autenticar um usuário com base em um login e senha criptografada
        internal static bool AutenticarUsuario(string login, string senhaCriptografada)
        {
            try
            {
                // Tenta selecionar o usuário com base no login
                Usuario user = UsuarioDAO.SelecionarUsuario(login);

                // Se o usuário for encontrado
                if (user != null)
                {
                    // Verifica se a senha criptografada fornecida corresponde à senha do usuário
                    if (user.Senha == senhaCriptografada)
                    {
                        // A autenticação é bem-sucedida
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Lida com a exceção, indicando falha na autenticação
                return false;
            }

            // Caso o usuário não seja encontrado ou ocorra uma exceção, a autenticação falha
            return false;
        }
    }
}