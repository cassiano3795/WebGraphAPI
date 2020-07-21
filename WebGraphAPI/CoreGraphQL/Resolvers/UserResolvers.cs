using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD.Models;
using Core.Services;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace CoreGraphQL.Resolvers
{
    public class UserResolvers : BaseResolver
    {
        private readonly IUserService _userService;

        public UserResolvers(IUserService userService, ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor) : base(tokenService, httpContextAccessor)
        {
            _userService = userService;
        }

        public Usuarios ResolveUser(ResolveFieldContext context, int id)
        {
            return _userService.FindById(id);
        }

        public List<Usuarios> ResolveUsers(ResolveFieldContext context, int? id, int size, int offSet, out int count)
        {
            var users = _userService.GetUsersByClient(IdClient, id);
            count = users.Count();
            return users.Skip(offSet).Take(size).ToList();
        }

        public async Task<Usuarios> CreateUser(ResolveFieldContext context)
        {
            var userInput = context.GetArgument<Usuarios>("user");
            await _userService.CreateAsync(userInput);
            await _userService.SaveAsync();
            return userInput;
        }

        public Usuarios UpdateUser(ResolveFieldContext context)
        {
            var userInput = context.GetArgument<Usuarios>("user");
            _userService.Update(userInput);
            _userService.Save();
            return userInput;
        }

        public bool DeleteUser(ResolveFieldContext context)
        {
            try
            {
                var id = context.GetArgument<int>("id");
                var user = _userService.FindById(id);
                _userService.Delete(user);
                _userService.Save();
                return true;
            }
            catch (Exception e)
            {
                //TODO: CREATE A METHOD CATCHEXCPETION AND SAVE "e"
                return false;
            }
        }
    }
}
