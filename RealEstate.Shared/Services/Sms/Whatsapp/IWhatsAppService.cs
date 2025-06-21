// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Sms.Whatsapp;

public interface IWhatsAppService
{
    Task<WhatsAppApiResponse> SendWhatsAppMessage(string to, string message);

}
