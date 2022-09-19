using Microsoft.AspNetCore.Mvc;

namespace PlayStore.Controllers
{
    public class ErrorController:Controller
    {
        public ViewResult Error()=> View();
    }
}
