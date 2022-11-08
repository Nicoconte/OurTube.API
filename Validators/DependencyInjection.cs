using OurTube.API.Validators.InputValidators;

namespace OurTube.API.Validators
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGraphQLCustomValidators(this IServiceCollection service)
        {
            service.AddTransient<MutationValidators>();
            service.AddTransient<QueryValidators>();

            return service;
        }
    }
}
