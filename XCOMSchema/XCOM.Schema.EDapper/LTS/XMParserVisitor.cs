using Dapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.Realization;
using XCOM.Schema.Standard.Utility;

namespace XCOM.Schema.EDapper.LTS
{
    internal class XMParserVisitor : ExpressionVisitor
    {
        /// <summary>
        /// 参数对象
        /// </summary>
        public DynamicParameters Parameters { get; set; }
        /// <summary>
        /// 默认MySql
        /// </summary>
        private readonly XMProviderType DbType = XMProviderType.MySql;
        public XMParserVisitor(XMProviderType dbType)
        {
            Parameters = new DynamicParameters();
            DbType = dbType;
        }

        /// <summary>
        /// 条件连接
        /// </summary>
        private readonly Stack<ExpressionType> _conditionLink = new();
        /// <summary>
        /// 等式连接
        /// </summary>
        private readonly Stack<string> _equationLink = new();
        /// <summary>
        /// 等式字段
        /// </summary>
        private readonly Stack<string> _fieldCondition = new();
        /// <summary>
        /// 等式值
        /// </summary>
        private readonly Stack<object> _fieldValue = new();
        /// <summary>
        /// Where条件
        /// </summary>
        private readonly Stack<string> _whereCondition = new();
        /// <summary>
        /// 方法结果值
        /// </summary>
        private readonly Stack<object> _methodResultValue = new();
        /// <summary>
        /// 是否参数
        /// </summary>
        private bool _isParameter = false;

        /// <summary>
        /// 是否参数列表
        /// </summary>
        private readonly Stack<bool> _isParamCondition = new();


        public string GetConditionSql()
        {
            if (this._whereCondition.Count != 1)
            {
                throw new Exception("没有解析处查询语句");
            }
            if (this._conditionLink.Count > 0)
            {
                throw new Exception($"还有表达式类型【{string.Join(',', this._conditionLink.ToArray())}】未解析");
            }
            return this._whereCondition.Pop();
        }

