using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class TableWeapon
{
    private static TableWeaponData[] _table;
    private static TableWeaponData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableWeaponData[] {

                     new TableWeaponData(20001, "すっぽん", "Plunger", 1, WeaponAppearanceType.Plunger, 0.92f, 1, 4, 0.97f, 0, 0.5f, 2f, 1, 0.4f, 5f, 1.5f, 0.01f, 0, 0.1f, 2f, "トイレのあれ。無いよりはましかも。" , "That in the toilet. It might be better than nothing.")
, new TableWeaponData(20002, "スティック", "Stick", 1, WeaponAppearanceType.HockeyStick, 0.92f, 1, 6, 0.95f, 0, 0.5f, 2f, 1, 0.4f, 5f, 1.5f, 0.01f, 0, 0.1f, 2f, "ホッケースティック。軽くて扱いやすい。" , "Hockey stick. It is light and easy to handle.")
, new TableWeaponData(20003, "金属バット", "Metal Bat", 1, WeaponAppearanceType.Bat, 0.92f, 1, 7, 0.94f, 0, 0.2f, 3f, 1, 0.4f, 5f, 1.5f, 0.01f, 0, 0.1f, 2f, "見た目より軽くて扱いやすいバット。" , "A bat lighter and easier to handle than it looks.")
, new TableWeaponData(20004, "魔法の杖", "Magic Wand", 1, WeaponAppearanceType.Staff, 0.92f, 1, 6, 0.95f, 0, 0.8f, 1.5f, 1, 0.4f, 5f, 1.5f, 0.01f, 0, 0.1f, 2f, "先端に水晶がついた杖。魔法は使えない。" , "A cane with crystal attached to the tip. You can not use magic.")
, new TableWeaponData(20005, "銀のナイフ", "Silver Knife", 1, WeaponAppearanceType.Knife, 0.92f, 1, 6, 0.95f, 0, 0.5f, 2f, 1, 0.4f, 2f, 1.5f, 0.01f, 0, 0.1f, 2f, "銀でできたナイフ。小さい分、軽くて扱いやすい。" , "A knife made of silver. Small, light and easy to handle.")
, new TableWeaponData(20006, "鉄の剣", "Iron Sword", 1, WeaponAppearanceType.Sword, 0.92f, 1, 9, 0.95f, 0, 0.5f, 2f, 1, 0.4f, 5f, 1.5f, 0.01f, 0, 0.5f, 2f, "スタンダードな鉄の剣。" , "A standard iron sword.")
, new TableWeaponData(20007, "クギバット", "Spiked Bat", 1, WeaponAppearanceType.Mace, 0.92f, 1, 10, 0.91f, 0, 0.5f, 2f, 1, 0.4f, 5f, 2f, 0.1f, 0, 0.5f, 2f, "クギが打ち込まれたバット。強そう。" , "A bat where a spike was struck. looks strong.")
, new TableWeaponData(20008, "魔斧マールブルグ", "Death Ax Marburg", 1, WeaponAppearanceType.Axe, 0.92f, 1, 20, 0.95f, 0, 0.8f, 1.5f, 1, 0.4f, 5f, 1.5f, 0.01f, 0, 0.9f, 1.2f, "かつて世界に脅威をもたらしたと伝えられる伝説の斧。振るった後には赤い十字が描かれるという。" , "Legendary ax that can be told that it brought a threat to the world long ago. It is said that a red cross is drawn after swing.")
, new TableWeaponData(20009, "すっぽん", "Plunger", 2, WeaponAppearanceType.Plunger, 0.92f, 1, 4, 0.97f, 3, 0.8f, 2f, 1, 0.8f, 5f, 1.5f, 0.01f, 0, 0.3f, 1.2f, "トイレのあれ。無いよりはましかも。" , "That in the toilet. It might be better than nothing.")
, new TableWeaponData(20010, "スティック", "Stick", 2, WeaponAppearanceType.HockeyStick, 0.92f, 1, 6, 0.95f, 0, 0.8f, 2f, 1, 0.8f, 5f, 1.5f, 0.01f, 0, 0.3f, 1.2f, "ホッケースティック。軽くて扱いやすい。" , "Hockey stick. It is light and easy to handle.")
, new TableWeaponData(20011, "金属バット", "Metal Bat", 2, WeaponAppearanceType.Bat, 0.92f, 1, 7, 0.94f, 0, 0.5f, 5f, 1, 0.8f, 5f, 1.5f, 0.01f, 0, 0.3f, 1.2f, "見た目より軽くて扱いやすいバット。" , "A bat lighter and easier to handle than it looks.")
, new TableWeaponData(20012, "魔法の杖", "Magic Wand", 2, WeaponAppearanceType.Staff, 0.92f, 1, 6, 0.95f, 0, 0.9f, 1.5f, 1, 0.8f, 5f, 1.5f, 0.01f, 0, 0.3f, 1.2f, "先端に水晶がついた杖。魔法は使えない。" , "A cane with crystal attached to the tip. You can not use magic.")
, new TableWeaponData(20013, "銀のナイフ", "Silver Knife", 2, WeaponAppearanceType.Knife, 0.92f, 1, 6, 0.95f, 0, 0.8f, 2f, 1, 0.8f, 2f, 1.5f, 0.01f, 0, 0.3f, 1.2f, "銀でできたナイフ。小さい分、軽くて扱いやすい。" , "A knife made of silver. Small, light and easy to handle.")
, new TableWeaponData(20014, "鉄の剣", "Iron Sword", 2, WeaponAppearanceType.Sword, 0.92f, 1, 9, 0.95f, 0, 0.8f, 2f, 1, 0.8f, 5f, 1.5f, 0.01f, 0, 0.7f, 2f, "スタンダードな鉄の剣。" , "A standard iron sword.")
, new TableWeaponData(20015, "クギバット", "Spiked Bat", 2, WeaponAppearanceType.Mace, 0.92f, 1, 10, 0.91f, 0, 0.8f, 2f, 1, 0.8f, 5f, 2f, 0.1f, 0, 0.7f, 2f, "クギが打ち込まれたバット。" , "A bat where a spike was struck. looks strong.")
, new TableWeaponData(20016, "魔斧マールブルグ", "Death Ax Marburg", 2, WeaponAppearanceType.Axe, 0.92f, 1, 20, 0.95f, 3, 0.8f, 2f, 1, 0.8f, 2f, 1.5f, 0.01f, 2, 0.9f, 1.2f, "かつて世界に脅威をもたらしたと伝えられる伝説の斧。振るった後には赤い十字が描かれるという。" , "Legendary ax that can be told that it brought a threat to the world long ago. It is said that a red cross is drawn after swing.")
, new TableWeaponData(20017, "すっぽん", "Plunger", 3, WeaponAppearanceType.Plunger, 0.92f, 1, 6, 0.97f, 1, 0.8f, 2f, 1, 0.8f, 3f, 1.5f, 0.01f, 0, 0.1f, 2f, "トイレのあれ。無いよりはましかも。" , "That in the toilet. It might be better than nothing.")
, new TableWeaponData(20018, "スティック", "Stick", 3, WeaponAppearanceType.HockeyStick, 0.92f, 1, 8, 0.95f, 1, 0.5f, 5f, 1, 0.8f, 3f, 1.5f, 0.01f, 0, 0.1f, 2f, "ホッケースティック。軽くて扱いやすい。" , "Hockey stick. It is light and easy to handle.")
, new TableWeaponData(20019, "金属バット", "Metal Bat", 3, WeaponAppearanceType.Bat, 0.92f, 1, 9, 0.94f, 1, 0.9f, 1.5f, 1, 0.8f, 3f, 1.5f, 0.01f, 0, 0.1f, 2f, "見た目より軽くて扱いやすいバット。" , "A bat lighter and easier to handle than it looks.")
, new TableWeaponData(20020, "魔法の杖", "Magic Wand", 3, WeaponAppearanceType.Staff, 0.92f, 1, 8, 0.95f, 1, 0.8f, 2f, 1, 0.8f, 3f, 1.5f, 0.01f, 0, 0.1f, 2f, "先端に水晶がついた杖。魔法は使えない。" , "A cane with crystal attached to the tip. You can not use magic.")
, new TableWeaponData(20021, "銀のナイフ", "Silver Knife", 3, WeaponAppearanceType.Knife, 0.92f, 1, 8, 0.95f, 1, 0.8f, 2f, 1, 0.8f, 1.7f, 1.5f, 0.01f, 0, 0.1f, 2f, "銀でできたナイフ。小さい分、軽くて扱いやすい。" , "A knife made of silver. Small, light and easy to handle.")
, new TableWeaponData(20022, "鉄の剣", "Iron Sword", 3, WeaponAppearanceType.Sword, 0.92f, 1, 10, 0.95f, 1, 0.8f, 2f, 1, 0.8f, 3f, 1.5f, 0.01f, 0, 0.5f, 2f, "スタンダードな鉄の剣。" , "A standard iron sword.")
, new TableWeaponData(20023, "クギバット", "Spiked Bat", 3, WeaponAppearanceType.Mace, 0.92f, 1, 12, 0.91f, 1, 0.4f, 3f, 1, 0.8f, 3f, 2f, 0.1f, 0, 0.5f, 2f, "クギが打ち込まれたバット。強そう。" , "A bat where a spike was struck. looks strong.")
, new TableWeaponData(20024, "魔斧マールブルグ", "Death Ax Marburg", 3, WeaponAppearanceType.Axe, 0.92f, 1, 25, 0.95f, 3, 0.9f, 1.5f, 1, 0.8f, 1.7f, 1.5f, 0.01f, 0, 0.9f, 1.2f, "かつて世界に脅威をもたらしたと伝えられる伝説の斧。振るった後には赤い十字が描かれるという。" , "Legendary ax that can be told that it brought a threat to the world long ago. It is said that a red cross is drawn after swing.")
, new TableWeaponData(20025, "すっぽん", "Plunger", 4, WeaponAppearanceType.Plunger, 0.92f, 1, 6, 0.97f, 1, 0.8f, 1.7f, 1, 0.8f, 3f, 1.5f, 0.01f, 0, 0.1f, 2f, "トイレのあれ。無いよりはましかも。" , "That in the toilet. It might be better than nothing.")
, new TableWeaponData(20026, "スティック", "Stick", 4, WeaponAppearanceType.HockeyStick, 0.92f, 1, 8, 0.95f, 1, 0.8f, 1.7f, 1, 0.8f, 3f, 1.5f, 0.01f, 0, 0.1f, 2f, "ホッケースティック。軽くて扱いやすい。" , "Hockey stick. It is light and easy to handle.")
, new TableWeaponData(20027, "金属バット", "Metal Bat", 4, WeaponAppearanceType.Bat, 0.92f, 1, 9, 0.94f, 1, 0.5f, 3f, 1, 0.8f, 3f, 1.5f, 0.01f, 0, 0.1f, 2f, "見た目より軽くて扱いやすいバット。" , "A bat lighter and easier to handle than it looks.")
, new TableWeaponData(20028, "魔法の杖", "Magic Wand", 4, WeaponAppearanceType.Staff, 0.92f, 1, 8, 0.95f, 1, 0.9f, 1.3f, 1, 0.8f, 3f, 1.5f, 0.01f, 0, 0.1f, 2f, "先端に水晶がついた杖。魔法は使えない。" , "A cane with crystal attached to the tip. You can not use magic.")
, new TableWeaponData(20029, "銀のナイフ", "Silver Knife", 4, WeaponAppearanceType.Knife, 0.92f, 1, 8, 0.95f, 1, 0.8f, 1.7f, 1, 0.8f, 1.7f, 1.5f, 0.01f, 0, 0.1f, 2f, "銀でできたナイフ。小さい分、軽くて扱いやすい。" , "A knife made of silver. Small, light and easy to handle.")
, new TableWeaponData(20030, "鉄の剣", "Iron Sword", 4, WeaponAppearanceType.Sword, 0.92f, 1, 10, 0.95f, 1, 0.8f, 1.7f, 1, 0.8f, 3f, 1.5f, 0.01f, 0, 0.5f, 2f, "スタンダードな鉄の剣。" , "A standard iron sword.")
, new TableWeaponData(20031, "クギバット", "Spiked Bat", 4, WeaponAppearanceType.Mace, 0.92f, 1, 12, 0.91f, 1, 0.8f, 1.7f, 1, 0.8f, 3f, 2f, 0.1f, 0, 0.5f, 2f, "クギが打ち込まれたバット。強そう。" , "A bat where a spike was struck. looks strong.")
, new TableWeaponData(20032, "魔斧マールブルグ", "Death Ax Marburg", 4, WeaponAppearanceType.Axe, 0.92f, 1, 25, 0.95f, 3, 0.9f, 1.3f, 1, 0.8f, 1.7f, 1.5f, 0.01f, 0, 0.9f, 1.2f, "かつて世界に脅威をもたらしたと伝えられる伝説の斧。振るった後には赤い十字が描かれるという。" , "Legendary ax that can be told that it brought a threat to the world long ago. It is said that a red cross is drawn after swing.")
, new TableWeaponData(20033, "すっぽん", "Plunger", 5, WeaponAppearanceType.Plunger, 0.92f, 1, 8, 0.97f, 1, 0.8f, 1.7f, 1, 0.8f, 5f, 1.5f, 0.01f, 0, 0.1f, 2f, "トイレのあれ。無いよりはましかも。" , "That in the toilet. It might be better than nothing.")
, new TableWeaponData(20034, "スティック", "Stick", 5, WeaponAppearanceType.HockeyStick, 0.92f, 1, 10, 0.95f, 1, 0.8f, 1.7f, 1, 0.8f, 5f, 1.5f, 0.01f, 0, 0.1f, 2f, "ホッケースティック。軽くて扱いやすい。" , "Hockey stick. It is light and easy to handle.")
, new TableWeaponData(20035, "金属バット", "Metal Bat", 5, WeaponAppearanceType.Bat, 0.92f, 1, 12, 0.94f, 1, 0.5f, 3f, 1, 0.8f, 5f, 1.5f, 0.01f, 0, 0.1f, 2f, "見た目より軽くて扱いやすいバット。" , "A bat lighter and easier to handle than it looks.")
, new TableWeaponData(20036, "魔法の杖", "Magic Wand", 5, WeaponAppearanceType.Staff, 0.92f, 1, 10, 0.95f, 1, 0.9f, 1.3f, 1, 0.8f, 5f, 1.5f, 0.01f, 0, 0.1f, 2f, "先端に水晶がついた杖。魔法は使えない。" , "A cane with crystal attached to the tip. You can not use magic.")
, new TableWeaponData(20037, "銀のナイフ", "Silver Knife", 5, WeaponAppearanceType.Knife, 0.92f, 1, 10, 0.95f, 1, 0.8f, 1.7f, 1, 0.8f, 1.7f, 1.5f, 0.01f, 0, 0.1f, 2f, "銀でできたナイフ。小さい分、軽くて扱いやすい。" , "A knife made of silver. Small, light and easy to handle.")
, new TableWeaponData(20038, "鉄の剣", "Iron Sword", 5, WeaponAppearanceType.Sword, 0.92f, 1, 12, 0.95f, 1, 0.8f, 1.7f, 1, 0.8f, 5f, 1.5f, 0.01f, 0, 0.5f, 2f, "スタンダードな鉄の剣。" , "A standard iron sword.")
, new TableWeaponData(20039, "クギバット", "Spiked Bat", 5, WeaponAppearanceType.Mace, 0.92f, 1, 15, 0.91f, 1, 0.8f, 1.7f, 1, 0.8f, 5f, 2f, 0.1f, 0, 0.5f, 2f, "クギが打ち込まれたバット。強そう。" , "A bat where a spike was struck. looks strong.")
, new TableWeaponData(20040, "魔斧マールブルグ", "Death Ax Marburg", 5, WeaponAppearanceType.Axe, 0.92f, 1, 28, 0.95f, 3, 0.9f, 1.3f, 1, 0.8f, 1.7f, 1.5f, 0.01f, 0, 0.9f, 1.2f, "かつて世界に脅威をもたらしたと伝えられる伝説の斧。振るった後には赤い十字が描かれるという。" , "Legendary ax that can be told that it brought a threat to the world long ago. It is said that a red cross is drawn after swing.")
, new TableWeaponData(20041, "すっぽん", "Plunger", 6, WeaponAppearanceType.Plunger, 0.92f, 1, 8, 0.97f, 1, 0.8f, 1.5f, 1, 0.8f, 2.5f, 1.5f, 0.01f, 0, 0.1f, 2f, "トイレのあれ。無いよりはましかも。" , "That in the toilet. It might be better than nothing.")
, new TableWeaponData(20042, "スティック", "Stick", 6, WeaponAppearanceType.HockeyStick, 0.92f, 1, 10, 0.95f, 1, 0.8f, 1.5f, 1, 0.8f, 2.5f, 1.5f, 0.01f, 0, 0.1f, 2f, "ホッケースティック。軽くて扱いやすい。" , "Hockey stick. It is light and easy to handle.")
, new TableWeaponData(20043, "金属バット", "Metal Bat", 6, WeaponAppearanceType.Bat, 0.92f, 1, 12, 0.94f, 1, 0.5f, 2f, 1, 0.8f, 2.5f, 1.5f, 0.01f, 0, 0.1f, 2f, "見た目より軽くて扱いやすいバット。" , "A bat lighter and easier to handle than it looks.")
, new TableWeaponData(20044, "魔法の杖", "Magic Wand", 6, WeaponAppearanceType.Staff, 0.92f, 1, 10, 0.95f, 1, 0.9f, 1.15f, 1, 0.8f, 2.5f, 1.5f, 0.01f, 0, 0.1f, 2f, "先端に水晶がついた杖。魔法は使えない。" , "A cane with crystal attached to the tip. You can not use magic.")
, new TableWeaponData(20045, "銀のナイフ", "Silver Knife", 6, WeaponAppearanceType.Knife, 0.92f, 1, 10, 0.95f, 1, 0.8f, 1.5f, 1, 0.8f, 1.5f, 1.5f, 0.01f, 0, 0.1f, 2f, "銀でできたナイフ。小さい分、軽くて扱いやすい。" , "A knife made of silver. Small, light and easy to handle.")
, new TableWeaponData(20046, "鉄の剣", "Iron Sword", 6, WeaponAppearanceType.Sword, 0.92f, 1, 12, 0.95f, 1, 0.8f, 1.5f, 1, 0.8f, 2.5f, 1.5f, 0.01f, 0, 0.5f, 2f, "スタンダードな鉄の剣。" , "A standard iron sword.")
, new TableWeaponData(20047, "クギバット", "Spiked Bat", 6, WeaponAppearanceType.Mace, 0.92f, 1, 15, 0.91f, 1, 0.8f, 1.5f, 1, 0.8f, 2.5f, 2f, 0.1f, 0, 0.5f, 2f, "クギが打ち込まれたバット。強そう。" , "A bat where a spike was struck. looks strong.")
, new TableWeaponData(20048, "魔斧マールブルグ", "Death Ax Marburg", 6, WeaponAppearanceType.Axe, 0.92f, 1, 28, 0.95f, 3, 0.9f, 1.15f, 1, 0.8f, 1.5f, 1.5f, 0.01f, 0, 0.9f, 1.2f, "かつて世界に脅威をもたらしたと伝えられる伝説の斧。振るった後には赤い十字が描かれるという。" , "Legendary ax that can be told that it brought a threat to the world long ago. It is said that a red cross is drawn after swing.")
, new TableWeaponData(20049, "すっぽん", "Plunger", 7, WeaponAppearanceType.Plunger, 0.92f, 1, 10, 0.97f, 1, 0.8f, 1.5f, 1, 0.8f, 2.5f, 1.5f, 0.01f, 0, 0.1f, 2f, "トイレのあれ。無いよりはましかも。" , "That in the toilet. It might be better than nothing.")
, new TableWeaponData(20050, "スティック", "Stick", 7, WeaponAppearanceType.HockeyStick, 0.92f, 1, 12, 0.95f, 1, 0.8f, 1.5f, 1, 0.8f, 2.5f, 1.5f, 0.01f, 0, 0.1f, 2f, "ホッケースティック。軽くて扱いやすい。" , "Hockey stick. It is light and easy to handle.")
, new TableWeaponData(20051, "金属バット", "Metal Bat", 7, WeaponAppearanceType.Bat, 0.92f, 1, 14, 0.94f, 1, 0.5f, 1.7f, 1, 0.8f, 2.5f, 1.5f, 0.01f, 0, 0.1f, 2f, "見た目より軽くて扱いやすいバット。" , "A bat lighter and easier to handle than it looks.")
, new TableWeaponData(20052, "魔法の杖", "Magic Wand", 7, WeaponAppearanceType.Staff, 0.92f, 1, 12, 0.95f, 1, 0.9f, 1.15f, 1, 0.8f, 2.5f, 1.5f, 0.01f, 0, 0.1f, 2f, "先端に水晶がついた杖。魔法は使えない。" , "A cane with crystal attached to the tip. You can not use magic.")
, new TableWeaponData(20053, "銀のナイフ", "Silver Knife", 7, WeaponAppearanceType.Knife, 0.92f, 1, 12, 0.95f, 1, 0.8f, 1.5f, 1, 0.8f, 1.5f, 1.5f, 0.01f, 0, 0.1f, 2f, "銀でできたナイフ。小さい分、軽くて扱いやすい。" , "A knife made of silver. Small, light and easy to handle.")
, new TableWeaponData(20054, "鉄の剣", "Iron Sword", 7, WeaponAppearanceType.Sword, 0.92f, 1, 15, 0.95f, 1, 0.8f, 1.5f, 1, 0.8f, 2.5f, 1.5f, 0.01f, 0, 0.5f, 2f, "スタンダードな鉄の剣。" , "A standard iron sword.")
, new TableWeaponData(20055, "クギバット", "Spiked Bat", 7, WeaponAppearanceType.Mace, 0.92f, 1, 19, 0.91f, 1, 0.8f, 1.5f, 1, 0.8f, 2.5f, 2f, 0.1f, 0, 0.5f, 2f, "クギが打ち込まれたバット。強そう。" , "A bat where a spike was struck. looks strong.")
, new TableWeaponData(20056, "魔斧マールブルグ", "Death Ax Marburg", 7, WeaponAppearanceType.Axe, 0.92f, 1, 33, 0.95f, 3, 0.9f, 1.15f, 1, 0.8f, 1.5f, 1.5f, 0.01f, 0, 0.9f, 1.2f, "かつて世界に脅威をもたらしたと伝えられる伝説の斧。振るった後には赤い十字が描かれるという。" , "Legendary ax that can be told that it brought a threat to the world long ago. It is said that a red cross is drawn after swing.")
, new TableWeaponData(20057, "すっぽん", "Plunger", 8, WeaponAppearanceType.Plunger, 0.92f, 1, 10, 0.97f, 1, 0.8f, 1.25f, 1, 0.8f, 2f, 1.5f, 0.01f, 0, 0.1f, 2f, "トイレのあれ。無いよりはましかも。" , "That in the toilet. It might be better than nothing.")
, new TableWeaponData(20058, "スティック", "Stick", 8, WeaponAppearanceType.HockeyStick, 0.92f, 1, 12, 0.95f, 1, 0.8f, 1.25f, 1, 0.8f, 2f, 1.5f, 0.01f, 0, 0.1f, 2f, "ホッケースティック。軽くて扱いやすい。" , "Hockey stick. It is light and easy to handle.")
, new TableWeaponData(20059, "金属バット", "Metal Bat", 8, WeaponAppearanceType.Bat, 0.92f, 1, 14, 0.94f, 1, 0.5f, 1.7f, 1, 0.8f, 2f, 1.5f, 0.01f, 0, 0.1f, 2f, "見た目より軽くて扱いやすいバット。" , "A bat lighter and easier to handle than it looks.")
, new TableWeaponData(20060, "魔法の杖", "Magic Wand", 8, WeaponAppearanceType.Staff, 0.92f, 1, 12, 0.95f, 1, 0.9f, 1.05f, 1, 0.8f, 2f, 1.5f, 0.01f, 0, 0.1f, 2f, "先端に水晶がついた杖。魔法は使えない。" , "A cane with crystal attached to the tip. You can not use magic.")
, new TableWeaponData(20061, "銀のナイフ", "Silver Knife", 8, WeaponAppearanceType.Knife, 0.92f, 1, 12, 0.95f, 1, 0.8f, 1.25f, 1, 0.8f, 1.3f, 1.5f, 0.01f, 0, 0.1f, 2f, "銀でできたナイフ。小さい分、軽くて扱いやすい。" , "A knife made of silver. Small, light and easy to handle.")
, new TableWeaponData(20062, "鉄の剣", "Iron Sword", 8, WeaponAppearanceType.Sword, 0.92f, 1, 15, 0.95f, 1, 0.8f, 1.25f, 1, 0.8f, 2f, 1.5f, 0.01f, 0, 0.5f, 2f, "スタンダードな鉄の剣。" , "A standard iron sword.")
, new TableWeaponData(20063, "クギバット", "Spiked Bat", 8, WeaponAppearanceType.Mace, 0.92f, 1, 19, 0.91f, 1, 0.8f, 1.25f, 1, 0.8f, 2f, 2f, 0.1f, 0, 0.5f, 2f, "クギが打ち込まれたバット。強そう。" , "A bat where a spike was struck. looks strong.")
, new TableWeaponData(20064, "魔斧マールブルグ", "Death Ax Marburg", 8, WeaponAppearanceType.Axe, 0.92f, 1, 33, 0.95f, 3, 0.9f, 1.05f, 1, 0.8f, 1.3f, 1.5f, 0.01f, 0, 0.9f, 1.2f, "かつて世界に脅威をもたらしたと伝えられる伝説の斧。振るった後には赤い十字が描かれるという。" , "Legendary ax that can be told that it brought a threat to the world long ago. It is said that a red cross is drawn after swing.")

                };


                return _table;
            }
        }
    }
    public static WeaponBase GetItem(long objNo,bool isRandomValue)
    {
        TableWeaponData data = Array.Find(Table, i => i.ObjNo == objNo);
        WeaponBase item = new WeaponBase();
        item.Initialize();
        item.ObjNo = data.ObjNo;
        if(GameStateInformation.IsEnglish == false)
        {
            item.DisplayName = data.DisplayName;
            item.Description = data.Description;
        }
        else
        {
            item.DisplayName = data.DisplayNameEn;
            item.Description = data.DescriptionEn;
        }
        item.ApType = data.AppType;
        item.ThrowDexterity = data.ThrowDexterity;
        item.WeaponBaseAttack = data.BaseAttack;
        item.WeaponBaseDexterity = data.Dexterity;
        item.WeaponBaseCritical = data.Critical;
        item.WeaponBaseDexterityCritical = data.Critical;
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
            if(index > 30)
            {
                break;
            }
            index++;
            uint rnd = CommonFunction.GetRandomUInt32();
            BaseOption newOpt = TableOptionCommon.GetValue(OptionBaseType.Weapon, rnd, data.OptionPowStart, data.OptionPowContinue, data.OptionPowReduce);

            //同じオプションがすでに含まれていたらもう一度算出
            if (CommonFunction.IsNull(newOpt) == true)
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

    private class TableWeaponData
    {
        public TableWeaponData(ushort objNo,
            string displayName,
            string displayNameEn,
            int level,
            WeaponAppearanceType appType,
            float throwDexterity,
            int range,
            float baseAttack,
            float dexterity,
            int optionAddStart,
            float optionAddContinue,
            float optionAddReduce,
            int optionPowStart,
            float optionPowContinue,
            float optionPowReduce,
            float critical,
            float criticalDexterity,
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
            AppType = appType;
            ThrowDexterity = throwDexterity;
            Range = range;
            BaseAttack = baseAttack;
            Dexterity = dexterity;
            OptionAddStart = optionAddStart;
            OptionAddContinue = optionAddContinue;
            OptionAddReduce = optionAddReduce;
            OptionPowStart = optionPowStart;
            OptionPowContinue = optionPowContinue;
            OptionPowReduce = optionPowReduce;
            Critical = critical;
            CriticalDexterity = criticalDexterity;
            StrengthAddStart = strengthAddStart;
            StrengthAddContinue = strengthAddContinue;
            StrengthnAddReduce = strengthnAddReduce;
            Description = description;
            DescriptionEn = descriptionEn;
        }
        public ushort ObjNo;
        public int Level;
        public string DisplayName;
        public string DisplayNameEn;
        public WeaponAppearanceType AppType;
        public float ThrowDexterity;
        public int Range;
        public float BaseAttack;
        public float Dexterity;
        public int OptionAddStart;
        public float OptionAddContinue;
        public float OptionAddReduce;
        public int OptionPowStart;
        public float OptionPowContinue;
        public float OptionPowReduce;
        public float Critical;
        public float CriticalDexterity;
        public int StrengthAddStart;
        public float StrengthAddContinue;
        public float StrengthnAddReduce;
        public string Description;
        public string DescriptionEn;
    }
}
