using Common.Helper.SqlOpertion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Enum_My;

namespace Common.Helper.LambdaOpertion
{
    /// <summary>
    /// 表达式解析
    /// </summary>
    public class ExpressionAnalysis : SingleTon<ExpressionAnalysis>
    {
        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="isRight"></param>
        /// <returns></returns>
        public string RouteExpressionHandler(Expression exp, SqlOpertionHelper helper, Enum_Opertion opertion, ref List<IDbDataParameter> List_SqlParameter, bool isRight = false)
        {
            if (exp is BinaryExpression)
            {
                BinaryExpression be = (BinaryExpression)exp;
                //重新拆分树,形成递归
                return BinaryExpressionHandler(be.Left, be.Right, be.NodeType, helper, opertion, ref List_SqlParameter);
            }
            else if (exp is MemberExpression)
            {
                MemberExpression mExp = (MemberExpression)exp;
                if (isRight)//按表达式右边值
                {
                    var obj = Expression.Lambda(mExp).Compile().DynamicInvoke();
                    if (obj is Enum)
                    {
                        obj = (int)obj;
                    }
                    return obj + "";
                }
                return mExp.Member.Name;
            }
            else if (exp is NewArrayExpression)
            {
                #region 数组
                NewArrayExpression naExp = (NewArrayExpression)exp;
                StringBuilder sb = new StringBuilder();
                foreach (Expression expression in naExp.Expressions)
                {
                    sb.AppendFormat(",{0}", RouteExpressionHandler(expression, helper, opertion, ref List_SqlParameter));
                }
                return sb.Length == 0 ? "" : sb.Remove(0, 1).ToString();
                #endregion
            }
            else if (exp is MethodCallExpression)
            {
                MethodCallExpression cExp = (MethodCallExpression)exp;
                if (isRight)
                {
                    return Expression.Lambda(exp).Compile().DynamicInvoke() + "";
                }
                if (cExp.Method.Name == "Contains")
                {
                    if (cExp.Arguments.Count == 1)
                    {
                        var ParmName = cExp.Object.ToString();
                        var ParmLastName = ParmName.Split('.').Last();
                        string Search_Value = RouteExpressionHandler(cExp.Arguments[0], helper, opertion, ref List_SqlParameter, true);
                        var Count = List_SqlParameter.Where(p => p.ParameterName.Contains("@" + ParmLastName)).Count();
                        IDbDataParameter parameter = helper.GetParameter();
                        parameter.ParameterName = "@" + ParmLastName + Count;
                        parameter.Value = "%" + Search_Value + "%";
                        List_SqlParameter.Add(parameter);
                        return ParmLastName + " like @" + ParmLastName + Count;
                    }
                    else if (cExp.Arguments.Count == 2)
                    {
                        MemberExpression ConstantExp = cExp.Arguments[0] as MemberExpression;
                        var ParmLastName = ConstantExp.Member.Name;
                        string Search_Value = RouteExpressionHandler(cExp.Arguments[1], helper, opertion, ref List_SqlParameter, true);
                        var Count = List_SqlParameter.Where(p => p.ParameterName.Contains("@" + ParmLastName)).Count();
                        IDbDataParameter parameter = helper.GetParameter();
                        parameter.ParameterName = "@" + ParmLastName + Count;
                        parameter.Value = "%" + Search_Value + "%";
                        List_SqlParameter.Add(parameter);
                        return ParmLastName + " like @" + ParmLastName + Count;
                    }
                }
                else if (cExp.Method.Name == "ContainsIn")
                {
                    if (cExp.Arguments.Count == 1)
                    {
                        var ParmName = cExp.Object.ToString();
                        var ParmLastName = ParmName.Split('.').Last();
                        MemberExpression ConstantExp = cExp.Arguments[0] as MemberExpression;
                        var obj = (List<int>)Expression.Lambda(ConstantExp).Compile().DynamicInvoke();
                        StringBuilder builder = new StringBuilder();
                        if (obj != null && obj.Count > 0)
                        {
                            builder.Append(ParmLastName).Append(" in (");
                            foreach (var item in obj)
                            {
                                var Count = List_SqlParameter.Where(p => p.ParameterName.Contains("@" + ParmLastName)).Count();
                                IDbDataParameter parameter = helper.GetParameter();
                                parameter.ParameterName = "@" + ParmLastName + Count;
                                parameter.Value = item;
                                List_SqlParameter.Add(parameter);
                                builder.Append("@").Append(ParmLastName).Append(Count).Append(",");
                            }
                            builder.Remove(builder.Length - 1, 1).Append(") ");
                        }
                        else
                        {
                            builder.Append(ParmLastName).Append(" in (-1)");
                        }

                        return builder.ToString();
                    }
                    else if (cExp.Arguments.Count == 2)
                    {
                        MemberExpression NameExp = cExp.Arguments[0] as MemberExpression;
                        var ParmLastName = NameExp.Member.Name;
                        MemberExpression ConstantExp = cExp.Arguments[1] as MemberExpression;
                        var obj = (List<int>)Expression.Lambda(ConstantExp).Compile().DynamicInvoke();
                        StringBuilder builder = new StringBuilder();
                        if (obj != null && obj.Count > 0)
                        {
                            builder.Append(ParmLastName).Append(" in (");
                            foreach (var item in obj)
                            {
                                var Count = List_SqlParameter.Where(p => p.ParameterName.Contains("@" + ParmLastName)).Count();
                                IDbDataParameter parameter = helper.GetParameter();
                                parameter.ParameterName = "@" + ParmLastName + Count;
                                parameter.Value = item;
                                List_SqlParameter.Add(parameter);
                                builder.Append("@").Append(ParmLastName).Append(Count).Append(",");
                            }
                            builder.Remove(builder.Length - 1, 1).Append(") ");
                        }
                        else
                        {
                            builder.Append(ParmLastName).Append(" in (-1)");
                        }
                        return builder.ToString();
                    }
                }
                else if (cExp.Method.Name == "ContainsNotIn")
                {
                    if (cExp.Arguments.Count == 1)
                    {
                        var ParmName = cExp.Object.ToString();
                        var ParmLastName = ParmName.Split('.').Last();
                        MemberExpression ConstantExp = cExp.Arguments[0] as MemberExpression;
                        var obj = (List<int>)Expression.Lambda(ConstantExp).Compile().DynamicInvoke();
                        StringBuilder builder = new StringBuilder();
                        if (obj != null && obj.Count > 0)
                        {
                            builder.Append(ParmLastName).Append(" not in (");
                            foreach (var item in obj)
                            {
                                var Count = List_SqlParameter.Where(p => p.ParameterName.Contains("@" + ParmLastName)).Count();
                                IDbDataParameter parameter = helper.GetParameter();
                                parameter.ParameterName = "@" + ParmLastName + Count;
                                parameter.Value = item;
                                List_SqlParameter.Add(parameter);
                                builder.Append("@").Append(ParmLastName).Append(Count).Append(",");
                            }
                            builder.Remove(builder.Length - 1, 1).Append(") ");
                        }
                        else
                        {
                            builder.Append(ParmLastName).Append(" not in (-1)");
                        }
                        return builder.ToString();
                    }
                    else if (cExp.Arguments.Count == 2)
                    {
                        MemberExpression NameExp = cExp.Arguments[0] as MemberExpression;
                        var ParmLastName = NameExp.Member.Name;
                        MemberExpression ConstantExp = cExp.Arguments[1] as MemberExpression;
                        var obj = (List<int>)Expression.Lambda(ConstantExp).Compile().DynamicInvoke();
                        StringBuilder builder = new StringBuilder();
                        if (obj != null && obj.Count > 0)
                        {
                            builder.Append(ParmLastName).Append(" not in (");
                            foreach (var item in obj)
                            {
                                var Count = List_SqlParameter.Where(p => p.ParameterName.Contains("@" + ParmLastName)).Count();
                                IDbDataParameter parameter = helper.GetParameter();
                                parameter.ParameterName = "@" + ParmLastName + Count;
                                parameter.Value = item;
                                List_SqlParameter.Add(parameter);
                                builder.Append("@").Append(ParmLastName).Append(Count).Append(",");
                            }
                            builder.Remove(builder.Length - 1, 1).Append(") ");
                        }
                        else
                        {
                            builder.Append(ParmLastName).Append(" not in (-1)");
                        }
                        return builder.ToString();
                    }
                }
                //在这里解析方法
                throw new Exception("暂不支持");
            }
            else if (exp is ConstantExpression)
            {
                #region 常量
                ConstantExpression cExp = (ConstantExpression)exp;
                if (cExp.Value == null)
                    return "null";
                else
                {
                    SqlParameter parameter = new SqlParameter("cExp", cExp.Value);
                    return cExp.Value.ToString();
                }
                #endregion
            }
            else if (exp is UnaryExpression)
            {
                UnaryExpression ue = ((UnaryExpression)exp);
                return RouteExpressionHandler(ue.Operand, helper, opertion, ref List_SqlParameter, isRight);
            }
            return null;
        }

