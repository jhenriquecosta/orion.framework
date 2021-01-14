using Gestor.Settings.Domain.Dtos;
using Gestor.Settings.Domain.Entities;
using Orion.Framework.App.Services.Domain.Dtos;
using Orion.Framework.App.Services.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zeus.Domains.Services;
using Zeus.Maps;
using Zeus.Utility;
using Zeus.Webs.Api;

namespace Orion.Framework.App.Services.Services
{
    public interface IMessageService
    {
        Task<ApiResponse> Create(MessageDto messageDto);
        List<MessageDto> GetList();
        Task<ApiResponse> Delete(int id);
    }
    public class MessageService : IMessageService
    {


        private readonly DbContextGenericDao<Message> _dao = new DbContextGenericDao<Message>();
        public async Task<ApiResponse> Create(MessageDto messageDto)
        {
            Message message = messageDto.MapTo<Message>();
            await _dao.SaveOrUpdateAsync(message);
            await _dao.FlushChangesAsync();
            return new ApiResponse(200, "Created Message", message);
        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _dao.DeleteAsync(id);
            await _dao.FlushChangesAsync();
            return new ApiResponse(200, "Deleted Message", id);
        }

        public List<MessageDto> GetList()
        {
            var _all = AsyncUtil.RunSync(()=> _dao.FindAllAsync());
            var _ordered = _all.ToList().OrderBy(o => o.DataLast).Take(10).ToList().MapToList<MessageDto>();
            return _ordered;
        }
    }
}
