using DAL;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IContactRequestService
    {
        Task<bool> AddRequestService(ContactRequestDTO crdto);
        Task<List<ContactRequest>> GetAllRequestsService();
        Task<ContactRequest> GetRequestByIdService(int id);
        Task<bool> DeleteRequestService(int id);
        Task<bool> EditRequestService(ContactRequestDTOSecondary request);
    }
    public class ContactRequestService : IContactRequestService
    {
        private readonly IContactRequestRepository repository;
        public ContactRequestService(IContactRequestRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> AddRequestService(ContactRequestDTO crdto)
        {
            return await repository.AddRequestRepo(crdto);
        }

        public async Task<List<ContactRequest>> GetAllRequestsService()
        {
            return await repository.GetAllRequestsRepo();
        }

        public async Task<ContactRequest> GetRequestByIdService(int id)
        {
            return await repository.GetRequestByIdRepo(id);
        }

        public async Task<bool> DeleteRequestService(int id)
        {
            return await repository.DeleteRequestRepo(id);
        }

        public async Task<bool> EditRequestService(ContactRequestDTOSecondary request)
        {
            return await repository.EditRequestRepo(request);
        }
    }
}

