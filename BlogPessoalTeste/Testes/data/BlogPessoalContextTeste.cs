using BlogPessoal.src.data;
using BlogPessoal.src.modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;//Para fazer testes unitários

namespace BlogPessoalTeste.Testes.data
{
    [TestClass]
    public class BlogPessoalContextTeste
    {
        private BlogPessoalContext _context;

        [TestInitialize]
        public void inicio()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                .Options;

            _context = new BlogPessoalContext(opt);
        }

        [TestMethod]
        public void InserirNovoUsuarioNoBancoRetornarUsuario()
        {
            UsuarioModelo usuario = new UsuarioModelo();

            usuario.Nome = "Venicio Tangamandapio";
            usuario.Email = "venicio@email.com";
            usuario.Senha = "136452";
            usuario.Foto = "AKITAOLINKDAFOTO";

            _context.Usuarios.Add(usuario); // Adicionando Usuario

            _context.SaveChanges(); // Commita criação

            Assert.IsNotNull(_context.Usuarios.FirstOrDefaultAsync(u => u.Email == "venicio@email.com"));
            //vai pegar primeiro elemento por default


        }
    }
}
