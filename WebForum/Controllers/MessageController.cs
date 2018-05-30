using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebForum.Repositories;

namespace WebForum.Controllers
{
    public class MessageController : ApiController
    {
        public IEnumerable<Message> GetMessagesFromGroup(int Id)
        {
            using (var context = new Context())
            {
                var group = context.Groups.FirstOrDefault(g => g.Id == Id);
                List<Message> messages = new List<Message>();
                foreach (var messageDto in group.Messages)
                {
                    var message = MapToMessage(messageDto);
                    messages.Add(message);
                }
                
                return messages;
            }
        }

        private Message MapToMessage(MessageDto dto)
        {
            return new Message { Text = dto.Text, Id = dto.Id };
        }

        private MessageDto MapToMessageDto(Message message)
        {
            return new MessageDto { Text = message.Text, Id = message.Id };
        }

        public async Task<Message> PostMessage(int id, Message message)
        {
            using (var context = new Context())
            {
                var group = context.Groups.FirstOrDefault(g => g.Id == id);
                group.Messages.Add(MapToMessageDto(message));

                await context.SaveChangesAsync();
            }

            return message;
        }

        [Route("api/message/{groupid}/{messageid}")]
        public async Task DeleteMessage(int groupId, int messageId)
        {
            using (var context = new Context())
            {
                var group = context.Groups.FirstOrDefault(u => u.Id == groupId);
                var message = group.Messages.FirstOrDefault(m => m.Id == messageId);

                group.Messages.Remove(message);
                context.Messages.Remove(message);
               
                await context.SaveChangesAsync();
            }
        }

       
    }
}