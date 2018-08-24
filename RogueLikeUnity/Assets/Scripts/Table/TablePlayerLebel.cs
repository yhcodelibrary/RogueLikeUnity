using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// レベルアップステータス管理テーブル
/// </summary>
public class TablePlayerLebel
{
    private static TablePlayerLebelData[] _table;
    private static TablePlayerLebelData[] table
    {
        get
        {
            if(_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TablePlayerLebelData[106];

                _table[0] = new TablePlayerLebelData(20, 0.1f, 2, 2, 3);
                _table[1] = new TablePlayerLebelData(21, 0.1f, 2, 2, 7);
                _table[2] = new TablePlayerLebelData(22, 0.11f, 2, 2, 13);
                _table[3] = new TablePlayerLebelData(24, 0.12f, 2, 2, 23);
                _table[4] = new TablePlayerLebelData(26, 0.13f, 3, 2, 35);
                _table[5] = new TablePlayerLebelData(28, 0.14f, 3, 2, 50);
                _table[6] = new TablePlayerLebelData(30, 0.15f, 3, 2, 69);
                _table[7] = new TablePlayerLebelData(32, 0.16f, 4, 2, 92);
                _table[8] = new TablePlayerLebelData(35, 0.17f, 4, 2, 119);
                _table[9] = new TablePlayerLebelData(38, 0.19f, 4, 2, 151);
                _table[10] = new TablePlayerLebelData(41, 0.2f, 5, 3, 188);
                _table[11] = new TablePlayerLebelData(44, 0.22f, 5, 3, 231);
                _table[12] = new TablePlayerLebelData(47, 0.23f, 5, 3, 280);
                _table[13] = new TablePlayerLebelData(50, 0.25f, 6, 3, 336);
                _table[14] = new TablePlayerLebelData(53, 0.26f, 6, 3, 399);
                _table[15] = new TablePlayerLebelData(57, 0.28f, 6, 3, 471);
                _table[16] = new TablePlayerLebelData(61, 0.3f, 7, 3, 552);
                _table[17] = new TablePlayerLebelData(65, 0.32f, 7, 3, 644);
                _table[18] = new TablePlayerLebelData(69, 0.34f, 7, 3, 746);
                _table[19] = new TablePlayerLebelData(73, 0.36f, 8, 4, 861);
                _table[20] = new TablePlayerLebelData(77, 0.38f, 8, 4, 989);
                _table[21] = new TablePlayerLebelData(81, 0.4f, 9, 4, 1131);
                _table[22] = new TablePlayerLebelData(85, 0.42f, 9, 4, 1290);
                _table[23] = new TablePlayerLebelData(89, 0.44f, 9, 4, 1467);
                _table[24] = new TablePlayerLebelData(93, 0.46f, 10, 4, 1664);
                _table[25] = new TablePlayerLebelData(97, 0.48f, 10, 4, 1882);
                _table[26] = new TablePlayerLebelData(101, 0.5f, 10, 4, 2125);
                _table[27] = new TablePlayerLebelData(104, 0.52f, 11, 5, 2393);
                _table[28] = new TablePlayerLebelData(107, 0.53f, 11, 5, 2690);
                _table[29] = new TablePlayerLebelData(110, 0.55f, 12, 5, 3019);
                _table[30] = new TablePlayerLebelData(113, 0.56f, 12, 5, 3383);
                _table[31] = new TablePlayerLebelData(116, 0.58f, 12, 5, 3786);
                _table[32] = new TablePlayerLebelData(119, 0.59f, 13, 5, 4230);
                _table[33] = new TablePlayerLebelData(122, 0.61f, 13, 5, 4721);
                _table[34] = new TablePlayerLebelData(125, 0.62f, 14, 6, 5263);
                _table[35] = new TablePlayerLebelData(128, 0.64f, 14, 6, 5861);
                _table[36] = new TablePlayerLebelData(131, 0.65f, 15, 6, 6521);
                _table[37] = new TablePlayerLebelData(134, 0.67f, 15, 6, 7249);
                _table[38] = new TablePlayerLebelData(137, 0.68f, 15, 6, 8052);
                _table[39] = new TablePlayerLebelData(140, 0.7f, 16, 6, 8938);
                _table[40] = new TablePlayerLebelData(143, 0.71f, 16, 6, 9913);
                _table[41] = new TablePlayerLebelData(146, 0.73f, 17, 7, 10989);
                _table[42] = new TablePlayerLebelData(149, 0.74f, 17, 7, 12173);
                _table[43] = new TablePlayerLebelData(152, 0.76f, 18, 7, 13479);
                _table[44] = new TablePlayerLebelData(155, 0.77f, 18, 7, 14916);
                _table[45] = new TablePlayerLebelData(158, 0.79f, 19, 7, 16500);
                _table[46] = new TablePlayerLebelData(161, 0.8f, 19, 7, 18244);
                _table[47] = new TablePlayerLebelData(164, 0.82f, 20, 8, 20164);
                _table[48] = new TablePlayerLebelData(167, 0.83f, 20, 8, 22279);
                _table[49] = new TablePlayerLebelData(170, 0.85f, 21, 8, 24606);
                _table[50] = new TablePlayerLebelData(173, 0.86f, 21, 8, 27169);
                _table[51] = new TablePlayerLebelData(176, 0.88f, 22, 8, 29990);
                _table[52] = new TablePlayerLebelData(179, 0.89f, 22, 8, 33095);
                _table[53] = new TablePlayerLebelData(182, 0.91f, 23, 9, 36512);
                _table[54] = new TablePlayerLebelData(185, 0.92f, 23, 9, 40274);
                _table[55] = new TablePlayerLebelData(188, 0.94f, 24, 9, 44413);
                _table[56] = new TablePlayerLebelData(191, 0.95f, 24, 9, 48968);
                _table[57] = new TablePlayerLebelData(194, 0.97f, 25, 9, 53981);
                _table[58] = new TablePlayerLebelData(197, 0.98f, 25, 9, 59497);
                _table[59] = new TablePlayerLebelData(200, 1f, 26, 10, 65566);
                _table[60] = new TablePlayerLebelData(202, 1.01f, 26, 10, 72245);
                _table[61] = new TablePlayerLebelData(204, 1.02f, 27, 10, 79594);
                _table[62] = new TablePlayerLebelData(206, 1.03f, 27, 10, 87679);
                _table[63] = new TablePlayerLebelData(208, 1.04f, 28, 10, 96575);
                _table[64] = new TablePlayerLebelData(210, 1.05f, 28, 10, 106362);
                _table[65] = new TablePlayerLebelData(212, 1.06f, 29, 11, 117130);
                _table[66] = new TablePlayerLebelData(214, 1.07f, 30, 11, 128977);
                _table[67] = new TablePlayerLebelData(216, 1.08f, 30, 11, 142011);
                _table[68] = new TablePlayerLebelData(218, 1.09f, 31, 11, 156350);
                _table[69] = new TablePlayerLebelData(220, 1.1f, 31, 11, 172125);
                _table[70] = new TablePlayerLebelData(222, 1.11f, 32, 12, 189479);
                _table[71] = new TablePlayerLebelData(224, 1.12f, 33, 12, 208571);
                _table[72] = new TablePlayerLebelData(226, 1.13f, 33, 12, 229574);
                _table[73] = new TablePlayerLebelData(228, 1.14f, 34, 12, 252680);
                _table[74] = new TablePlayerLebelData(230, 1.15f, 34, 12, 278097);
                _table[75] = new TablePlayerLebelData(232, 1.16f, 35, 13, 306059);
                _table[76] = new TablePlayerLebelData(234, 1.17f, 36, 13, 336819);
                _table[77] = new TablePlayerLebelData(236, 1.18f, 36, 13, 370657);
                _table[78] = new TablePlayerLebelData(238, 1.19f, 37, 13, 407881);
                _table[79] = new TablePlayerLebelData(240, 1.2f, 38, 14, 448829);
                _table[80] = new TablePlayerLebelData(242, 1.21f, 38, 14, 493873);
                _table[81] = new TablePlayerLebelData(244, 1.22f, 39, 14, 543425);
                _table[82] = new TablePlayerLebelData(246, 1.23f, 40, 14, 597933);
                _table[83] = new TablePlayerLebelData(248, 1.24f, 40, 14, 657894);
                _table[84] = new TablePlayerLebelData(250, 1.25f, 41, 15, 723854);
                _table[85] = new TablePlayerLebelData(252, 1.26f, 42, 15, 796411);
                _table[86] = new TablePlayerLebelData(254, 1.27f, 42, 15, 876226);
                _table[87] = new TablePlayerLebelData(256, 1.28f, 43, 15, 964025);
                _table[88] = new TablePlayerLebelData(258, 1.29f, 44, 16, 1060605);
                _table[89] = new TablePlayerLebelData(260, 1.3f, 45, 16, 1166845);
                _table[90] = new TablePlayerLebelData(262, 1.31f, 45, 16, 1283712);
                _table[91] = new TablePlayerLebelData(264, 1.32f, 46, 16, 1412267);
                _table[92] = new TablePlayerLebelData(266, 1.33f, 47, 17, 1553680);
                _table[93] = new TablePlayerLebelData(268, 1.34f, 48, 17, 1709236);
                _table[94] = new TablePlayerLebelData(270, 1.35f, 48, 17, 1880349);
                _table[95] = new TablePlayerLebelData(272, 1.36f, 49, 17, 2068576);
                _table[96] = new TablePlayerLebelData(274, 1.37f, 50, 18, 2275628);
                _table[97] = new TablePlayerLebelData(276, 1.38f, 51, 18, 2503386);
                _table[98] = new TablePlayerLebelData(278, 1.39f, 52, 18, 2753923);
                _table[99] = new TablePlayerLebelData(280, 1.4f, 52, 18, 3029515);
                _table[100] = new TablePlayerLebelData(282, 1.41f, 53, 19, 3332669);
                _table[101] = new TablePlayerLebelData(284, 1.42f, 54, 19, 3666140);
                _table[102] = new TablePlayerLebelData(286, 1.43f, 55, 19, 4032959);
                _table[103] = new TablePlayerLebelData(288, 1.44f, 56, 20, 4436463);
                _table[104] = new TablePlayerLebelData(290, 1.45f, 56, 20, 4880320);
                _table[105] = new TablePlayerLebelData(292, 1.46f, 57, 20, 5368563);


                return _table;
            }
        }
    }

    public static void SetLevel(PlayerCharacter player,int level)
    {
        TablePlayerLebelData next = table[level - 1];

        player.CurrentExperience -= player.NextLevelExperience;

        SetLevelInitialize(player, level);
    }

    public static void SetLevelInitialize(PlayerCharacter player, int level)
    {
        ScoreInformation.Info.iLevel = level;

        TablePlayerLebelData next = table[level - 1];
        player.Level = level;
        player.MaxHpDefault = next.MaxHp;
        player.CurrentHp = player.MaxHp;
        player.NextLevelExperience = next.NextLevelExperience;
        player.BaseAttack = next.BaseAttack;
        player.BaseDefense = next.BaseDefense;
        player.TrunRecoverHp = next.TrunRecoverHp;
    }


    private class TablePlayerLebelData
    {
        public TablePlayerLebelData(float maxhp,
            float trunRecoverHp,
            float baseAttack,
            float baseDefense,
            int nextLevelExperience)
        {
            MaxHp = maxhp;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            TrunRecoverHp = trunRecoverHp;
            NextLevelExperience = nextLevelExperience;
        }
        public float BaseAttack;
        public float BaseDefense;
        public float MaxHp;
        public float TrunRecoverHp;
        public int NextLevelExperience;
    }
}