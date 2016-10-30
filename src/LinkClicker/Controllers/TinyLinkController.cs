using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkClicker.Interfaces;
using LinkClicker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LinkClicker.Controllers
{
    [Route("api/[controller]")]
    public class TinyLinkController : Controller
    {
        private readonly ILinkLogic _linkLogic;
        private readonly ILogger<TinyLinkController> _logger;

        public TinyLinkController(ILinkLogic linkLogic, ILogger<TinyLinkController> logger = null)
        {
            if (linkLogic == null) throw new ArgumentNullException(nameof(linkLogic));
            _linkLogic = linkLogic;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<TinyLink>> GetAsync(int pageSize = 10, int page = 0)
        {
            return await _linkLogic.GetAllLinksAsync(pageSize, page);
        }

        [HttpGet("{id}")]
        public async Task<TinyLink> GetTinyLinkAsync(int id)
        {
            return await _linkLogic.GetLinkAsync(id);
        }

        [HttpPost]
        public async Task<TinyLink> CreateTinyLinkAsync([FromBody] TinyLink link)
        {
            return await _linkLogic.CreateLinkAsync(link);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _linkLogic.ExpireLinkAsync(id);
        }
    }
}
