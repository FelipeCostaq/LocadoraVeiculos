using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Infrastructure.Identity
{
    public static class HiddenEndpointsIdentityFilter
    {
        public static IServiceCollection AddHideEndpointsIdentityFilter(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    var hideRoutes = new[]
                    {
                    "auth/register",
                    "auth/refresh",
                    "auth/confirmEmail",
                    "auth/resendConfirmationEmail",
                    "auth/forgotPassword",
                    "auth/resetPassword",
                    "auth/manage/2fa"
                };

                    foreach (var path in hideRoutes)
                    {
                        document.Paths.Remove(path);
                        document.Paths.Remove("/" + path);
                    }

                    return Task.CompletedTask;
                });
            });

            return services;
        }
    }
}
