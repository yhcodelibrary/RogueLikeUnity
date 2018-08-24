using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableTrap
{

    private static TableTrapData[] _table;
    private static TableTrapData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableTrapData[]{

                     new TableTrapData(30001, "毒の沼", "Poison swamp", "TrapPoison", TrapType.Poison, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30002, "ぬかるみ", "Muddy", "TrapSlow", TrapType.Mud, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30003, "回転床", "Rotating pad", "TrapRoate", TrapType.Rotation, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30004, "子守歌", "Lullaby", "TrapSong", TrapType.Song, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30005, "痺れ霧", "Numbness fog", "TrapGas", TrapType.Palalysis, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30006, "砂嵐", "Sandstorm", "TrapSandStorm", TrapType.SandStorm, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30007, "水バケツ", "Water bucket", "TrapBucket", TrapType.WaterBucket, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30008, "極彩色モニター", "Extreme color monitor", "TrapColorMonitor", TrapType.ColorMonitor, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30009, "竜巻", "Tornado", "TrapCyclone", TrapType.Cyclone, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30010, "召喚陣", "Summons", "TrapSummon", TrapType.Summon, 1, 0.9f, 1.5f, 0, 0f)
, new TableTrapData(30011, "小型地雷", "Small mine", "TrapBomb", TrapType.Bomb, 0, 0f, 0f, 20, 3f)
, new TableTrapData(30012, "中型地雷", "Medium mine", "TrapBomb", TrapType.Bomb, 0, 0f, 0f, 100, 2f)
, new TableTrapData(30013, "大型地雷", "Large mine", "TrapBomb", TrapType.Bomb, 0, 0f, 0f, 200, 1.05f)
, new TableTrapData(30014, "ハエのたかり場", "Fly's scene", "TrapFly", TrapType.TheFly, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30015, "カレーコロッケの写真", "Photo of curry croquette", "TrapPhoto", TrapType.Photo, 0, 0f, 0f, 30, 0f)
, new TableTrapData(30016, "電流", "Electrical", "TrapElectric", TrapType.Electric, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30017, "花粉", "Pollen", "TrapPollen", TrapType.Pollen, 0, 0f, 0f, 1, 0f)
, new TableTrapData(30018, "猛毒の沼", "Deadly Poison swamp", "TrapPoison", TrapType.DeadlyPoison, 0, 0f, 0f, 0, 0f)
, new TableTrapData(30019, "火柱", "Pillar of fire", "TrapFloorFire", TrapType.Ember, 0, 0f, 0f, 1, 0f)


};
                return _table;
            }
        }
    }

    public static BaseTrap GetTrap(long objNo)
    {
        TableTrapData data = Array.Find(Table, i => i.ObjNo == objNo);
        BaseTrap item = new BaseTrap();
        item.Initialize();
        item.ObjNo = data.ObjNo;
        if (GameStateInformation.IsEnglish == false)
        {
            item.DisplayName = data.DisplayName;
        }
        else
        {
            item.DisplayName = data.DisplayNameEn;
        }
        item.InstanceName = data.InstanceName;
        item.TType = data.Ttype;
        item.CountStart = data.CountStart;
        item.ProbStart = data.ProbStart;
        item.ProbReduce = data.ProbReduce;
        item.CommonNumber = data.CommonNumber;
        item.PerPlayerDamage = data.PerPlayerDamage;
        return item;
    }

    private class TableTrapData
    {
        public TableTrapData(long objNo,
            string displayName,
            string displayNameEn,
            string instanceName,
            TrapType ttype,
            int countStart,
            float probStart,
            float probReduce,
            int commonNumber,
            float perPlayerDamage)
        {
            ObjNo = objNo;
            DisplayName = displayName;
            DisplayNameEn = displayNameEn;
            InstanceName = instanceName;
            Ttype = ttype;
            CountStart = countStart;
            ProbStart = probStart;
            ProbReduce = probReduce;
            CommonNumber = commonNumber;
            PerPlayerDamage = perPlayerDamage;
        }
                     

        public long ObjNo;
        public string DisplayName;
        public string DisplayNameEn;
        public string InstanceName;
        public TrapType Ttype;
        public int CountStart;
        public float ProbStart;
        public float ProbReduce;
        public int CommonNumber;
        public float PerPlayerDamage;
    }
}
