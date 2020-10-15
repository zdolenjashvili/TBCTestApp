using System;
using System.Collections.Generic;
using System.Text;

namespace TbcTestAppBLL.Models
{
    public class Page<TModel>
    {
        public Page(IEnumerable<TModel> items, long itemsCount, int pagesCount, int currentIndex)
        {
            Items = items;
            ItemsCount = itemsCount;
            PagesCount = pagesCount;
            CurrentIndex = currentIndex;
        }

        public IEnumerable<TModel> Items { get; }

        public long ItemsCount { get; }

        public int PagesCount { get; }

        public int CurrentIndex { get; }
    }
}
