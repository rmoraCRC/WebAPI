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
    public class GroupBusiness : IGroupBusiness
    {
        private static IGroupBusiness _groupInstance;

        private GroupBusiness()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Group, IGroupEntity>();
                cfg.CreateMap<IGroupEntity, Group>();
            });
        }
        public static IGroupBusiness GetNewGroup()
        {
            return _groupInstance ?? (_groupInstance = new GroupBusiness());
        }
        public void Save(IGroupEntity groupEntity)
        {
            var dataGroup = Mapper.Map<IGroupEntity, IGroup>(groupEntity);
            dataGroup.Save();
        }
        public void Update(IGroupEntity groupEntity)
        {
            var dataGroup = Mapper.Map<IGroupEntity, IGroup>(groupEntity);
            dataGroup.Update();
        }
        public void Delete(IGroupEntity groupEntity)
        {
            var dataGroup = Mapper.Map<IGroupEntity, IGroup>(groupEntity);
            dataGroup.Delete();
        }
        public IQueryable<IGroupEntity> GetAll()
        {
            var group = Group.GetNewGroup();
            var dataGroup = group.GetAll();

            if (dataGroup.Any())
                return Mapper.Map<IEnumerable<IGroup>, IEnumerable<IGroupEntity>>(dataGroup).AsQueryable();

            return null;

        }
        public IGroupEntity GetById(int id)
        {
            var group = Group.GetNewGroup();
            var findedGroup = group.GetById(id);

            return Mapper.Map<IGroup, IGroupEntity>(findedGroup);

        }
        public void FieldsValidation()
        {
            throw new NotImplementedException();
        }


    }
}