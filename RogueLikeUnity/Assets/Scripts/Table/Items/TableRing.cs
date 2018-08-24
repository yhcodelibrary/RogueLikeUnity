using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class TableRing
{

    private static TableRingData[] _table;
    private static TableRingData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableRingData[]
                    {

                         new TableRingData(22001, "ふたつの指輪","Two Ring", RingType.TwoRing, 0.92f, 0, "手にした者は世界を制すると言われる指輪。つけると敵に認識されなくなるが、常に状態異常が付与される。", "People who have a ring are said to control the world. If equipped, it will not be recognized by the enemy, but status abnormality is always given.")
, new TableRingData(22002, "目星の指輪","Spot Hidden Ring", RingType.OneEyes, 0.92f, 0, "フロア内のアイテムがマップ上に表示されるようになる。一定確率で壊れる。", "Items on the floor will be displayed on the map. It breaks with a certain probability.")
, new TableRingData(22003, "聞き耳の指輪","Listen Ring", RingType.Listen, 0.92f, 0, "フロア内の敵がマップ上に表示されるようになる。一定確率で壊れる。", "Enemies in the floor will be displayed on the map. It breaks with a certain probability.")
, new TableRingData(22004, "オカルトの指輪","Occult Ring", RingType.Occult, 0.92f, 0, "フロア内のトラップがマップ上に表示されるようになる。一定確率で壊れる。", "Traps in the floor will be displayed on the map. It breaks with a certain probability.")
, new TableRingData(22005, "混乱耐性の指輪","Confusion Resistant Ring", RingType.AbnormalPrevent, 0.92f, 32, "混乱になるのを確実に防ぐ。", "To prevent confusion surely.")
, new TableRingData(22006, "毒耐性の指輪","Poison Resistant Ring", RingType.AbnormalPrevent, 0.92f, 6, "毒になるのを確実に防ぐ。", "To prevent poisoning surely.")
, new TableRingData(22007, "睡眠耐性の指輪","Sleep Resistant Ring", RingType.AbnormalPrevent, 0.92f, 8, "睡眠になるのを確実に防ぐ。", "To prevent sleeping surely.")
, new TableRingData(22008, "麻痺耐性の指輪","Paralyze Resistant Ring", RingType.AbnormalPrevent, 0.92f, 16, "麻痺になるのを確実に防ぐ。", "To prevent paralysis surely.")
, new TableRingData(22009, "スロー耐性の指輪","Slow Resistant Ring", RingType.AbnormalPrevent, 0.92f, 256, "スローになるのを確実に防ぐ。", "To prevent slow surely.")
, new TableRingData(22010, "かぜ耐性の指輪","Cold Resistant Ring", RingType.AbnormalPrevent, 0.92f, 512, "かぜになるのを確実に防ぐ。", "To prevent cold surely.")
, new TableRingData(22011, "肩こり耐性の指輪","Stiff Shoulder Resistant Ring", RingType.AbnormalPrevent, 0.92f, 1024, "肩こりになるのを確実に防ぐ。", "To prevent stiff shoulder surely.")
, new TableRingData(22012, "コソ泥の指輪","Thief's Ring", RingType.Thief, 0.92f, 0, "フロア移動時のアイテム出現率が上がるが、不名誉な称号がつく。一度失った名誉を取り戻すのは難しい。", "Although the item appearance rate at the time of moving the floor rises, you get a disgraceful nickname. It is difficult to regain the honor lost.")
, new TableRingData(22013, "赤鉢巻の指輪","Red Headband Ring", RingType.Ryu, 0.92f, 0, "フロア移動時の敵出現率が上がる。強いやつに会いに行く。", "The enemy appearance rate at the time of moving the floor rises. I'm gonna meet someone stronger than me.")
, new TableRingData(22014, "暗闇耐性の指輪","Dark Resistant Ring", RingType.AbnormalPrevent, 0.92f, 1, "暗闇になるのを確実に防ぐ。", "To prevent dark surely.")
, new TableRingData(22015, "壮健の指輪","Healthy Ring", RingType.Health, 0.92f, 0, "すべての状態異常を一定確率で防ぐ。", "To prevent all state abnormalities with constant probability.")
, new TableRingData(22016, "厄の指輪","Trouble Ring", RingType.EvilLuck, 0.92f, 0, "フロア移動時のトラップ出現率が上がる。召喚陣が、気持ち多く出てくる。なんてことはなかった。", "The trap appearance rate at the time of moving the floor rises. The magic square comes out to a many extent. There was nothing like that.")
, new TableRingData(22017, "トンネルの指輪","Tunnel Ring", RingType.Tunnel, 0.92f, 0, "物を投げたとき誰かに当たるまで、どこまでも飛んでいくようになる。壁？ねぇよそんなもん。", "When throwing things, it fly anywhere until it hits someone.Wall? Hell no!")
, new TableRingData(22018, "全耐性の指輪","Total Resistant Ring", RingType.AbnormalPrevent, 0.92f, 1855, "全状態異常を完全に防ぐ", "Completely prevents all state abnormality")
, new TableRingData(22019, "不運の指輪","Bad Luck Ring", RingType.Unlucky, 0.92f, 0, "マップを移動中、まれにものを落とすようになる。", "Sometimes you drop items while on the move.")

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

    public static RingBase GetItem(long objNo)
    {
        TableRingData data = Array.Find(Table, i => i.ObjNo == objNo);
        RingBase item = new RingBase();
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
        item.RType = data.RType;
        item.PreventAbnormal = data.State;
        item.ThrowDexterity = data.ThrowDexterity;
        return item;
    }

    private class TableRingData
    {
        public TableRingData(ushort objNo,
            string displayName,
            string displayNameEn,
            RingType rtype,
            float throwDexterity,
            int state,
            string description,
            string descriptionEn)
        {
            ObjNo = objNo;
            DisplayName = displayName;
            DisplayNameEn = displayNameEn;
            RType = rtype;
            ThrowDexterity = throwDexterity;
            State = state;
            Description = description;
            DescriptionEn = descriptionEn;
        }
        public ushort ObjNo;
        public string DisplayName;
        public string DisplayNameEn;
        public RingType RType;
        public float ThrowDexterity;
        public int State;

        public string Description;
        public string DescriptionEn;
    }
}
