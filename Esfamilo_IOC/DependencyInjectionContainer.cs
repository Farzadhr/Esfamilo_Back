using Esfamilo_Core.Interfaces;
using Esfamilo_Core.Services;
using Esfamilo_Data.Repositories;
using Esfamilo_Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_IOC
{
    public static class DependencyInjectionContainer
    {
        public static void  AddDependencyInjection(this IServiceCollection service) {
            // add core layer services
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IFriendService, FriendService>();
            service.AddScoped<ILobbyService, LobbyService>();
            service.AddScoped<IUserInLobbyService, UserInLobbyService>();
            service.AddScoped<IWordForCategoryService, WordForCategoryService>();
            service.AddScoped<ICategoryInLobbyService, CategoryInLobbyService>();

            //add Domain and Data layer repository
            service.AddScoped<ICategroyRepository,CategroyRepository>();
            service.AddScoped<IFriendRepository,FriendRepository>();
            service.AddScoped<ILobbyRepository,LobbyRepository>();
            service.AddScoped<IUserInLobbyRepository, UserInLobbyRepository>();
            service.AddScoped<IWordForCategoryRepository, WordForCategoryRepository>();
            service.AddScoped<ICategoryInLobbyRepository, CategoryInLobbyRepository>();
        }
    }
}
