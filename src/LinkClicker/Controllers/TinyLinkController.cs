using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkClicker.Interfaces;
using LinkClicker.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LinkClicker.Controllers
{
    [Route("api/[controller]")]
    public class TinyLinkController : Controller
    {
        private readonly ILinkLogic _linkLogic;

        public TinyLinkController(ILinkLogic linkLogic)
        {
            if (linkLogic == null) throw new ArgumentNullException(nameof(linkLogic));
            _linkLogic = linkLogic;
        }
        [HttpGet]
        public async Task<IEnumerable<TinyLink>> GetAsync(int pageSize = 10, int page = 0)
        {
            return await _linkLogic.GetAllLinksAsync(10, 0);
        }

        [HttpGet("{id}")]
        public async Task<TinyLink> GetTinyLinkAsync(int id)
        {
            return null;
        }

        [HttpPost]
        public async Task<TinyLink> PostAsync([FromBody]TinyLink value)
        {
            return null;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TinyLink value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
