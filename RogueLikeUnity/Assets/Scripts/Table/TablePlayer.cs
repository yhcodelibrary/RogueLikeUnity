using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class TablePlayer
{
    private static TablePlayerData[] _table;
    private static TablePlayerData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TablePlayerData[] {

                      new TablePlayerData(11001, "ユニティちゃん", "Unity", "UnityChanBody", "UnityChanCameraDammy", PlayerType.UnityChan, 30, false, "不思議なレシピ", "Magical Recipe", "次回の調合が必ず成功する", "Next compounding surely succeeds.", "(Power1消費)", "(Use1Power)", 0, "中", "middle", 0.95f)
, new TablePlayerData(11002, "ステラちゃん", "Stella", "SD_Stellachan", "StellaChanCameraDammy", PlayerType.OricharChan, 20, true, "クーゲルタンズ", "Kugeltanz", "全方向3マス先まで攻撃", "Attack up to 3 distance in all directions.", "(Power2消費)", "(Use2Power)", 5, "高", "high", 0.97f)


                };
                
                return _table;
            }
        }
    }

    public static Dictionary<long, PlayerInformation> GetAllValue()
    {

        Dictionary<long, PlayerInformation> result = new Dictionary<long, PlayerInformation>();

        foreach (TablePlayerData data in Table)
        {
            PlayerInformation inf = new PlayerInformation();
            AttachValue(inf, data);

            result.Add(inf.ObjNo, inf);
        }

        return result;
    }

    public static PlayerInformation GetValue(long objno)
    {
        TablePlayerData data = Array.Find(Table, i => i.ObjNo == objno);

        PlayerInformation inf = new PlayerInformation();

        AttachValue(inf, data);

        return inf;
    }

    private static void AttachValue(PlayerInformation inf, TablePlayerData data)
    {
        inf.ObjNo = data.ObjNo;
        inf.CameraName = data.CameraName;
        inf.InstanceName = data.InstanceName;
        if (GameStateInformation.IsEnglish == false)
        {
            inf.DefaultName = data.DefaultName;
            inf.DeathblowName = data.DeathblowName;
            inf.DeathblowDescription = data.DeathblowDescription;
            inf.DeathblowDescription2 = data.DeathblowDescription2;
            inf.AlcReasonable = data.AlcReasonable;
        }
        else
        {
            inf.DefaultName = data.DefaultNameEn;
            inf.DeathblowName = data.DeathblowNameEn;
            inf.DeathblowDescription = data.DeathblowDescriptionEn;
            inf.DeathblowDescription2 = data.DeathblowDescription2En;
            inf.AlcReasonable = data.AlcReasonableEn;
        }
        inf.PType = data.PType;
        inf.ItemMaxCount = data.ItemMaxCount;
        inf.HasDeathblow = data.HasDeathblow;
        inf.DeathblowMp = data.DeathblowMp;
        inf.AlcReasonableFloat = data.AlcReasonableFloat;
    }

    private class TablePlayerData
    {
        public TablePlayerData(long objNo,
            string defaultName,
            string defaultNameEn,
            string instanceName,
            string cameraname,
            PlayerType pType,
            ushort itemMaxCount,
            bool hasDeathblow,
            string deathblowName,
            string deathblowNameEn,
            string deathblowDescription,
            string deathblowDescriptionEn,
            string deathblowDescription2,
            string deathblowDescription2En,
            int deathblowMp,
            string alcReasonable,
            string alcReasonableEn,
            float alcReasonableFloat)
        {
            ObjNo = objNo;
            DefaultName = defaultName;
            DefaultNameEn = defaultNameEn;
            InstanceName = instanceName;
            CameraName = cameraname;
            PType = pType;
            ItemMaxCount = itemMaxCount;
            HasDeathblow = hasDeathblow;
            DeathblowName = deathblowName;
            DeathblowNameEn = deathblowNameEn;
            DeathblowDescription = deathblowDescription;
            DeathblowDescriptionEn = deathblowDescriptionEn;
            DeathblowDescription2 = deathblowDescription2;
            DeathblowDescription2En = deathblowDescription2En;
            DeathblowMp = deathblowMp;
            AlcReasonable = alcReasonable;
            AlcReasonableEn = alcReasonableEn;
            AlcReasonableFloat = alcReasonableFloat;
        }

        public long ObjNo;
        public string DefaultName;
        public string DefaultNameEn;
        public string InstanceName;
        public string CameraName;
        public PlayerType PType;
        public ushort ItemMaxCount;
        public bool HasDeathblow;
        public string DeathblowName;
        public string DeathblowNameEn;
        public string DeathblowDescription;
        public string DeathblowDescriptionEn;
        public string DeathblowDescription2;
        public string DeathblowDescription2En;
        public int DeathblowMp;
        public string AlcReasonable;
        public string AlcReasonableEn;
        public float AlcReasonableFloat;
    }
}
