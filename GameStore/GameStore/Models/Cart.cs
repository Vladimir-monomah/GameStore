﻿using System.Collections.Generic;
using System.Linq;

namespace GameStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Game game, int quantity)
        {
            CartLine line = this.lineCollection
                .Where(p => p.Game.GameId == game.GameId)
                .FirstOrDefault();

            if (line == null)
            {
                this.lineCollection.Add(new CartLine
                {
                    Game = game,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Game game)
        {
            this.lineCollection.RemoveAll(l => l.Game.GameId == game.GameId);
        }

        public decimal ComputeTotalValue()
        {
            return this.lineCollection.Sum(e => e.Game.Price * e.Quantity);
        }

        public void Clear()
        {
            this.lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return this.lineCollection; }
        }
    }

    public class CartLine
    {
        public Game Game { get; set; }
        public int Quantity { get; set; }
    }
}