using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableCandy
{
    private static TableCandyData[] _table;
    private static TableCandyData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableCandyData[] {

                     new TableCandyData(24001, "回復の飴", "Recovery Candy", CandyType.Common, 0.92f, 30, 0, 0, 0, 0, 0, "HPを小回復する。", "Recover HP a little.")
, new TableCandyData(24002, "大回復の飴", "Big Recovery Candy", CandyType.Common, 0.92f, 100, 0, 0, 0, 0, 0, "HPを大回復する。", "Recover HP a lot.")
, new TableCandyData(24003, "毒消し飴", "Antidote Candy", CandyType.Common, 0.92f, 0, 0, 0, 0, 0, 6, "毒、猛毒状態を回復する。", "Cure poison and deadly poison.")
, new TableCandyData(24004, "無敵の飴", "Invincible Candy", CandyType.Common, 0.92f, 0, 0, 0, 0, 0, 6, "", "")
, new TableCandyData(24005, "のど飴", "Throat Candy", CandyType.Common, 0.92f, 0, 0, 0, 0, 0, 512, "のどが痛くなったらなめよう。かぜを治す。", "Cure cold. Let's lick if your throat hurts.")
, new TableCandyData(24006, "漢方の飴", "Medicine Candy", CandyType.Common, 0.92f, 0, 0, 0, 0, 0, 1024, "体にいい飴。肩こりを治す。", "Cure stiff shoulder. Good healthy candy.")
, new TableCandyData(24007, "万能飴", "Panacea Candy", CandyType.Common, 0.92f, 0, 0, 0, 0, 0, 2047, "すべての状態異常を治す。", "Cure all abnormal states.")
, new TableCandyData(24008, "ブルーベリー飴", "Blueberry Candy", CandyType.Common, 0.92f, 0, 0, 0, 0, 0, 1, "なめると目がよくなる気がする飴。暗闇状態を治す。", "Cure dark. feels like eyes get better as you lick.")
, new TableCandyData(24009, "Unknown", "", CandyType.Common, 0.92f, 0, 0, 0, 0, 0, 1, "一定時間移動速度が上がる。スロー状態なら回復する。", "")
, new TableCandyData(24010, "ラベンダー飴", "Lavender Candy", CandyType.Common, 0.92f, 0, 0, 0, 0, 0, 32, "なめると落ち着く飴。混乱状態を治す。", "Cure confusion. Candy to settle down when licking.")
, new TableCandyData(24011, "エナジー飴", "Energy Candy", CandyType.Energy, 0.92f, 0, 0, 0, 0, 0, 0, "なめると力が湧いてくる飴。力が減っていると回復し、減っていないと最大値が増える。", "Candy whose power lingers when licking. It recovers when the power is decreasing, the maximum value increases unless it is decreasing.")
, new TableCandyData(24012, "手作り飴", "Handmade Candy", CandyType.Handmaid, 0.92f, 15, 0, 0, 0, 0, 0, "自分で作った手作りの飴。", "Handmade candy made by myself.")
, new TableCandyData(24013, "ハエのたかる飴", "Rotten Candy", CandyType.Common, 0.92f, 0, 0, 0, 0, 4, 0, "ハエがたかっている飴。なめるのは危険である。", "Rotten candy. It is dangerous to lick.")
, new TableCandyData(24014, "爆発飴", "Explosive Candy", CandyType.Bomb, 0.92f, 0, 0, 0, 0, 0, 0, "なめると突如爆発する飴。自分と周りの相手にダメージを与える。", "A suddenly explosive candy when licking. Damage to yourself and others around you.")
, new TableCandyData(24015, "大きい飴ちゃん", "Big Candy", CandyType.Common, 0.92f, 0, 0, 5, 0, 0, 0, "大きい飴、特に効果はない。おなかが少し膨れる。", "Big candy, not particularly effective. Hunger will be slightly relaxed")
, new TableCandyData(24016, "天国の飴", "Heaven's Candy", CandyType.Common, 0.92f, 0, 0, 0, 0, 2048, 0, "なめると行動速度が上昇する飴。", "Candy to be accelerated when licking")
, new TableCandyData(24017, "スタミナの飴", "Stamina Candy", CandyType.Garlic, 0.92f, 0, 0, 0, 0, 0, 0, "なめるとHPの最大値が上がる、健康に良いニンニク味の飴。", "When licking the HP's maximum value goes up. healthy garlic taste candy.")

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
    public static CandyBase GetItem(long objNo)
    {
        TableCandyData data = Array.Find(Table, i => i.ObjNo == objNo);
        CandyBase item = new CandyBase();
        item.Initialize();
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
        item.CType = data.CType;
        item.ThrowDexterity = data.ThrowDexterity;
        item.HpRecoverPoint = data.HpRecover;
        item.SatRecoverPoint = data.SatRecover;
        item.StatusBadTarget = data.StateBad;
        item.StatusRecoverTarget = data.StateRecover;
        return item;
    }

    private class TableCandyData
    {
        public TableCandyData(ushort objNo,
            string displayName,
            string displayNameEn,
            CandyType ctype,
            float throwDexterity,
            int hpRecover,
            int hpBad,
            int satRecover,
            int satBad,
            int stateBad,
            int stateRecover,
            string description,
            string descriptionEn)
        {
            ObjNo = objNo;
            DisplayName = displayName;
            DisplayNameEn = displayNameEn;
            CType = ctype;
            ThrowDexterity = throwDexterity;
            HpRecover = hpRecover;
            HpBad = hpBad;
            SatRecover = satRecover;
            SatBad = satBad;
            StateBad = stateBad;
            StateRecover = stateRecover;
            Description = description;
            DescriptionEn = descriptionEn;
        }
        public ushort ObjNo;
        public string DisplayName;
        public string DisplayNameEn;
        public CandyType CType ;
        public float ThrowDexterity;
        public int HpRecover;
        public int HpBad;
        public int SatRecover;
        public int SatBad;
        public int StateBad;
        public int StateRecover;
        public string Description;
        public string DescriptionEn;
    }
}
