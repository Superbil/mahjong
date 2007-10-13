using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Brands;

namespace Mahjong.Players
{
    class TeamBrandsInterator : Iterator
    {
        TeamBrands items;
        int position = 0;

        public TeamBrandsInterator(TeamBrands items)
        {
            this.items = items;
        }
        public Object next()
        {
            Object item = items.getBrand(position);
            position++;
            return item;
            
        }
        public bool hasNext()
        {
            if (position >= items.getNumber() || items.getBrand(position) == null)
                return false;
            else
                return true;
        }
    }
}
