using Common;
using Common.LambdaOpertion;
using Common.Result;
using DbOpertion.Models;
using DbOpertion.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Cache
{
  public partial class Cache_Message:SingleTon<Cache_Message>
    {
        /// <summary>
        /// 显示消息信息列表
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="start">开始数据</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public Tuple<List<Message>, int, int> SelectMessageList(string SearchKey, string Key, int start, int PageSize, DataTablesOrderDir desc)
        {
            bool asc;
            if (desc == DataTablesOrderDir.Asc)
            {
                asc = true;
            }
            else
            {
                asc = false;
            }
            var list = MessageOper.Instance.SelectByPage(SearchKey, Key, start, PageSize, asc);
            var All_Count = MessageOper.Instance.SelectMessageCount(null);
            var Count = MessageOper.Instance.SelectMessageCount(SearchKey);
            return new Tuple<List<Message>, int, int>(list, All_Count, Count);
        }
        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="message">消息信息</param>
        /// <returns></returns>
        public bool InsertMessage(Message message)
        {
            //var Check_Message = MessageOper.Instance.SelectrMessageByName(message.MessageName,message.MessageContains);
            //if (Check_Message.Count > 0)
            //{
            //    return false;
            //}
            //else
            //{
                return MessageOper.Instance.Insert(message);
           // }
        }
        /// <summary>
        /// 更新消息内容
        /// </summary>
        /// <param name="message">消息信息</param>
        /// <returns></returns>
        public bool Update_Message(Message message)
        {
            return MessageOper.Instance.Update_Message(message);
        }
        /// <summary>
        /// 根据Id获得消息信息
        /// </summary>
        /// <param name="MessageID">消息ID</param>
        /// <returns></returns>
        public Message GetMessageInfoById(int MessageID)
        {
            var List_Message = MessageOper.Instance.SelectById(MessageID);
            Message message;
            if (List_Message != null && List_Message.Count != 0)
            {
                message = List_Message.FirstOrDefault();
            }
            else
            {
                message = null;
            }
            return message;
        }

        /// <summary>
        /// 根据Id多选删除数据
        /// </summary>
        /// <param name="KeyIds">Id</param>
        /// <returns></returns>
        public bool DeleteMessageByIds(List<int> KeyIds)
        {
            var flag = MessageOper.Instance.DeleteByIds(KeyIds);
            return flag;
        }
    }
}
