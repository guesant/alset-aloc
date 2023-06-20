using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Interfaces
{
    interface IItem
    {
        public long Id { get; set; }
    }

    class TableEntry<T>
    {
        public T Item { get; set; }

        private long Id
        {
            get
            {
                var type = Item.GetType();

                var prop = type.GetProperty("Id");

                var id = (long)prop.GetValue(Item);

                return id;
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.idsSelected.Contains(this.Id);
            }
            set
            {
                if(value)
                {
                    if(!IsSelected)
                    {
                        this.idsSelected.Add(this.Id);
                    }
                } else
                {
                    if(IsSelected)
                    {
                        this.idsSelected.Remove(this.Id);
                    }
                }
            }
        }

        private List<long> idsSelected { get; set; }

        public TableEntry(T item, List<long> idsSelected)
        {
            this.Item = item;
            this.idsSelected = idsSelected;
        }
    }
}
