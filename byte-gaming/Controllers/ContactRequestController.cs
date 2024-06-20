using AutoMapper;
using BLL;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace byte_gaming.Controllers
{
    [Route("api/contact-request")]
    [ApiController]
    public class ContactRequestController : ControllerBase
    {

        private readonly IContactRequestService _crService;
        private readonly IMapper _mapper;

        public ContactRequestController(IContactRequestService crService, IMapper mapper)
        {
            _crService = crService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/contact-requests")]
        public async Task<IActionResult> GetAllRequest()
        {
            List<ContactRequest> crList = await _crService.GetAllRequestsService();

            if (crList.Count == 0)
            {
                return NotFound("No contact requests found, try submitting some!");
            } else
            {
                return Ok(crList);
            }
        }

        [HttpGet]
        [Route("/contact-request/{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var request = await _crService.GetRequestByIdService(id);

            if (request == null)
            {
                return NotFound($"No contact requests with the id of {id} were found");
            }
            else
            {
                return Ok(request);
            }
        }

        [HttpPost]
        [Route("/contact-request/add")]
        public async Task<IActionResult> AddRequest(ContactRequestDTO crdto)
        {
            if (crdto.ContactName == null)
            {
                return NotFound("Please enter a valid contact request name");
            } else if (crdto.ContactEmail == null)
            {
                return NotFound("Please enter a valid contact request email");
            } else if (crdto.ContactMessage == null)
            {
                return NotFound("Please enter a valid contact request message");
            }
            else
            {
                var response = await _crService.AddRequestService(crdto);
                if (response != false)
                {
                    return Ok("Contact request successfully added!");
                }
                else
                {
                    return NotFound("Something went wrong internally, please try again!");
                }
            }
        }

        [HttpDelete]
        [Route("/contact-request/remove/{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var response = await _crService.DeleteRequestService(id);

            if (response != false)
            {
                return Ok($"Contact request with the id of {id} was successfully deleted!");
            }
            else
            {
                return NotFound($"No contact request with the id of {id} was found");
            }
        }

        [HttpPut]
        [Route("/contact-request/edit")]
        public async Task<IActionResult> EditRequest(ContactRequestDTOSecondary cr)
        {
            var response = await _crService.EditRequestService(cr);

            if (response != false)
            {
                return Ok($"Product with the id of {cr.ContactId} was successfully updated to have the" +
                          $" following variables: " +
                          $"{cr.ContactName}," +
                          $" {cr.ContactEmail}," +
                          $" and {cr.ContactMessage},");
            }
            else
            {
                return NotFound($"No product with the id of {cr.ContactId} was found");
            }
        }
    }
}