using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualia.Umb.GReCaptcha
{
    public class GReCaptchaFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly GReCatchaService gReCatchaService;
        private readonly ILogger<GReCaptchaFilter> logger;

        public GReCaptchaFilter(GReCatchaService gReCatchaService, ILogger<GReCaptchaFilter> logger)
        {
            this.gReCatchaService = gReCatchaService;
            this.logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var gResponse =
                    Get_gResponse_fromHeaders(context)
                    ?? Get_gResponse_fromQuery(context)
                    ;

                if (gResponse is null || !(await gReCatchaService.Verify(gResponse)))
                {
                    context.Result = new BadRequestResult();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception in recaptcha validation.");
                context.Result = new BadRequestResult();
            }
        }

        private string? Get_gResponse_fromHeaders(AuthorizationFilterContext context)
            => context.HttpContext.Request.Headers["_recaptcha"].ToString().NullIfEmpty();

        private string? Get_gResponse_fromQuery(AuthorizationFilterContext context)
            => context.HttpContext.Request.Query["_recaptcha"].ToString().NullIfEmpty();
    }

}
