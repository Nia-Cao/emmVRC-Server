using Microsoft.AspNetCore.Mvc;
using emmVRC.Models;
using emmVRC.Services;
using System.Text;
using static emmVRC.classes;
using Newtonsoft.Json;

namespace emmVRC.Controllers
{    
    [ApiController]

    public class avatar : ControllerBase
    {
        private readonly dbService _dbService;

        public avatar(dbService dbService) =>
            _dbService = dbService;

        //Not really an avatar but it's here for now ^^
        [Route("configuration.php")]
        [HttpGet]
        public async Task Config()
        {
            Console.WriteLine("Config");
            byte[] bytes = Encoding.UTF8.GetBytes("{   \"APIUrl\": \"http://localhost:80\",    \"MessageUpdateRate\": 45,    \"DisableAuthFile\": false,    \"DeleteAndDisableAuthFile\": false,    \"DisableAvatarChecks\": true,    \"APICallsAllowed\": true,    \"MessagingEnabled\": true,    \"NetworkAutoRetries\": 3,    \"AvatarPedestalScansAllowed\": true,    \"StartupMessage\": \"\",    \"MessageID\":-1}')}");
            HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            await HttpContext.Response.Body.WriteAsync(bytes);
            await HttpContext.Response.CompleteAsync();
        }
    

        [Route("api/avatar/info/{id}")]
        [HttpGet]
        public async Task Get(string id)
        {
            Console.WriteLine("AVID" + id);

            var avatarFull = new avatarClassStripped();
            avatarClass avatar = await _dbService.GetAvatarInfo(id);

            if (avatar != null)
            {
               string reply = JsonConvert.SerializeObject(avatar);
                byte[] bytes = Encoding.UTF8.GetBytes(reply);
                HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                await HttpContext.Response.Body.WriteAsync(bytes);
                await HttpContext.Response.CompleteAsync();

            }
        }

        [Route("api/avatar")]
        [HttpPut]
        public async Task Put()
        {
            var req = HttpContext.Request;
            string body = null;
            using (StreamReader reader
                  = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }
            Console.WriteLine("Putting");
            Console.WriteLine(body);
            avatarClass temp = JsonConvert.DeserializeObject<avatarClass>(body);

            await _dbService.PutAvatar(temp);
        }
        [Route("api/avatar")]
        [HttpGet]
        //[BearerAuthorization]
        public async Task Get()
        {
            Console.WriteLine("av req");
            var authToken = HttpContext.Request.Headers["Authorization"].ToString();
            var token = new tokens();
            if (authToken != "")
            {
                authToken = authToken.Remove(0, 7);
                token = await _dbService.GetAsync(authToken);
            }
            if (token == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await HttpContext.Response.CompleteAsync();
                Console.WriteLine("UNAUTH");
            }
            else
            {
                //Send users avatars
                var body = "[";
                List<avatarClass> avatars = new List<avatarClass>();
                List<avatarClassStripped> avatarStripped = new List<avatarClassStripped>();
                avatars = await _dbService.GetAvatarAsync(token.userid);
                foreach (var avatar in avatars)
                {
                    body += JsonConvert.SerializeObject(avatar);
                    body += "\n";
                }
                body += "]";
                byte[] bytes = Encoding.UTF8.GetBytes(body);
                HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                await HttpContext.Response.Body.WriteAsync(bytes);
                await HttpContext.Response.CompleteAsync();
            }

        }

        [Route("api/avatar/search")]
        [HttpPost]
        public async Task AvatarSearch()
        {
            var req = HttpContext.Request;
            string body = null;
            using (StreamReader reader
                  = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }

            dynamic temp = JsonConvert.DeserializeObject<dynamic>(body);

            Console.WriteLine(temp.SelectToken("query"));


            Console.WriteLine("Searching");
            Console.WriteLine(body);
            //avatarClass temp = JsonConvert.DeserializeObject<avatarClass>(body);

            var reply = "[";
            List<avatarClass> avatars = new List<avatarClass>();
            avatars = await _dbService.SearchAvatar(temp.SelectToken("query").ToString());
            foreach (var avatar in avatars)
            {
                reply += JsonConvert.SerializeObject(avatar);
                reply += "\n";
            }
            reply += "]";
            byte[] bytes = Encoding.UTF8.GetBytes(reply);
            HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            await HttpContext.Response.Body.WriteAsync(bytes);
            await HttpContext.Response.CompleteAsync();
        }
    }

}


