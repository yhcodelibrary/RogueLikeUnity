//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//public class ManageUseItem
//{
//    public static Guid UseItemTemp;
//    public static Guid ParentDrive;
//    /// <summary>
//    /// どの動作を行うか
//    /// </summary>
//    public static MenuItemActionType ItemUseType;

//    public static void UseItem(PlayerCharacter player)
//    {
//        bool result;

//        //選択した項目によって分岐
//        switch (ItemUseType)
//        {
//            case MenuItemActionType.Eat:
//                //食べる
//                result = PlayerCharacter.ItemList[UseItemTemp].Eat(player);

//                //使用に成功したら対象をアイテムから削除
//                if(result == true)
//                {
//                    PlayerCharacter.ItemList.Remove(UseItemTemp);
//                }


//                break;
//            case MenuItemActionType.Equip:
//                //装備

//                //装備済みのものがあったら外す
//                Dictionary<Guid, BaseItem> equips = PlayerCharacter.ItemList.Where(
//                    i => i.Value.ItemType == PlayerCharacter.ItemList[UseItemTemp].ItemType
//                        && i.Value.IsEquip == true).ToDictionary(i => i.Key,i => i.Value);
//                if(equips.Count > 0)
//                {
//                    equips.Values.First().RemoveEquip();
//                }

//                //対象を装備する
//                PlayerCharacter.ItemList[UseItemTemp].Equip();

//                break;
//            case MenuItemActionType.Put:
//                //置く

//                //使用に成功したら対象をアイテムから削除
//                PlayerCharacter.ItemList.Remove(UseItemTemp);

//                break;
//            case MenuItemActionType.Putin:
//                //選択中だったアイテムを格納する
//                break;
//            case MenuItemActionType.RemoveEquip:
//                //外す

//                //対象を外す
//                PlayerCharacter.ItemList[UseItemTemp].RemoveEquip();
//                DisplayInformation.Info.AddMessage(
//                    string.Format(CommonConst.Message.RemoveEquipItem, PlayerCharacter.ItemList[UseItemTemp].DisplayName));
//                break;
//            case MenuItemActionType.Throw:
//                //投げる
//                break;
//            case MenuItemActionType.Use:
//                //使う
//                break;
//        }
//    }
//}
