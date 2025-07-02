using AutoMapper;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Transversal.Common.Contracts;
using MassTransit;
using Serilog;

namespace BlogFlow.Core.Application.UseCases.Users
{
    public class RegisterUserHandler : IRegisterUserHandler
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Transversal.Common.Response<bool>> HandleAsync(UserDTO userDto, CancellationToken cancellationToken = default)
        {
            var response = new Transversal.Common.Response<bool>();

            try
            {
                var user = _mapper.Map<User>(userDto);

                if (await _unitOfWork.Users.InsertAsync(user))
                {
                    response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                    if (response.Data)
                    {
                        var messageId = Guid.NewGuid().ToString();

                        await _publishEndpoint.Publish(new UserRegistered(Guid.NewGuid().ToString(), 
                                                                          user.Email, 
                                                                          user.UserName),
                                                       cancellationToken);

                        Log.Logger.Information($"Published message {messageId}");

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
    }
}
