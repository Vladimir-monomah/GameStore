using GameStore.Models;
using GameStore.Models.Repository;
using System;
using System.Collections.Generic;

namespace GameStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repository = new Repository();

        protected IEnumerable<Game> GetGames()
        {
            return repository.Games;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}