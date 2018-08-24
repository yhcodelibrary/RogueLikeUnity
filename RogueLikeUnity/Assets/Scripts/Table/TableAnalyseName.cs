using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Table
{
    public class TableAnalyseName
    {
        private static TableAnalyseNameData[] _table;
        private static TableAnalyseNameData[] Table
        {
            get
            {
                if (_table != null)
                {
                    return _table;
                }
                else
                {
                    _table = new TableAnalyseNameData[]
                    {


                        new TableAnalyseNameData(101, ItemType.Ball, 25501, "薄水色のボール", "White Ball")
, new TableAnalyseNameData(102, ItemType.Ball, 25502, "柿色のボール", "Beige Ball")
, new TableAnalyseNameData(103, ItemType.Ball, 25503, "鶯色のボール", "Magenta Ball")
, new TableAnalyseNameData(104, ItemType.Ball, 25504, "緋色のボール", "Maroon Ball")
, new TableAnalyseNameData(105, ItemType.Ball, 25505, "群青色のボール", "Mauve Ball")
, new TableAnalyseNameData(106, ItemType.Ball, 25506, "玉虫色のボール", "Lilac Ball")
, new TableAnalyseNameData(107, ItemType.Ball, 25507, "牡丹色のボール", "Lavender Ball")
, new TableAnalyseNameData(108, ItemType.Ball, 25508, "山吹色のボール", "Leghorn Ball")
, new TableAnalyseNameData(109, ItemType.Ball, 25509, "若芽色のボール", "Red Ball")
, new TableAnalyseNameData(110, ItemType.Ball, 25510, "鈍色のボール", "Ivory Ball")
, new TableAnalyseNameData(111, ItemType.Ball, 25511, "浅黄色のボール", "Apricot Ball")
, new TableAnalyseNameData(112, ItemType.Ball, 25512, "亜麻色のボール", "Amber Ball")
, new TableAnalyseNameData(113, ItemType.Ball, 25513, "茜色のボール", "Yellow Ball")
, new TableAnalyseNameData(114, ItemType.Ball, 25514, "藍色のボール", "Wistaria Ball")
, new TableAnalyseNameData(115, ItemType.Ball, 25515, "紺色のボール", "Orchid Ball")
, new TableAnalyseNameData(116, ItemType.Ball, 25516, "象牙色のボール", "Olive Ball")
, new TableAnalyseNameData(117, ItemType.Ball, 25517, "雀色のボール", "Orange Ball")
, new TableAnalyseNameData(118, ItemType.Ball, 25518, "蘇芳色のボール", "Khaki Ball")
, new TableAnalyseNameData(119, ItemType.Ball, 25519, "石竹色のボール", "Carmine Ball")
, new TableAnalyseNameData(120, ItemType.Ball, 25520, "鴇色のボール", "Green Ball")
, new TableAnalyseNameData(121, ItemType.Ball, 25521, "どどめ色のボール", "Grey Ball")
, new TableAnalyseNameData(122, ItemType.Ball, 25522, "鼠色のボール", "Cork Ball")
, new TableAnalyseNameData(123, ItemType.Ball, 25523, "茶色のボール", "Sepia Ball")
, new TableAnalyseNameData(124, ItemType.Ball, 25524, "狐色のボール", "Tan Ball")
, new TableAnalyseNameData(125, ItemType.Ball, 25525, "青竹色のボール", "Terracotta Ball")
, new TableAnalyseNameData(126, ItemType.Melody, 26501, "テクノの旋律", "Techno Melody")
, new TableAnalyseNameData(127, ItemType.Melody, 26502, "ポップの旋律", "Pop Melody")
, new TableAnalyseNameData(128, ItemType.Melody, 26503, "メタルの旋律", "Metal Melody")
, new TableAnalyseNameData(129, ItemType.Melody, 26504, "ロックの旋律", "Rock Melody")
, new TableAnalyseNameData(130, ItemType.Melody, 26505, "ワルツの旋律", "Waltz Melody")
, new TableAnalyseNameData(131, ItemType.Melody, 26506, "アカペラの旋律", "A cappella Melody")
, new TableAnalyseNameData(132, ItemType.Melody, 26507, "雅楽の旋律", "Gagaku Melody")
, new TableAnalyseNameData(133, ItemType.Melody, 26508, "クラシックの旋律", "Classic Melody")
, new TableAnalyseNameData(134, ItemType.Melody, 26509, "ケルトの旋律", "Celtic Melody")
, new TableAnalyseNameData(135, ItemType.Melody, 26510, "サルサの旋律", "Salsa Melody")
, new TableAnalyseNameData(136, ItemType.Melody, 26511, "タンゴの旋律", "Tango Melody")
, new TableAnalyseNameData(137, ItemType.Melody, 26512, "バラードの旋律", "Bard's melody")
, new TableAnalyseNameData(138, ItemType.Melody, 26513, "ボレロの旋律", "Bolero Melody")
, new TableAnalyseNameData(139, ItemType.Candy, 24501, "しょっぱい飴", "Salty Candy")
, new TableAnalyseNameData(140, ItemType.Candy, 24502, "塩辛い飴", "Salty Candy")
, new TableAnalyseNameData(141, ItemType.Candy, 24503, "甘辛い飴", "Sweet Candy")
, new TableAnalyseNameData(142, ItemType.Candy, 24504, "甘酸っぱい飴", "Sweet Sour Candy")
, new TableAnalyseNameData(143, ItemType.Candy, 24505, "あっさり飴", "Soft Candy")
, new TableAnalyseNameData(144, ItemType.Candy, 24506, "甘い飴", "Sweet Candy")
, new TableAnalyseNameData(145, ItemType.Candy, 24507, "酸っぱい飴", "Sour Candy")
, new TableAnalyseNameData(146, ItemType.Candy, 24508, "苦い飴", "Bitter Candy")
, new TableAnalyseNameData(147, ItemType.Candy, 24509, "おいしい飴", "Delicious Candy")
, new TableAnalyseNameData(148, ItemType.Candy, 24510, "薄味の飴", "Light taste")
, new TableAnalyseNameData(149, ItemType.Candy, 24511, "まろやかな飴", "Mild Candy")
, new TableAnalyseNameData(150, ItemType.Candy, 24512, "辛い飴", "Spicy Candy")
, new TableAnalyseNameData(151, ItemType.Candy, 24513, "脂っこい飴", "Greasy Candy")
, new TableAnalyseNameData(152, ItemType.Candy, 24514, "ほろ苦い飴", "Bittersweet Candy")
, new TableAnalyseNameData(153, ItemType.Candy, 24515, "まずい飴", "Bad Candy")
, new TableAnalyseNameData(154, ItemType.Candy, 24516, "焦げ臭い飴", "Burning stinky Candy")
, new TableAnalyseNameData(155, ItemType.Candy, 24517, "酒臭い飴", "Alcohol Smell Candy")
, new TableAnalyseNameData(156, ItemType.Candy, 24518, "磯臭い飴", "Odysseous Candy")
, new TableAnalyseNameData(157, ItemType.Candy, 24519, "香ばしい飴", "Savory Candy")
, new TableAnalyseNameData(158, ItemType.Bag, 28501, "古びたかばん", "Old Bag")
, new TableAnalyseNameData(159, ItemType.Bag, 28502, "新品かばん", "Brand New Bag")
, new TableAnalyseNameData(160, ItemType.Bag, 28503, "中古のかばん", "Second Hand Bag")
, new TableAnalyseNameData(161, ItemType.Bag, 28504, "軽いかばん", "Light Bag")
, new TableAnalyseNameData(162, ItemType.Bag, 28505, "重たいかばん", "Heavy Bag")
, new TableAnalyseNameData(163, ItemType.Bag, 28506, "小ぶりのかばん", "Small Bag")
, new TableAnalyseNameData(164, ItemType.Bag, 28507, "大きいかばん", "Large Bag")
, new TableAnalyseNameData(165, ItemType.Bag, 28508, "汚れたかばん", "Dirty Bag")
, new TableAnalyseNameData(166, ItemType.Bag, 28509, "綺麗なかばん", "Clear Bag")
, new TableAnalyseNameData(167, ItemType.Ring, 22501, "火山岩の指輪", "Volcanic Rock Ring")
, new TableAnalyseNameData(168, ItemType.Ring, 22502, "流紋岩の指輪", "Rhythmic Ring")
, new TableAnalyseNameData(169, ItemType.Ring, 22503, "粗面岩の指輪", "Rough Rock Ring")
, new TableAnalyseNameData(170, ItemType.Ring, 22504, "玄武岩の指輪", "Basalt Ring")
, new TableAnalyseNameData(171, ItemType.Ring, 22505, "斑岩の指輪", "Porphyry Ring ")
, new TableAnalyseNameData(172, ItemType.Ring, 22506, "珪長岩の指輪", "Silicone rock ring")
, new TableAnalyseNameData(173, ItemType.Ring, 22507, "花崗岩の指輪", "Granite Ring")
, new TableAnalyseNameData(174, ItemType.Ring, 22508, "閃緑岩の指輪", "Diorite Ring")
, new TableAnalyseNameData(175, ItemType.Ring, 22509, "かんらん岩の指輪", "Irrigation Rock Ring")
, new TableAnalyseNameData(176, ItemType.Ring, 22510, "蛇紋岩の指輪", "Serpentine Rock Ring")
, new TableAnalyseNameData(177, ItemType.Ring, 22511, "角礫岩の指輪", "The Conglomerate Ring")
, new TableAnalyseNameData(178, ItemType.Ring, 22512, "砂岩の指輪", "Sandstone Ring")
, new TableAnalyseNameData(179, ItemType.Ring, 22513, "泥岩の指輪", "Mudstone Ring")
, new TableAnalyseNameData(180, ItemType.Ring, 22514, "凝灰岩の指輪", "Tuff Ring")
, new TableAnalyseNameData(181, ItemType.Ring, 22515, "石灰岩の指輪", "Limestone Ring")
, new TableAnalyseNameData(182, ItemType.Ring, 22516, "石炭の指輪", "Coal Ring")
, new TableAnalyseNameData(183, ItemType.Ring, 22517, "琥珀の指輪", "Amber Ring")
, new TableAnalyseNameData(184, ItemType.Ring, 22518, "岩塩の指輪", "salt Rock Ring")
, new TableAnalyseNameData(185, ItemType.Ring, 22519, "珪岩の指輪", "Quartz Ring")
, new TableAnalyseNameData(186, ItemType.Ring, 22520, "千枚岩の指輪", "Thousands Rock Ring")
, new TableAnalyseNameData(187, ItemType.Ring, 22521, "片麻岩の指輪", "Gem Rock Ring")
, new TableAnalyseNameData(188, ItemType.Ring, 22522, "圧砕岩の指輪", "Crush Rock Ring")




                    };
                    return _table;
                }
            }
        }
        //private static Dictionary<ItemType, Dictionary<ushort, string>> MemoryStack = new Dictionary<ItemType, Dictionary<ushort, string>>(new ItemTypeComparer());

        private static Dictionary<ushort, string> MemoryAll;
        private static Dictionary<ushort, ushort> MemorySortAll;

        //public static Dictionary<ushort, string> GetAllTypeValue(ItemType type)
        //{
        //    //if (MemoryStack.ContainsKey(type) == true)
        //    //{
        //    //    return MemoryStack[type];
        //    //}
        //    Dictionary<ushort, string> datas = Array.FindAll(Table, i => i.IType == type).ToDictionary(i => i.ObjNo, i => i.DisplayMame);
        //    return datas;
        //}
        public static List<ushort> GetAllTypeValue(ItemType type)
        {
            List<ushort> datas = Array.FindAll(Table, i => i.IType == type).Select(i=>i.ObjNo).ToList();
            return datas;
        }
        public static Dictionary<ushort, string> GetAllValue()
        {
            if (CommonFunction.IsNull(MemoryAll) == false)
            {
                return MemoryAll;
            }

            if (GameStateInformation.IsEnglish == false)
            {
                MemoryAll = Table.ToDictionary(i => i.ObjNo, i => i.DisplayMame);
            }
            else
            {
                MemoryAll = Table.ToDictionary(i => i.ObjNo, i => i.DisplayMameEn);
            }

            return MemoryAll;
        }

        public static string GetName(ushort objNo)
        {
            if(CommonFunction.IsNull( MemoryAll) == false)
            {
                return MemoryAll[objNo];
            }
            if (GameStateInformation.IsEnglish == false)
            {
                MemoryAll = Table.ToDictionary(i => i.ObjNo, i => i.DisplayMame);
            }
            else
            {
                MemoryAll = Table.ToDictionary(i => i.ObjNo, i => i.DisplayMameEn);
            }

            return MemoryAll[objNo];
        }

        public static ushort GetSort(ushort objNo)
        {
            if (CommonFunction.IsNull(MemorySortAll) == false)
            {
                return MemorySortAll[objNo];
            }
            MemorySortAll = Table.ToDictionary(i => i.ObjNo, i => i.Sort);

            return MemorySortAll[objNo];
        }

        private class TableAnalyseNameData : IEquatable<TableAnalyseNameData>
        {
            public TableAnalyseNameData(ushort objNo,
                ItemType itype,
                ushort sott,
                string name,
                string nameEn)
            {
                ObjNo = objNo;
                IType = itype;
                Sort = sott;
                DisplayMame = name;
                DisplayMameEn = nameEn;
            }
            public ushort ObjNo;
            public ItemType IType;
            public ushort Sort;
            public string DisplayMame;
            public string DisplayMameEn;

            public bool Equals(TableAnalyseNameData other)
            {
                return this.ObjNo == other.ObjNo;
            }
            public override bool Equals(object obj)
            {
                //objがnullか、型が違うときは、等価でない
                if (obj == null || this.GetType() != obj.GetType())
                {
                    return false;
                }

                return this.ObjNo == ((TableAnalyseNameData)obj).ObjNo;
            }

            public override int GetHashCode()
            {
                return ObjNo;
            }
        }
    }
}
