using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swas.Client.HelperClasses
{
    public class OnlyBreadCrumbMvcSiteMapNodeAttribute : MvcSiteMapNodeAttribute
    {
        public OnlyBreadCrumbMvcSiteMapNodeAttribute(string title, string parentKey)
        {
            Title = title;
            ParentKey = parentKey;
            VisibilityProvider = typeof(BreadCrumbOnlyVisibilityProvider).AssemblyQualifiedName;
        }
        public OnlyBreadCrumbMvcSiteMapNodeAttribute(string title, string parentKey, string key)
        {
            Title = title;
            Key = key;
            ParentKey = parentKey;
            VisibilityProvider = typeof(BreadCrumbOnlyVisibilityProvider).AssemblyQualifiedName;
        }
    }

    public class BreadCrumbOnlyVisibilityProvider : ISiteMapNodeVisibilityProvider
    {
        public bool AppliesTo(string providerName)
        {
            //throw new NotImplementedException();
            return true;
        }

        public bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            //throw new NotImplementedException();
            return true;
        }

        public bool IsVisible(SiteMapNode node, System.Web.HttpContext context, IDictionary<string, object> sourceMetadata)
        {
            if (sourceMetadata["HtmlHelper"] == null || (string)sourceMetadata["HtmlHelper"] == "MvcSiteMapProvider.Web.Html.SiteMapPathHelper")
            {
                return true;
            }
            return false;
        }
    }


}
