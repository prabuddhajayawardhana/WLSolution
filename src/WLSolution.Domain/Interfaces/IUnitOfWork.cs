namespace WLSolution.Domain.Interfaces;

public interface IUnitOfWork 
{
    IProductRepository ProductRepository();
    Task<int> SaveChangesAsync();
}
