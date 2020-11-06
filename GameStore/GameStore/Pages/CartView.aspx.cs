using GameStore.Models;
using GameStore.Models.Repository;
using GameStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace GameStore.Pages
{
    public partial class CartView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                var repository = new Repository();
                int gameId;
                if (int.TryParse(this.Request.Form["remove"], out gameId))
                {
                    Game gameToRemove = repository.Games
                        .Where(g => g.GameId == gameId).FirstOrDefault();
                    if (gameToRemove != null)
                    {
                        SessionHelper.GetCart(this.Session).RemoveLine(gameToRemove);
                    }
                }
            }
        }

        public IEnumerable<CartLine> GetCartLines()
        {
            return SessionHelper.GetCart(this.Session).Lines;
        }

        public decimal CartTotal
        {
            get
            {
                return SessionHelper.GetCart(this.Session).ComputeTotalValue();
            }
        }

        public string ReturnUrl
        {
            get
            {
                return SessionHelper.Get<string>(this.Session, SessionKey.RETURN_URL);
            }
        }

        public string CheckoutUrl
        {
            get
            {
                return RouteTable.Routes.GetVirtualPath(null, "checkout",
                    null).VirtualPath;
            }
        }
    }
}