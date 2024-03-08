using Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    //
    public class ProdutoCadastro
    {
        public string Nome { get; set; }
    }
    //

    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly DbLoja Contexto = new DbLoja();

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> ObterLista()
        {
            try 
            {
                var produtos = Contexto.Produtos.ToList();

                return Ok(produtos);
            }
            catch
            {
                return StatusCode(500, "O problema foi sério, mas a gente passa bem!");
            }
        }
        
        [HttpGet("{id}")]
        public ActionResult<Produto> ObterPelaId(Guid id)
        {
            try
            {
                var produto = Contexto.Produtos.Find(id);
                // var produto = Contexto.Produtos.FirstOrDefault(p => p.Id == id);

                if(produto == null)
                {
                    return BadRequest();
                }

                return Ok(produto);
            }
            catch
            {
                return StatusCode(500, "O problema foi sério, mas a gente passa bem!");
            }
        }

        [HttpPost]
        public ActionResult<Produto> Criar([FromBody] ProdutoCadastro novoProduto)
        {
            try 
            {
                var produto = new Produto { Nome = novoProduto.Nome };
                
                Contexto.Produtos.Add(produto);
                Contexto.SaveChanges();

                // return Created();
                return CreatedAtAction(nameof(ObterPelaId), produto);
            }
            catch
            {
                return StatusCode(500, "O problema foi sério, mas a gente passa bem!");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Produto> AtualizarPelaId(Guid id, Produto produtoAtualizado)
        {
            try
            {
                var produto = Contexto.Produtos.Find(id);
                // var produto = Contexto.Produtos.FirstOrDefault(p => p.Id == id);

                if(produto == null || produto.Id != produtoAtualizado.Id)
                {
                    return BadRequest();
                }

                Contexto.Produtos.Entry(produtoAtualizado).State = EntityState.Modified;
                Contexto.SaveChanges();

                // return NoContent();
                return Ok(produtoAtualizado);
            }
            catch
            {
                return StatusCode(500, "O problema foi sério, mas a gente passa bem!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletarPorId(Guid id)
        {
            try
            {
                var produto = Contexto.Produtos.Find(id);
                // var produto = Contexto.Produtos.FirstOrDefault(p => p.Id == id);

                if(produto == null)
                {
                    return BadRequest();
                }

                Contexto.Produtos.Remove(produto);
                Contexto.SaveChanges();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "O problema foi sério, mas a gente passa bem!");
            }
        }
    }
}
