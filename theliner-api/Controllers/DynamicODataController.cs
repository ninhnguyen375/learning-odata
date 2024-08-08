using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using theliner_api.Data;
using theliner_api.Models;

namespace theliner_api.Controllers
{
    public class DynamicODataController<TEntity> : ODataController
        where TEntity : class
    {
        public readonly ApplicationDbContext _context;
        public readonly DbSet<TEntity> _dbSet;

        public DynamicODataController(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        [EnableQuery(PageSize = 2000)]
        public virtual IActionResult Get()
        {
            return Ok(_dbSet);
        }

        [EnableQuery]
        public IActionResult Get([FromODataUri] int key)
        {
            var entity = _dbSet.Find(key);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        public async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        public async Task<IActionResult> Patch(
            [FromODataUri] int key,
            [FromBody] Delta<TEntity> delta
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _dbSet.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            delta.Patch(entity);
            await _context.SaveChangesAsync();
            return Updated(entity);
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var entity = await _dbSet.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class UsersController : DynamicODataController<User>
    {
        public UsersController(ApplicationDbContext context)
            : base(context) { }
    }

    public class ProductsController : DynamicODataController<Product>
    {
        public ProductsController(ApplicationDbContext context)
            : base(context) { }
    }

    public class ProductDetailsController : DynamicODataController<ProductDetail>
    {
        public ProductDetailsController(ApplicationDbContext context)
            : base(context) { }
    }
}