        /// <summary>
        /// 拆分表达式树
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string BinaryExpressionHandler(Expression left, Expression right, ExpressionType type, SqlOpertionHelper helper, Enum_Opertion opertion, ref List<IDbDataParameter> List_SqlParameter)
        {
            StringBuilder sb = new StringBuilder();
            if (opertion == Enum_Opertion.Where)
            {
                sb.Append("(");
            }
            string needParKey = "=,>,<,>=,<=,<>";
            string leftPar = RouteExpressionHandler(left, helper, opertion, ref List_SqlParameter);
            string typeStr = null;
            if (opertion == Enum_Opertion.Where)
            {
                typeStr = ExpressionTypeCast(type);
            }
            else if (opertion == Enum_Opertion.Set)
            {
                typeStr = ExpressionTypeCastSet(type);
            }
            var isRight = needParKey.IndexOf(typeStr) > -1;
            string rightPar = RouteExpressionHandler(right, helper, opertion, ref List_SqlParameter, isRight);
            if (!ContainsKey(leftPar) && !ContainsKey(rightPar))
            {
                var Count = List_SqlParameter.Where(p => p.ParameterName.Contains("@" + leftPar)).Count();
                IDbDataParameter parameter = helper.GetParameter();
                parameter.ParameterName = "@" + leftPar + Count;
                parameter.Value = rightPar;
                List_SqlParameter.Add(parameter);

                rightPar = "@" + leftPar + Count;
            }
            string appendLeft = leftPar;
            sb.Append(appendLeft);//字段名称
            if (rightPar.ToUpper() == "NULL")
            {
                if (typeStr == "=")
                    rightPar = " IS NULL ";
                else if (typeStr == "<>")
                    rightPar = " IS NOT NULL ";
            }
            else
            {
                sb.Append(typeStr);
            }
            sb.Append(rightPar);
            if (opertion == Enum_Opertion.Where)
            {
                sb.Append(")");
            }
            return sb.ToString();
        }