        #region 私有方法
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string NodeTypeConvert(ExpressionType type)
        {
            return type switch
            {
                ExpressionType.AndAlso => " ({0}) and ({1}) ",
                ExpressionType.OrElse => " ({0}) or ({1}) ",
                ExpressionType.Equal => " {0}={1} ",
                ExpressionType.GreaterThan => " {0}>{1} ",
                ExpressionType.GreaterThanOrEqual => " {0}>={1} ",
                ExpressionType.LessThan => " {0}<{1} ",
                ExpressionType.LessThanOrEqual => " {0}<={1} ",
                ExpressionType.NotEqual => " {0}<>{1} ",
                _ => null,
            };
        }
        /// <summary>
        /// 是否条件连接
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsConditon(ExpressionType type)
        {
            return type == ExpressionType.AndAlso || type == ExpressionType.OrElse;
        }
        /// <summary>
        /// 是否等式连接
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsEquation(ExpressionType type)
        {
            return type == ExpressionType.Equal || type == ExpressionType.GreaterThan
                || type == ExpressionType.GreaterThanOrEqual || type == ExpressionType.LessThan
                || type == ExpressionType.LessThanOrEqual || type == ExpressionType.NotEqual;
        }
        /// <summary>
        /// 是否运算
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsOperation(ExpressionType type)
        {
            return type == ExpressionType.Add || type == ExpressionType.AddAssign || type == ExpressionType.AddAssignChecked || type == ExpressionType.AddChecked || type == ExpressionType.And || type == ExpressionType.AndAssign
                || type == ExpressionType.ArrayIndex || type == ExpressionType.Coalesce || type == ExpressionType.Conditional
                || type == ExpressionType.Divide || type == ExpressionType.DivideAssign
                || type == ExpressionType.ExclusiveOr || type == ExpressionType.ExclusiveOrAssign
                || type == ExpressionType.Increment
                || type == ExpressionType.LeftShift || type == ExpressionType.LeftShiftAssign

                || type == ExpressionType.Modulo || type == ExpressionType.ModuloAssign
                || type == ExpressionType.Multiply || type == ExpressionType.MultiplyAssign
                || type == ExpressionType.MultiplyAssignChecked || type == ExpressionType.MultiplyChecked
                || type == ExpressionType.Negate || type == ExpressionType.NegateChecked
                 || type == ExpressionType.OnesComplement
                 || type == ExpressionType.PostDecrementAssign || type == ExpressionType.PostIncrementAssign
                 || type == ExpressionType.Power || type == ExpressionType.PowerAssign
                 || type == ExpressionType.PreDecrementAssign || type == ExpressionType.PreIncrementAssign
                 || type == ExpressionType.RightShift || type == ExpressionType.RightShiftAssign
                 || type == ExpressionType.Subtract || type == ExpressionType.SubtractAssign
                 || type == ExpressionType.SubtractAssignChecked || type == ExpressionType.SubtractChecked
                 || type == ExpressionType.UnaryPlus
                ;
        }
        /// <summary>
        /// 是否转换
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsConvert(ExpressionType type)
        {
            return type == ExpressionType.Convert || type == ExpressionType.ConvertChecked;
        }
        /// <summary>
        /// 判定操作符是否可以解析
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private static bool IsOperationConvert(string methodName)
        {
            return methodName switch
            {
                "Not" => true,
                "StartsWith" => true,
                "EndsWith" => true,
                "Contains" => true,
                "Equals" => true,
                "IsNullOrEmpty" => true,
                "IsNullOrWhiteSpace" => true,
                _ => false,
            };
        }
        /// <summary>
        /// 操作符转换
        /// </summary>
        /// <param name="methodName"></param>
        private int OperationConvert(string methodName)
        {
            var resultData = 2;
            if (methodName.Equals("Contains")
                && this._isParamCondition.Count > 0 && this._isParamCondition.Peek())
            {
                methodName = "XMIn";
            }
            if (methodName.Equals("IsNullOrWhiteSpace") || methodName.Equals("IsNullOrEmpty"))
            {
                resultData = 1;
            }
            switch (methodName)
            {
                case "Not":
                    this._equationLink.Push("Not");
                    break;
                default:
                    var isNegate = false;
                    if (this._conditionLink.Count > 0 && this._conditionLink.Peek() == ExpressionType.Not)
                    {
                        this._conditionLink.Pop();
                        isNegate = true;
                    }
                    var parser = XMRealization.GetPolymorphism(this.DbType);
                    var format = parser.MethodAnalysis(methodName, isNegate);
                    if (!string.IsNullOrWhiteSpace(format))
                    {
                        this._equationLink.Push(format);
                    }
                    break;
            }
            return resultData;
        }
        /// <summary>
        /// 获取参数名称
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        private string GetParameter(string paramName)
        {
            var validParamName = paramName;
            if (paramName.StartsWith("@"))
            {
                _ = validParamName.TrimStart('@');
            }
            var pattern = $"^{validParamName}[0-9]*$";
            if (this.Parameters.ParameterNames.Any(e => Regex.IsMatch(e, pattern)))
            {
                validParamName = $"{validParamName}{this.Parameters.ParameterNames.Where(e => Regex.IsMatch(e, pattern)).Count() - 1}";
            }
            return $"@{validParamName}";
        }
        /// <summary>
        /// 构造条件
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private string StructureCondition(string first, string second, string format)
        {
            var paramName = string.Empty;
            var isParamList = false;
            if (this._isParamCondition.Count > 0)
            {
                isParamList = this._isParamCondition.Pop();
            }
            if (isParamList)
            {
                // 如果是列表，构建sql为in条件，值肯定在后面
                if (first.Equals("@"))
                {
                    paramName = GetParameter(second);
                    first = second;
                    second = paramName;
                }
                else
                {
                    paramName = second = GetParameter(first);
                }
            }
            else
            {
                if (first.Equals("@"))
                {
                    paramName = first = GetParameter(second);
                }
                else if (second.Equals("@"))
                {
                    paramName = second = GetParameter(first);
                }
            }
            this._whereCondition.Push(string.Format(format, first, second));
            return paramName;
        }
        /// <summary>
        /// 设置结果值
        /// </summary>
        /// <param name="value"></param>
        private void SetFinallyValue(object value)
        {
            // 重新计算值，删除之前的计算
            for (var i = 0; i < this._fieldValue.Count; i++)
            {
                this._fieldValue.Pop();
                this._fieldCondition.Pop();
            }
            if (value.GetType().Name == typeof(List<>).Name)
            {
                this._isParamCondition.Push(true);
            }
            this._fieldValue.Push(value);
            this._fieldCondition.Push("@");
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg"></param>
        private static void Log(string msg)
        {
            //XMLog.Info(msg);
        }

        #endregion

        [return: NotNullIfNotNull("node")]
        public override Expression Visit(Expression node)
        {
            Log($"{Environment.NewLine}访问了 Visit{Environment.NewLine}内容：{node}");
            this._isParameter = false;
            base.Visit(node);
            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitBinary{Environment.NewLine}内容：{node}");
            if (IsConditon(node.NodeType) || IsEquation(node.NodeType))
            {
                this._conditionLink.Push(node.NodeType);
                this.Visit(node.Left);
                this.Visit(node.Right);
                string rightContent;
                string leftContent;
                if (IsConditon(node.NodeType))
                {
                    rightContent = this._whereCondition.Pop();
                    leftContent = this._whereCondition.Pop();
                }
                else
                {
                    rightContent = this._fieldCondition.Pop();
                    leftContent = this._fieldCondition.Pop();
                }
                var paramName = this.StructureCondition(leftContent, rightContent, NodeTypeConvert(this._conditionLink.Pop()));
                if (!IsConditon(node.NodeType))
                {
                    this.Parameters.Add(paramName, this._fieldValue.Pop());
                }
                return node;
            }
            else if (IsOperation(node.NodeType))
            {
                LambdaExpression lambda = Expression.Lambda(node);
                var value = lambda.Compile().DynamicInvoke(null);
                this.SetFinallyValue(value);
                return node;
            }
            else
            {
                return base.VisitBinary(node);
            }

        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitBlock{Environment.NewLine}内容：{node}");
            return base.VisitBlock(node);
        }

        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            Log($"{Environment.NewLine} 访问了 VisitCatchBlock{Environment.NewLine}内容：{node}");
            return base.VisitCatchBlock(node);
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitConditional{Environment.NewLine}内容：{node}");
            return base.VisitConditional(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitConstant{Environment.NewLine}内容：{node}");
            if (node.Type.BaseType == typeof(Enum))
            {
                var enumConstant = Expression.Constant(node.Value, node.Type);
                var convert = Expression.Convert(enumConstant, typeof(int));
                LambdaExpression lambda = Expression.Lambda(convert);
                var enumValue = (int)lambda.Compile().DynamicInvoke(null);
                this._methodResultValue.Push(enumValue);
                return node;
            }
            else if (node.Type == typeof(Boolean))
            {
                var enumConstant = Expression.Constant(node.Value, node.Type);
                var convert = Expression.Convert(enumConstant, typeof(int));
                LambdaExpression lambda = Expression.Lambda(convert);
                var value = (int)lambda.Compile().DynamicInvoke(null);
                this.SetFinallyValue(value);
                return node;
            }
            else
            {
                this.SetFinallyValue(node.Value);
                return node;
            }
        }

        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitDebugInfo{Environment.NewLine}内容：{node}");
            return base.VisitDebugInfo(node);
        }

