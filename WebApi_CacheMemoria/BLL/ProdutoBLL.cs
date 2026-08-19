using WebApi_CacheMemoria.DAL;
using WebApi_CacheMemoria.Models;

namespace WebApi_CacheMemoria.BLL
{
    public class ProdutoBLL
    {
        private readonly ProdutoDAL _produtoDAL;

        public ProdutoBLL(ProdutoDAL produtoDAL)
        {
            _produtoDAL = produtoDAL;
        }

        public List<Produto> Listar()
        {
            return _produtoDAL.Listar();
        }

        public Produto? BuscarPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O ID deve ser maior que zero.");
            }

            return _produtoDAL.BuscarPorId(id);
        }

        public int Inserir(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                throw new ArgumentException("O nome do produto é obrigatório.");
            }

            if (produto.Preco <= 0)
            {
                throw new ArgumentException( "O preço deve ser maior que zero.");
            }

            if (produto.Estoque < 0)
            {
                throw new ArgumentException("O estoque não pode ser negativo.");
            }

            return _produtoDAL.Inserir(produto);
        }

        public void Atualizar(int id, Produto produto)
        {
            var produtoExistente = _produtoDAL.BuscarPorId(id);

            if (produtoExistente is null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            produto.Id = id;

            _produtoDAL.Atualizar(produto);
        }

        public void Excluir(int id)
        {
            var produto = _produtoDAL.BuscarPorId(id);

            if (produto is null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            _produtoDAL.Excluir(id);
        }
    }
}