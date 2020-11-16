using GameStore.Models;
using GameStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;

namespace GameStore.Pages.Admin
{
    public partial class Games : System.Web.UI.Page
    {
        private Repository repository = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Game> GetGames()
        {
            return this.repository.Games;
        }

        public void UpdateGame(int GameID)
        {
            Game myGame = this.repository.Games
                .Where(p => p.GameId == GameID).FirstOrDefault();
            if (myGame != null && this.TryUpdateModel(myGame,
                new FormValueProvider(this.ModelBindingExecutionContext)))
            {
                this.repository.SaveGame(myGame);
            }
        }

        public void DeleteGame(int GameID)
        {
            Game myGame = this.repository.Games
                .Where(p => p.GameId == GameID).FirstOrDefault();
            if (myGame != null)
            {
                this.repository.DeleteGame(myGame);
            }
        }

        public void InsertGame()
        {
            Game myGame = new Game();
            if (this.TryUpdateModel(myGame,
                new FormValueProvider(this.ModelBindingExecutionContext)))
            {
                this.repository.SaveGame(myGame);
            }
        }
    }
}