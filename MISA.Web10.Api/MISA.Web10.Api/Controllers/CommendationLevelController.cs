using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web10.Core.Interfaces.Repository;
using MISA.Web10.Core.Interfaces.Services;
using MISA.Web10.Core.Models;

namespace MISA.Web10.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommendationLevelController : BaseController<CommendationLevel>
    {
        public CommendationLevelController(IBaseRepository<CommendationLevel> baseRepository, IBaseService<CommendationLevel> baseService) : base(baseRepository, baseService)
        {
        }
    }
}
