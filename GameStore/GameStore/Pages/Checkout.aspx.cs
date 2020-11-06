using GameStore.Models;
using GameStore.Models.Repository;
using GameStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Web.ModelBinding;

namespace GameStore.Pages
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.checkoutForm.Visible = true;
            this.checkoutMessage.Visible = false;

            if (this.IsPostBack)
            {
                var myOrder = new Order();
                if (this.TryUpdateModel(myOrder,
                   new FormValueProvider(this.ModelBindingExecutionContext)))
                {

                    myOrder.OrderLines = new List<OrderLine>();

                    Cart myCart = SessionHelper.GetCart(this.Session);

                    foreach (CartLine line in myCart.Lines)
                    {
                        myOrder.OrderLines.Add(new OrderLine
                        {
                            Order = myOrder,
                            Game = line.Game,
                            Quantity = line.Quantity
                        });
                    }

                    new Repository().SaveOrder(myOrder);
                    myCart.Clear();

                    this.checkoutForm.Visible = false;
                    this.checkoutMessage.Visible = true;
                }
            }
        }
    }
}