        /// <summary>
        /// 包含的主键
        /// </summary>
        /// <param name="Par"></param>
        /// <returns></returns>
        public bool ContainsKey(string Par)
        {
            if (Par == null)
            {
                return false;
            }
            Par = Par.ToUpper();
            if (Par.Contains("HTTPS://"))
            {
                return false;
            }

            if (Par.Contains("=") || Par.Contains("NULL") || Par.Contains(" IS NULL ") || Par.Contains(" IS NOT NULL ") || Par.Contains(" LIKE @"))
            {
                if (Par.Contains("<DIV>") || Par.Contains("<OL>") || Par.Contains("<UL>") || Par.Contains("<P>") || Par.Contains("</BODY>"))
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 处理符号
        /// </summary>
        /// <param name="expType"></param>
        /// <returns></returns>
        public string ExpressionTypeCast(ExpressionType expType)
        {
            switch (expType)
            {
                case ExpressionType.And:
                    return "&";
                case ExpressionType.AndAlso:
                    return " AND ";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                    return "|";
                case ExpressionType.OrElse:
                    return " OR ";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                default:
                    throw new InvalidCastException("不支持的运算符");
            }
        }

        /// <summary>
        /// 处理符号
        /// </summary>
        /// <param name="expType"></param>
        /// <returns></returns>
        public string ExpressionTypeCastSet(ExpressionType expType)
        {
            switch (expType)
            {
                case ExpressionType.And:
                    return " , ";
                case ExpressionType.AndAlso:
                    return " , ";
                case ExpressionType.Equal:
                    return "=";
                default:
                    throw new InvalidCastException("不支持的运算符");
            }
        }
    }
}
