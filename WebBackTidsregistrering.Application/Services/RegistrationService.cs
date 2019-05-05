using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackTidsregistrering.Application.Interfaces;
using WebBackTidsregistrering.Domain.Entities;

namespace WebBackTidsregistrering.Application.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<Registration> _registrationRepository;

        public RegistrationService(IRepository<Registration> registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public IEnumerable<Registration> GetAllByIdAsync(string userId) => _registrationRepository.GetAll().Where(x => x.UserId == userId && x.Date > new DateTime(2019));

        public async Task<Registration> GetByIdAsync(int id) => await _registrationRepository.GetByIdAsync(id);

        public async Task CreateAsync(Registration entity)
        {
            await _registrationRepository.CreateAsync(entity);
        }


        //public async Task<IList<Registration>> GetAllByIdAsync(string userId)
        //{
        //    return await RegistrationRepository.GetAllAsync(). .Where(x => x.UserId == userId);
        //}

        //public async Task<Registration> GetByIdAsync(string userId, int id)
        //{
        //    //       return await Context.Registrations.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);
        //}
    }
}