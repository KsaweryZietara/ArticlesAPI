using ArticlesAPI.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesAPI.Api.Controllers{
    
    [Route("api/[controller]")]
    [Controller]
    public class ArticlesController : ControllerBase{
        private readonly IAppRepo _repo;

        public ArticlesController(IAppRepo repo){
            _repo = repo;
        }

        
    }
}