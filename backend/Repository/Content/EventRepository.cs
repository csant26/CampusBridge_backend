using backend.Data;
using backend.Models.Domain.Content.Events;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Content
{
    public class EventRepository : IEventRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public EventRepository(CampusBridgeDbContext campusBridgeDbContext, 
            UserManager<IdentityUser> userManager)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.userManager = userManager;
        }
        public async Task<Event> CreateEvent(Event eventToAdd)
        {
            var user = await userManager.FindByEmailAsync(eventToAdd.CreatorId);
            var roles = await userManager.GetRolesAsync(user);
            if (roles.Contains("ClubHead")) { eventToAdd.Creator = "ClubHead"; }
            else if (roles.Contains("College")) { eventToAdd.Creator = "College"; }
            else if (roles.Contains("University")) { eventToAdd.Creator = "University"; }
            else { return null; }
            eventToAdd.DateUpdated = eventToAdd.DatePosted;
            await campusBridgeDbContext.Events.AddAsync(eventToAdd);
            await campusBridgeDbContext.SaveChangesAsync();
            return eventToAdd;
        }

        public async Task<List<Event>> GetEvent()
        {
            var events = await campusBridgeDbContext.Events.ToListAsync();
            if (events == null) { return null; }
            return events;
        }
        public async Task<Event> GetEventById(string EventId)
        {
            var existingEvent = await campusBridgeDbContext.Events
                .FirstOrDefaultAsync(x=>x.EventId==EventId);
            if (existingEvent == null) { return null; }
            return existingEvent;
        }

        public async  Task<List<Event>> GetEventByAudience(string Audience)
        {
            var existingEvent = await campusBridgeDbContext.Events
                .Where(x => x.DirectedTo.Contains(Audience))
                .ToListAsync();
            if (existingEvent == null) { return null; }
            return existingEvent;
        }

        public async Task<List<Event>> GetEventByCreator(string Creator)
        {
            var existingEvent = await campusBridgeDbContext.Events
                .Where(x => x.Creator==Creator)
                .ToListAsync();
            if (existingEvent == null) { return null; }
            return existingEvent;
        }

        public async Task<Event> UpdateEvent(string EventId, Event eventToUpdate)
        {
            var existingEvent = await GetEventById(EventId);
            if (existingEvent == null) { return null; }
            if (existingEvent.CreatorId != eventToUpdate.CreatorId) { return null; }

            existingEvent.Title = eventToUpdate.Title;
            existingEvent.Description = eventToUpdate.Description;
            existingEvent.EventDate = eventToUpdate.EventDate;
            existingEvent.DateUpdated = eventToUpdate.DateUpdated;
            existingEvent.DirectedTo = eventToUpdate.DirectedTo;

            await campusBridgeDbContext.SaveChangesAsync();

            return existingEvent;

        }
        public async Task<Event> DeleteEvent(string EventId, string CreatorId)
        {
            var existingEvent = await GetEventById(EventId);
            if (existingEvent == null) { return null; }
            if (existingEvent.CreatorId != CreatorId) { return null; }

            campusBridgeDbContext.Events.Remove(existingEvent);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingEvent;
        }
    }
}
