using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class TableMelody
{
    private static TableMelodyData[] _table;
    private static TableMelodyData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableMelodyData[]
                {

                    new TableMelodyData(26001, "放電の旋律", "Electric Melody", MelodyType.Electric, 0.92f, "奏でると電撃が走る。部屋の中にいる相手にダメージを与える。","Electric shocks run when playing. Damage the enemies in the same room.")
, new TableMelodyData(26002, "まどろみの旋律", "Doze Off Melody", MelodyType.Sleep, 0.92f, "眠りを誘う旋律。部屋の中にいる相手を眠らせる。","A melody which invites sleep. Sleep the enemies in the same room.")
, new TableMelodyData(26003, "狂乱の旋律", "Frenzy Melody", MelodyType.Confusion, 0.92f, "同じ部屋にいる相手を混乱状態にする旋律。","A melody that puts enemies in the same room into a state of confusion.")
, new TableMelodyData(26004, "薄ら日の旋律", "Twilight Melody", MelodyType.Light, 0.92f, "今いるフロア内のマップ、敵、アイテムの場所が聴こえてくる不思議な旋律。","A mysterious melody where you can hear the maps, enemies, and items in the floor you are on now.")
, new TableMelodyData(26005, "無秩序の旋律", "Disorderly Melody", MelodyType.Anarchy, 0.92f, "同じ部屋にいる敵がすべてデコイになる旋律。","A melody where all enemies in the same room become decoys.")
, new TableMelodyData(26006, "角笛の旋律", "Horn Melody", MelodyType.Horn, 0.92f, "自分の周辺に敵を呼び出す旋律。闘争本能を刺激させるらしい。","A melody that calls new enemies around yourself. It seems to stimulate fighting instinct.")
, new TableMelodyData(26007, "忘却の旋律", "Oblivion Melody", MelodyType.Forget, 0.92f, "現在いるフロアのマップを忘れる旋律。","A melody that forgets the map of the current floor.")
, new TableMelodyData(26008, "断捨離の旋律", "Decluttering Melody", MelodyType.ThrowAway, 0.92f, "現在いるフロアでアイテムが拾えなくなる旋律。","A melody that makes it impossible to pick up items on the current floor.")


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

    public static MelodyBase GetItem(long objNo)
    {
        TableMelodyData data = Array.Find(Table, i => i.ObjNo == objNo);
        MelodyBase item = new MelodyBase();
        item.Initialize();
        item.ObjNo = data.ObjNo;
        item.MType = data.Mtype;
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

    private class TableMelodyData
    {
        public TableMelodyData(ushort objNo,
            string displayName,
            string displayNameEn,
            MelodyType mtype,
            float throwDexterity,
            string description,
            string descriptionEn)
        {
            ObjNo = objNo;
            DisplayName = displayName;
            DisplayNameEn = displayNameEn;
            Mtype = mtype;
            ThrowDexterity = throwDexterity;
            Description = description;
            DescriptionEn = descriptionEn;
        }
        public ushort ObjNo;
        public string DisplayName;
        public string DisplayNameEn;
        public MelodyType Mtype;
        public float ThrowDexterity;
        public string Description;
        public string DescriptionEn;
    }

}