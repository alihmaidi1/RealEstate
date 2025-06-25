// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.Extensions.Options;

namespace RealEstate.Shared.Services.Sms.Whatsapp;

public class WhatsAppService: IWhatsAppService
{

    private readonly IWhatsAppCloudApi _iWhatsAppCloudApi;
    private readonly WhatsappMessageSetting _whatsappMessageSetting;
    public WhatsAppService(IWhatsAppCloudApi iWhatsAppCloudApi,IOptions<WhatsappMessageSetting>  whatsappMessageSetting)
    {
        _iWhatsAppCloudApi = iWhatsAppCloudApi;
        _whatsappMessageSetting= whatsappMessageSetting.Value;
        
    }
    public async Task<WhatsAppApiResponse> SendWhatsAppMessage(string to, string message)
    {
        var request = new WhatsAppMessageRequest
        {
            to = to.Replace("+", "").Replace(" ", ""),
            text = new TextContent { body = message }
        };

        return await _iWhatsAppCloudApi.SendMessage(
            _whatsappMessageSetting.PhoneNumberId,
            request,
            $"Bearer {_whatsappMessageSetting.AccessToken}"
        );
    }
}
