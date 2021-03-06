﻿/****************************************************************************
Copyright (c) 2013-2015 scutgame.com

http://www.scutgame.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using System;
using System.Data;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Common;
using ZyGames.Tianjiexing.Lang;
using ZyGames.Tianjiexing.Model;
using ZyGames.Tianjiexing.Model.Config;
using ZyGames.Tianjiexing.Model.Enum;

namespace ZyGames.Tianjiexing.BLL.Action
{

    /// <summary>
    /// 1455_法宝升级物品列表接口
    /// </summary>
    public class Action1455 : BaseAction
    {
        private UserItemInfo[] userItemArray = new UserItemInfo[0];

        public Action1455(ZyGames.Framework.Game.Contract.HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1455, httpGet)
        {

        }

        public override void BuildPacket()
        {
            this.PushIntoStack(userItemArray.Length);
            foreach (var item in userItemArray)
            {
                ItemBaseInfo itemInfo = new ConfigCacheSet<ItemBaseInfo>().FindKey(item.ItemID);
                DataStruct dsItem = new DataStruct();
                dsItem.PushIntoStack(item.UserItemID.ToNotNullString());
                dsItem.PushIntoStack(itemInfo == null ? string.Empty : itemInfo.ItemName.ToNotNullString());
                dsItem.PushIntoStack(itemInfo == null ? string.Empty : itemInfo.HeadID.ToNotNullString());
                dsItem.PushIntoStack(itemInfo == null ? (short)0 : itemInfo.QualityType.ToShort());
                dsItem.PushIntoStack(itemInfo == null ? 0 : itemInfo.EffectNum);
                this.PushIntoStack(dsItem);
            }
        }

        public override bool GetUrlElement()
        {
            if (true)
            {
                return true;
            }
        }

        public override bool TakeAction()
        {
            var package = UserItemPackage.Get(ContextUser.UserID);
            userItemArray = package.ItemPackage.FindAll(m => !m.IsRemove && m.ItemType == ItemType.DaoJu && m.PropType == 10).ToArray();
            if(userItemArray.Length==0)
            {
                ErrorCode = LanguageManager.GetLang().ErrorCode;
                ErrorInfo = LanguageManager.GetLang().St1456_UpTrumpItemNotEnough;
                return false;
            }
            return true;
        }
    }
}