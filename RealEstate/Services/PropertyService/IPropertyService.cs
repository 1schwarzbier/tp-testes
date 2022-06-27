using RealEstate.Models;
using RealEstate.Services.PropertyService.Models;

namespace RealEstate.Services.PropertyService;

public interface IPropertyService
{
    public Task<ServiceResponse<Property>> Get(Guid id);
    public Task<ServiceResponse<PaginatedList<Property>>> GetAll(PaginatedRequest request);
    public Task<ServiceResponse<object>> Insert (InsertPropertyRequest request);
    public Task<ServiceResponse<object>> Update(UpdatePropertyRequest request);
    public Task<ServiceResponse<object>> Delete (Guid id);
}