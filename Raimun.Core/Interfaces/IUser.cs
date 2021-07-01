using Raimun.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raimun.Core.Interfaces
{
   public interface IUser
   {
      Task<User> InsertUser(User user);
      bool LoginUser(string UserName, string password);
      List<User> GetAllUsers();
     
   }
}
