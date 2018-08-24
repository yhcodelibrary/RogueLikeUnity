﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// 2次元レイヤー
public class Layer2D<T>
{

    int _width; // 幅
    int _height; // 高さ
    T _outOfRange; // 領域外を指定した時の値
    T[] _values = null; // マップデータ

    /// 幅
    public int Width
    {
        get { return _width; }
    }
    /// 高さ
    public int Height
    {
        get { return _height; }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public Layer2D(int width = 0, int height = 0)
    {

        if (width > 0 && height > 0)
        {
            Create(width, height);
        }
    }

    /// 作成
    public void Create(int width, int height)
    {
        _width = width;
        _height = height;
        _values = new T[Width * Height];
    }

    /// 座標をインデックスに変換する
    public int ToIdx(int x, int y)
    {
        return x + (y * Width);
    }

    /// 領域外かどうかチェックする
    public bool IsOutOfRange(int x, int y)
    {
        if (x < 0 || x >= Width) { return true; }
        if (y < 0 || y >= Height) { return true; }

        // 領域内
        return false;
    }


    public T Get(MapPoint p)
    {
        return Get(p.X, p.Y);
    }

    /// <summary>
    /// 値の取得
    /// </summary>
    /// <param name="x">X座標</param>
    /// <param name="y">Y座標</param>
    /// <returns>指定の座標の値（領域外を指定したら_outOfRangeを返す）</returns>
    public T Get(int x, int y)
    {
        if (IsOutOfRange(x, y))
        {
            return _outOfRange;
        }

        return _values[y * Width + x];
    }

    /// 値の設定
    // @param x X座標
    // @param y Y座標
    // @param v 設定する値
    public void Set(int x, int y, T v)
    {
        if (IsOutOfRange(x, y))
        {
            // 領域外を指定した
            return;
        }

        _values[y * Width + x] = v;
    }

    /// <summary>
    /// すべてのセルを特定の値で埋める
    /// </summary>
    /// <param name="val">埋める値</param>
    public void Fill(T val)
    {
        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                Set(i, j, val);
            }
        }
    }

    /// <summary>
    /// 矩形領域を指定の値で埋める
    /// </summary>
    /// <param name="x">矩形の左上(X座標)</param>
    /// <param name="y">矩形の左上(Y座標)</param>
    /// <param name="w">矩形の幅</param>
    /// <param name="h">矩形の高さ</param>
    /// <param name="val">埋める値</param>
    public void FillRect(int x, int y, int w, int h, T val)
    {
        for (int j = 0; j < h; j++)
        {
            for (int i = 0; i < w; i++)
            {
                int px = x + i;
                int py = y + j;
                Set(px, py, val);
            }
        }
    }

    /// <summary>
    /// 矩形領域を指定の値で埋める（4点指定)
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="top">上</param>
    /// <param name="right">右</param>
    /// <param name="bottom">下</param>
    /// <param name="val">埋める値</param>
    public void FillRectLTRB(int left, int top, int right, int bottom, T val)
    {
        FillRect(left, top, right - left, bottom - top, val);
    }

    /// デバッグ出力
    public void Dump()
    {
        System.IO.StreamWriter writer =
            new System.IO.StreamWriter(@"C:\Unity\test.txt", true, System.Text.Encoding.UTF8);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        Debug.Log("[Layer2D] (w,h)=(" + Width + "," + Height + ")");
        for (int y = 0; y < Height; y++)
        {
            string s = "";
            for (int x = 0; x < Width; x++)
            {
                s += Get(x, y) + ",";

                sb.Append(Get(x, y));
                sb.Append(",");
            }
            //           Debug.Log(s);
            sb.AppendLine("");
        }

        writer.WriteLine(sb.ToString());
        writer.Close();
    }
}