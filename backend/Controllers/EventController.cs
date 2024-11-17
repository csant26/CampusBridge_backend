using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Content.Events;
using backend.Models.DTO.Content.Events;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly EventRepository eventRepository;

        public EventController(IMapper mapper, EventRepository eventRepository)
        {
            this.mapper = mapper;
            this.eventRepository = eventRepository;
        }
        [HttpPost("CreateEvent")]
        [ValidateModel]
        public async Task<IActionResult> CreateEvent([FromBody]AddEventDTO addEventDTO)
        {
            var addedEvent = await eventRepository.CreateEvent(mapper.Map<Event>(addEventDTO));
            if (addedEvent == null) { return BadRequest("Event couldn't be added."); }
            return Ok(mapper.Map<EventDTO>(addedEvent));
        }
        [HttpGet("GetEvent")]
        [ValidateModel]
        public async Task<IActionResult> GetEvent()
        {
            var existingEvents = await eventRepository.GetEvent();
            if (existingEvents == null) { return BadRequest("No events found."); }
            return Ok(mapper.Map<List<EventDTO>>(existingEvents));
        }
        [HttpGet("GetEventById/{EventId}")]
        [ValidateModel]
        public async Task<IActionResult> GetEventById([FromRoute]string EventId)
        {
            var existingEvent = await eventRepository.GetEventById(EventId);
            if (existingEvent == null) { return BadRequest("No events found."); }
            return Ok(mapper.Map<EventDTO>(existingEvent));
        }
        [HttpGet("GetEventByCreator/{Creator}")]
        [ValidateModel]
        public async Task<IActionResult> GetEventByCreator(string Creator)
        {
            var existingEvents = await eventRepository.GetEventByCreator(Creator);
            if (existingEvents == null) { return BadRequest("No events found."); }
            return Ok(mapper.Map<List<EventDTO>>(existingEvents));
        }
        [HttpGet("GetEventByAudience/{Audience}")]
        [ValidateModel]
        public async Task<IActionResult> GetEventByAudience(string Audience)
        {
            var existingEvents = await eventRepository.GetEventByCreator(Audience);
            if (existingEvents == null) { return BadRequest("No events found."); }
            return Ok(mapper.Map<List<EventDTO>>(existingEvents));
        }
        [HttpPut("UpdateEvent/{EventId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateEvent([FromRoute] string EventId,
            [FromBody] UpdateEventDTO updateEventDTO)
        {
            var updatedEvent = await eventRepository.UpdateEvent(EventId, mapper.Map<Event>(updateEventDTO));
            if (updatedEvent == null) { return BadRequest("Event couldn't be updated."); }
            return Ok(mapper.Map<EventDTO>(updatedEvent));
        }
        [HttpDelete("DeleteEvent/{EventId}/{CreatorId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteEvent([FromRoute] string EventId,
            [FromRoute]string CreatorId)
        {
            var deletedEvent = await eventRepository.DeleteEvent(EventId,CreatorId);
            if (deletedEvent == null) { return BadRequest("Event couldn't be deleted."); }
            return Ok(mapper.Map<EventDTO>(deletedEvent));
        }
    }
}
