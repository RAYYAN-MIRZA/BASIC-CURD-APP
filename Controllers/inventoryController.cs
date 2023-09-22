using crud.data;
using crud.madals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud.Controllers
{
    [ApiController]// to tell asp .net this is not a viwew controller
    [Route("api/inventory")]

    public class inventoryController : Controller
    {
        private  // : making it look alike of c++
        readonly databaseClass _databaseClass;// object hai  ye
        
        public        //  : making it look like c++
        inventoryController(databaseClass database) // 
        {
            _databaseClass = database;
        }

        [HttpGet] // get all data

        public async Task<IActionResult> GetData()
        {
          var inventory=  await _databaseClass.Inventory.ToListAsync();
            return Ok(inventory);
        }
        [HttpPost] // add a data(row)
        [Route("add")]
        public async Task<IActionResult> Add_prod([FromBody]Data datarequest)
        {
            datarequest.ID=Guid.NewGuid();
            await _databaseClass.Inventory.AddAsync(datarequest);
            await _databaseClass.SaveChangesAsync();
            return Ok(datarequest);
        }
        [HttpPut] // update a row 
        [Route("update")]
        public async Task<IActionResult> Update([FromBody]Data updateData)
        {
          var invent=  await _databaseClass.Inventory.FindAsync(updateData.ID);
            if (invent == null)
            {
                return NotFound();
            }
            invent.prod_name=updateData.prod_name;
            invent.price=updateData.price;
            invent.description=updateData.description;

            await _databaseClass.SaveChangesAsync();
            return Ok(invent);
        }
        [HttpDelete] // delete a row
        [Route("delete/{id:Guid}")]
           public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var invent = await _databaseClass.Inventory.FindAsync(id);
            if(invent   == null ){
                return NotFound();
            }
            _databaseClass.Inventory.Remove(invent);
            await _databaseClass.SaveChangesAsync();
            return Ok();
        }
    }
}
    