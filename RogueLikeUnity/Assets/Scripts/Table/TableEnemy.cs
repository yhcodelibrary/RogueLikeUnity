using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TableEnemy
{

    private static TableEnemyData[] _table;
    private static TableEnemyData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableEnemyData[]
                {

                    new TableEnemyData(10001, 1, 1, "ピョンガー", "Rabbiter", 9, EnemyType.Rabbit_1, "Rabbit_1", 11f, 1, 1, true, "", 0.7f, 0.95f, 2f, 15, 0f, 1f, 1, 0.5f, 0.075f, 0.7f, 1.4f, 1, "")
, new TableEnemyData(10002, 2, 1, "プランタス", "Plantas", 2, EnemyType.Plant_1, "Plant_1", 14f, 3, 1, false, "", 1.2f, 0.92f, 2f, 15, 0f, 1f, 2, 0.35f, 0.08f, 0.5f, 1.4f, 1, "")
, new TableEnemyData(10003, 3, 1, "ブーバット", "Boubad", 33, EnemyType.Bat_1, "Bat_1", 9f, 1, 1, true, "2:0.02", 1f, 0.92f, 1f, 15, 0f, 1f, 1, 0.35f, 0.085f, 0.5f, 1.4f, 1, "")
, new TableEnemyData(10004, 4, 1, "ゼラッチ", "Gelatche", 4, EnemyType.Slime_1, "Slime_1", 15f, 1, 1, true, "", 0.9f, 0.95f, 1f, 15, 0f, 1f, 1, 0.7f, 0.085f, 0.6f, 1.4f, 1, "2")
, new TableEnemyData(10005, 5, 1, "レイスン", "Leyson", 32, EnemyType.Ghost_1, "Ghost_1", 10f, 1, 2, true, "", 0.5f, 0.95f, 1f, 15, 0f, 1f, 1, 0.6f, 0.06f, 0.4f, 1.4f, 1, "")
, new TableEnemyData(10006, 6, 1, "獄彩蝶", "Extfly", 48, EnemyType.Butterfly_1, "Butterfly_1", 11f, 1, 1, true, "8:0.02,16:0.05", 0.9f, 0.97f, 6f, 15, 0f, 1f, 1, 0.7f, 0.075f, 0.5f, 1.4f, 1, "")
, new TableEnemyData(10007, 7, 1, "バッディベア", "Baddy Bear", 9, EnemyType.TeddyBear_1, "TeddyBear_1", 10f, 1, 1, true, "", 1.1f, 0.92f, 4f, 15, 0f, 1f, 1, 0.85f, 0.1f, 0.4f, 1.5f, 1, "0.04")
, new TableEnemyData(10008, 8, 1, "デクモス", "Dexmus", 16, EnemyType.SPIDER_1, "SPIDER_1", 10f, 1, 1, true, "", 1f, 0.95f, 1f, 15, 0f, 1f, 1, 0.65f, 0.09f, 0.8f, 1.5f, 1, "0.02")
, new TableEnemyData(10009, 9, 1, "シャーレックス", "Sharrex", 9, EnemyType.Whale_1, "Whale_1", 11f, 1, 1, true, "", 1.2f, 0.95f, 1f, 15, 0f, 1f, 2, 0.9f, 0.105f, 0.3f, 1.5f, 1, "0.05")
, new TableEnemyData(10010, 10, 1, "バルドローン", "Baldrone", 36, EnemyType.PA_Drone_1, "PA_Drone_1", 11f, 2, 1, true, "", 0.7f, 0.92f, 2f, 15, 0f, 1f, 1, 0.5f, 0.08f, 0.8f, 1.4f, 1, "1")
, new TableEnemyData(10011, 11, 1, "エフレキル", "Effrekill", 96, EnemyType.PA_Warrior_1, "PA_Warrior_1", 9f, 1, 1, true, "", 0.8f, 0.95f, 1f, 15, 0f, 1f, 1, 0.45f, 0.085f, 0.7f, 1.4f, 1, "0.02,30011")
, new TableEnemyData(10012, 12, 1, "獄彩蝶", "Extfly", 48, EnemyType.Butterfly_1, "Butterfly_1", 11f, 1, 1, true, "8:0.02,32:0.05,256:0.05", 0.9f, 0.97f, 6f, 15, 0f, 1f, 1, 0.7f, 0.075f, 0.5f, 1.4f, 1, "")
, new TableEnemyData(10013, 13, 1, "エフレキル", "Effrekill", 96, EnemyType.PA_Warrior_1, "PA_Warrior_1", 9f, 1, 1, true, "", 0.8f, 0.95f, 1f, 15, 0f, 1f, 1, 0.9f, 0.085f, 0.7f, 1.4f, 1, "0.02,30012")
, new TableEnemyData(10014, 14, 1, "獄彩蝶", "Extfly", 48, EnemyType.Butterfly_1, "Butterfly_1", 11f, 1, 1, true, "8:0.02,32:0.05,256:0.05", 0.9f, 0.97f, 6f, 15, 0f, 1f, 1, 0.7f, 0.075f, 0.5f, 1.4f, 1, "")
, new TableEnemyData(10015, 15, 1, "エフレキル", "Effrekill", 96, EnemyType.PA_Warrior_1, "PA_Warrior_1", 9f, 1, 1, true, "", 0.8f, 0.95f, 1f, 15, 0f, 1f, 1, 0.9f, 0.085f, 0.7f, 1.4f, 1, "0.02,30013")
, new TableEnemyData(10016, 16, 1, "レイスン", "Leyson", 32, EnemyType.Ghost_1, "Ghost_1", 10f, 1, 3, true, "", 0.3f, 0.95f, 1f, 15, 0f, 1f, 1, 0.9f, 0.04f, 0.4f, 1.4f, 1, "")
, new TableEnemyData(10017, 17, 1, "ピョンナー", "Stealer", 9, EnemyType.Rabbit_2, "Rabbit_2", 10f, 1, 1, true, "", 0.7f, 0.95f, 2f, 15, 0f, 1f, 1, 0.5f, 0.075f, 0.7f, 1.4f, 1, "0.06")
, new TableEnemyData(10018, 18, 1, "ゴースン", "Ghosun", 32, EnemyType.Ghost_2, "Ghost_2", 11f, 1, 1, true, "", 1f, 0.95f, 2f, 15, 0f, 1f, 1, 0.5f, 0.085f, 0.7f, 1.4f, 1, "0.075")
, new TableEnemyData(10019, 19, 1, "ゼリーン", "Zelen", 4, EnemyType.Slime_2, "EnemyDammyItem", 15f, 1, 1, true, "", 0.9f, 0.95f, 1f, 15, 0f, 1f, 1, 0.7f, 0.085f, 0.6f, 1.4f, 1, "")
, new TableEnemyData(10020, 20, 1, "ジーバック", "Geebag", 4, EnemyType.Slime_3, "Slime_3", 15f, 1, 1, true, "", 0.9f, 0.95f, 1f, 15, 0f, 1f, 1, 0.7f, 0.06f, 0.6f, 1.4f, 1, "0.4")
, new TableEnemyData(10021, 21, 1, "ブロバット", "Blobat", 33, EnemyType.Bat_2, "Bat_2", 9f, 1, 1, true, "", 1f, 0.92f, 1f, 15, 0f, 1f, 1, 0.35f, 0.085f, 0.5f, 1.4f, 1, "0.1")
, new TableEnemyData(10022, 22, 1, "ピョンジー", "Rapair", 9, EnemyType.Rabbit_3, "Rabbit_3", 10f, 1, 1, true, "", 0.7f, 0.95f, 2f, 15, 0f, 1f, 1, 0.5f, 0.075f, 0.7f, 1.4f, 1, "1.5")
, new TableEnemyData(10023, 23, 1, "アンガーベア", "Anger Bear", 9, EnemyType.TeddyBear_2, "TeddyBear_2", 10f, 1, 1, true, "", 1.1f, 0.92f, 4f, 15, 0f, 1f, 1, 0.85f, 0.1f, 0.4f, 1.5f, 1, "0.7")
, new TableEnemyData(10024, 24, 1, "ピョンガーキング", "Rabbiter King", 9, EnemyType.Rabbit_King, "Rabbit_King", 200f, 1, 1, true, "", 1f, 0.95f, 2f, 15, 0f, 1f, 5, 0.7f, 0.075f, 0.7f, 1.4f, 10000, "0.2")
, new TableEnemyData(10025, 25, 1, "プラランテ", "Pralante", 2, EnemyType.Plant_King, "Plant_King", 300f, 10, 1, false, "", 1.2f, 0.92f, 2f, 15, 0f, 1f, 8, 0.35f, 0.08f, 0.5f, 1.4f, 20000, "0.13")
, new TableEnemyData(10026, 26, 1, "ダークステラ", "Dark Stella", 12, EnemyType.DarkStella_1, "DarkStella_1", 300f, 1, 1, true, "", 1.1f, 0.92f, 2f, 15, 0f, 1f, 8, 0.85f, 0.08f, 0.5f, 1.4f, 20000, "0.13")
, new TableEnemyData(10027, 27, 1, "アラーマー", "Alarmer", 36, EnemyType.Alarmer_1, "Alarmer_1", 400f, 1, 1, false, "", 1.1f, 0.92f, 2f, 15, 0f, 1f, 8, 1f, 0.08f, 0.5f, 1.4f, 20000, "0.17")
, new TableEnemyData(10028, 28, 1, "ダークユニティ", "Dark Unity", 12, EnemyType.DarkUnity_1, "DarkUnity_1", 250f, 1, 1, true, "", 1.1f, 0.92f, 2f, 15, 0f, 1f, 8, 0.85f, 0.08f, 0.5f, 1.4f, 20000, "0.13")
, new TableEnemyData(10029, 29, 1, "ドラングウィン", "Drangwin", 9, EnemyType.Drangwin_1, "Drangwin_1", 500f, 1, 1, true, "", 0.9f, 0.92f, 2f, 15, 0f, 1f, 8, 0.85f, 0.075f, 0.5f, 1.4f, 50000, "0.1")
, new TableEnemyData(10030, 30, 1, "メガレックス", "Megarex", 9, EnemyType.Whale_King, "Whale_King", 350f, 1, 1, true, "", 1.2f, 0.95f, 1f, 15, 0f, 1f, 2, 0.9f, 0.105f, 0.3f, 1.5f, 1, "0.04")


                };
                return _table;
            }
        }
    }

    public static bool SetLevel(BaseEnemyCharacter unit,int level, ushort floor, float hpProb, float atkProb, float expProb, float startProbHp, float startProbAtk, float startProbExp)
    {
        TableEnemyData data = Array.Find(Table, i => i.ObjNo == unit.ObjNo);
        if(CommonFunction.IsNull(data) == true)
        {
            return false;
        }
        else
        {
            SetData(unit, data, floor, level, hpProb, atkProb, expProb, startProbHp, startProbAtk, startProbExp);
            return true;
        }
    }

    public static BaseEnemyCharacter GetEnemy(long objNo,ushort floor, float hpProb, float atkProb, float expProb, float startProbHp, float startProbAtk, float startProbExp)
    {

        TableEnemyData data = Array.Find(Table, i => i.ObjNo == objNo);
        BaseEnemyCharacter item = new BaseEnemyCharacter();
        item.Initialize();
        SetData(item, data, floor,data.Level, hpProb, atkProb, expProb, startProbHp, startProbAtk, startProbExp);

        switch (item.EType)
        {
            case EnemyType.Plant_1:
                //item.PosY = 3.18f;
                item.WEType = WeaponEffectType.Stone;
                break;
            case EnemyType.Plant_King:
                //item.PosY = 3.18f;
                item.WEType = WeaponEffectType.Stone;
                break;
            case EnemyType.PA_Drone_1:
                //item.PosY = 2.9f;
                item.WEType = WeaponEffectType.MachineGun;
                break;
            default:
                break;
        }
        return item;
    }
    private static void SetData(BaseEnemyCharacter item,TableEnemyData data, ushort floor,int level,float hpProb,float atkProb,float expProb,float startProbHp, float startProbAtk, float startProbExp)
    {
        item.ObjNo = data.ObjNo;
        item.UnitNo = data.UnitNo;
        if (GameStateInformation.IsEnglish == false)
        {
            item.DisplayName = data.DisplayName;
        }
        else
        {
            item.DisplayName = data.DisplayNameEn;
        }
        item.EType = data.EType;
        item.InstanceName = data.Instance;
        item.Level = level;
        item.Race = data.Race;
        item.CurrentHp = (data.Hp * level * startProbHp) + (data.MagHp * hpProb * floor);
        item.MaxHpDefault = item.CurrentHp;
        item.FiringRange = data.Range;
        item.DefaultActionTurn = data.TurnMove;
        item.ActionTurn = item.DefaultActionTurn;
        item.IsMove = data.IsMove;
        item.AddAbnormalProb = data.StateBad;
        item.BaseAttack = ((data.Attack * startProbAtk) + (data.MagAtk * atkProb * floor)) * level;
        item.Dexterity = data.Dex;
        item.BaseDefense = ((data.Defence * startProbAtk) + (data.MagDef * atkProb * floor)) * level;
        item.PowerValue = data.Power;
        item.Critical = data.Cri;
        item.CriticalDexterity = data.CriDex;
        item.HaveExperience = Mathf.CeilToInt(((data.Exp * startProbExp) + (data.MagExp * expProb * floor)) * level);
        item.CommonString = data.Common;
        item.CurrentExperience = 0;
        item.NextLevelExperience = 1;

        item.HaveScore = data.HaveScore;
    }

    private class TableEnemyData
    {
        public TableEnemyData(int objNo,
            ushort unitNo,
            int level,
            string displayName,
            string displayNameEn,
            int race,
            EnemyType etype,
            string instance,
            float hp,
            ushort range,
            int turnMove,
            bool isMove,
            string stateBad,
            float attack,
            float dex,
            float defence,
            ushort power,
            float criDex,
            float cri,
            int exp,
            float magHp,
            float magAtk,
            float magDef,
            float magExp,
            int haveScore,
            string common)
        {
            ObjNo = objNo;
            Level = level;
            UnitNo = unitNo;
            DisplayName = displayName;
            DisplayNameEn = displayNameEn;
            Race = race;
            EType = etype;
            Instance = instance;
            Hp = hp;
            Range = range;
            TurnMove = turnMove;
            IsMove = isMove;
            StateBad = stateBad;
            Attack = attack;
            Dex = dex;
            Defence = defence;
            Power = power;
            CriDex = criDex;
            Cri = cri;
            Exp = exp;
            MagHp = magHp;
            MagAtk = magAtk;
            MagDef = magDef;
            MagExp = magExp;
            HaveScore = haveScore;
            Common = common;
        }
        public int ObjNo;
        public ushort UnitNo;
        public int Level;
        public string DisplayName;
        public string DisplayNameEn;
        public int Race;
        public EnemyType EType;
        public string Instance;
        public float Hp;
        public ushort Range;
        public int TurnMove;
        public bool IsMove;
        public string StateBad;
        public float Attack;
        public float Dex;
        public float Defence;
        public ushort Power;
        public float CriDex;
        public float Cri;
        public int Exp;
        public float MagHp;
        public float MagAtk;
        public float MagDef;
        public float MagExp;
        public int HaveScore;
        public string Common;
    }
}
