﻿using BlogFlow.Auth.Transversal.Common;
using BlogFlow.Core.Application.DTO;

namespace BlogFlow.Common.Application.Interface.UserCases
{
    public interface IBlogsApplication
    {
        Task<Response<bool>> InsertAsync(BlogDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> UpdateAsync(string id, BlogDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<BlogDTO>> GetAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<IEnumerable<BlogDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Response<int>> CountAsync(CancellationToken cancellationToken = default);
    }
}
