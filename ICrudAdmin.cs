using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public interface ICrudAdmin
    {
        void Create(User user);
        User Read(int idPos);
        void Update(int userPosition, int id, string login, string password, Position position);
        void Delete(User user);

        List<User> ReadAllRecords();
        List<User> ReadById(int id);
        List<User> ReadByLogin(string login);
        List<User> ReadByPassword(string password);
        List<User> ReadByPosition(Position position);

    }
}
