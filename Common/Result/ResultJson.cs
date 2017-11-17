using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Result
{
    public class ResultJson
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }

    public class ResultJson<T1>
    {
        public ResultJson()
        {
            ListData = new List<T1>();
        }
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据列表1
        /// </summary>
        public List<T1> ListData { get; set; }
    }

    public class ResultJson<T1, T2>
    {
        public ResultJson()
        {
            ListData = new List<T1>();
            ListData2 = new List<T2>();
        }
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据列表1
        /// </summary>
        public List<T1> ListData { get; set; }
        /// <summary>
        /// 数据列表2
        /// </summary>
        public List<T2> ListData2 { get; set; }
    }

    public class ResultJson<T1, T2, T3>
    {
        public ResultJson()
        {
            ListData = new List<T1>();
            ListData2 = new List<T2>();
            ListData3 = new List<T3>();
        }
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据列表1
        /// </summary>
        public List<T1> ListData { get; set; }
        /// <summary>
        /// 数据列表2
        /// </summary>
        public List<T2> ListData2 { get; set; }
        /// <summary>
        /// 数据列表3
        /// </summary>
        public List<T3> ListData3 { get; set; }
    }

    public class ResultJson<T1, T2, T3, T4>
    {
        public ResultJson()
        {
            ListData = new List<T1>();
            ListData2 = new List<T2>();
            ListData3 = new List<T3>();
            ListData4 = new List<T4>();
        }
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据列表1
        /// </summary>
        public List<T1> ListData { get; set; }
        /// <summary>
        /// 数据列表2
        /// </summary>
        public List<T2> ListData2 { get; set; }
        /// <summary>
        /// 数据列表3
        /// </summary>
        public List<T3> ListData3 { get; set; }
        /// <summary>
        /// 数据列表4
        /// </summary>
        public List<T4> ListData4 { get; set; }
    }

    public class ResultJsonModel
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }

    public class ResultJsonModel<T1>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 模型1
        /// </summary>
        public T1 Model1 { get; set; }
    }

    public class ResultJsonModel<T1, T2>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 模型1
        /// </summary>
        public T1 Model1 { get; set; }
        /// <summary>
        /// 模型2
        /// </summary>
        public T2 Model2 { get; set; }
    }

    public class ResultJsonModel<T1, T2, T3>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 模型1
        /// </summary>
        public T1 Model1 { get; set; }
        /// <summary>
        /// 模型2
        /// </summary>
        public T2 Model2 { get; set; }
        /// <summary>
        /// 模型3
        /// </summary>
        public T3 Model3 { get; set; }
    }

    public class ResultJsonModel<T1, T2, T3, T4>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 模型1
        /// </summary>
        public T1 Model1 { get; set; }
        /// <summary>
        /// 模型2
        /// </summary>
        public T2 Model2 { get; set; }
        /// <summary>
        /// 模型3
        /// </summary>
        public T3 Model3 { get; set; }
        /// <summary>
        /// 模型4
        /// </summary>
        public T4 Model4 { get; set; }
    }

    public class ResultJsonModel<T1, T2, T3, T4, T5>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 模型1
        /// </summary>
        public T1 Model1 { get; set; }
        /// <summary>
        /// 模型2
        /// </summary>
        public T2 Model2 { get; set; }
        /// <summary>
        /// 模型3
        /// </summary>
        public T3 Model3 { get; set; }
        /// <summary>
        /// 模型4
        /// </summary>
        public T4 Model4 { get; set; }
        /// <summary>
        /// 模型5
        /// </summary>
        public T5 Model5 { get; set; }
    }

    public class ResultJsonModel<T1, T2, T3, T4, T5,T6>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 模型1
        /// </summary>
        public T1 Model1 { get; set; }
        /// <summary>
        /// 模型2
        /// </summary>
        public T2 Model2 { get; set; }
        /// <summary>
        /// 模型3
        /// </summary>
        public T3 Model3 { get; set; }
        /// <summary>
        /// 模型4
        /// </summary>
        public T4 Model4 { get; set; }
        /// <summary>
        /// 模型5
        /// </summary>
        public T5 Model5 { get; set; }
        /// <summary>
        /// 模型6
        /// </summary>
        public T6 Model6 { get; set; }
    }
}
