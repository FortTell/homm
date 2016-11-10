﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HoMM.Generators
{
    public class Vector2i
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        
        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public static Vector2i operator +(Vector2i first, Vector2i second)
        {
            return new Vector2i(first.X + second.X, first.Y + second.Y);
        }

        public static Vector2i operator -(Vector2i first, Vector2i second)
        {
            return new Vector2i(first.X - second.X, first.Y - second.Y);
        }

        public double EuclideanDistance(Vector2i other)
        {
            return Math.Sqrt(Dot(this - other));
        }

        public double ManhattanDistance(Vector2i other)
        {
            var diff = this - other;
            return Math.Abs(diff.X) + Math.Abs(diff.Y);
        }

        public double Dot(Vector2i other)
        {
            return X * other.X + Y * other.Y;
        }

        #region *** GetHashCode and Equals  ***
        public override int GetHashCode()
        {
            return X * 1023 ^ Y;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Vector2i;

            if (other != null)
                return X == other.X && Y == other.Y;

            return false;
        }
        #endregion
    }
}
