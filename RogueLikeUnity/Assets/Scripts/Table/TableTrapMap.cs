using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class TableTrapMap
{
    private static TableTrapMapData[] _table;
    private static TableTrapMapData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableTrapMapData[] {

      new TableTrapMapData(50001, 1, 10, 1)
 ,new TableTrapMapData(50001, 11, 100, 2)
 ,new TableTrapMapData(50002, 1, 5, 1)
 ,new TableTrapMapData(50002, 6, 12, 2)
 ,new TableTrapMapData(50002, 13, 18, 3)
 ,new TableTrapMapData(50002, 19, 27, 4)
 ,new TableTrapMapData(50002, 28, 40, 5)
 ,new TableTrapMapData(50002, 41, 100, 6)
 ,new TableTrapMapData(50003, 1, 5, 1)
 ,new TableTrapMapData(50003, 6, 12, 2)
 ,new TableTrapMapData(50003, 13, 18, 3)
 ,new TableTrapMapData(50003, 19, 27, 4)
 ,new TableTrapMapData(50003, 28, 40, 5)
 ,new TableTrapMapData(50003, 41, 100, 6)
 ,new TableTrapMapData(50004, 1, 5, 1)
 ,new TableTrapMapData(50004, 6, 12, 2)
 ,new TableTrapMapData(50004, 13, 18, 3)
 ,new TableTrapMapData(50004, 19, 27, 4)
 ,new TableTrapMapData(50004, 28, 40, 5)
 ,new TableTrapMapData(50004, 41, 100, 6)
 ,new TableTrapMapData(50005, 1, 5, 1)
 ,new TableTrapMapData(50005, 6, 12, 2)
 ,new TableTrapMapData(50005, 13, 18, 3)
 ,new TableTrapMapData(50005, 19, 27, 4)
 ,new TableTrapMapData(50005, 28, 40, 5)
 ,new TableTrapMapData(50005, 41, 100, 6)
 ,new TableTrapMapData(50006, 1, 5, 1)
 ,new TableTrapMapData(50006, 6, 12, 2)
 ,new TableTrapMapData(50006, 13, 18, 3)
 ,new TableTrapMapData(50006, 19, 27, 4)
 ,new TableTrapMapData(50006, 28, 40, 5)
 ,new TableTrapMapData(50006, 41, 100, 6)
 ,new TableTrapMapData(50008, 1, 5, 1)
 ,new TableTrapMapData(50008, 6, 12, 2)
 ,new TableTrapMapData(50008, 13, 18, 3)
 ,new TableTrapMapData(50008, 19, 27, 4)
 ,new TableTrapMapData(50008, 28, 40, 5)
 ,new TableTrapMapData(50008, 41, 100, 6)



                };
                return _table;
            }
        }
    }


    public static int GetValue(long dungeonNo, int floor)
    {
        TableTrapMapData data = Array.Find(Table, i => i.DungeonNo == dungeonNo
                && i.FloorStart <= floor && floor <= i.FloorEnd);
        //Table.Where(i => i.DungeonNo == dungeonNo
        //&& i.FloorStart <= floor && floor <= i.FloorEnd).First();
        return data.EnemyMap;
    }

    private class TableTrapMapData
    {
        public TableTrapMapData(int dungeonNo,
            int floorStart,
            int floorEnd,
            int enemyMap)
        {
            DungeonNo = dungeonNo;
            FloorStart = floorStart;
            FloorEnd = floorEnd;
            EnemyMap = enemyMap;
        }
        public int DungeonNo;
        public int FloorStart;
        public int FloorEnd;
        public int EnemyMap;
    }
}
