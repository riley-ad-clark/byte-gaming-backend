using Entities.Context;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IContactRequestRepository
    {
        Task<List<ContactRequest>> GetAllRequestsRepo();
        Task<bool> AddRequestRepo(ContactRequestDTO crdto);
        Task<ContactRequest> GetRequestByIdRepo(int id);
        Task<bool> DeleteRequestRepo(int id);
        Task<bool> EditRequestRepo(ContactRequestDTOSecondary contact);
    }

    public class ContactRequestRepository : IContactRequestRepository
    {
        ByteGamingContext context = new ByteGamingContext();

        public async Task<List<ContactRequest>> GetAllRequestsRepo()
        {
            return await context.ContactRequests.ToListAsync();
        }

        public async Task<bool> AddRequestRepo(ContactRequestDTO crdto)
        {
            var currentTime = DateTime.Now;

            if (crdto == null)
            {
                return false;
            }
            else if (crdto.ContactName == null)
            {
                return false;
            }
            else if (crdto.ContactEmail == null)
            {
                return false;
            }
            else if (crdto.ContactMessage == null)
            {
                return false;
            }
            else
            {
                ContactRequest cr = new ContactRequest
                {
                    ContactName = crdto.ContactName,
                    ContactEmail = crdto.ContactEmail,
                    ContactMessage = crdto.ContactMessage,
                    DateOfSubmission = currentTime
                };

                await context.ContactRequests.AddAsync(cr);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<ContactRequest> GetRequestByIdRepo(int id)
        {
            var request = await context.ContactRequests.Where(x => x.ContactId == id).FirstOrDefaultAsync();

            if (request != null)
            {
                return request;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteRequestRepo(int id)
        {
            var request = await context.ContactRequests.Where(x => x.ContactId == id).FirstOrDefaultAsync();

            if (request != null)
            {
                context.ContactRequests.Remove(request);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EditRequestRepo(ContactRequestDTOSecondary cr)
        {
            var foundRequest = await context.ContactRequests.Where(x => x.ContactId == cr.ContactId).FirstOrDefaultAsync();

            if (foundRequest != null)
            {
                foundRequest.ContactName = cr.ContactName;
                foundRequest.ContactEmail = cr.ContactEmail;
                foundRequest.ContactMessage = cr.ContactMessage;

                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
