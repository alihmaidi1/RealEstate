// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Sms.Whatsapp;

public class WhatsAppApiResponse
{
    
    public string messaging_product { get; set; }
    
    public List<Contact> contacts { get; set; }
    
    public List<Message> messages { get; set; }
    
}

public class Contact
{
    
    public string input { get; set; }

    public string wa_id { get; set; }

    
}

public class Message
{
    public string id { get; set; }
}
