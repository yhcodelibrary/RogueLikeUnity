using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : IEquatable<MapPoint>
{
    public int X;
    public int Y;

    private static Dictionary<int, MapPoint> _Maps;
    private static Dictionary<int, MapPoint> Maps
    {
        get
        {
            if(CommonFunction.IsNull(_Maps) == false)
            {
                return _Maps;
            }
            _Maps = new Dictionary<int, MapPoint>();
            return _Maps;
        }
    }
    public static MapPoint Get(int x, int y)
    {
        int r = x + y * 1000;

        if (Maps.ContainsKey(r) == true)
        {
            return Maps[r];
        }
        else
        {
            MapPoint mp = new MapPoint(x, y);
            Maps.Add(r, mp);
            return mp;
        }
    }

    public MapPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        //objがnullか、型が違うときは、等価でない
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        return (this.X == ((MapPoint)obj).X) && (this.Y == ((MapPoint)obj).Y);
    }
    public override int GetHashCode()
    {
        return X + Y * 1000;
    }

    public bool Equal(MapPoint target)
    {
        return Equal(target.X, target.Y);
    }
    public bool Equals(MapPoint other)
    {
        throw new NotImplementedException();
    }

    public bool Equal(int x,int y)
    {
        if (this.X == x && this.Y == y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public MapPoint Add(MapPoint target)
    {
        return Get(this.X + target.X, this.Y + target.Y);
    }
    public MapPoint Add(int x,int y)
    {
        return Get(this.X + x, this.Y + y);
    }

    /// <summary>
    /// 二つのポイントの距離を計算
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public MapPoint Distance(MapPoint target)
    {
        return Get(this.X - target.X, this.Y - target.Y);
    }
    /// <summary>
    /// 二つのポイントの距離を計算
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public int DistanceAbs(int x,int y)
    {
        return Mathf.Max(Mathf.Abs(this.X - x),Mathf.Abs(this.Y - y));
    }
    /// <summary>
    /// 二つのポイントの距離を計算
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public int DistanceAbs(MapPoint target)
    {
        return Mathf.Max(Mathf.Abs(this.X - target.X), Mathf.Abs(this.Y - target.Y));
    }

    /// <summary>
    /// 対象ポイントが直線上にあるか
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool IsStraightLine(MapPoint target)
    {
        int i = X - target.X;
        int j = Y - target.Y;

        //隣接なら正とする
        if (-1 <= i && i <= 1
            && -1 <= j && j <= 1)
        {
            return true;
        }
        
        //二マス以上離れているとき　直線状にいるかどうか
        //斜め方向
        if((Mathf.Abs(i) == Mathf.Abs(j)))
        {
            return true;
        }
        //縦
        if ((i) == 0)
        {
            return true;
        }
        //横
        if ((j) == 0)
        {
            return true;
        }
        bool result = Mathf.Abs(this.X - target.X) == Mathf.Abs(this.Y - target.Y);
        return result;
    }

}