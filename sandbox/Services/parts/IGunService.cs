namespace Orchard.Sandbox.Services;

public interface IGunService
{
    Task<IEnumerable<Part>> GetAll();
    Task<List<Part>> Search(Part search);
    Task<Part> GetById(int id);
    Task<int> Create(params Part[] model);
    Task Update(int id, Part model);
    Task Delete(int id);
}