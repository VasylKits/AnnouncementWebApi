using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace AnnouncementWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : ControllerBase
    {
        // в контролерах методи - прочитати в метаніті, які є типи повернень і атрибути для них
        // в сервісах логіка для екшенів контролера
        // в інтерфейсах створити інтерфейс і оголосити в ньому методи, які потрібні для реалізація завдання CRUD
        // в імплемент має бути реалізація методів() EditAnnoun, AddAn, DelAnn..
        // GitHub  і контролери
    }
}
