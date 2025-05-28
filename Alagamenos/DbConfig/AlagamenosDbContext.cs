using Microsoft.EntityFrameworkCore;

namespace Alagamenos.DbConfig;

public class AlagamenosDbContext : DbContext
{
    
    public AlagamenosDbContext(DbContextOptions<AlagamenosDbContext> options) : base(options){}
    
    
}