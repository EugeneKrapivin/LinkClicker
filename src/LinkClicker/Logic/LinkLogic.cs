using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LinkClicker.Interfaces;
using LinkClicker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Microsoft.Extensions.Logging;

namespace LinkClicker.Logic
{
    public class LinkLogic : ILinkLogic
    {
        private readonly LinkClickerContext _ctx;
        private readonly ILogger<LinkLogic> _logger;

        public LinkLogic(LinkClickerContext ctx, ILogger<LinkLogic> logger = null)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<TinyLink> GetLinkAsync(int id)
        {
            return await _ctx.TinyLinks.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TinyLink>> GetAllLinksAsync(int pageSize, int page)
        {
            return await _ctx.TinyLinks
                .Skip(pageSize * page)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TinyLink> CreateLinkAsync(TinyLink tinyLink)
        {
            tinyLink.Active = true;
            tinyLink.CreatedAt = DateTime.UtcNow;
            tinyLink.ExpiresAt = tinyLink.ExpiresAt ?? DateTime.UtcNow.AddDays(7);
            tinyLink.Public = tinyLink.Public ?? true; 
            
            var r = await _ctx.TinyLinks.AddAsync(tinyLink);
            await _ctx.SaveChangesAsync();

            return r.Entity;
        }

        public async Task<bool> DeleteLinkAsync(int id)
        {
            var r = _ctx.TinyLinks.Remove(new TinyLink {Id = id}).State == EntityState.Deleted;
            await _ctx.SaveChangesAsync();

            return r;
        }

        public async Task<bool> ExpireLinkAsync(int id)
        {
            var r = await _ctx.TinyLinks.SingleOrDefaultAsync(x => x.Id == id && (x.Active ?? false));
            if (r != null)
            {
                r.Active = false;
                await _ctx.SaveChangesAsync();
            }

            return r != null;
        }

        public async Task<bool> ExtendLeaseAsync(int id, DateTime newExpirationDate)
        {
            var r = await _ctx.TinyLinks.SingleOrDefaultAsync(x => x.Id == id && (x.Active ?? false));

            if (r.ExpiresAt >= newExpirationDate)
            {
                return false;
            }

            r.ExpiresAt = newExpirationDate;
            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}
