using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cmta.Clients.Spa.Security
{
    public class PermissionAuthorizeView : AuthorizeView
    {
        [Parameter]
        public string[] Permissions
        {
            //set => Policy = $"{PermissionAuthorizeAttribute.PolicyPrefix}{string.Join(",", value)}";
            set => Policy = "";
        }        
        
        [Parameter]
        public string Permission
        {
            set => Permissions = new string[] {value};
        }

        public PermissionAuthorizeView() : base()
        {
        }
    }
}