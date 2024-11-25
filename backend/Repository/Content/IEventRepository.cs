using backend.Models.Domain.Content.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public interface IEventRepository
    {
        Task<Event> CreateEvent(Event eventToAdd);
        Task<List<Event>> GetEvent();
        Task<Event> GetEventById(string EventId);
        Task<List<Event>> GetEventByCreator(string Creator);
        Task<List<Event>> GetEventByAudience(string Audience);
        Task<Event> UpdateEvent(string EventId,  Event eventToUpdate);
        Task<Event> DeleteEvent(string EventId, string CreatorId);
      
    }
}
