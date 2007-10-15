using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Mahjong.Players
{
    /// <summary>
    /// ª±®a¤ÏÂÐ¾¹
    /// </summary>
    class PlayerIteratorLimit : Iterator
    {
        ArrayList items;
        int position = 0;
        int limit = 0;

        public PlayerIteratorLimit(ArrayList items,int number)
        {
            this.items = items;
            this.limit = number;
        }
        public Object next()
        {
            Object item = items[position];
            position++;
            return item;            
        }
        public bool hasNext()
        {
            if (position >= items.Count || items[position] == null || position >= limit)
                return false;
            else
                return true;
        }
    }
}
