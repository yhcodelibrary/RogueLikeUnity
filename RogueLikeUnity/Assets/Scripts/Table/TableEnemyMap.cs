
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class TableEnemyMap
{
    private static TableEnemyMapData[] _table;
    private static TableEnemyMapData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableEnemyMapData[]
                {

 new TableEnemyMapData(50002, 1, 2, 1)
 ,new TableEnemyMapData(50002, 3, 6, 2)
 ,new TableEnemyMapData(50002, 7, 10, 3)
 ,new TableEnemyMapData(50002, 11, 15, 4)
 ,new TableEnemyMapData(50002, 16, 20, 5)
 ,new TableEnemyMapData(50002, 21, 26, 6)
 ,new TableEnemyMapData(50002, 27, 35, 7)
 ,new TableEnemyMapData(50002, 36, 43, 8)
 ,new TableEnemyMapData(50002, 44, 100, 9)
 ,new TableEnemyMapData(50001, 1, 4, 10)
 ,new TableEnemyMapData(50001, 5, 9, 11)
 ,new TableEnemyMapData(50001, 10, 15, 12)
 ,new TableEnemyMapData(50001, 16, 19, 13)
 ,new TableEnemyMapData(50001, 20, 1000, 1)
 ,new TableEnemyMapData(50003, 1, 4, 14)
 ,new TableEnemyMapData(50003, 5, 9, 15)
 ,new TableEnemyMapData(50003, 10, 15, 16)
 ,new TableEnemyMapData(50003, 16, 20, 17)
 ,new TableEnemyMapData(50003, 21, 25, 18)
 ,new TableEnemyMapData(50003, 26, 100, 19)
 ,new TableEnemyMapData(50004, 1, 4, 32)
 ,new TableEnemyMapData(50004, 5, 9, 33)
 ,new TableEnemyMapData(50004, 10, 15, 34)
 ,new TableEnemyMapData(50004, 16, 20, 35)
 ,new TableEnemyMapData(50004, 21, 25, 36)
 ,new TableEnemyMapData(50004, 26, 100, 37)
 ,new TableEnemyMapData(50005, 1, 4, 20)
 ,new TableEnemyMapData(50005, 5, 9, 21)
 ,new TableEnemyMapData(50005, 10, 15, 22)
 ,new TableEnemyMapData(50005, 16, 20, 23)
 ,new TableEnemyMapData(50005, 21, 25, 24)
 ,new TableEnemyMapData(50005, 26, 100, 25)
 ,new TableEnemyMapData(50006, 1, 4, 26)
 ,new TableEnemyMapData(50006, 5, 9, 27)
 ,new TableEnemyMapData(50006, 10, 15, 28)
 ,new TableEnemyMapData(50006, 16, 20, 29)
 ,new TableEnemyMapData(50006, 21, 25, 30)
 ,new TableEnemyMapData(50006, 26, 100, 31)
 ,new TableEnemyMapData(50008, 1, 4, 38)
 ,new TableEnemyMapData(50008, 5, 9, 39)
 ,new TableEnemyMapData(50008, 10, 15, 40)
 ,new TableEnemyMapData(50008, 16, 20, 41)
 ,new TableEnemyMapData(50008, 21, 25, 42)
 ,new TableEnemyMapData(50008, 26, 100, 43)



            };
                return _table;
            }
        }
    }


    public static int GetValue(long dungeonNo,int floor)
    {
        TableEnemyMapData data = Array.Find(Table, i => i.DungeonNo == dungeonNo
            && i.FloorStart <= floor && floor <= i.FloorEnd);
        //i => i.DungeonNo == dungeonNo 
        //&& i.FloorStart <= floor && floor <= i.FloorEnd).First();
        return data.EnemyMap;
    }

    private class TableEnemyMapData
    {
        public TableEnemyMapData(long dungeonNo,
            int floorStart,
            int floorEnd,
            int enemyMap)
        {
            DungeonNo = dungeonNo;
            FloorStart = floorStart;
            FloorEnd = floorEnd;
            EnemyMap = enemyMap;
        }
        public long DungeonNo;
        public int FloorStart;
        public int FloorEnd;
        public int EnemyMap;
    }
}
