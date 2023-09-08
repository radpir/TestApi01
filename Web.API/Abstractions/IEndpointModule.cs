namespace Web.API.Abstractions;

public interface IEndpointDefinition
{
    void RegisterEndpoints(WebApplication app);
}
