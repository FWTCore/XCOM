using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Model
{
    /// <summary>
    /// 生成类型枚举
    /// </summary>
    [Flags]
    public enum BuilderType
    {
        /// <summary>
        /// 不生成
        /// </summary>
        NONE = 0,
        /// <summary>
        /// 生成Entity
        /// </summary>
        BuilderEntity = 1 << 0,
        /// <summary>
        /// 生成Service
        /// </summary>
        BuilderService = 1 << 1,
        /// <summary>
        /// 生成Repository
        /// </summary>
        BuilderRepository = 1 << 2,
    }


    /// <summary>
    /// 数据库字段枚举
    /// </summary>
    public enum DBFieldTypeEnum
    {
        VARCHAR,
        VARCHAR2,
        NVARCHAR,
        CHAR,

        TINYINT,
        INT,
        INTEGER,
        SMALLINT,

        LONG,
        BIGINT,

        DATE,
        DATETIME,
        DATETIME2,
        DATETIMEOFFSET,

        DECIMAL,
        NUMBER,
        MONEY,
        NUMERIC,

        DOUBLE,

        FLOAT,

        UNIQUEIDENTIFIER,

        BIT,


    }

}
