using Microsoft.EntityFrameworkCore;

namespace PlayStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            //ApplicationContext context = app.ApplicationServices.GetRequiredService<ApplicationContext>();
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                //context.Database.Migrate();
                
            }
        }
    }
}
