﻿namespace BlogFlow.Core.Application.Interface.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body);
    }
}
