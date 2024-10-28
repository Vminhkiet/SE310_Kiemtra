using WebApplication3.Models;

namespace WebApplication3.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly QlbanVaLiContext _context;
        public LoaiSpRepository(QlbanVaLiContext context)
        {
            _context = context;
        }
        public TLoaiSp Add(TLoaiSp loaiSp)
        {
            _context.TLoaiSps.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        public TLoaiSp Delete(string maloaiSp)
        {
            // Tìm bản ghi TLoaiSp theo maloaiSp
            var loaiSp = _context.TLoaiSps.Find(maloaiSp);

            // Nếu không tìm thấy, trả về null hoặc ném ngoại lệ
            if (loaiSp == null)
            {
                return null; // Hoặc throw new KeyNotFoundException("Không tìm thấy mã loại sản phẩm.");
            }

            // Xóa bản ghi nếu tìm thấy
            _context.TLoaiSps.Remove(loaiSp);
            _context.SaveChanges();

            // Trả về đối tượng đã xóa
            return loaiSp;
        }


        public IEnumerable<TLoaiSp> GetAllLoaiSp()
        {
            return _context.TLoaiSps;
        }
        public TLoaiSp GetLoaiSp(string maloaiSp)
        {
            return _context.TLoaiSps.Find(maloaiSp);
        }

        public TLoaiSp Update(TLoaiSp loaiSp)
        {
            _context.Update(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }
    }
}
