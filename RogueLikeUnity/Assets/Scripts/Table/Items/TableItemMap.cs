using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableItemMap
{
    private static TableItemMapData[] _table;
    private static TableItemMapData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableItemMapData[]
                {

                    new TableItemMapData(50001, 1, 6, 1)
 ,new TableItemMapData(50001, 7, 13, 2)
 ,new TableItemMapData(50001, 14, 19, 3)
 ,new TableItemMapData(50001, 20, 100, 4)
 ,new TableItemMapData(50002, 1, 6, 1)
 ,new TableItemMapData(50002, 7, 13, 2)
 ,new TableItemMapData(50002, 14, 19, 3)
 ,new TableItemMapData(50002, 20, 26, 4)
 ,new TableItemMapData(50002, 27, 35, 5)
 ,new TableItemMapData(50002, 36, 42, 6)
 ,new TableItemMapData(50002, 43, 100, 7)
 ,new TableItemMapData(50003, 1, 6, 1)
 ,new TableItemMapData(50003, 7, 13, 2)
 ,new TableItemMapData(50003, 14, 19, 3)
 ,new TableItemMapData(50003, 20, 26, 4)
 ,new TableItemMapData(50003, 27, 100, 5)
 ,new TableItemMapData(50004, 1, 6, 1)
 ,new TableItemMapData(50004, 7, 13, 2)
 ,new TableItemMapData(50004, 14, 19, 3)
 ,new TableItemMapData(50004, 20, 26, 4)
 ,new TableItemMapData(50004, 27, 100, 5)
 ,new TableItemMapData(50005, 1, 6, 9)
 ,new TableItemMapData(50005, 7, 13, 10)
 ,new TableItemMapData(50005, 14, 19, 11)
 ,new TableItemMapData(50005, 20, 26, 12)
 ,new TableItemMapData(50005, 27, 100, 13)
 ,new TableItemMapData(50006, 1, 100, 8)
 ,new TableItemMapData(50008, 1, 100, 14)



            };
                return _table;
            }
        }
    }

    public static int GetValue(long dungeonNo, int floor)
    {
        TableItemMapData data = Array.Find(Table, i => i.DungeonNo == dungeonNo
                    && i.FloorStart <= floor && floor <= i.FloorEnd);
        //Table.Where(i => i.DungeonNo == dungeonNo
        //&& i.FloorStart <= floor && floor <= i.FloorEnd).First();
        return data.Map;
    }

    private class TableItemMapData
    {
        public TableItemMapData(ushort dungeonNo,
            int floorStart,
            int floorEnd,
            int map)
        {
            DungeonNo = dungeonNo;
            FloorStart = floorStart;
            FloorEnd = floorEnd;
            Map = map;
        }
        public ushort DungeonNo;
        public int FloorStart;
        public int FloorEnd;
        public int Map;
    }
}
