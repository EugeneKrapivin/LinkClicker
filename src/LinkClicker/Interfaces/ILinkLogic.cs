using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkClicker.Models;

namespace LinkClicker.Interfaces
{
    public interface ILinkLogic
    {
        Task<TinyLink> GetLinkAsync(int id);
        Task<IEnumerable<TinyLink>> GetAllLinksAsync(int pageSize, int page);
        Task<TinyLink> CreateLinkAsync(TinyLink tinyLink);
        Task<bool> DeleteLinkAsync(int id);
        Task<bool> ExpireLinkAsync(int id);
        Task<bool> ExtendLeaseAsync(int id, DateTime newExpirationDate);
    }
}
