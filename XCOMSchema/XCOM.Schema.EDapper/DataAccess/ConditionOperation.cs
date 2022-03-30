using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.DataAccess
{
    /// <summary>
    /// 条件操作符
    /// </summary>
    public enum ConditionOperation
    {
        Equal,
        NotEqual,
        MoreThan,
        MoreThanEqual,
        LessThan,
        LessThanEqual,
        Like,
        LikeRight,
        LikeLeft,
        NotLike,
        NotLikeRight,
        NotLikeLeft,
        In,
        NotIn
    }
}
