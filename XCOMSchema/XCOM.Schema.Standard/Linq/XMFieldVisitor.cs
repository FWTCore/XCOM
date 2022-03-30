using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Linq
{
    /// <summary>
    /// 获取字段
    /// </summary>
    public sealed class XMFieldVisitor : ExpressionVisitor
    {
        /// <summary>
        /// 字段集合（不重复）
        /// </summary>
        private readonly HashSet<string> _conditionFields = new();

        public string ResultFields
        {
            get
            {
                var str = string.Join(",", this._conditionFields);
                return str;
            }
        }

        /// <summary>
        /// 重写 字段 属性
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>00.
        protected sealed override Expression VisitMember(MemberExpression node)
        {
            if (node != null && node.Member.Name != "Value")
            {
                PropertyInfo propertyInfo = node.Member as PropertyInfo;
                if (propertyInfo == null)
                {
                    return node;
                }
                this._conditionFields.Add(propertyInfo.Name);
            }
            else
            {
                return base.VisitMember(node);
            }
            return node;
        }



    }
}
