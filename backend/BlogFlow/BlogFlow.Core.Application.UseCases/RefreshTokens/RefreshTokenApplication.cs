using AutoMapper;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Transversal.Common;

namespace BlogFlow.Core.Application.UseCases.RefreshTokens
{
    public class RefreshTokenApplication : IRefreshTokenApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RefreshTokenApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<bool>> InsertAsync(RefreshTokenDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var refreshToken = _mapper.Map<RefreshToken>(entity);

                if (await _unitOfWork.RefreshTokens.InsertAsync(refreshToken))
                {
                    response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                    if (response.Data)
                    {
                        response.IsSuccess = true;
                        response.Message = "Insert succeded!!";
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> ExistsRefreshAsync(RefreshTokenDTO token, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var storedToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(token.Token, cancellationToken);

                if (storedToken != null || storedToken.IsRevoked || storedToken.ExpiryDate < DateTime.UtcNow)
                {
                    response.Data = true;
                    response.IsSuccess = true;
                    response.Message = "Get succeded!!";
                }
                else
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.Message = "Invalid or expired Refresh Token";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Invalid or expired Refresh Token";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> MarkRefreshTokenAsUsed(RefreshTokenDTO token, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var storedToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(token.Token, cancellationToken);

                if (storedToken != null)
                {
                    storedToken.IsUsed = true;

                    await _unitOfWork.RefreshTokens.UpdateAsync(storedToken);

                    response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                    if (response.Data)
                    {
                        response.IsSuccess = true;
                        response.Message = "Update succeded!!";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Refresh token doesn't exist!!";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
