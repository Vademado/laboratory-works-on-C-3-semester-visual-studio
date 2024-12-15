using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using laboratory_work_11_part_2_WebAPI.Models;

namespace laboratory_work_11_part_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : Controller
    {
        private readonly NorthwindContext context;

        public SuppliersController(NorthwindContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<Supplier>> GetSuppliers()
        {
            return await context.Suppliers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var supplier = await (from suplr in context.Suppliers
                                  where suplr.SupplierId == id
                                  select suplr).FirstOrDefaultAsync();
            if (supplier == null) return NotFound();
            return supplier;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return BadRequest("ID в запросе не совпадает с ID поставщика");
            }

            var supplierDB = await (from suplr in context.Suppliers
                                    where suplr.SupplierId == id
                                    select suplr).FirstOrDefaultAsync();
            if (supplierDB == null) return NotFound();
            foreach (var field in typeof(Supplier).GetProperties())
            {
                if (field.Name == "SupplierId") continue;
                field.SetValue(supplierDB, field.GetValue(supplier));
            }
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateSupplier(Supplier supplier)
        {
            context.Suppliers.Add(supplier);
            await context.SaveChangesAsync();

            return Ok(supplier);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplierDB = await (from suplr in context.Suppliers
                                    where suplr.SupplierId == id
                                    select suplr).FirstOrDefaultAsync();
            if (supplierDB == null) return NotFound();
            context.Suppliers.Remove(supplierDB);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
