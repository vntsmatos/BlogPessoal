﻿using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using System.Collections.Generic;

namespace BlogPessoal.src.repositorios
{
    /// <summary>
    /// <para>Resumo:: Responsavel por representar açoes de CRUD de postagem</para>
    /// <para>Criado por: Vinicius Santos Matos</para>
    /// <para>Versao: 1.0 </para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface IPostagem
    {
        void NovaPostagem(NovaPostagemDTO postagem);
        void AtualizarPostageem(AtualizarPostagemDTO postagem);
        void DeletarPostagem(int id);
        PostagemModelo PegarPostagemPeloId(int id);
        List<PostagemModelo> PegarTodasPostagens();
        List<PostagemModelo> PegarPostagensPeloTitulo(string titulo);
        List<PostagemModelo> PegarPostagensPelaDescricao(string descricao);
    }
}
