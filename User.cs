using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    //легендарный класс юзер аж 52 ссылки жесть
    public class User
    {
        public User(int id, string login, string password, Position position)
        {
            ID = id;
            Login = login;
            Password = password;
            Position = position;
        }

        public int ID { get; }
        public string Login { get; }
        public string Password { get; }
        public Position Position { get; set; }


    }
}
