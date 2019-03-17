using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuminiHire.Models;

namespace LuminiHire.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserContext _Context;

        public UserService( UserContext context )
        {
            this._Context = context;
        }

        public User Authenticate( string username, string password )
        {
            var user = this._Context.User.SingleOrDefault( o => o.UserName.Equals( username ) && o.Password == password );

            // return null if user not found
            if( user == null )
                return null;

            // authentication successful so return user details without password
            user.Password = null;
            return user;
        }

        public UserContext GetUserContext()
        {
            return this._Context;
        }
    }
}
