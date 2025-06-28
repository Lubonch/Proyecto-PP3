using Backend.Repository.Contracts;
using Backend.Repository;
using Backend.Service.Contracts;
using Backend.Service;
using AutoMapper;
using Backend.Domain;
using Backend.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Configs
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Model to Domain
                cfg.CreateMap<UserModel, User>();

                //Configuring Domain to Model
                cfg.CreateMap<User, UserModel>();
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}