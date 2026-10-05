using Microsoft.EntityFrameworkCore;

namespace HW3Admin.Data
{
	public class HW3AdminDBContext : DbContext
	{
		public HW3AdminDBContext()
		{
		}
        public HW3AdminDBContext(DbContextOptions<HW3AdminDBContext> options)
            : base(options)
        { }
        public DbSet<HW3Admin.Models.PRODUCTS> PRODUCT { get; set; } //ตาราง database
    }
}





