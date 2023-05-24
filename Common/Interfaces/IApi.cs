using System.Collections;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;

namespace Common.Interfaces;

public interface IApi
{
    public void Register(WebApplication app, ApiVersionSet apiSet);
}