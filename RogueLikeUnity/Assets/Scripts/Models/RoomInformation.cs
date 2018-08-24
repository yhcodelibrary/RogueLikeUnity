using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInformation
{

    public RoomInformation(int top, int bottom, int right, int left, Guid name)
    {
        Top = (ushort)top;
        Bottom = (ushort)bottom;
        Right = (ushort)right;
        Left = (ushort)left;
        Name = name;
        Entrance = new List<MapPoint>();
        Exits = new List<MapPoint>();
    }

    public RoomInformation GetVisiblity()
    {
        return new RoomInformation(Top - 1, Bottom, Right, Left - 1, Guid.Empty);
    }

    public ushort Top { get; private set; }
    public ushort Bottom { get; private set; }
    public ushort Right { get; private set; }
    public ushort Left { get; private set; }
    public Guid Name;
    public List<MapPoint> Entrance;
    public List<MapPoint> Exits;
}
