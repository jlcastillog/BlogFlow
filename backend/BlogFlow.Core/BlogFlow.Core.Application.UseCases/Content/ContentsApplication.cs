using AutoMapper;
using BlogFlow.Auth.Transversal.Common;
using BlogFlow.Common.Application.Interface.Persistence;
using BlogFlow.Common.Application.Interface.UseCases;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Core.Application.UseCases.Content
{
    public class ContentsApplication : IContentsApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContentsApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> CountAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<int>();

            try
            {
                var countContents = await _unitOfWork.Contents.CountAsync(cancellationToken);

                response.Data = countContents;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                await _unitOfWork.Contents.DeleteAsync(id);

                response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Delete succeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Content doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Content doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<IEnumerable<ContentDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<IEnumerable<ContentDTO>>();

            try
            {
                var posts = await _unitOfWork.Contents.GetAllAsync(cancellationToken);

                response.Data = _mapper.Map<IEnumerable<ContentDTO>>(posts);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "GetAll succeded!!";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<ContentDTO>> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = new Response<ContentDTO>();

            try
            {
                var content = await _unitOfWork.Contents.GetAsync(id, cancellationToken);

                response.Data = _mapper.Map<ContentDTO>(content);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Get succeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Content doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Content doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> InsertAsync(ContentDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var content = _mapper.Map<Domain.Entities.Content>(entity);

                if (await _unitOfWork.Contents.InsertAsync(content))
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

        public async Task<Response<bool>> UpdateAsync(string id, ContentDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var contentExists = await _unitOfWork.Contents.GetAsync(id, cancellationToken);

                var content = _mapper.Map<Domain.Entities.Content>(entity);

                if (contentExists != null)
                {
                    contentExists.TextContent = content.TextContent;

                    await _unitOfWork.Contents.UpdateAsync(contentExists);

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
                    response.Message = "Content doesn't exist!!";
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
