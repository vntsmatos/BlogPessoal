using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositorios.implementacoes
{
    public class TemaRepositorio : ITema
    {
        #region Atributos
        private readonly BlogPessoalContext _context;
        #endregion Atributos
        #region Construtores
        public TemaRepositorio(BlogPessoalContext context)
        {
            _context = context;
        }

        public void AtualizarTema(AtualizarTemaDTO tema)
        {
            var temaExistente = PegarTemaPeloId(tema.Id);
            temaExistente.Descricao = tema.Descricao;
            _context.Temas.Update(temaExistente);
            _context.SaveChanges();

        }

        public void DeletarTema(int id)
        {
            _context.Temas.Remove(PegarTemaPeloId(id));
            _context.SaveChanges();

        }

        public void NovoTema(NovoTemaDTO tema)
        {
            _context.Temas.Add(new TemaModelo
            {
                Descricao = tema.Descricao
            });
            _context.SaveChanges();
        }

        public List<TemaModelo> PegarTodosTemas()
        {
            return _context.Temas.ToList();
        }

        public List<TemaModelo> PegarTemaPelaDescricao(string descricao)
        {
            return _context.Temas
            .Where(u => u.Descricao.Contains(descricao))
            .ToList();
        }

        public TemaModelo PegarTemaPeloId(int id)
        {
            return _context.Temas.FirstOrDefault(u => u.Id == id);

        }

        public List<TemaModelo> PegarTemasPelaDescricao(string descricao)
        {
            return _context.Temas.Where(T => T.Descricao.Contains(descricao)).ToList();
        }
        #endregion Construtores
        #region Métodos
        #endregion Métodos


    }
}
