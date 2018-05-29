using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebForum.Models;
using WebForum.Repositories;

namespace WebForum.Controllers
{
    public class MessageController : ApiController
    {
        [HttpGet()]
        public IEnumerable<Message> GetAllMessagesFromGroup(int groupId)
        {
            using (var context = new Context())
            {
                var group = context.Groups.FirstOrDefault(g => g.Id == groupId);
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

       
    }
}