namespace SecurityAppDataAccess.Interface
{
    public interface ICustomer : IDataAccessMethods<ICustomer>
    {
         int IdCompany { get; set; }
         string Name { get; set; }
         string Address { get; set; }
         string Email { get; set; }
         string Phone { get; set; }
         string Status { get; set; }
    }
}