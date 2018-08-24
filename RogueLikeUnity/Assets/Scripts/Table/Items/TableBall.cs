using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TableBall
{

    private static TableBallData[] _table;
    private static TableBallData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableBallData[]
                {
                    new TableBallData(25001, "ファイアボール", "Fire Ball", BallType.Fire, 0.99f, 3, 0.8f, 1.1f, 9, "火をまとったボール。投げると前方の相手を攻撃できる。", "A ball in fire. If you throw it, you can attack the enemies ahead.")
,new TableBallData(25002, "ジャイロボール", "Gyro Ball", BallType.Gyro, 0.99f, 1, 0.8f, 1.1f, 9, "すごい勢いのボール。その勢いは当たった相手を吹っ飛ばすほど。", "A strong momentum ball. That momentum is enough to blow away the opponent.")
,new TableBallData(25003, "チェンジボール", "Change Ball", BallType.Change, 0.99f, 1, 0.8f, 1.1f, 9, "当たった相手と場所を交換するボール。", "A ball that exchanges places with the opponent you hit.")
,new TableBallData(25004, "ピックオフボール", "Pickoff Ball", BallType.Pickoff, 0.99f, 1, 0.8f, 1.1f, 9, "当たった相手を混乱させるボール。", "A ball that makes the opponent to confusion.")
,new TableBallData(25005, "ナックルボール", "Knuckle Ball", BallType.Knuckle, 0.99f, 1, 0.8f, 1.05f, 9, "投げた瞬間ランダムで別のボールに変化するボール。", "A ball that turns into another ball randomly at the moment of throwing.")
,new TableBallData(25006, "トラップブレイカー", "Trap Breaker", BallType.Trap, 0.99f, 1, 0.8f, 1.05f, 9, "延長線上にあるトラップを破壊するボール。", "A ball that destroys a trap on an extension line.")
,new TableBallData(25007, "デコイボール", "Decoy Ball", BallType.Decoy, 0.99f, 1, 0.8f, 1.1f, 9, "当たった相手をデコイに変化させるボール。", "A ball that changes the opponent who hits it to a decoy.")
,new TableBallData(25008, "ビーンボール", "Bean Ball", BallType.Bean, 0.99f, 1, 0.8f, 1.1f, 9, "当たった相手を麻痺にするボール。", "A ball that makes the opponent to paralyzed.")
,new TableBallData(25009, "スローボール", "Slow Ball", BallType.Slow, 0.99f, 1, 0.8f, 1.1f, 9, "当たった相手のターン行動回数を一時的に減少させるボール。", "A ball that makes the opponent to Slow.")
,new TableBallData(25010, "手作りボール", "Handmade Ball", BallType.Handmaid, 0.99f, 1, 0.8f, 1.1f, 9, "自作のボール。当たるとダメージを与える。", "handmade  ball. If you hit an enemy, it deals damage.")
,new TableBallData(25011, "ファンブルボール", "Fumble Ball", BallType.Fumble, 0.99f, 1, 0.8f, 1.1f, 9, "当たった相手のレベルが1下がるボール。元が1ならそれ以上は下がらない。", "A ball that lowers the level of the opponent's opponent by 1. If the original level is 1 it will not go down.")
,new TableBallData(25012, "ウイニングボール", "Winning Ball", BallType.Winning, 0.99f, 1, 0.8f, 1.1f, 9, "当たった相手のレベルが1上がるボール。", "A ball whose level of opponent's opponent rises by 1.")
,new TableBallData(25013, "フォアボール", "Four Ball", BallType.Four, 0.99f, 2, 0.9f, 1.05f, 9, "同じ相手に4回当たると相手が倒れるボール。ボスには効かない。", "A ball whose opponent falls when you hit the same opponent four times. It does not work for the boss.")
,new TableBallData(25014, "エメリーボール", "Emery Ball", BallType.Emery, 0.99f, 1, 0.5f, 1.5f, 9, "当たった相手とその種族を現在のフロアから追放させるボール。ボスには効かない。", "A ball that expels the opponent and its tribe from the current floor. It does not work for the boss.")

                };
                return _table;
            }
        }
    }
    public static ushort[] GetObjNoList()
    {
        ushort[] datas = Array.ConvertAll(Table, i => i.ObjNo);
        return datas;
    }

    public static BallBase GetItem(long objNo)
    {
        TableBallData data = Array.Find(Table, i => i.ObjNo == objNo);
        BallBase item = new BallBase();
        item.Initialize(data.BallType);
        item.RestCount = (sbyte)CommonFunction.ConvergenceRandom(data.StartGap, data.Startprob, data.Con, data.MaxGap);
        item.ObjNo = data.ObjNo;
        if (GameStateInformation.IsEnglish == false)
        {
            item.DisplayName = data.DisplayName;
            item.Description = data.Description;
        }
        else
        {
            item.DisplayName = data.DisplayNameEn;
            item.Description = data.DescriptionEn;
        }
        item.ThrowDexterity = data.ThrowDexterity;
        return item;
    }

    private class TableBallData
    {
        public TableBallData(ushort objNo,
            string displayName,
            string displayNameEn,
            BallType ballType,
            float throwDexterity,
            int startGap,
            float startprob,
            float con,
            int maxGap,
            string description,
            string descriptionEn)
        {
            ObjNo = objNo;
            DisplayName = displayName;
            DisplayNameEn = displayNameEn;
            BallType = ballType;
            ThrowDexterity = throwDexterity;
            StartGap = startGap;
            Startprob = startprob;
            Con = con;
            MaxGap = maxGap;
            Description = description;
            DescriptionEn = descriptionEn;
        }
        public ushort ObjNo;
        public BallType BallType;
        public string DisplayName;
        public string DisplayNameEn;
        public float ThrowDexterity;
        public int StartGap;
        public float Startprob;
        public float Con;
        public int MaxGap;
        public string Description;
        public string DescriptionEn;
    }
}
