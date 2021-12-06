using System.Collections.Generic;

namespace Models
{
    public class Cart
    {
        public IDictionary<int, int> Items { get; set; } = new Dictionary<int, int>();
        public decimal TotalPrice { get; set; }

        public void Remove(int id)
        {
            Items.Remove(id);
        }

        public void Reduce(int id)
        {
            if (Items[id] == 1)
                Remove(id);
            else
                Items[id]--;
        }

        public void Add(int id)
        {
            Items[id]++;
        }
    }
}