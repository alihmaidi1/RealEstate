// Licensed to the .NET Foundation under one or more agreements.

using Hangfire;

namespace RealEstate.Infrastructure.BackgroundServer;
    
public static class Queues
{

    public static string Critical = "critical";
    
    public static string Normal=  "normal";

    public static string Low=  "low";

    public static string Slow=  "slow";
    
}
