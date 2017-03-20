using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SecurityAppBusiness.Abstract;
using SecurityAppBusiness.Interface;
using SecurityAppDataAccess.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.BusinessObject
{
    public class UserBusiness : IUserBusiness
    {
        private static IUserBusiness _userInstance;
        private UserBusiness()
        {
          
        }

        private static void MapperConfiguration()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, IUserEntity>();
                cfg.CreateMap<IUserEntity, User>();
            });
        }

        public static IUserBusiness GetNewUser()
        {
            MapperConfiguration();
            return _userInstance ?? (_userInstance = new UserBusiness());
        }
        public IQueryable<IUserEntity> GetAll()
        {
            var user = User.GetNewUser();
            var dataUsers = user.GetAll();

            if (dataUsers.Any())
                return Mapper.Map<IEnumerable<IUser>, IEnumerable<IUserEntity>>(dataUsers).AsQueryable();

            return null;
        }
        public IUserEntity GetById(int id)
        {
            var user = User.GetNewUser();
            var findedUser = user.GetById(id);
            if (findedUser.Name.Equals(null))
                return null;

            return Mapper.Map<IUser, IUserEntity>(findedUser);

        }
        public void FieldsValidation()
        {
            throw new NotImplementedException();
        }
        public void Save(IUserEntity userEntity)
        {
            var dataUser = Mapper.Map<IUserEntity, User>(userEntity);
            dataUser.Save();
        }
        public void Update(IUserEntity userEntity)
        {
            var dataUser = Mapper.Map<IUserEntity, User>(userEntity);
            dataUser.Update();
        }
        public void Delete(IUserEntity userEntity)
        {
            var dataUser = Mapper.Map<IUserEntity, User>(userEntity);
            dataUser.Delete();
        }
        public int Authenticate(string userName, string password)
        {
            var user = User.GetNewUser();
            return user.Authenticate(userName, password);
        }
    }
}