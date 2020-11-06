using GameStore.Models;
using GameStore.Models.Repository;
using GameStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

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
                int prodCount = this.FilterGames().Count();
                return (int)Math.Ceiling((decimal)prodCount / this.pageSize);
            }
        }

        private int GetPageFromRequest()
        {
            int page;
            string reqValue = (string)this.RouteData.Values["page"] ??
                this.Request.QueryString["page"];
            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
        }

        public IEnumerable<Game> GetGames()
        {
            return this.FilterGames()
                .OrderBy(g => g.GameId)
                .Skip((this.CurrentPage - 1) * this.pageSize)
                .Take(this.pageSize);
        }

        // Новый вспомогательный метод для фильтрации игр по категориям
        private IEnumerable<Game> FilterGames()
        {
            IEnumerable<Game> games = this.repository.Games;
            string currentCategory = (string)this.RouteData.Values["category"] ??
                this.Request.QueryString["category"];
            return currentCategory == null ? games :
                games.Where(p => p.Category == currentCategory);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                int selectedGameId;
                if(int.TryParse(this.Request.Form["add"],out selectedGameId))
                {
                    Game selectedGame = this.repository.Games
                        .Where(g => g.GameId == selectedGameId).FirstOrDefault();

                    if (selectedGame != null)
                    {
                        SessionHelper.GetCart(this.Session).AddItem(selectedGame, 1);
                        SessionHelper.Set(this.Session, SessionKey.RETURN_URL,
                            this.Request.RawUrl);

                        this.Response.Redirect(RouteTable.Routes
                            .GetVirtualPath(null, "cart", null).VirtualPath);
                    }
                }
            }
        }
    }
}