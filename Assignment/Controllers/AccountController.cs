namespace Assignment.Controllers
{
    using DataAccess.Interfaces;
    using Dtos; 
    using Models;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates new user in db
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>
        /// Status 200 if user was successfully created 
        /// Status 400 if were dto is not valid 
        /// Status 400 if user with similar username already exists 
        /// </returns>
        //POST api/account/register
        [HttpPost]
        [Route("register")]
        public async Task<HttpResponseMessage> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var account = await _unitOfWork.AccountRepository
                .GetByPredicate(a => a.Username.Equals(dto.Username));
            
            if (account != null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "User with this name already exists");
            }

            account = new Account
            {
                Username = dto.Username, 
                Amount = dto.Amount, 
                Password = dto.Password,
            };

            var id = _unitOfWork.AccountRepository.Add(account);

            await _unitOfWork.SaveChangesAsync(default);

            return Request.CreateResponse(HttpStatusCode.OK, $"User by name {account.Username} was successfully registered {id}");
        }
        
        /// <summary>
        /// Login new user to system. Unfinished.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>
        /// Status 200 if result of login was effectively
        /// Status 400 if dto is not valid
        /// Status 400 if user was not find by username or password is incorrect
        /// </returns>
        //GET api/account/login
        [HttpGet]
        [Route("login")]
        public async Task<HttpResponseMessage> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var account = await _unitOfWork.AccountRepository
                .GetByPredicate(a => a.Username.Equals(dto.Username));
            
            if (account == null || !account.Password.Equals(dto.Password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect user or password");
            }

            //Login logic
            
            return Request.CreateResponse(HttpStatusCode.OK, $"Login from user by name {account.Username} was successfully");
        }
    }
}
