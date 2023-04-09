using BTLWeb.Models;
using BTLWeb.Repository;
using Microsoft.AspNetCore.Mvc;
namespace BTLWeb.ViewComponents
{
    public class LoaiSpMenuComponent : ViewComponent
    {
        private readonly IELoaiSpRepository _loaisp;
        public LoaiSpMenuComponent(IELoaiSpRepository loaiSpRepository)
        {
            _loaisp = loaiSpRepository;
        }
        public IViewComponentResult Invoke()
        {
            var loaiSp = _loaisp.GetAllLoaiSp().OrderBy(x => x.Loai);
            return View(loaiSp);
        }
    }
}
