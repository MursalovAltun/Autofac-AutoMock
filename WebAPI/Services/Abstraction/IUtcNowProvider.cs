using System;

namespace WebAPI.Services.Abstraction
{
    public interface IUtcNowProvider
    {
        DateTime UtcNow();
    }
}