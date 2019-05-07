using System.Collections.Generic;
using System.Threading.Tasks;
using WebBackTidsregistrering.Domain.Entities;

namespace WebBackTidsregistrering.Application.Interfaces
{
    public interface IRegistrationService
    {
        IEnumerable<Registration> GetAllByIdAsync(string userId);
        Task<Registration> GetByIdAsync(int id);

        Task CreateAsync(Registration entity);
        Task UpdateAsync(Registration entity);
        Task DeleteAsync(int id);
    }
}