using System;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace Qualia.Umb.GReCaptcha
{
    [Weight(55)]
    public class GReCaptchaDashboard : IDashboard
    {
        public string Alias => "google recaptcha";

        public string[] Sections => new[]
        {
            Umbraco.Cms.Core.Constants.Applications.Settings
        };

        public string View => "/App_Plugins/Qualia.Umb.GReCaptcha/views/dashboard.html";

        public IAccessRule[] AccessRules
        {
            get
            {
                var rules = new IAccessRule[]
                {
                    new AccessRule {Type = AccessRuleType.Grant, Value = Umbraco.Cms.Core.Constants.Security.AdminGroupAlias}
                };
                return rules;
            }
        }
    }
}
