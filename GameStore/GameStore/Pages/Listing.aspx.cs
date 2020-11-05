using GameStore.Models;
using GameStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        private int pageSize = 4;

        protected int CurrentPage
        {
            get
            {
                int page;
                page = this.GetPageFromRequest();
                return page > this.MaxPage ? this.MaxPage : page;
            }
        }

        protected int MaxPage
        {
            get
            {
                return (int)Math.Ceiling((decimal)this.repository.Games.Count() / this.pageSize);
            }
        }

        private int GetPageFromRequest()
        {
            int page;
            string reqValue = (string)this.RouteData.Values["page"] ??
                this.Request.QueryString["page"];
            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
        }

        protected IEnumerable<Game> GetGames()
        {
            return this.repository.Games
                .OrderBy(g => g.GameId)
                .Skip((this.CurrentPage - 1) * this.pageSize)
                .Take(this.pageSize);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}