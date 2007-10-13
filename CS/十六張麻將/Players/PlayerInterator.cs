using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Mahjong.Players
{
    /// <summary>
    /// ª±®a¤ÏÂÐ¾¹
    /// </summary>
    class PlayerIterator : Iterator
    {
        ArrayList items;
        int position = 0;

        public PlayerIterator(ArrayList items)
        {
            this.items = items;
        }
        public Object next()
        {
            Object item = items[position];
            position++;
            return item;            
        }
        public bool hasNext()
        {
            if (position >= items.Count || items[position] == null)
                return false;
            else
                return true;
        }
    }
}
