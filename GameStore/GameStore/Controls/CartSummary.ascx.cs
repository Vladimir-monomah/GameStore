﻿using GameStore.Models;
using GameStore.Pages.Helpers;
using System;
using System.Linq;
using System.Web.Routing;

namespace GameStore.Controls
{
    public partial class CartSummary : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cart myCart = SessionHelper.GetCart(this.Session);
            this.csQuantity.InnerText = myCart.Lines.Sum(x => x.Quantity).ToString();
            this.csTotal.InnerText = myCart.ComputeTotalValue().ToString("c");
            this.csLink.HRef = RouteTable.Routes.GetVirtualPath(null, "cart",
                null).VirtualPath;
        }
    }
}