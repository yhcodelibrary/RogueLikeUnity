using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableShield
{
    private static TableShieldData[] _table;
    private static TableShieldData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableShieldData[]
                {

                      new TableShieldData(21001, "鍋のふた", "Pot Lid", 1, ShieldAppearanceType.Podlit, 0.92f, 6, 0, 0.5f, 3f, 1, 0.4f, 3f, 0, 0.1f, 2f, "鍋に使われるふた。伝説の合金で作られているらしい。", "A lid used for a pot. It seems that it is made of a legendary alloy.")
, new TableShieldData(21002, "木の盾", "Wood Shield", 1, ShieldAppearanceType.Wood, 0.92f, 8, 0, 0.5f, 3f, 1, 0.4f, 5f, 0, 0.1f, 2f, "普通サイズの木の盾。", "A shield of an ordinary size wood.")
, new TableShieldData(21003, "ハリボテの盾", "Papier Shield", 1, ShieldAppearanceType.Paper, 0.92f, 5, 0, 0.5f, 2.5f, 1, 0.4f, 5f, 0, 0.1f, 2f, "演劇の小道具として使われた盾。見た目は立派だが実用性に乏しい。", "Shield used as a prop of the theater. Although it looks fine, it is not practical.")
, new TableShieldData(21004, "騎士の盾", "Knight's Shield", 1, ShieldAppearanceType.Knight, 0.92f, 15, 0, 0.3f, 5f, 1, 0.4f, 5f, 0, 0.1f, 2f, "どこかの国の騎士が使っていた盾。軽さと丈夫さを兼ねそろえた万能盾。", "The shield that the knight of some country used. A versatile shield that combines lightness and durability.")
, new TableShieldData(21005, "帝国の盾", "Empire's Shield", 1, ShieldAppearanceType.Empire, 0.92f, 13, 0, 0.5f, 3f, 1, 0.4f, 5f, 0, 0.5f, 2f, "かつて世界を支配した帝国で標準採用されていた盾。", "A shield that was regarded as standard equipment in the empire which once dominated the world.")
, new TableShieldData(21006, "隕鉄の盾", "Meteoric Shield", 1, ShieldAppearanceType.Stars, 0.92f, 30, 0, 0.5f, 2.5f, 1, 0.4f, 3f, 0, 0.9f, 1.2f, "遥か昔に落ちた、隕石によってもたらされた素材から作られた盾。投げて敵にぶつかっても謎の物理法則により、一定確率で手元に戻ってくる。", "A shield made from the material brought by the meteorite that fell long ago. Even if you throw this and hit an enemy, it comes back to hand with a certain probability by mystery physical law.")
, new TableShieldData(21007, "鍋のふた", "Pot Lid", 2, ShieldAppearanceType.Podlit, 0.92f, 6, 0, 0.8f, 3f, 1, 0.4f, 2.5f, 0, 0.1f, 2f, "鍋に使われるふた。伝説の合金で作られているらしい。", "A lid used for a pot. It seems that it is made of a legendary alloy.")
, new TableShieldData(21008, "木の盾", "Wood Shield", 2, ShieldAppearanceType.Wood, 0.92f, 8, 0, 0.8f, 2.5f, 1, 0.4f, 4f, 0, 0.1f, 2f, "普通サイズの木の盾。", "A shield of an ordinary size wood.")
, new TableShieldData(21009, "ハリボテの盾", "Papier Shield", 2, ShieldAppearanceType.Paper, 0.92f, 5, 0, 0.8f, 2f, 1, 0.4f, 4f, 0, 0.1f, 2f, "演劇の小道具として使われた盾。見た目は立派だが実用性に乏しい。", "Shield used as a prop of the theater. Although it looks fine, it is not practical.")
, new TableShieldData(21010, "騎士の盾", "Knight's Shield", 2, ShieldAppearanceType.Knight, 0.92f, 15, 0, 0.5f, 4f, 1, 0.4f, 4f, 0, 0.1f, 2f, "どこかの国の騎士が使っていた盾。軽さと丈夫さを兼ねそろえた万能盾。", "The shield that the knight of some country used. A versatile shield that combines lightness and durability.")
, new TableShieldData(21011, "帝国の盾", "Empire's Shield", 2, ShieldAppearanceType.Empire, 0.92f, 13, 0, 0.8f, 2.5f, 1, 0.4f, 4f, 0, 0.5f, 2f, "かつて世界を支配した帝国で標準採用されていた盾。", "A shield that was regarded as standard equipment in the empire which once dominated the world.")
, new TableShieldData(21012, "隕鉄の盾", "Meteoric Shield", 2, ShieldAppearanceType.Stars, 0.92f, 30, 0, 0.8f, 2f, 1, 0.4f, 2.5f, 0, 0.9f, 1.2f, "遥か昔に落ちた、隕石によってもたらされた素材から作られた盾。投げて敵にぶつかっても謎の物理法則により、一定確率で手元に戻ってくる。", "A shield made from the material brought by the meteorite that fell long ago. Even if you throw this and hit an enemy, it comes back to hand with a certain probability by mystery physical law.")
, new TableShieldData(21013, "鍋のふた", "Pot Lid", 3, ShieldAppearanceType.Podlit, 0.92f, 6, 1, 0.8f, 3f, 1, 0.6f, 2.5f, 0, 0.2f, 1.7f, "鍋に使われるふた。伝説の合金で作られているらしい。", "A lid used for a pot. It seems that it is made of a legendary alloy.")
, new TableShieldData(21014, "木の盾", "Wood Shield", 3, ShieldAppearanceType.Wood, 0.92f, 8, 1, 0.8f, 2.5f, 1, 0.6f, 4f, 0, 0.2f, 1.7f, "普通サイズの木の盾。", "A shield of an ordinary size wood.")
, new TableShieldData(21015, "ハリボテの盾", "Papier Shield", 3, ShieldAppearanceType.Paper, 0.92f, 5, 1, 0.8f, 2f, 1, 0.6f, 4f, 0, 0.2f, 1.7f, "演劇の小道具として使われた盾。見た目は立派だが実用性に乏しい。", "Shield used as a prop of the theater. Although it looks fine, it is not practical.")
, new TableShieldData(21016, "騎士の盾", "Knight's Shield", 3, ShieldAppearanceType.Knight, 0.92f, 15, 1, 0.5f, 4f, 1, 0.6f, 4f, 0, 0.2f, 1.7f, "どこかの国の騎士が使っていた盾。軽さと丈夫さを兼ねそろえた万能盾。", "The shield that the knight of some country used. A versatile shield that combines lightness and durability.")
, new TableShieldData(21017, "帝国の盾", "Empire's Shield", 3, ShieldAppearanceType.Empire, 0.92f, 13, 1, 0.8f, 2.5f, 1, 0.6f, 4f, 0, 0.2f, 1.7f, "かつて世界を支配した帝国で標準採用されていた盾。", "A shield that was regarded as standard equipment in the empire which once dominated the world.")
, new TableShieldData(21018, "隕鉄の盾", "Meteoric Shield", 3, ShieldAppearanceType.Stars, 0.92f, 30, 1, 0.8f, 2f, 1, 0.6f, 2.5f, 0, 0.2f, 1.2f, "遥か昔に落ちた、隕石によってもたらされた素材から作られた盾。投げて敵にぶつかっても謎の物理法則により、一定確率で手元に戻ってくる。", "A shield made from the material brought by the meteorite that fell long ago. Even if you throw this and hit an enemy, it comes back to hand with a certain probability by mystery physical law.")
, new TableShieldData(21019, "鍋のふた", "Pot Lid", 4, ShieldAppearanceType.Podlit, 0.92f, 6, 1, 0.8f, 2.2f, 1, 0.6f, 2f, 0, 0.2f, 1.7f, "鍋に使われるふた。伝説の合金で作られているらしい。", "A lid used for a pot. It seems that it is made of a legendary alloy.")
, new TableShieldData(21020, "木の盾", "Wood Shield", 4, ShieldAppearanceType.Wood, 0.92f, 8, 1, 0.8f, 1.7f, 1, 0.6f, 3f, 0, 0.2f, 1.7f, "普通サイズの木の盾。", "A shield of an ordinary size wood.")
, new TableShieldData(21021, "ハリボテの盾", "Papier Shield", 4, ShieldAppearanceType.Paper, 0.92f, 5, 1, 0.8f, 2.2f, 1, 0.6f, 3f, 0, 0.2f, 1.7f, "演劇の小道具として使われた盾。見た目は立派だが実用性に乏しい。", "Shield used as a prop of the theater. Although it looks fine, it is not practical.")
, new TableShieldData(21022, "騎士の盾", "Knight's Shield", 4, ShieldAppearanceType.Knight, 0.92f, 15, 1, 0.5f, 3f, 1, 0.6f, 3f, 0, 0.2f, 1.7f, "どこかの国の騎士が使っていた盾。軽さと丈夫さを兼ねそろえた万能盾。", "The shield that the knight of some country used. A versatile shield that combines lightness and durability.")
, new TableShieldData(21023, "帝国の盾", "Empire's Shield", 4, ShieldAppearanceType.Empire, 0.92f, 13, 1, 0.8f, 2.2f, 1, 0.6f, 3f, 0, 0.2f, 1.7f, "かつて世界を支配した帝国で標準採用されていた盾。", "A shield that was regarded as standard equipment in the empire which once dominated the world.")
, new TableShieldData(21024, "隕鉄の盾", "Meteoric Shield", 4, ShieldAppearanceType.Stars, 0.92f, 30, 1, 0.8f, 1.7f, 1, 0.6f, 2f, 0, 0.2f, 1.2f, "遥か昔に落ちた、隕石によってもたらされた素材から作られた盾。投げて敵にぶつかっても謎の物理法則により、一定確率で手元に戻ってくる。", "A shield made from the material brought by the meteorite that fell long ago. Even if you throw this and hit an enemy, it comes back to hand with a certain probability by mystery physical law.")
, new TableShieldData(21025, "鍋のふた", "Pot Lid", 5, ShieldAppearanceType.Podlit, 0.92f, 7, 1, 0.8f, 2.2f, 1, 0.8f, 2f, 0, 0.2f, 1.7f, "鍋に使われるふた。伝説の合金で作られているらしい。", "A lid used for a pot. It seems that it is made of a legendary alloy.")
, new TableShieldData(21026, "木の盾", "Wood Shield", 5, ShieldAppearanceType.Wood, 0.92f, 9, 1, 0.8f, 1.7f, 1, 0.8f, 3f, 0, 0.2f, 1.7f, "普通サイズの木の盾。", "A shield of an ordinary size wood.")
, new TableShieldData(21027, "ハリボテの盾", "Papier Shield", 5, ShieldAppearanceType.Paper, 0.92f, 6, 1, 0.8f, 2.2f, 1, 0.8f, 3f, 0, 0.2f, 1.7f, "演劇の小道具として使われた盾。見た目は立派だが実用性に乏しい。", "Shield used as a prop of the theater. Although it looks fine, it is not practical.")
, new TableShieldData(21028, "騎士の盾", "Knight's Shield", 5, ShieldAppearanceType.Knight, 0.92f, 17, 1, 0.5f, 3f, 1, 0.8f, 3f, 0, 0.2f, 1.7f, "どこかの国の騎士が使っていた盾。軽さと丈夫さを兼ねそろえた万能盾。", "The shield that the knight of some country used. A versatile shield that combines lightness and durability.")
, new TableShieldData(21029, "帝国の盾", "Empire's Shield", 5, ShieldAppearanceType.Empire, 0.92f, 14, 1, 0.8f, 2.2f, 1, 0.8f, 3f, 0, 0.2f, 1.7f, "かつて世界を支配した帝国で標準採用されていた盾。", "A shield that was regarded as standard equipment in the empire which once dominated the world.")
, new TableShieldData(21030, "隕鉄の盾", "Meteoric Shield", 5, ShieldAppearanceType.Stars, 0.92f, 32, 1, 0.8f, 1.7f, 1, 0.8f, 2f, 0, 0.2f, 1.2f, "遥か昔に落ちた、隕石によってもたらされた素材から作られた盾。投げて敵にぶつかっても謎の物理法則により、一定確率で手元に戻ってくる。", "A shield made from the material brought by the meteorite that fell long ago. Even if you throw this and hit an enemy, it comes back to hand with a certain probability by mystery physical law.")
, new TableShieldData(21031, "鍋のふた", "Pot Lid", 6, ShieldAppearanceType.Podlit, 0.92f, 7, 1, 0.8f, 1.6f, 1, 0.8f, 1.7f, 0, 0.2f, 1.7f, "鍋に使われるふた。伝説の合金で作られているらしい。", "A lid used for a pot. It seems that it is made of a legendary alloy.")
, new TableShieldData(21032, "木の盾", "Wood Shield", 6, ShieldAppearanceType.Wood, 0.92f, 9, 1, 0.8f, 1.2f, 1, 0.8f, 2.3f, 0, 0.2f, 1.7f, "普通サイズの木の盾。", "A shield of an ordinary size wood.")
, new TableShieldData(21033, "ハリボテの盾", "Papier Shield", 6, ShieldAppearanceType.Paper, 0.92f, 6, 1, 0.8f, 1.6f, 1, 0.8f, 2.3f, 0, 0.2f, 1.7f, "演劇の小道具として使われた盾。見た目は立派だが実用性に乏しい。", "Shield used as a prop of the theater. Although it looks fine, it is not practical.")
, new TableShieldData(21034, "騎士の盾", "Knight's Shield", 6, ShieldAppearanceType.Knight, 0.92f, 17, 1, 0.5f, 2f, 1, 0.8f, 2.3f, 0, 0.2f, 1.7f, "どこかの国の騎士が使っていた盾。軽さと丈夫さを兼ねそろえた万能盾。", "The shield that the knight of some country used. A versatile shield that combines lightness and durability.")
, new TableShieldData(21035, "帝国の盾", "Empire's Shield", 6, ShieldAppearanceType.Empire, 0.92f, 14, 1, 0.8f, 1.6f, 1, 0.8f, 2.3f, 0, 0.2f, 1.7f, "かつて世界を支配した帝国で標準採用されていた盾。", "A shield that was regarded as standard equipment in the empire which once dominated the world.")
, new TableShieldData(21036, "隕鉄の盾", "Meteoric Shield", 6, ShieldAppearanceType.Stars, 0.92f, 32, 1, 0.8f, 1.2f, 1, 0.8f, 1.7f, 0, 0.2f, 1.2f, "遥か昔に落ちた、隕石によってもたらされた素材から作られた盾。投げて敵にぶつかっても謎の物理法則により、一定確率で手元に戻ってくる。", "A shield made from the material brought by the meteorite that fell long ago. Even if you throw this and hit an enemy, it comes back to hand with a certain probability by mystery physical law.")
, new TableShieldData(21037, "鍋のふた", "Pot Lid", 7, ShieldAppearanceType.Podlit, 0.92f, 7, 1, 0.8f, 1.6f, 1, 0.9f, 1.7f, 0, 0.4f, 1.4f, "鍋に使われるふた。伝説の合金で作られているらしい。", "A lid used for a pot. It seems that it is made of a legendary alloy.")
, new TableShieldData(21038, "木の盾", "Wood Shield", 7, ShieldAppearanceType.Wood, 0.92f, 9, 1, 0.8f, 1.2f, 1, 0.9f, 2.3f, 0, 0.4f, 1.4f, "普通サイズの木の盾。", "A shield of an ordinary size wood.")
, new TableShieldData(21039, "ハリボテの盾", "Papier Shield", 7, ShieldAppearanceType.Paper, 0.92f, 6, 1, 0.8f, 1.6f, 1, 0.9f, 2.3f, 0, 0.4f, 1.4f, "演劇の小道具として使われた盾。見た目は立派だが実用性に乏しい。", "Shield used as a prop of the theater. Although it looks fine, it is not practical.")
, new TableShieldData(21040, "騎士の盾", "Knight's Shield", 7, ShieldAppearanceType.Knight, 0.92f, 17, 1, 0.5f, 2f, 1, 0.9f, 2.3f, 0, 0.4f, 1.4f, "どこかの国の騎士が使っていた盾。軽さと丈夫さを兼ねそろえた万能盾。", "The shield that the knight of some country used. A versatile shield that combines lightness and durability.")
, new TableShieldData(21041, "帝国の盾", "Empire's Shield", 7, ShieldAppearanceType.Empire, 0.92f, 14, 1, 0.8f, 1.6f, 1, 0.9f, 2.3f, 0, 0.4f, 1.4f, "かつて世界を支配した帝国で標準採用されていた盾。", "A shield that was regarded as standard equipment in the empire which once dominated the world.")
, new TableShieldData(21042, "隕鉄の盾", "Meteoric Shield", 7, ShieldAppearanceType.Stars, 0.92f, 32, 1, 0.8f, 1.2f, 1, 0.9f, 1.7f, 0, 0.4f, 1.2f, "遥か昔に落ちた、隕石によってもたらされた素材から作られた盾。投げて敵にぶつかっても謎の物理法則により、一定確率で手元に戻ってくる。", "A shield made from the material brought by the meteorite that fell long ago. Even if you throw this and hit an enemy, it comes back to hand with a certain probability by mystery physical law.")
, new TableShieldData(21043, "鍋のふた", "Pot Lid", 8, ShieldAppearanceType.Podlit, 0.92f, 7, 1, 0.8f, 1.3f, 1, 0.9f, 1.7f, 0, 0.4f, 1.4f, "鍋に使われるふた。伝説の合金で作られているらしい。", "A lid used for a pot. It seems that it is made of a legendary alloy.")
, new TableShieldData(21044, "木の盾", "Wood Shield", 8, ShieldAppearanceType.Wood, 0.92f, 9, 1, 0.8f, 1.05f, 1, 0.9f, 2.3f, 0, 0.4f, 1.4f, "普通サイズの木の盾。", "A shield of an ordinary size wood.")
, new TableShieldData(21045, "ハリボテの盾", "Papier Shield", 8, ShieldAppearanceType.Paper, 0.92f, 6, 1, 0.8f, 1.3f, 1, 0.9f, 2.3f, 0, 0.4f, 1.4f, "演劇の小道具として使われた盾。見た目は立派だが実用性に乏しい。", "Shield used as a prop of the theater. Although it looks fine, it is not practical.")
, new TableShieldData(21046, "騎士の盾", "Knight's Shield", 8, ShieldAppearanceType.Knight, 0.92f, 17, 1, 0.5f, 1.7f, 1, 0.9f, 2.3f, 0, 0.4f, 1.4f, "どこかの国の騎士が使っていた盾。軽さと丈夫さを兼ねそろえた万能盾。", "The shield that the knight of some country used. A versatile shield that combines lightness and durability.")
, new TableShieldData(21047, "帝国の盾", "Empire's Shield", 8, ShieldAppearanceType.Empire, 0.92f, 14, 1, 0.8f, 1.3f, 1, 0.9f, 2.3f, 0, 0.4f, 1.4f, "かつて世界を支配した帝国で標準採用されていた盾。", "A shield that was regarded as standard equipment in the empire which once dominated the world.")
, new TableShieldData(21048, "隕鉄の盾", "Meteoric Shield", 8, ShieldAppearanceType.Stars, 0.92f, 32, 1, 0.8f, 1.05f, 1, 0.9f, 1.7f, 0, 0.4f, 1.2f, "遥か昔に落ちた、隕石によってもたらされた素材から作られた盾。投げて敵にぶつかっても謎の物理法則により、一定確率で手元に戻ってくる。", "A shield made from the material brought by the meteorite that fell long ago. Even if you throw this and hit an enemy, it comes back to hand with a certain probability by mystery physical law.")


                };

                return _table;
            }
        }
    }
    public static ShieldBase GetItem(long objNo,bool isRandomValue)
    {
        TableShieldData data = Array.Find(Table, i => i.ObjNo == objNo);
        ShieldBase item = new ShieldBase();
        item.Initialize();
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
        item.ApType = data.ApType;
        item.ObjNo = data.ObjNo;
        item.ShieldBaseDefense = data.Defense;
        item.ThrowDexterity = data.ThrowDexterity;

        if (isRandomValue == false)
        {
            return item;
        }
        QualifyInformation q = TableQualify.GetRandomName(data.Level);
        item.DisplayNameBefore = q.Name;
        item.DisplayNameBeforeObjNo = q.ObjNo;
        item.StrengthValue = CommonFunction.ConvergenceRandom(data.StrengthAddStart, data.StrengthAddContinue, data.StrengthnAddReduce);
        int optioncount = CommonFunction.ConvergenceRandom(data.OptionAddStart, data.OptionAddContinue, data.OptionAddReduce);
        int index = 0;
        for (int i = 0; i < optioncount; i++)
        {
            //30回回して終わらなかったら強制終了
            if (index > 30)
            {
                break;
            }
            index++;
            uint rnd = CommonFunction.GetRandomUInt32();
            BaseOption newOpt = TableOptionCommon.GetValue(OptionBaseType.Shield, rnd, data.OptionPowStart, data.OptionPowContinue, data.OptionPowReduce);

            //同じオプションがすでに含まれていたらもう一度算出
            if(CommonFunction.IsNull(newOpt) == true)
            {
                i--;
                continue;
            }
            BaseOption containOpt = item.Options.Find(o => o.ObjNo == newOpt.ObjNo);
            if (CommonFunction.IsNull(containOpt) == false)
            {
                i--;
                continue;
            }
            item.Options.Add(newOpt);
        }
        return item;
    }

    private class TableShieldData
    {
        public TableShieldData(ushort objNo,
            string displayName,
            string displayNameEn,
            int level,
            ShieldAppearanceType aptype,
            float throwDexterity,
            float defense,
            int optionAddStart,
            float optionAddContinue,
            float optionAddReduce,
            int optionPowStart,
            float optionPowContinue,
            float optionPowReduce,
            int strengthAddStart,
            float strengthAddContinue,
            float strengthnAddReduce,
            string description,
            string descriptionEn)
        {
            ObjNo = objNo;
            DisplayName = displayName;
            DisplayNameEn = displayNameEn;
            Level = level;
            ApType = aptype;
            ThrowDexterity = throwDexterity;
            Defense = defense;
            OptionAddStart = optionAddStart;
            OptionAddContinue = optionAddContinue;
            OptionAddReduce = optionAddReduce;
            OptionPowStart = optionPowStart;
            OptionPowContinue = optionPowContinue;
            OptionPowReduce = optionPowReduce;
            StrengthAddStart = strengthAddStart;
            StrengthAddContinue = strengthAddContinue;
            StrengthnAddReduce = strengthnAddReduce;
            Description = description;
            DescriptionEn = descriptionEn;
        }
        public ushort ObjNo;
        public string DisplayName;
        public string DisplayNameEn;
        public int Level;
        public ShieldAppearanceType ApType;
        public float ThrowDexterity;
        public float Defense;
        public int OptionAddStart;
        public float OptionAddContinue;
        public float OptionAddReduce;
        public int OptionPowStart;
        public float OptionPowContinue;
        public float OptionPowReduce;
        public int StrengthAddStart;
        public float StrengthAddContinue;
        public float StrengthnAddReduce;
        public string Description;
        public string DescriptionEn;
    }
}