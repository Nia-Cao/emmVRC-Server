using Microsoft.AspNetCore.Mvc;
using emmVRC.Models;
using emmVRC.Services;
using System.Text;
using Newtonsoft.Json;

namespace emmVRC.Controllers
{
    [ApiController]

    public class authentication : ControllerBase
    {
        static public string randomString()
        {


            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[32];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        private readonly dbService _dbService;

        public authentication(dbService dbService) =>
            _dbService = dbService;

        [Route("api/authentication")]
        [HttpPost]
        public async Task Post()
        {

            var req = HttpContext.Request;
            string body = null;
            using (StreamReader reader
                  = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }
            Console.WriteLine(body);
            dynamic temp = JsonConvert.DeserializeObject<dynamic>(body);
            var authToken = HttpContext.Request.Headers["Authorization"].ToString();
            Console.WriteLine("Bearer: " + authToken);
            var token = new tokens();
            var loginKey = new loginKeys();

            string loginToken = temp.SelectToken("loginToken");
            string password = temp.SelectToken("password");
            string user = temp.SelectToken("username");
            Console.WriteLine(loginToken);
            pins pin = await _dbService.Getpin(user);

            if (pin != null && password == pin.pin)
            {
                //Pin provided and is correct, so return an auth token and new login token
                string _token = randomString();
                byte[] _tempTokenBytes = Encoding.UTF8.GetBytes(_token);
                await _dbService.SetToken(BitConverter.ToString(_tempTokenBytes).Replace("-", ""), user);
                string _loginKey = randomString();
                byte[] _tempKeyBytes = Encoding.UTF8.GetBytes(_loginKey);
                await _dbService.SetLoginKey(BitConverter.ToString(_tempKeyBytes).Replace("-", ""), user);
                string _reply = "";

                _reply += "{\"token\": \"" + BitConverter.ToString(_tempTokenBytes).Replace("-", "") + "\"," +
                    "\"loginKey\": \"" + BitConverter.ToString(_tempKeyBytes).Replace("-", "") + "\"," +
                    "\"reset\": \"false\"}";

                byte[] _bytes = Encoding.UTF8.GetBytes(_reply);
                HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                await HttpContext.Response.Body.WriteAsync(_bytes);
                await HttpContext.Response.CompleteAsync();
            }

            else if (pin == null && password != "")
            {
                //No pin
                Console.WriteLine("NO PIN");
                Console.WriteLine("New PIN is: " + password);

                await _dbService.SetPin(password, user);

                string _token = randomString();
                byte[] _tempTokenBytes = Encoding.UTF8.GetBytes(_token);
                await _dbService.SetToken(BitConverter.ToString(_tempTokenBytes).Replace("-", ""), user);
                string _loginKey = randomString();
                byte[] _tempKeyBytes = Encoding.UTF8.GetBytes(_loginKey);
                await _dbService.SetLoginKey(BitConverter.ToString(_tempKeyBytes).Replace("-", ""), user);
                string _reply = "";

                _reply += "{\"token\": \"" + BitConverter.ToString(_tempTokenBytes).Replace("-", "") + "\"," +
                    "\"loginKey\": \"" + BitConverter.ToString(_tempKeyBytes).Replace("-", "") + "\"," +
                    "\"reset\": \"false\"}";

                byte[] _bytes = Encoding.UTF8.GetBytes(_reply);
                HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                await HttpContext.Response.Body.WriteAsync(_bytes);
                await HttpContext.Response.CompleteAsync();
            }

            else if (loginToken != "")
            {
                //Login Token
                Console.WriteLine(loginToken);
                var _loginToken = await _dbService.GetLoginKey(loginToken);
                if (_loginToken != null && _loginToken.userid == user)
                {
                    Console.WriteLine(_loginToken.userid);
                    Console.WriteLine(user);                    string _token = randomString();
                    byte[] _tempTokenBytes = Encoding.UTF8.GetBytes(_token);
                    await _dbService.SetToken(BitConverter.ToString(_tempTokenBytes).Replace("-", ""), user);
                    string _loginKey = randomString();
                    byte[] _tempKeyBytes = Encoding.UTF8.GetBytes(_loginKey);
                    await _dbService.SetLoginKey(BitConverter.ToString(_tempKeyBytes).Replace("-", ""), user);
                    string _reply = "";

                    _reply += "{\"token\": \"" + BitConverter.ToString(_tempTokenBytes).Replace("-", "") + "\"," +
                        "\"loginKey\": \"" + BitConverter.ToString(_tempKeyBytes).Replace("-", "") + "\"," +
                        "\"reset\": \"false\"}";

                    byte[] _bytes = Encoding.UTF8.GetBytes(_reply);
                    HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                    await HttpContext.Response.Body.WriteAsync(_bytes);
                    await HttpContext.Response.CompleteAsync();
                }
                else
                {
                    string _reply = "";

                    _reply += "{\"message:\": \"invalid combination\"}";

                    byte[] _bytes = Encoding.UTF8.GetBytes(_reply);
                    HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await HttpContext.Response.Body.WriteAsync(_bytes);
                }

            }

            else
            {
                //No match so return invalid combination
                Console.WriteLine("Invalid Combination");
                string _reply = "";

                _reply += "{\"message:\": \"invalid combination\"}";

                byte[] _bytes = Encoding.UTF8.GetBytes(_reply);
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await HttpContext.Response.Body.WriteAsync(_bytes);
                await HttpContext.Response.CompleteAsync();
            }
        }


        [Route("api/authentication/logout")]
        [HttpGet]
        public async Task Logout()
        {
            var req = HttpContext.Request;
            string body = null;
            using (StreamReader reader
                  = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }
            dynamic temp = JsonConvert.DeserializeObject<dynamic>(body);
            var authToken = HttpContext.Request.Headers["Authorization"].ToString();
            string user = temp.SelectToken("username");
            _dbService.RemoveToken(user);
        }
    }
}

