﻿using System;
using System.Collections;
using Binding.GameOfLife.Mart.Utility;

namespace Binding.GameOfLife.Mart
{
    /// <summary>
    /// Manages a two dimensional array of bool in which the left and right edges,
    /// and the top and bottom edges are adjacent - a torus.
    /// </summary>
    class LifeTorus
    {
        readonly BitArray _array;
        readonly int _width;
        readonly int _height;
        private int _xGlobalLifeTorusCount;
        

        public LifeTorus(Dimensions dim)
        {
            _width = dim.Width;
            _height = dim.Height;
            int size = _width * _height;
            _array = new BitArray(size);
            
            
        }


        public void Clear()
        {
            _array.SetAll(false);
        }

        public void CopyTo(LifeTorus dest)
        {
            // Copies this torus to another usually differently sized torus
            int width = Math.Min(this._width, dest._width);
            int height = Math.Min(this._height, dest._height);
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    dest[x, y] = this[x, y];
        }

        public bool this[int x, int y]
        {
            get
            {
                // the getter can index one outside the normal range to handle the torus folding
                if (x < -1 || x >= (_width + 1) ||
                    y < -1 || y >= (_height + 1))
                    throw new ArgumentOutOfRangeException();

                int x1 = (x < 0 ? x + _width : x) % _width;
                int y1 = (y < 0 ? y + _height : y) % _height;
                int index = y1 * _width + x1;
                return _array[index];
            }
            set
            {
                if (x < 0 || x >= _width ||
                    y < 0 || y >= _height)
                    throw new ArgumentOutOfRangeException();

                int x1 = x % _width;
                int y1 = y % _height;
                int index = y1 * _width + x1;
                _array[index] = value;
            }
        }
    }
}
