namespace SecurityAppBusiness.Abstract
{
    public abstract class AbstractBusinessMehods<TBusinnes,TDataAccess> 
    {
        protected abstract TBusinnes ConvertBusinessObjetToDataAccessObject(TDataAccess dataAccessObject);
        protected abstract TDataAccess ConvertDataAccessObjectToBusinessObjet();
    }
}