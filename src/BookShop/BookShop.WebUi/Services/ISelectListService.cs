using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.WebUi.Services
{
    public interface ISelectListService
    {
        public Task<SelectList> GetPublisherAsync(int? id = null);
    }
}
