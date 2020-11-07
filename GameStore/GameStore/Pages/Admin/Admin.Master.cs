using System;
using System.Web.Routing;

namespace GameStore.Pages.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string OrdersUrl
        {
            get
            {
                return this.generateURL("admin_orders");
            }
        }

        public string GamesUrl
        {
            get
            {
                return this.generateURL("admin_games");
            }
        }

        private string generateURL(string routeName)
        {
            return RouteTable.Routes.GetVirtualPath(null, routeName, null).VirtualPath;
        }
    }
}