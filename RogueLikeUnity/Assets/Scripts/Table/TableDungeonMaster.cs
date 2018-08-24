using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableDungeonMaster
{

    private static TableDungeonMasterData[] _table;
    private static TableDungeonMasterData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableDungeonMasterData[]
                {

                     new TableDungeonMasterData(50001, "始まりの洞窟" , "Beginning Cave" , MusicInformation.MusicType.Rogue1Alpha, "CaveCameraDammy", LoadStatus.Wall, 30, 20, 0.98f, false, false, false, true, 1f, 1f, 1f, 1f, 3, 0.9f, 1.2f, 30, 2, 0.9f,  1.5f, 20, 3, 0.9f, 1.2f, 20, 1f, 1f, 1.2f, "1:10024", "誰もが最初に挑む洞窟。モンスターもさほど強くない。","A cave where everyone challenges first. Monsters are not so strong either.")
, new TableDungeonMasterData(50003, "水辺の日塔" , "Waterside Shade Tower" , MusicInformation.MusicType.WaterSide, "WaterSideTowerCameraDammy", LoadStatus.Water, 40, 30, 0.98f, false, false, false, false, 0.5f, 1f, 1.1f, 1.2f, 4, 0.9f, 1.2f, 30, 3, 0.9f,  1.3f, 20, 5, 0.9f, 1.1f, 20, 1.15f, 1.3f, 1.8f, "1:10025", "水辺にたたずむ大きな塔。不思議な素材で建築されており、強い浸透圧によって上階まで水で覆われている。","A large tower nestling at the water's edge. It is built with mysterious material and covered with water to the upper floor.")
, new TableDungeonMasterData(50002, "古森の塔" , "Ancient Forest Tower" , MusicInformation.MusicType.OldTower, "ForestTowerCameraDammy", LoadStatus.Wall, 40, 30, 0.98f, false, true, false, true, 0.5f, 1f, 1.05f, 1.2f, 4, 0.9f, 1.2f, 30, 3, 0.9f,  1.1f, 20, 5, 0.9f, 1.1f, 20, 1.15f, 1.5f, 1.8f, "1:10030", "古森の奥にひっそりとたたずむ塔。かつて修練場として使われていた塔は、モンスターが住み着きその険しさを増した。","A tower standing quietly behind the forest. It was used as a training ground in the past, but monsters now control it.")
, new TableDungeonMasterData(50004, "リズミカルタイムストラテジー" , "Rhythmical Time Strategy" , MusicInformation.MusicType.RTS, "RTSCameraDammy", LoadStatus.Wall, 40, 30, 0.98f, true, false, false, true, 0.5f, 1f, 1.1f, 1.2f, 4, 0.9f, 1.2f, 30, 3, 0.9f,  1.3f, 20, 5, 0.9f, 1.1f, 20, 1.15f, 1.2f, 1.8f, "1:10026", "戦略にリアルなタイムを要する塔。その不思議な力を持つ空間は立ち止まることを許さない。","A tower that requires real time for strategy. You can not stop in that magical space.")
, new TableDungeonMasterData(50005, "ワイドワイドアナーキズム" , "Wide Wide Anarchism" , MusicInformation.MusicType.Anarchy, "WWAnarchyCameraDammy", LoadStatus.Wall, 40, 30, 0.6f, false, false, false, true, 0.5f, 1f, 1.1f, 1.2f, 8, 0.9f, 1.1f, 30, 4, 0.9f,  1.2f, 20, 5, 0.9f, 1.05f, 20, 1.15f, 1.5f, 1.8f, "1:10027", "広い部屋が多い塔。アイテムを駆使して敵の猛攻をかいくぐろう。","A tower with many large rooms. Let 's make use of items to survive the enemy' s onslaught.")
, new TableDungeonMasterData(50006, "ユニティのアトリエ" , "Atelier Of Unity" , MusicInformation.MusicType.Atelier, "AtelierCameraDammy", LoadStatus.Wall, 40, 30, 0.98f, false, false, false, true, 1f, 1f, 1f, 1.2f, 6, 0.9f, 1.02f, 30, 3, 0.9f,  1.3f, 20, 5, 0.9f, 1.1f, 20, 1.1f, 1.2f, 1.8f, "1:10028", "素材アイテムしか出ない塔。調合を駆使して道を切り開こう。","A tower with only material items. Let's make way by making use of compounding.")
, new TableDungeonMasterData(50008, "フィールドオブナイトメア" , "Field Of Nightmare" , MusicInformation.MusicType.FON, "FONCameraDammy", LoadStatus.Water, 40, 30, 0.98f, false, false, false, true, 0.5f, 1f, 1.5f, 1.2f, 4, 0.9f, 1.05f, 30, 3, 0.9f,  1.1f, 20, 5, 0.9f, 1.1f, 20, 1.1f, 3f, 1.8f, "3:10010,3:10020,3:10023,3:10019", "かつて、野球場を建設しようとして頓挫した計画地は、ボールとモンスターの墓場と化し、強力な暴力により支配されている。","The place where the baseball field construction plan collapsed. Currently there are lots of balls and monsters.")
, new TableDungeonMasterData(50009, "深雪の魔塔" , "Eternal Snow Tower" , MusicInformation.MusicType.EST, "MageTowerCameraDammy", LoadStatus.Wall, 40, 50, 0.98f, false, true, false, true, 0.5f, 1f, 1.2f, 1.2f, 4, 0.9f, 1.2f, 30, 3, 0.9f,  1.1f, 20, 5, 0.9f, 1.1f, 20, 1.25f, 1.5f, 1.8f, "1:10029", "雪の解けない遥か高山にそびえる謎の塔。古代の魔物が住まうという。","A magical tower rising in the mountains surrounded by eternal snow. The ancient devil seems to be alive.")

                };

                return _table;
            }
        }
    }
    //public static void SetValue(ManageDungeon dun)
    //{
    //    TableDungeonMasterData data = Array.Find(Table, i => i.DungeonObjNo == dun.DungeonObjNo);
        
    //    dun.X = data.X;
    //    dun.Y = data.Y;
    //    dun.DungeonName = data.Name;
    //    dun.CommonProb = data.Prob;
    //    dun.DisruptFloor = data.DisruptFloor;
    //    dun.IsTimer = data.IsTimer;
    //    dun.IsAnalyze = data.IsAnalyze;
    //    dun.IsBringing = data.IsBringing;
    //    dun.ItemS = data.ItemS;
    //    dun.ItemP = data.ItemP;
    //    dun.ItemC = data.ItemC;
    //    dun.ItemM = data.ItemM;
    //    dun.EnemyS = data.EnemyS;
    //    dun.EnemyP = data.EnemyP;
    //    dun.EnemyC = data.EnemyC;
    //    dun.EnemyM = data.EnemyM;
    //    dun.TrapS = data.TrapS;
    //    dun.TrapP = data.TrapP;
    //    dun.TrapC = data.TrapC;
    //    dun.TrapM = data.TrapM;
    //}
    public static Dictionary<long, DungeonInformation> GetAllValue()
    {
        Dictionary<long, DungeonInformation> result = new Dictionary<long, DungeonInformation>();
        
        foreach(TableDungeonMasterData data in Table)
        {
            DungeonInformation dun = new DungeonInformation();
            AttachValue(dun, data);
            result.Add(data.DungeonObjNo, dun);
        }

        return result;
    }

    public static DungeonInformation GetValue(long objno)
    {

        TableDungeonMasterData data = Array.Find(Table, i => i.DungeonObjNo == objno);

        DungeonInformation dun = new DungeonInformation();

        AttachValue(dun, data);

        return dun;
    }

    private static void AttachValue(DungeonInformation dun, TableDungeonMasterData data)
    {
        dun.DungeonObjNo = data.DungeonObjNo;
        dun.X = data.X;
        dun.Y = data.Y;
        dun.MType = data.MType;
        dun.CameraName = data.CameraName;
        dun.BaseLoadState = data.BaseLoadState;
        dun.Prob = data.Prob;
        dun.DisruptFloor = data.DisruptFloor;
        dun.IsTimer = data.IsTimer;
        dun.IsAnalyze = data.IsAnalyze;
        dun.IsBringing = data.IsBringing;
        dun.IsBadVisible = data.IsBadVisible;
        dun.KilnProb = data.Kilnprob;
        dun.StartProbHp = data.StartProbHp;
        dun.StartProbAtk = data.StartProbAtk;
        dun.StartProbExp = data.StartProbExp;
        dun.ItemS = data.ItemS;
        dun.ItemP = data.ItemP;
        dun.ItemC = data.ItemC;
        dun.ItemM = data.ItemM;
        dun.EnemyS = data.EnemyS;
        dun.EnemyP = data.EnemyP;
        dun.EnemyC = data.EnemyC;
        dun.EnemyM = data.EnemyM;
        dun.TrapS = data.TrapS;
        dun.TrapP = data.TrapP;
        dun.TrapC = data.TrapC;
        dun.TrapM = data.TrapM;
        dun.EnemyHpProb = data.EnemyHpProb;
        dun.EnemyAtkProb = data.EnemyAtkProb;
        dun.EnemyExpProb = data.EnemyExpProb;

        dun.BossObjNo = data.BossObjNo;
        if (GameStateInformation.IsEnglish == false)
        {
            dun.Name = data.Name;
            dun.Description = data.Description;
        }
        else
        {
            dun.Name = data.NameEn;
            dun.Description = data.DescriptionEn;
        }
    }

    private class TableDungeonMasterData
    {
        public TableDungeonMasterData(long dungeonId,
            string name,
            string nameEn,
            MusicInformation.MusicType mtype,
            string cameraName,
            LoadStatus baseLoadState,
            int width,
            int disruptFloor,
            float prob,
            bool isTimer,
            bool isAnalyze,
            bool isBringing,
            bool isVisible,
            float kilnprob,
            float startprobHp,
            float startprobAtk,
            float startprobExp,
            int itemS,
            float itemP,
            float itemC,
            int itemM,
            int enemyS,
            float enemyP,
            float enemyC,
            int enemyM,
            int trapS,
            float trapP,
            float trapC,
            int trapM,
            float enemyHpProb,
            float enemyAtkProb,
            float enemyExpProb,
            string bossObjNo,
            string description,
            string descriptionEn)
        {
            DungeonObjNo = dungeonId;
            CameraName = cameraName;
            MType = mtype;
            BaseLoadState = baseLoadState;
            Name = name;
            NameEn = nameEn;
            DisruptFloor = disruptFloor;
            X = width;
            Y = width;
            Prob = prob;
            IsTimer = isTimer;
            IsAnalyze = isAnalyze;
            IsBringing = isBringing;
            IsBadVisible = isVisible;
            Kilnprob = kilnprob;
            StartProbHp = startprobHp;
            StartProbAtk = startprobAtk;
            StartProbExp = startprobExp;
            ItemS = itemS;
            ItemP = itemP;
            ItemC = itemC;
            ItemM = itemM;
            EnemyS = enemyS;
            EnemyP = enemyP;
            EnemyC = enemyC;
            EnemyM = enemyM;
            TrapS = trapS;
            TrapP = trapP;
            TrapC = trapC;
            TrapM = trapM;
            EnemyHpProb = enemyHpProb;
            EnemyAtkProb = enemyAtkProb;
            EnemyExpProb = enemyExpProb;
            BossObjNo = bossObjNo;
            Description = description;
            DescriptionEn = descriptionEn;

        }
        public long DungeonObjNo;
        public string Name;
        public string NameEn;
        public MusicInformation.MusicType MType;
        public string CameraName;
        public LoadStatus BaseLoadState;
        public int X;
        public int Y;
        public int DisruptFloor;
        public float Prob;
        public bool IsTimer;
        public bool IsAnalyze;
        public bool IsBringing;
        public bool IsBadVisible;
        public float Kilnprob;
        public float StartProbHp;
        public float StartProbAtk;
        public float StartProbExp;
        public int ItemS;
        public float ItemP;
        public float ItemC;
        public int ItemM;
        public int EnemyS;
        public float EnemyP;
        public float EnemyC;
        public int EnemyM;
        public int TrapS;
        public float TrapP;
        public float TrapC;
        public int TrapM;
        public float EnemyHpProb;
        public float EnemyAtkProb;
        public float EnemyExpProb;
        public string BossObjNo;
        public string Description;
        public string DescriptionEn;
    }
}