        protected override Expression VisitDefault(DefaultExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitDefault{Environment.NewLine}内容：{node}");
            return base.VisitDefault(node);
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitDynamic{Environment.NewLine}内容：{node}");
            return base.VisitDynamic(node);
        }

        protected override ElementInit VisitElementInit(ElementInit node)
        {
            Log($"{Environment.NewLine}访问了 VisitElementInit{Environment.NewLine}内容：{node}");
            return base.VisitElementInit(node);
        }

        protected override Expression VisitExtension(Expression node)
        {
            Log($"{Environment.NewLine}访问了 VisitExtension{Environment.NewLine}内容：{node}");
            return base.VisitExtension(node);
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitGoto{Environment.NewLine}内容：{node}");
            return base.VisitGoto(node);
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitIndex{Environment.NewLine}内容：{node}");
            return base.VisitIndex(node);
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitInvocation{Environment.NewLine}内容：{node}");
            return base.VisitInvocation(node);
        }

        protected override Expression VisitLabel(LabelExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitLabel{Environment.NewLine}内容：{node}");
            return base.VisitLabel(node);
        }

        [return: NotNullIfNotNull("node")]
        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            Log($"{Environment.NewLine}访问了 VisitLabelTarget{Environment.NewLine}内容：{node}");
            return base.VisitLabelTarget(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            Log($"{Environment.NewLine}访问了 VisitLambda<T>{Environment.NewLine}内容：{node}");
            return base.VisitLambda(node);
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitListInit{Environment.NewLine}内容：{node}");
            return base.VisitListInit(node);
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitLoop{Environment.NewLine}内容：{node}");
            return base.VisitLoop(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitMember{Environment.NewLine}内容：{node}");
            base.VisitMember(node);
            if (this._isParameter)
            {
                if (this._equationLink.Count == 0 && node.Type == typeof(Boolean))
                {
                    if (this._conditionLink.Count > 0 && this._conditionLink.Peek() == ExpressionType.Equal)
                    {
                        this._fieldCondition.Push(node.Member.Name);
                    }
                    else
                    {
                        var paramName = this.GetParameter(node.Member.Name);
                        this._whereCondition.Push($" {node.Member.Name}={paramName} ");
                        if (this._conditionLink.Count > 0 && this._conditionLink.Peek() == ExpressionType.Not)
                        {
                            this._conditionLink.Pop();
                            this.Parameters.Add(paramName, 0);
                        }
                        else
                        {
                            this.Parameters.Add(paramName, 1);
                        }
                    }
                }
                else
                {
                    this._fieldCondition.Push(node.Member.Name);
                }
            }
            else
            {
                LambdaExpression lambda = Expression.Lambda(node);
                var value = lambda.Compile().DynamicInvoke(null);
                this.SetFinallyValue(value);
            }
            return node;
            //return base.VisitMember(node);
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            Log($"{Environment.NewLine}访问了 VisitMemberAssignment{Environment.NewLine}内容：{node}");
            return base.VisitMemberAssignment(node);
        }

        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            Log($"{Environment.NewLine}访问了 VisitMemberBinding{Environment.NewLine}内容：{node}");
            return base.VisitMemberBinding(node);
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitMemberInit{Environment.NewLine}内容：{node}");
            return base.VisitMemberInit(node);
        }

        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            Log($"{Environment.NewLine}访问了 VisitMemberListBinding{Environment.NewLine}内容：{node}");
            return base.VisitMemberListBinding(node);
        }

        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            Log($"{Environment.NewLine}访问了 VisitMemberMemberBinding，{Environment.NewLine}内容：{node}");
            return base.VisitMemberMemberBinding(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitMethodCall{Environment.NewLine}内容：{node}");
            if (IsOperationConvert(node.Method.Name))
            {
                base.VisitMethodCall(node);
                var conditionCount = OperationConvert(node.Method.Name);
                var rightContent = string.Empty;
                string leftContent;
                if (conditionCount == 1)
                {
                    leftContent = this._fieldCondition.Pop();
                }
                else
                {
                    rightContent = this._fieldCondition.Pop();
                    leftContent = this._fieldCondition.Pop();
                }
                var paramName = this.StructureCondition(leftContent, rightContent, this._equationLink.Pop());
                if (!string.IsNullOrWhiteSpace(paramName))
                {
                    this.Parameters.Add(paramName, this._fieldValue.Pop());
                }
                return node;
            }
            else
            {
                LambdaExpression lambda = Expression.Lambda(node);
                var value = lambda.Compile().DynamicInvoke(null);
                this.SetFinallyValue(value);
                return node;
            }
            //return base.VisitMethodCall(node);
        }

