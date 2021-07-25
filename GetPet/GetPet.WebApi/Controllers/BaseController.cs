using GetPet.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetPet.WebApi.Controllers
{
    public class BaseController: ControllerBase
    {
        protected User CurrentUser => HttpContext.Items["User"] as User;

        protected bool IsLogin => CurrentUser != null;        
    }
}
