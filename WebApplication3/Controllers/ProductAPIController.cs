using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.ProductModels;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var sanPham = (from p in db.TDanhMucSps
                           select new Product
                           {
                               MaSp = p.MaSp,
                               TenSp = p.TenSp,
                               MaLoai = p.MaLoai,
                               AnhDaiDien = p.AnhDaiDien,
                               GiaNhoNhat = p.GiaNhoNhat,
                           }).ToList();
            return sanPham;
        }
        [HttpGet("maloai={maloai}")]
        public IEnumerable<Product> GetProductsByCategory(string maLoai)
        {
            var sanPham = (from p in db.TDanhMucSps
                           where p.MaLoai == maLoai
                           select new Product
                           {
                               MaSp = p.MaSp,
                               TenSp = p.TenSp,
                               MaLoai = p.MaLoai,
                               AnhDaiDien = p.AnhDaiDien,
                               GiaNhoNhat = p.GiaNhoNhat,
                           }).ToList();
            return sanPham;
        }
        [HttpGet("masp={masp}")]
        public IActionResult GetProductsDetailByCategory(string masp)
        {
            var sanPham = (from p in db.TChiTietSanPhams
                           where p.MaSp.Trim().Equals(masp.Trim())
                           select p
                           ).FirstOrDefault();
            if (sanPham == null)
            {
                return NotFound();
            }

            return Ok(sanPham);
        }
    }
}
