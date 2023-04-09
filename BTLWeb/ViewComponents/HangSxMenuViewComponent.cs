using BTLWeb.Models;
using BTLWeb.Repository;
using Microsoft.AspNetCore.Mvc;
namespace BTLWeb.ViewComponents
{
    public class HangSxMenuViewComponent : ViewComponent
    {
        private readonly IHangSxRepository _hangSx;
        public HangSxMenuViewComponent(IHangSxRepository HangSxRepository)
        {
            _hangSx = HangSxRepository;
        }
        public IViewComponentResult Invoke()
        {
            var hangSx = _hangSx.GetAllhangSx().OrderBy(x => x.HangSx);
            return View(hangSx);   
        }
    }
}