        protected override Expression VisitNew(NewExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitNew{Environment.NewLine}内容：{node}");
            return base.VisitNew(node);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitNewArray{Environment.NewLine}内容：{node}");
            return base.VisitNewArray(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitParameter{Environment.NewLine}内容：{node}");
            this._isParameter = true;
            //return base.VisitParameter(node);
            return node;
        }

        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitRuntimeVariables{Environment.NewLine}内容：{node}");
            return base.VisitRuntimeVariables(node);
        }

        protected override Expression VisitSwitch(SwitchExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitSwitch{Environment.NewLine}内容：{node}");
            return base.VisitSwitch(node);
        }

        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            Log($"{Environment.NewLine}访问了 VisitSwitchCase{Environment.NewLine}内容：{node}");
            return base.VisitSwitchCase(node);
        }

        protected override Expression VisitTry(TryExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitTry{Environment.NewLine}内容：{node}");
            return base.VisitTry(node);
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitTypeBinary{Environment.NewLine}内容：{node}");
            return base.VisitTypeBinary(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            Log($"{Environment.NewLine}访问了 VisitUnary{Environment.NewLine}内容：{node}");
            if (IsConvert(node.NodeType))
            {
                base.VisitUnary(node);
                if (!this._isParameter)
                {
                    var convert = node;
                    if (this._methodResultValue.Count > 0)
                    {
                        var value = this._methodResultValue.Pop();
                        var constantExpression = Expression.Constant(value, value.GetType());
                        convert = Expression.Convert(constantExpression, node.Type);
                    }
                    LambdaExpression lambda = Expression.Lambda(convert);
                    var convertVaue = lambda.Compile().DynamicInvoke(null);
                    this.SetFinallyValue(convertVaue);
                }
                return node;
            }
            if (node.NodeType == ExpressionType.Not)
            {
                this._conditionLink.Push(node.NodeType);
                return base.VisitUnary(node);
            }
            else
            {
                return base.VisitUnary(node);
            }
        }
    }
}
