using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableOptionCommon
{

    private static TableOptionCommonData[] _table;
    private static TableOptionCommonData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableOptionCommonData[]{

                     new TableOptionCommonData(40002, "防御力プラス", "DEF Plus", ItemType.Shield, OptionBaseType.Shield, 0, 294175843, OptionType.Defence, 2f, 0, 0, true, "防御力に付加値がつく。", "Defense will increase.")
, new TableOptionCommonData(40009, "毒抵抗", "Resist Poison", ItemType.Shield, OptionBaseType.Shield, 294175843, 382428595, OptionType.DefenceAbnormal, 0.1f, 6, 0, true, "毒の状態異常を一定確率で防ぐ。", "Prevents abnormal state of poison with a certain probability.")
, new TableOptionCommonData(40010, "麻痺抵抗", "Resist Paralysis", ItemType.Shield, OptionBaseType.Shield, 382428595, 470681348, OptionType.DefenceAbnormal, 0.1f, 16, 0, true, "麻痺の状態異常を一定確率で防ぐ。", "Prevents abnormal state of paralysis with a certain probability.")
, new TableOptionCommonData(40011, "混乱抵抗", "Resist Confusion", ItemType.Shield, OptionBaseType.Shield, 470681348, 558934101, OptionType.DefenceAbnormal, 0.1f, 32, 0, true, "混乱の状態異常を一定確率で防ぐ。", "Prevents abnormal state of confusion with a certain probability.")
, new TableOptionCommonData(40012, "睡眠抵抗", "Resist Sleep", ItemType.Shield, OptionBaseType.Shield, 558934101, 647186853, OptionType.DefenceAbnormal, 0.1f, 8, 0, true, "睡眠の状態異常を一定確率で防ぐ。", "Prevents abnormal state of sleep with a certain probability.")
, new TableOptionCommonData(40067, "スロー抵抗", "Resist Slow", ItemType.Shield, OptionBaseType.Shield, 647186853, 853109943, OptionType.DefenceAbnormal, 0.1f, 256, 0, true, "スローの状態異常を一定確率で防ぐ。", "Prevents abnormal state of slow with a certain probability.")
, new TableOptionCommonData(40068, "かぜ抵抗", "Resist Cold", ItemType.Shield, OptionBaseType.Shield, 853109943, 1059033032, OptionType.DefenceAbnormal, 0.1f, 512, 0, true, "かぜの状態異常を一定確率で防ぐ。", "Prevents abnormal state of cold with a certain probability.")
, new TableOptionCommonData(40069, "肩こり抵抗", "Resist Stiff Shoulder", ItemType.Shield, OptionBaseType.Shield, 1059033032, 1264956122, OptionType.DefenceAbnormal, 0.1f, 1024, 0, true, "肩こりの状態異常を一定確率で防ぐ。", "Prevents abnormal state of stiff shoulder with a certain probability.")
, new TableOptionCommonData(40013, "暗闇抵抗", "Resist Dark", ItemType.Shield, OptionBaseType.Shield, 1264956122, 1353208874, OptionType.DefenceAbnormal, 0.1f, 1, 0, true, "暗闇の状態異常を一定確率で防ぐ。", "Prevents abnormal state of dark with a certain probability.")
, new TableOptionCommonData(40015, "被弾回復", "Recovery at hit", ItemType.Shield, OptionBaseType.Shield, 1353208874, 1441461627, OptionType.DefenceRecover, 0.01f, 0, 0, true, "被弾時にダメージの一部を回復する。", "Recover a small amount of HP when it receives damage.")
, new TableOptionCommonData(40022, "反射", "Reflection", ItemType.Shield, OptionBaseType.Shield, 1441461627, 1500296795, OptionType.Reflection, 0.02f, 0, 0, true, "被弾時にダメージの一部を反射する。", "When receiving damage, it reflects part of the damage.")
, new TableOptionCommonData(40029, "鉄壁", "Iron wall", ItemType.Shield, OptionBaseType.Shield, 1500296795, 1735637469, OptionType.IronWall, 0.5f, 0, 0, true, "敵に囲まれると防御力が上がる。", "Defense power rises when enemies are surrounded.")
, new TableOptionCommonData(40032, "ナルコレプシー", "Narcolepsy", ItemType.Shield, OptionBaseType.Shield, 1735637469, 1823890222, OptionType.Narcolepsy, 0.01f, 0, 0, true, "一定確率で睡眠状態に陥る。", "Fall asleep with constant probability.")
, new TableOptionCommonData(40037, "HPプラス", "HP Plus", ItemType.Shield, OptionBaseType.Shield, 1823890222, 2059230895, OptionType.HP, 2f, 0, 0, true, "HPに付加値がつく。", "HP will increase.")
, new TableOptionCommonData(40038, "HP自然回復増加", "HP natural recovery increase", ItemType.Shield, OptionBaseType.Shield, 2059230895, 2118066064, OptionType.WalkRecover, 0.4f, 0, 0, true, "ターン毎のHP自然回復量が増加する。", "The amount of HP natural recovery per turn increases.")
, new TableOptionCommonData(40040, "小食", "Diet", ItemType.Shield, OptionBaseType.Shield, 2118066064, 2235736401, OptionType.SatSmall, 0.05f, 0, 0, true, "おなかが減りにくくなる。", "A stomach reduction slows down.")
, new TableOptionCommonData(40041, "犠牲", "Sacrifice", ItemType.Shield, OptionBaseType.Shield, 2235736401, 2294571569, OptionType.Sacrifice, 0, 0, 0, false, "死亡時に装備していると一度だけアイテムを犠牲に復活できる。", "If you are equipped with this item at the time of death, you can revive only once at the expense of this.")
, new TableOptionCommonData(40042, "トラップ回避", "Trap avoidance", ItemType.Shield, OptionBaseType.Shield, 2294571569, 2412241906, OptionType.DexTrap, 0.05f, 0, 0, true, "トラップを踏んでも一定確率で回避する。", "Even if you step on a trap, avoid it with a certain probability.")
, new TableOptionCommonData(40046, "大食", "Gluttony", ItemType.Shield, OptionBaseType.Shield, 2412241906, 2471077074, OptionType.SatBig, 0.05f, 0, 0, true, "おなかが減りやすくなる。", "The decrease in stomach faster.")
, new TableOptionCommonData(40047, "経験値プラス", "Exp Plus", ItemType.Shield, OptionBaseType.Shield, 2471077074, 2529912243, OptionType.ExpPlus, 0.05f, 0, 0, true, "取得経験値が上昇する。", "Exp value to be acquired increases")
, new TableOptionCommonData(40048, "樫の盾", "Oakenshield", ItemType.Shield, OptionBaseType.Shield, 2529912243, 2588747411, OptionType.Oakenshield, 0.1f, 0, 0, true, "死亡するダメージを受けてもHP1で耐えることがある。", "Even if you receive the death damage it will withstand with HP1 with a certain probability.")
, new TableOptionCommonData(40049, "経験値マイナス", "Exp Minus", ItemType.Shield, OptionBaseType.Shield, 2588747411, 2677000164, OptionType.ExpMinus, 0.05f, 0, 0, true, "取得経験値が減少する。", "Exp value to be acquired decreases")
, new TableOptionCommonData(40052, "ガラス", "Glass", ItemType.Shield, OptionBaseType.Shield, 2677000164, 2765252916, OptionType.Glass, 0.01f, 0, 0, true, "攻撃時や防御時に一定確率で壊れる。", "It breaks with a certain probability when attacking or defending.")
, new TableOptionCommonData(40060, "動物特防", "DEF Animal", ItemType.Shield, OptionBaseType.Shield, 2765252916, 2941758422, OptionType.DefenceRace, 0.2f, 0, 1, true, "動物系の敵から受けるダメージが減少する。", "Damage from an animal type enemy decreases.")
, new TableOptionCommonData(40061, "植物特防", "DEF Plant", ItemType.Shield, OptionBaseType.Shield, 2941758422, 3118263927, OptionType.DefenceRace, 0.2f, 0, 2, true, "植物系の敵から受けるダメージが減少する。", "Damage from a Plant type enemies decreases.")
, new TableOptionCommonData(40062, "無機物特防", "DEF Inorganic", ItemType.Shield, OptionBaseType.Shield, 3118263927, 3294769432, OptionType.DefenceRace, 0.2f, 0, 4, true, "無機物系の敵から受けるダメージが減少する。", "Damage from a Inorganic type enemies decreases.")
, new TableOptionCommonData(40063, "二足特防", "DEF Two-legged", ItemType.Shield, OptionBaseType.Shield, 3294769432, 3471274938, OptionType.DefenceRace, 0.2f, 0, 8, true, "二足系の敵から受けるダメージが減少する。", "Damage from a Two-legged type enemies decreases.")
, new TableOptionCommonData(40064, "虫特防", "DEF Insect", ItemType.Shield, OptionBaseType.Shield, 3471274938, 3647780443, OptionType.DefenceRace, 0.2f, 0, 16, true, "虫系の敵から受けるダメージが減少する。", "Damage from a Insect type enemies decreases.")
, new TableOptionCommonData(40065, "空中特防", "DEF Airborne", ItemType.Shield, OptionBaseType.Shield, 3647780443, 3824285948, OptionType.DefenceRace, 0.2f, 0, 32, true, "空中系の敵から受けるダメージが減少する。", "Damage from a Airborne type enemies decreases.")
, new TableOptionCommonData(40066, "多足特防", "DEF Multiple-legged", ItemType.Shield, OptionBaseType.Shield, 3824285948, 4000791453, OptionType.DefenceRace, 0.2f, 0, 64, true, "多足系の敵から受けるダメージが減少する。", "Damage from a Multiple-legged type enemies decreases.")
, new TableOptionCommonData(40175, "接着", "Adhesion", ItemType.Shield, OptionBaseType.Shield, 4000791453, 4294967295, OptionType.Adhesive, 0.2f, 0, 0, false, "装備するとくっついて離れなくなってしまう。", "It will become stuck when equipped.")
, new TableOptionCommonData(40001, "HPプラス", "HP Plus", ItemType.Weapon, OptionBaseType.Weapon, 0, 238609295, OptionType.HP, 2f, 0, 0, true, "HPに付加値がつく。", "HP will increase.")
, new TableOptionCommonData(40003, "攻撃力プラス", "ATK Plus", ItemType.Weapon, OptionBaseType.Weapon, 238609295, 536870912, OptionType.Attack, 2f, 0, 0, true, "攻撃力に付加値がつく。", "Attack will increase.")
, new TableOptionCommonData(40005, "毒付与", "Add Poison", ItemType.Weapon, OptionBaseType.Weapon, 536870912, 656175559, OptionType.AttackAbnormal, 0.01f, 2, 0, true, "まれに毒を付与することがある。", "In rare cases may give poison.")
, new TableOptionCommonData(40006, "麻痺付与", "Add Paralysis", ItemType.Weapon, OptionBaseType.Weapon, 656175559, 835132530, OptionType.AttackAbnormal, 0.01f, 16, 0, true, "まれに麻痺を付与することがある。", "In rare cases may give paralysis.")
, new TableOptionCommonData(40007, "混乱付与", "Add Confusion", ItemType.Weapon, OptionBaseType.Weapon, 835132530, 954437177, OptionType.AttackAbnormal, 0.01f, 32, 0, true, "まれに混乱を付与することがある。", "In rare cases may give confusion.")
, new TableOptionCommonData(40008, "睡眠付与", "Add Sleep", ItemType.Weapon, OptionBaseType.Weapon, 954437177, 1073741824, OptionType.AttackAbnormal, 0.01f, 8, 0, true, "まれに睡眠を付与することがある。", "In rare cases may give sleep.")
, new TableOptionCommonData(40070, "デコイ付与", "Add Decoy", ItemType.Weapon, OptionBaseType.Weapon, 1073741824, 1252698795, OptionType.AttackAbnormal, 0.01f, 128, 0, true, "まれにデコイを付与することがある。", "In rare cases may give decoy.")
, new TableOptionCommonData(40172, "スロー付与", "Add Sleep", ItemType.Weapon, OptionBaseType.Weapon, 1252698795, 1372003442, OptionType.AttackAbnormal, 0.01f, 256, 0, true, "まれにスローを付与することがある。", "In rare cases may give slow.")
, new TableOptionCommonData(40173, "かぜ付与", "Add Cold", ItemType.Weapon, OptionBaseType.Weapon, 1372003442, 1550960413, OptionType.AttackAbnormal, 0.01f, 1024, 0, true, "まれにかぜを付与することがある。", "In rare cases may give cold.")
, new TableOptionCommonData(40174, "肩こり付与", "Add Stiff Shoulder", ItemType.Weapon, OptionBaseType.Weapon, 1550960413, 1729917383, OptionType.AttackAbnormal, 0.01f, 2048, 0, true, "まれに肩こりを付与することがある。", "In rare cases may give stiff shoulder.")
, new TableOptionCommonData(40016, "体力吸収", "HP absorption", ItemType.Weapon, OptionBaseType.Weapon, 1729917383, 1908874354, OptionType.AttackRecover, 0.02f, 0, 0, true, "攻撃命中時に与ダメージの一部を吸収する。", "Absorb some of the damage given when hit.")
, new TableOptionCommonData(40018, "HP自然回復増加", "HP walk recovery increase", ItemType.Weapon, OptionBaseType.Weapon, 1908874354, 2028179001, OptionType.WalkRecover, 0.4f, 0, 0, true, "ターン毎のHP自然回復量が増加する。", "The amount of HP natural recovery per turn increases.")
, new TableOptionCommonData(40019, "逆境", "Adversity", ItemType.Weapon, OptionBaseType.Weapon, 2028179001, 2266788295, OptionType.Adversity, 0.5f, 0, 0, true, "HPが少ないとき、攻撃力が上昇する。", "When HP is low, the attack power rises.")
, new TableOptionCommonData(40021, "小食", "Diet", ItemType.Weapon, OptionBaseType.Weapon, 2266788295, 2386092942, OptionType.SatSmall, 0.05f, 0, 0, true, "おなかが減りにくくなる。", "A stomach reduction slows down.")
, new TableOptionCommonData(40023, "犠牲", "Sacrifice", ItemType.Weapon, OptionBaseType.Weapon, 2386092942, 2415919104, OptionType.Sacrifice, 0f, 0, 0, false, "死亡時に装備していると一度だけアイテムを犠牲に復活できる。", "If you are equipped with this item at the time of death, you can revive only once at the expense of this.")
, new TableOptionCommonData(40024, "トラップ回避", "Trap avoidance", ItemType.Weapon, OptionBaseType.Weapon, 2415919104, 2505397589, OptionType.DexTrap, 0.05f, 0, 0, true, "トラップを踏んでも一定確率で回避する。", "Even if you step on a trap, avoid it with a certain probability.")
, new TableOptionCommonData(40028, "打開", "Breakthrough", ItemType.Weapon, OptionBaseType.Weapon, 2505397589, 2744006883, OptionType.Breakthrough, 0.5f, 0, 0, true, "敵に囲まれているとき攻撃力が上がる。", "The attack power increases when surrounded by enemies.")
, new TableOptionCommonData(40030, "致命", "Fatal", ItemType.Weapon, OptionBaseType.Weapon, 2744006883, 2803659207, OptionType.Fatal, 0.01f, 0, 0, true, "攻撃時に一定確率で敵のHPを1にする。", "At the time of attack, set the enemy's HP to 1 with constant probability.")
, new TableOptionCommonData(40031, "大食", "Gluttony", ItemType.Weapon, OptionBaseType.Weapon, 2803659207, 2833485369, OptionType.SatBig, 0.05f, 0, 0, true, "おなかが減りやすくなる。", "The decrease in stomach faster.")
, new TableOptionCommonData(40033, "ガラス", "Glass", ItemType.Weapon, OptionBaseType.Weapon, 2833485369, 2922963854, OptionType.Glass, 0.01f, 0, 0, true, "攻撃時や防御時に一定確率で壊れる。", "It breaks with a certain probability when attacking or defending.")
, new TableOptionCommonData(40034, "経験値プラス", "Exp Plus", ItemType.Weapon, OptionBaseType.Weapon, 2922963854, 2982616178, OptionType.ExpPlus, 0.05f, 0, 0, true, "取得経験値が上昇する。", "Exp value to be acquired increases")
, new TableOptionCommonData(40050, "経験値マイナス", "Exp Minus", ItemType.Weapon, OptionBaseType.Weapon, 2982616178, 3072094663, OptionType.ExpMinus, 0.05f, 0, 0, true, "取得経験値が減少する。", "Exp value to be acquired decreases")
, new TableOptionCommonData(40051, "ナルコレプシー", "Narcolepsy", ItemType.Weapon, OptionBaseType.Weapon, 3072094663, 3161573148, OptionType.Narcolepsy, 0.01f, 0, 0, true, "一定確率で睡眠状態に陥る。", "Fall asleep with constant probability.")
, new TableOptionCommonData(40053, "動物特攻", "ATK Animal", ItemType.Weapon, OptionBaseType.Weapon, 3161573148, 3280877795, OptionType.AttackRace, 0.2f, 0, 1, true, "動物系の敵に与えるダメージが増加する。", "Damage to Animal type enemies will increase.")
, new TableOptionCommonData(40054, "植物特攻", "ATK Plant", ItemType.Weapon, OptionBaseType.Weapon, 3280877795, 3400182442, OptionType.AttackRace, 0.2f, 0, 2, true, "植物系の敵に与えるダメージが増加する。", "Damage to Plant type enemies will increase.")
, new TableOptionCommonData(40055, "無機物特攻", "ATK Inorganic", ItemType.Weapon, OptionBaseType.Weapon, 3400182442, 3519487089, OptionType.AttackRace, 0.2f, 0, 4, true, "無機物系の敵に与えるダメージが増加する。", "Damage to Inorganic type enemies will increase.")
, new TableOptionCommonData(40056, "二足特攻", "ATK Two-legged", ItemType.Weapon, OptionBaseType.Weapon, 3519487089, 3638791737, OptionType.AttackRace, 0.2f, 0, 8, true, "二足系の敵に与えるダメージが増加する。", "Damage to Two-legged type enemies will increase.")
, new TableOptionCommonData(40057, "虫特攻", "ATK Insect", ItemType.Weapon, OptionBaseType.Weapon, 3638791737, 3758096384, OptionType.AttackRace, 0.2f, 0, 16, true, "虫系の敵に与えるダメージが増加する。", "Damage to Insect type enemies will increase.")
, new TableOptionCommonData(40058, "空中特攻", "ATK Airborne", ItemType.Weapon, OptionBaseType.Weapon, 3758096384, 3877401031, OptionType.AttackRace, 0.2f, 0, 32, true, "空中系の敵に与えるダメージが増加する。", "Damage to Airborne type enemies will increase.")
, new TableOptionCommonData(40059, "多足特攻", "ATK Multiple-legged", ItemType.Weapon, OptionBaseType.Weapon, 3877401031, 3996705678, OptionType.AttackRace, 0.2f, 0, 64, true, "多足系の敵に与えるダメージが増加する。", "Damage to Multi-legged type enemies will increase.")
, new TableOptionCommonData(40176, "接着", "Adhesion", ItemType.Weapon, OptionBaseType.Weapon, 3996705678, 4294967295, OptionType.Adhesive, 0.2f, 0, 0, false, "装備するとくっついて離れなくなってしまう。", "It will become stuck when equipped.")
, new TableOptionCommonData(40161, "毒中和", "Recover Poison", ItemType.Material, OptionBaseType.Material, 0, 222537166, OptionType.DefenceAbnormal, 0.01f, 6, 0, false, "毒状態の回復効果がつく。", "Cure for poison, deadly poison.")
, new TableOptionCommonData(40162, "麻痺中和", "Recover Paralysis", ItemType.Material, OptionBaseType.Material, 222537166, 445074332, OptionType.DefenceAbnormal, 0.01f, 16, 0, false, "麻痺状態の回復効果がつく。", "Cure for paralysis.")
, new TableOptionCommonData(40163, "混乱中和", "Recover Confusion", ItemType.Material, OptionBaseType.Material, 445074332, 667611497, OptionType.DefenceAbnormal, 0.01f, 32, 0, false, "混乱状態の回復効果がつく。", "Cure for confusion.")
, new TableOptionCommonData(40164, "睡眠中和", "Recover Sleep", ItemType.Material, OptionBaseType.Material, 667611497, 890148663, OptionType.DefenceAbnormal, 0.01f, 8, 0, false, "睡眠状態の回復効果がつく。", "Cure for sleep.")
, new TableOptionCommonData(40165, "デコイ中和", "Recover Decoy", ItemType.Material, OptionBaseType.Material, 890148663, 1112685828, OptionType.DefenceAbnormal, 0.01f, 128, 0, false, "デコイ状態の回復効果がつく。", "Cure for decoy.")
, new TableOptionCommonData(40166, "スロー中和", "Recover Slow", ItemType.Material, OptionBaseType.Material, 1112685828, 1335222994, OptionType.DefenceAbnormal, 0.01f, 256, 0, false, "スロー状態の回復効果がつく。", "Cure for slow.")
, new TableOptionCommonData(40167, "かぜ中和", "Recover Cold", ItemType.Material, OptionBaseType.Material, 1335222994, 1557760159, OptionType.DefenceAbnormal, 0.01f, 1024, 0, false, "かぜ状態の回復効果がつく。", "Cure for cold.")
, new TableOptionCommonData(40168, "肩こり中和", "Recover Stiff Shoulder", ItemType.Material, OptionBaseType.Material, 1557760159, 1780297325, OptionType.DefenceAbnormal, 0.01f, 2048, 0, false, "肩こり状態の回復効果がつく。", "Cure for stiff shoulder.")
, new TableOptionCommonData(40169, "状態効果反転", "State effect reversal", ItemType.Material, OptionBaseType.Material, 1780297325, 1847058475, OptionType.ReverceAbnormal, 0f, 0, 0, false, "状態異常中和の効果が、回復から付与に変わる。", "The effect of abnormal state changes from cure to giving.")
, new TableOptionCommonData(40170, "基本効果反転", "Basic effect reversal", ItemType.Material, OptionBaseType.Material, 1847058475, 1913819624, OptionType.ReverceDamage, 0f, 0, 0, false, "アイテムの基本効果が反転する。", "The basic effect of the item is reversed.")
, new TableOptionCommonData(40171, "引火", "Ignition", ItemType.Material, OptionBaseType.Material, 1913819624, 2358893955, OptionType.CatchingFire, 0f, 0, 0, true, "発動時に火が付く。", "A fire strikes during activation.")
, new TableOptionCommonData(40077, "範囲拡大", "Extend range", ItemType.Material, OptionBaseType.Material, 2358893955, 2803968286, OptionType.Wildfire, 0f, 0, 0, true, "発動時の効果範囲が拡大する。", "The range of effect at activation is expanded.")
, new TableOptionCommonData(40081, "効果増大", "Increase effect", ItemType.Material, OptionBaseType.Material, 2803968286, 3249042617, OptionType.EffectUp, 2f, 0, 0, true, "効果が大きくなる。", "The effect is increased.")
, new TableOptionCommonData(40082, "効果縮小", "Decrease effect", ItemType.Material, OptionBaseType.Material, 3249042617, 3360311200, OptionType.EffectDown, 2f, 0, 0, true, "効果が小さくなる。", "The effect is reduced.")
, new TableOptionCommonData(40083, "効果安定", "Stable effect", ItemType.Material, OptionBaseType.Material, 3360311200, 3805385531, OptionType.EffectStabile, 0.5f, 0, 0, true, "効果が安定する。", "The effect stabilizes.")
, new TableOptionCommonData(40084, "効果不安定", "Instable Effect ", ItemType.Material, OptionBaseType.Material, 3805385531, 4250459862, OptionType.EffectNotStabile, 0.5f, 0, 0, true, "効果が不安定になる。", "The effect becomes unstable.")
, new TableOptionCommonData(40178, "Power回復", "Power recovery", ItemType.Material, OptionBaseType.Material, 4250459862, 4294967295, OptionType.RecoverPower, 0f, 0, 0, false, "Power減少時にPowerが回復する", "Power recovers when Power decreases.")
, new TableOptionCommonData(40091, "逆境", "Adversity", ItemType.Material, OptionBaseType.MaterialStrength, 0, 357913942, OptionType.Adversity, 0.5f, 0, 0, true, "HPが少ないとき、攻撃力が上昇する。", "When HP is low, the attack power rises.")
, new TableOptionCommonData(40092, "攻撃力プラス", "ATK P;us", ItemType.Material, OptionBaseType.MaterialStrength, 357913942, 805306368, OptionType.Attack, 2f, 0, 0, true, "攻撃力に付加値がつく。", "Attack will increase.")
, new TableOptionCommonData(40093, "体力吸収", "HP absorption", ItemType.Material, OptionBaseType.MaterialStrength, 805306368, 1073741824, OptionType.AttackRecover, 0.02f, 0, 0, true, "攻撃命中時に与ダメージの一部を吸収する。", "You absorb a part of the damage given at attack hit.")
, new TableOptionCommonData(40094, "打開", "Breakthrough", ItemType.Material, OptionBaseType.MaterialStrength, 1073741824, 1431655765, OptionType.Breakthrough, 0.5f, 0, 0, true, "敵に囲まれているとき攻撃力が上がる。", "The attack power increases when surrounded by enemies.")
, new TableOptionCommonData(40095, "防御力プラス", "DEF Plus", ItemType.Material, OptionBaseType.MaterialStrength, 1431655765, 1879048192, OptionType.Defence, 2f, 0, 0, true, "防御力に付加値がつく。", "Defense will increase.")
, new TableOptionCommonData(40096, "被弾回復", "Recovery at hit", ItemType.Material, OptionBaseType.MaterialStrength, 1879048192, 2013265920, OptionType.DefenceRecover, 0.01f, 0, 0, true, "被弾時にダメージの一部を回復する。", "Recover a small amount of HP when it receives damage.")
, new TableOptionCommonData(40097, "トラップ回避", "Trap avoidance", ItemType.Material, OptionBaseType.MaterialStrength, 2013265920, 2192222891, OptionType.DexTrap, 0.05f, 0, 0, true, "トラップを踏んでも一定確率で回避する。", "Even if you step on a trap, avoid it with a certain probability.")
, new TableOptionCommonData(40098, "経験値マイナス", "Exp Minus", ItemType.Material, OptionBaseType.MaterialStrength, 2192222891, 2281701376, OptionType.ExpMinus, 0.05f, 0, 0, true, "取得経験値が減少する。", "Exp value to be acquired decreases")
, new TableOptionCommonData(40099, "経験値プラス", "Exp Plus", ItemType.Material, OptionBaseType.MaterialStrength, 2281701376, 2371179861, OptionType.ExpPlus, 0.05f, 0, 0, true, "取得経験値が上昇する。", "Exp value to be acquired increases")
, new TableOptionCommonData(40100, "致命", "Fatal", ItemType.Material, OptionBaseType.MaterialStrength, 2371179861, 2460658347, OptionType.Fatal, 0.01f, 0, 0, true, "攻撃時に一定確率で敵のHPを1にする。", "At the time of attack, set the enemy's HP to 1 with constant probability.")
, new TableOptionCommonData(40101, "ガラス", "Glass", ItemType.Material, OptionBaseType.MaterialStrength, 2460658347, 2594876075, OptionType.Glass, 0.01f, 0, 0, true, "攻撃時や防御時に一定確率で壊れる。", "It breaks with a certain probability when attacking or defending.")
, new TableOptionCommonData(40102, "HPプラス", "HP Plus", ItemType.Material, OptionBaseType.MaterialStrength, 2594876075, 2952790016, OptionType.HP, 2f, 0, 0, true, "HPに付加値がつく。", "HP will increase.")
, new TableOptionCommonData(40103, "鉄壁", "Iron Wall", ItemType.Material, OptionBaseType.MaterialStrength, 2952790016, 3310703957, OptionType.IronWall, 0.5f, 0, 0, true, "敵に囲まれると防御力が上がる。", "Defense power rises when enemies are surrounded.")
, new TableOptionCommonData(40104, "ナルコレプシー", "Narcolepsy", ItemType.Material, OptionBaseType.MaterialStrength, 3310703957, 3489660928, OptionType.Narcolepsy, 0.01f, 0, 0, true, "一定確率で睡眠状態に陥る。", "Fall asleep with constant probability.")
, new TableOptionCommonData(40105, "樫の盾", "Oakenshield", ItemType.Material, OptionBaseType.MaterialStrength, 3489660928, 3579139413, OptionType.Oakenshield, 0.1f, 0, 0, true, "死亡するダメージを受けてもHP1で耐えることがある。", "Even if you receive the death damage it will withstand with HP1 with a certain probability.")
, new TableOptionCommonData(40106, "反射", "Reflection", ItemType.Material, OptionBaseType.MaterialStrength, 3579139413, 3668617898, OptionType.Reflection, 0.02f, 0, 0, true, "被弾時にダメージの一部を反射する。", "When receiving damage, it reflects part of the damage.")
, new TableOptionCommonData(40107, "犠牲", "Sacrifice", ItemType.Material, OptionBaseType.MaterialStrength, 3668617898, 3713357141, OptionType.Sacrifice, 0, 0, 0, true, "死亡時に装備していると一度だけアイテムを犠牲に復活できる。", "If you are equipped with this item at the time of death, you can revive only once at the expense of this.")
, new TableOptionCommonData(40108, "大食", "Gluttony", ItemType.Material, OptionBaseType.MaterialStrength, 3713357141, 3892314112, OptionType.SatBig, 0.05f, 0, 0, true, "おなかが減りやすくなる。", "The decrease in stomach faster.")
, new TableOptionCommonData(40109, "小食", "Diet", ItemType.Material, OptionBaseType.MaterialStrength, 3892314112, 4071271082, OptionType.SatSmall, 0.05f, 0, 0, true, "おなかが減りにくくなる。", "A stomach reduction slows down.")
, new TableOptionCommonData(40110, "HP自然回復増加", "HP walk recovery increase", ItemType.Material, OptionBaseType.MaterialStrength, 4071271082, 4250228053, OptionType.WalkRecover, 0.4f, 0, 0, true, "ターン毎のHP自然回復量が増加する。", "The amount of HP natural recovery per turn increases.")
, new TableOptionCommonData(40177, "接着", "Adhesion", ItemType.Material, OptionBaseType.MaterialStrength, 4250228053, 4294967295, OptionType.Adhesive, 0.2f, 0, 0, false, "装備するとくっついて離れなくなってしまう。", "It will become stuck when equipped.")
, new TableOptionCommonData(40111, "毒中和", "Recover Poison", ItemType.Food, OptionBaseType.Food, 0, 39045158, OptionType.DefenceAbnormal, 0.01f, 6, 0, false, "毒状態の回復効果がつく。", "Cure for poison, deadly poison.")
, new TableOptionCommonData(40112, "麻痺中和", "Recover Paralysis", ItemType.Food, OptionBaseType.Food, 39045158, 78090315, OptionType.DefenceAbnormal, 0.01f, 16, 0, false, "麻痺状態の回復効果がつく。", "Cure for paralysis.")
, new TableOptionCommonData(40113, "混乱中和", "Recover Confusion", ItemType.Food, OptionBaseType.Food, 78090315, 117135472, OptionType.DefenceAbnormal, 0.01f, 32, 0, false, "混乱状態の回復効果がつく。", "Cure for confusion.")
, new TableOptionCommonData(40114, "睡眠中和", "Recover Sleep", ItemType.Food, OptionBaseType.Food, 117135472, 156180629, OptionType.DefenceAbnormal, 0.01f, 8, 0, false, "睡眠状態の回復効果がつく。", "Cure for sleep.")
, new TableOptionCommonData(40115, "デコイ中和", "Recover Decoy", ItemType.Food, OptionBaseType.Food, 156180629, 195225787, OptionType.DefenceAbnormal, 0.01f, 128, 0, false, "デコイ状態の回復効果がつく。", "Cure for decoy.")
, new TableOptionCommonData(40116, "スロー中和", "Recover Slow", ItemType.Food, OptionBaseType.Food, 195225787, 234270944, OptionType.DefenceAbnormal, 0.01f, 256, 0, false, "スロー状態の回復効果がつく。", "Cure for slow.")
, new TableOptionCommonData(40117, "かぜ中和", "Recover Cold", ItemType.Food, OptionBaseType.Food, 234270944, 273316101, OptionType.DefenceAbnormal, 0.01f, 1024, 0, false, "かぜ状態の回復効果がつく。", "Cure for cold.")
, new TableOptionCommonData(40118, "肩こり中和", "Recover Stiff Shoulder", ItemType.Food, OptionBaseType.Food, 273316101, 312361258, OptionType.DefenceAbnormal, 0.01f, 2048, 0, false, "肩こり状態の回復効果がつく。", "Cure for stiff shoulder.")
, new TableOptionCommonData(40119, "状態効果反転", "Reverce Abnormal", ItemType.Food, OptionBaseType.Food, 312361258, 351406416, OptionType.ReverceAbnormal, 0f, 0, 0, true, "状態異常中和の効果が、回復から付与に変わる。", "The effect of abnormal state changes from cure to giving.")
, new TableOptionCommonData(40120, "基本効果反転", "Reverce Damage", ItemType.Food, OptionBaseType.Food, 351406416, 390451573, OptionType.ReverceDamage, 0f, 0, 0, true, "基本効果が反転する。回復ならダメージに。", "The basic effect of the item is reversed.")
, new TableOptionCommonData(40121, "効果増大", "Effect Up", ItemType.Food, OptionBaseType.Food, 390451573, 1171354717, OptionType.EffectUp, 5f, 0, 0, true, "効果が大きくなる。", "The effect is increased.")
, new TableOptionCommonData(40122, "効果縮小", "Effect Down", ItemType.Food, OptionBaseType.Food, 1171354717, 1952257862, OptionType.EffectDown, 5f, 0, 0, true, "効果が小さくなる。", "The effect is reduced.")
, new TableOptionCommonData(40123, "効果安定", "Effect Stabile", ItemType.Food, OptionBaseType.Food, 1952257862, 2733161006, OptionType.EffectStabile, 0.05f, 0, 0, true, "効果が安定する。", "The effect stabilizes.")
, new TableOptionCommonData(40124, "効果不安定", "Effect Stabile", ItemType.Food, OptionBaseType.Food, 2733161006, 3514064151, OptionType.EffectNotStabile, 0.05f, 0, 0, true, "効果が不安定になる。", "The effect becomes unstable.")
, new TableOptionCommonData(40179, "Power回復", "Recover Power", ItemType.Food, OptionBaseType.Food, 3514064151, 4294967295, OptionType.RecoverPower, 0f, 0, 0, false, "Power減少時にPowerが回復する", "Power recovers when Power decreases.")
, new TableOptionCommonData(40125, "毒中和", "Defence Abnormal", ItemType.Candy, OptionBaseType.Candy, 0, 39045158, OptionType.DefenceAbnormal, 0.01f, 6, 0, false, "毒状態の回復効果がつく。", "Cure for poison, deadly poison.")
, new TableOptionCommonData(40126, "麻痺中和", "Defence Abnormal", ItemType.Candy, OptionBaseType.Candy, 39045158, 78090315, OptionType.DefenceAbnormal, 0.01f, 16, 0, false, "麻痺状態の回復効果がつく。", "Cure for paralysis.")
, new TableOptionCommonData(40127, "混乱中和", "Defence Abnormal", ItemType.Candy, OptionBaseType.Candy, 78090315, 117135472, OptionType.DefenceAbnormal, 0.01f, 32, 0, false, "混乱状態の回復効果がつく。", "Cure for confusion.")
, new TableOptionCommonData(40128, "睡眠中和", "Defence Abnormal", ItemType.Candy, OptionBaseType.Candy, 117135472, 156180629, OptionType.DefenceAbnormal, 0.01f, 8, 0, false, "睡眠状態の回復効果がつく。", "Cure for sleep.")
, new TableOptionCommonData(40129, "デコイ中和", "Defence Abnormal", ItemType.Candy, OptionBaseType.Candy, 156180629, 195225787, OptionType.DefenceAbnormal, 0.01f, 128, 0, false, "デコイ状態の回復効果がつく。", "Cure for decoy.")
, new TableOptionCommonData(40130, "スロー中和", "Defence Abnormal", ItemType.Candy, OptionBaseType.Candy, 195225787, 234270944, OptionType.DefenceAbnormal, 0.01f, 256, 0, false, "スロー状態の回復効果がつく。", "Cure for slow.")
, new TableOptionCommonData(40131, "かぜ中和", "Defence Abnormal", ItemType.Candy, OptionBaseType.Candy, 234270944, 273316101, OptionType.DefenceAbnormal, 0.01f, 1024, 0, false, "かぜ状態の回復効果がつく。", "Cure for cold.")
, new TableOptionCommonData(40132, "肩こり中和", "Defence Abnormal", ItemType.Candy, OptionBaseType.Candy, 273316101, 312361258, OptionType.DefenceAbnormal, 0.01f, 2048, 0, false, "肩こり状態の回復効果がつく。", "Cure for stiff shoulder.")
, new TableOptionCommonData(40133, "状態効果反転", "State effect reversal", ItemType.Candy, OptionBaseType.Candy, 312361258, 351406416, OptionType.ReverceAbnormal, 0f, 0, 0, true, "状態異常中和の効果が、回復から付与に変わる。", "The effect of abnormal state changes from cure to giving.")
, new TableOptionCommonData(40134, "基本効果反転", "Basic effect reversal", ItemType.Candy, OptionBaseType.Candy, 351406416, 390451573, OptionType.ReverceDamage, 0f, 0, 0, true, "基本効果が反転する。回復ならダメージに。", "The basic effect of the item is reversed.")
, new TableOptionCommonData(40135, "効果増大", "Increase effect", ItemType.Candy, OptionBaseType.Candy, 390451573, 1171354717, OptionType.EffectUp, 3f, 0, 0, true, "効果が大きくなる。", "The effect is increased.")
, new TableOptionCommonData(40136, "効果縮小", "Decrease effect", ItemType.Candy, OptionBaseType.Candy, 1171354717, 1952257862, OptionType.EffectDown, 3f, 0, 0, true, "効果が小さくなる。", "The effect is reduced.")
, new TableOptionCommonData(40137, "効果安定", "Stable effect", ItemType.Candy, OptionBaseType.Candy, 1952257862, 2733161006, OptionType.EffectStabile, 0.05f, 0, 0, true, "効果が安定する。", "The effect stabilizes.")
, new TableOptionCommonData(40138, "効果不安定", "Instable Effect ", ItemType.Candy, OptionBaseType.Candy, 2733161006, 3514064151, OptionType.EffectNotStabile, 0.05f, 0, 0, true, "効果が不安定になる。", "The effect becomes unstable.")
, new TableOptionCommonData(40180, "Power回復", "Power recovery", ItemType.Candy, OptionBaseType.Candy, 3514064151, 4294967295, OptionType.RecoverPower, 0f, 0, 0, false, "Power減少時にPowerが回復する", "Power recovers when Power decreases.")
, new TableOptionCommonData(40139, "引火", "Ignition", ItemType.Ball, OptionBaseType.Ball, 0, 613566757, OptionType.CatchingFire, 0f, 0, 0, true, "発動時に火が付く。", "A fire strikes during activation.")
, new TableOptionCommonData(40140, "範囲拡大", "Extend range", ItemType.Ball, OptionBaseType.Ball, 613566757, 1227133513, OptionType.Wildfire, 0f, 0, 0, true, "発動時の効果範囲が拡大する。", "The range of effect at activation is expanded.")
, new TableOptionCommonData(40141, "基本効果反転", "Basic effect reversal", ItemType.Ball, OptionBaseType.Ball, 1227133513, 1840700270, OptionType.ReverceDamage, 0f, 0, 0, true, "基本効果が反転する。回復ならダメージに。", "The basic effect of the item is reversed.")
, new TableOptionCommonData(40142, "効果増大", "Increase effect", ItemType.Ball, OptionBaseType.Ball, 1840700270, 2454267026, OptionType.EffectUp, 3f, 0, 0, true, "効果が大きくなる。", "The effect is increased.")
, new TableOptionCommonData(40143, "効果縮小", "Decrease effect", ItemType.Ball, OptionBaseType.Ball, 2454267026, 3067833783, OptionType.EffectDown, 3f, 0, 0, true, "効果が小さくなる。", "The effect is increased.")
, new TableOptionCommonData(40144, "効果安定", "Stable effect", ItemType.Ball, OptionBaseType.Ball, 3067833783, 3681400539, OptionType.EffectStabile, 0.05f, 0, 0, true, "効果が安定する。", "The effect stabilizes.")
, new TableOptionCommonData(40145, "効果不安定", "Instable Effect ", ItemType.Ball, OptionBaseType.Ball, 3681400539, 4294967295, OptionType.EffectNotStabile, 0.05f, 0, 0, true, "効果が不安定になる。", "The effect becomes unstable.")


                };
                
                return _table;
            }
        }
    }

    public static BaseOption GetValue(long objNo)
    {
        TableOptionCommonData data = Array.Find(Table, i => i.ObjNo == objNo);
        BaseOption op = new BaseOption();
        AttachValue(op, data);
        op.Plus = 5;

        return op;
    }
    public static BaseOption GetValue(ItemType itype,OptionType otype,int state)
    {
        TableOptionCommonData data = Array.Find(Table, i => i.IType == itype && i.OpType == otype && i.ABState == state);
        if(CommonFunction.IsNull(data)== true)
        {
            return null;
        }
        BaseOption op = new BaseOption();
        AttachValue(op, data);

        return op;
    }

    public static BaseOption GetValue(OptionBaseType type, uint rnd,int start,float prob,float con)
    {
        TableOptionCommonData data = Array.Find(Table, i => i.ObType == type
                && i.Start <= rnd && rnd <= i.End);
        if (CommonFunction.IsNull(data) == true)
        {
            return null;
        }
        BaseOption op = new BaseOption();
        AttachValue(op, data);
        if(data.IsPlus == true)
        {
            op.Plus = CommonFunction.ConvergenceRandom(start, prob, con, 5);
        }
        return op;
    }

    private static BaseOption AttachValue(BaseOption op, TableOptionCommonData data)
    {
        op.Initialize();
        op.ObjNo = data.ObjNo;
        op.TargetItemType = data.IType;
        op.OType = data.OpType;
        op.CommonFloat = data.CommonFloat;
        op.AbnormalStateTarget = data.ABState;
        op.RaceTarget = data.Race;
        if (GameStateInformation.IsEnglish == false)
        {
            op.DisplayName = data.DName;
            op.Description = data.Description;
        }
        else
        {
            op.DisplayName = data.DNameEn;
            op.Description = data.DescriptionEn;
        }

        return op;
    }

    private class TableOptionCommonData
    {
        public TableOptionCommonData(ushort objno,
            string name,
            string nameEn,
            ItemType itype,
            OptionBaseType obType,
            uint start,
            uint end,
            OptionType otype,
            float commonFloat,
            int aBState,
            int race,
            bool isPlus,
            string desc,
            string descEn)
        {
            ObjNo = objno;
            DName = name;
            DNameEn = nameEn;
            IType = itype;
            ObType = obType;
            Start = start;
            End = end;
            OpType = otype;
            CommonFloat = commonFloat;
            ABState = aBState;
            Race = race;
            IsPlus = isPlus;
            Description = desc;
            DescriptionEn = descEn;
        }

        public ushort ObjNo;
        public string DName;
        public string DNameEn;
        public ItemType IType;
        public OptionBaseType ObType;
        public uint Start;
        public uint End;
        public OptionType OpType;
        public float CommonFloat;
        public int ABState;
        public int Race;
        public bool IsPlus;
        public string Description;
        public string DescriptionEn;
    }
}
