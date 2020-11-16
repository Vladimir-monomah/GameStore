using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GameStore.Models.Repository
{
    public class Repository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Game> Games
        {
            get { return this.context.Games; }
        }

        // Чтение данных из таблицы Orders
        public IEnumerable<Order> Orders
        {
            get
            {
                return this.context.Orders
                    .Include(o => o.OrderLines.Select(ol => ol.Game));
            }
        }

        //добавление, редактирование и удаление записей о товарах в БД
        public void SaveGame(Game game)
        {
            if (game.GameId == 0)
            {
                game = this.context.Games.Add(game);
            }
            else
            {
                Game dbGame = this.context.Games.Find(game.GameId);
                if (dbGame != null)
                {
                    dbGame.Name = game.Name;
                    dbGame.Description = game.Description;
                    dbGame.Price = game.Price;
                    dbGame.Category = game.Category;
                }
            }
            this.context.SaveChanges();
        }

        public void DeleteGame(Game game)
        {
            IEnumerable<Order> orders = this.context.Orders
                .Include(o => o.OrderLines.Select(ol => ol.Game))
                .Where(o => o.OrderLines
                    .Count(ol => ol.Game.GameId == game.GameId) > 0)
                .ToArray();

            foreach(Order order in orders)
            {
                this.context.Orders.Remove(order);
            }
            this.context.Games.Remove(game);
            this.context.SaveChanges();
        }

        // Сохранить данные заказа в базе данных
        public void SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                order = this.context.Orders.Add(order);

                foreach (OrderLine line in order.OrderLines)
                {
                    this.context.Entry(line.Game).State
                        = EntityState.Modified;
                }

            }
            else
            {
                Order dbOrder = this.context.Orders.Find(order.OrderId);
                if (dbOrder != null)
                {
                    dbOrder.Name = order.Name;
                    dbOrder.Line1 = order.Line1;
                    dbOrder.Line2 = order.Line2;
                    dbOrder.Line3 = order.Line3;
                    dbOrder.City = order.City;
                    dbOrder.GiftWrap = order.GiftWrap;
                    dbOrder.Dispatched = order.Dispatched;
                }
            }
            this.context.SaveChanges();
        }
    }
}