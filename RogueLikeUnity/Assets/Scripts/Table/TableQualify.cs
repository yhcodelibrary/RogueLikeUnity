using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableQualify
{

    private static TableQualifyData[] _table;
    private static TableQualifyData[] table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }

            _table = new TableQualifyData[]
                {

                    new TableQualifyData(35001, 1, "雑魚の", "Indecisive ")
,new TableQualifyData(35002, 1, "汚染の", "Eccentric ")
,new TableQualifyData(35003, 1, "不浄な", "Inflexible ")
,new TableQualifyData(35004, 1, "汚濁の", "Pollution  ")
,new TableQualifyData(35005, 1, "劣悪な", "Obnoxious ")
,new TableQualifyData(35006, 1, "あざとい", "Conceited ")
,new TableQualifyData(35007, 1, "卑怯の", "Vain ")
,new TableQualifyData(35008, 1, "不心得な", "Gullible ")
,new TableQualifyData(35009, 1, "汚れた", "Timid ")
,new TableQualifyData(35010, 1, "愚かな", "Stupid ")
,new TableQualifyData(35011, 2, "自由な", "Noisy ")
,new TableQualifyData(35012, 2, "曲がった", "Devious ")
,new TableQualifyData(35013, 2, "バッドな", "Bad ")
,new TableQualifyData(35014, 2, "汚い", "Dirty ")
,new TableQualifyData(35015, 2, "優しい", "Selfish ")
,new TableQualifyData(35016, 2, "せこい", "Stingy ")
,new TableQualifyData(35017, 2, "未熟な", "Clumsy ")
,new TableQualifyData(35018, 2, "軽率な", "Careless ")
,new TableQualifyData(35019, 2, "ボロい", "Lazy ")
,new TableQualifyData(35020, 3, "気難しい", "Egotistical ")
,new TableQualifyData(35021, 3, "気難しい", "Stubborn ")
,new TableQualifyData(35022, 3, "内気な", "Inhibited ")
,new TableQualifyData(35023, 3, "煩い", "Loud ")
,new TableQualifyData(35024, 3, "ナイーブ", "Naive ")
,new TableQualifyData(35025, 3, "静かな", "Immature ")
,new TableQualifyData(35026, 3, "従順な", "Obedient ")
,new TableQualifyData(35027, 3, "控えめな", "Reserved ")
,new TableQualifyData(35028, 3, "嫌みな", "Judgmental ")
,new TableQualifyData(35029, 4, "皮肉な", "Sarcastic ")
,new TableQualifyData(35030, 4, "短気な", "Short Tempered ")
,new TableQualifyData(35031, 4, "芸術的な", "Artistic ")
,new TableQualifyData(35032, 4, "滑稽な", "Pathetic ")
,new TableQualifyData(35033, 4, "無礼な", "Rude ")
,new TableQualifyData(35034, 4, "無情な", "Prejudiced ")
,new TableQualifyData(35035, 4, "哀れな", "Passive ")
,new TableQualifyData(35036, 4, "楽しい", "Fun-Loving ")
,new TableQualifyData(35037, 4, "内気な", "Shy ")
,new TableQualifyData(35038, 4, "愚かな", "Simple-Minded ")
,new TableQualifyData(35039, 5, "単純な", "Gullible ")
,new TableQualifyData(35040, 5, "思慮深い", "Thoughtful ")
,new TableQualifyData(35041, 5, "慈悲深い", "Compassionate ")
,new TableQualifyData(35042, 5, "注意深い", "Attentive ")
,new TableQualifyData(35043, 5, "楽観的な", "Optimistic ")
,new TableQualifyData(35044, 5, "社交的な", "Outgoing ")
,new TableQualifyData(35045, 5, "忍耐強い", "Patient ")
,new TableQualifyData(35046, 5, "辛抱強い", "Polite ")
,new TableQualifyData(35047, 5, "慎重な", "Careful ")
,new TableQualifyData(35048, 5, "野心の", "Adventurous ")
,new TableQualifyData(35049, 6, "攻撃的な", "Aggressive ")
,new TableQualifyData(35050, 6, "朗らかな", "Friendly ")
,new TableQualifyData(35051, 6, "陽気な", "Cheerful ")
,new TableQualifyData(35052, 6, "知的な", "Intelligent ")
,new TableQualifyData(35053, 6, "大胆な", "Fearless ")
,new TableQualifyData(35054, 6, "愛情深い", "Affectionate ")
,new TableQualifyData(35055, 6, "気難しい", "Grumpy ")
,new TableQualifyData(35056, 6, "曲がった", "Impulsive ")
,new TableQualifyData(35057, 6, "強い", "Tough ")
,new TableQualifyData(35058, 6, "傲慢な", "Arrogant ")
,new TableQualifyData(35059, 7, "冷酷な", "Ruthless ")
,new TableQualifyData(35060, 7, "大望の", "Ambitious ")
,new TableQualifyData(35061, 7, "快活な", "Vivacious ")
,new TableQualifyData(35062, 7, "嫉妬の", "Jealous ")
,new TableQualifyData(35063, 7, "強欲な", "Greedy ")
,new TableQualifyData(35064, 7, "無慈悲な", "Ruthless ")
,new TableQualifyData(35065, 7, "単純な", "Simple ")
,new TableQualifyData(35066, 7, "気難しい", "Bad Tempered ")
,new TableQualifyData(35067, 7, "熱意の", "Enthusiastic ")
,new TableQualifyData(35068, 8, "聡明な", "Gentle ")
,new TableQualifyData(35069, 8, "自立した", "Independent ")
,new TableQualifyData(35070, 8, "忠実な", "Loyal ")
,new TableQualifyData(35071, 8, "義理堅い", "Helpful ")
,new TableQualifyData(35072, 8, "成熟した", "Mature ")
,new TableQualifyData(35073, 8, "几帳面な", "Neat ")
,new TableQualifyData(35074, 8, "勇敢な", "Courageous ")
,new TableQualifyData(35075, 8, "頼れる", "Dependable ")
,new TableQualifyData(35076, 8, "勤勉な", "Diligent ")
,new TableQualifyData(35077, 8, "カリスマ", "Charismatic ")
,new TableQualifyData(35078, 8, "英雄の", "Hero ")



                };


            return _table;
        }
    }

    public static QualifyInformation GetValue(ushort objno)
    {
        TableQualifyData tar = Array.Find(table, i => i.ObjNo == objno);
        
        QualifyInformation r = new QualifyInformation();

        if (CommonFunction.IsNull(tar) == true)
        {
            return r;
        }
        AttackValue(r, tar);

        return r;
    }

    public static QualifyInformation GetRandomName(int level)
    {
        TableQualifyData[] targets = Array.FindAll(table,i => i.Level == level);
        TableQualifyData tar = targets[UnityEngine.Random.Range(0, targets.Length)];
        QualifyInformation r = new QualifyInformation();

        AttackValue(r, tar);

        return r;
    }

    private static void AttackValue(QualifyInformation r, TableQualifyData tar)
    {
        r.ObjNo = tar.ObjNo;

        if (GameStateInformation.IsEnglish == false)
        {
            r.Name = tar.Name;
        }
        else
        {
            r.Name = tar.NameEn;
        }
    }

    private class TableQualifyData
    {
        public TableQualifyData(
            ushort objNo,
            int level,
            string name,
            string nameEn)
        {
            ObjNo = objNo;
            Level = level;
            Name = name;
            NameEn = nameEn;
        }
        public ushort ObjNo;
        public int Level;
        public string Name;
        public string NameEn;
    }
}