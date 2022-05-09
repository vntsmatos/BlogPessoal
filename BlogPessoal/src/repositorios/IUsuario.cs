using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using System.Collections.Generic;

namespace BlogPessoal.src.repositorios
{
    /// <summary>
    /// <para>Resumo:: Responsavel por representar açoes de CRUD de usuario</para>
    /// <para>Criado por: Vinicius Santos Matos</para>
    /// <para>Versao: 1.0 </para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>

    public interface IUsuario
    {
        void NovoUsuario(NovoUsuarioDTO usuario);// DTO = Data Transfer Object
        void AtualizarUsuario(AtualizarUsuarioDTO usuario);
        void DeletarUsuario(int id);

        UsuarioModelo PegarUsuarioPeloId(int id);
        UsuarioModelo PegarUsuarioPeloEmail(string email);
        List<UsuarioModelo> PegarUsuariosPeloNome(string nome);
    }
}
