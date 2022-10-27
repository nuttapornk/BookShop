using BookShop.Infra;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookShop.WebUi.Services
{
    public class SelectListService : ISelectListService
    {
        private readonly AppDbContext _context;
        public SelectListService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SelectList> GetPublisherAsync(int? id = null)
        {
            var data = await _context.Publishers
                .Where(a => a.Status == 1)
                .ToListAsync();

            return new SelectList(data, "Id", "Name", id);
        }
    }
}
