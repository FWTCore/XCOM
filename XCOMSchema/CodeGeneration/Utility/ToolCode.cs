using CodeGeneration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.SQLClient;
using XCOM.Schema.Standard.Utility;

namespace CodeGeneration.Utility
{
    public class ToolCode
    {
        public static void BuilderCode(
            DBConnection db,
            string dbName,
            List<string> tableNames,
            List<TableEntity> tableList,
            BuilderType builderType,
            string spaceNamePrefix,
            string filePath)
        {
            var cmd = new XMSqlCommand($"GetColumnList_{db.DBType}", db.Key);
            cmd.SetParameter("dbName", dbName);
            cmd.SetParameter("Tablenames", tableNames);
            List<TableColumnNameEntity> columnsDataList = cmd.Query<TableColumnNameEntity>().ToList();
            if ((builderType & BuilderType.BuilderEntity) == BuilderType.BuilderEntity)
            {
                BuilderEntity(columnsDataList, tableList, spaceNamePrefix, filePath);
            }
            if ((builderType & BuilderType.BuilderService) == BuilderType.BuilderService)
            {
                var currentTableList = tableList.Where(e => tableNames.Contains(e.TableName)).ToList();
                BuilderService(currentTableList, spaceNamePrefix, filePath);
            }
            if ((builderType & BuilderType.BuilderRepository) == BuilderType.BuilderRepository)
            {
                var currentTableList = tableList.Where(e => tableNames.Contains(e.TableName)).ToList();
                BuilderRepository(currentTableList, spaceNamePrefix, filePath);
            }
        }


        private static void BuilderEntity(List<TableColumnNameEntity> columnsDatas, List<TableEntity> tableList, string spaceNamePrefix, string filePath)
        {
            var entityContent = new StringBuilder();
            var tableColumnsDatas = columnsDatas.GroupBy(e => e.TableName).ToList();
            tableColumnsDatas.ForEach(titem =>
            {
                var currentTable = tableList.FirstOrDefault(e => e.TableName == titem.Key);
                entityContent.Clear();
                var entityName = UNCHelper.GenVarName(titem.Key);
                entityContent.AppendLine("using XCOM.Schema.Standard.DataAnnotations;").Append('\n');
                entityContent.AppendLine($"namespace {spaceNamePrefix}Entity");
                entityContent.AppendLine("{");
                entityContent.AppendLine("\t/// <summary>");
                entityContent.AppendLine($"\t/// {currentTable?.TableComment}");
                entityContent.AppendLine("\t/// <summary>");
                entityContent.AppendLine($"\t[XMTable(\"{titem.Key}\")]");
                entityContent.AppendLine($"\tpublic class {entityName}Entity");
                entityContent.AppendLine("\t{");

                var columnsList = titem.ToList();
                columnsList.ForEach(citem =>
                {
                    entityContent.AppendLine("\t\t/// <summary>");
                    entityContent.AppendLine($"\t\t/// {citem.ColumnComment}");
                    entityContent.AppendLine("\t\t/// <summary>");
                    if (citem.IsPrimaryKey)
                    {
                        entityContent.AppendLine($"\t\t[XMKey]");
                    }
                    if (citem.IsAuto)
                    {
                        entityContent.AppendLine($"\t\t[XMDatabaseGenerated]");
                    }
                    string type = DataBaseHelper.GetFieldType(citem.DataType, citem.IsNullable);
                    string fieldName = UNCHelper.GenVarName(citem.ColumnName);
                    entityContent.AppendLine($"\t\t[XMColumn(\"{citem.ColumnName}\")]");
                    entityContent.AppendLine($"\t\tpublic {type} {fieldName} {{ get; set; }}").Append("\n");
                });
                entityContent.AppendLine("\t}");
                entityContent.AppendLine("}");

                var path = System.IO.Path.Combine(filePath, ToolConstant.PatchEntityFile, $"{entityName}Entity.cs");
                XMFile.WriteFile(path, entityContent.ToString());
            });
        }


        private static void BuilderService(List<TableEntity> tableList, string spaceNamePrefix, string filePath)
        {
            var entityContent = new StringBuilder();
            tableList.ForEach(titem =>
            {
                entityContent.Clear();
                var entityName = UNCHelper.GenVarName(titem.TableName);
                var firstLetterName = UNCHelper.FirstLetter(entityName);
                entityContent.AppendLine($"using {spaceNamePrefix}Entity;");
                entityContent.AppendLine($"using {spaceNamePrefix}IRepository;");
                entityContent.AppendLine($"using {spaceNamePrefix}IService;");
                entityContent.AppendLine("using System;");
                entityContent.AppendLine("using System.Collections.Generic;");
                entityContent.AppendLine("using System.Text;").Append('\n');

                entityContent.AppendLine($"namespace {spaceNamePrefix}Service");
                entityContent.AppendLine("{");

                entityContent.AppendLine("\t/// <summary>");
                entityContent.AppendLine($"\t/// {titem.TableComment}");
                entityContent.AppendLine("\t/// <summary>");
                entityContent.AppendLine($"\tpublic class {entityName}Service : I{entityName}Service");
                entityContent.AppendLine("\t{");

                entityContent.AppendLine($"\t\tprivate I{entityName}Repository _{firstLetterName}Repository {{ get; set; }}").Append('\n');
                entityContent.AppendLine($"\t\tpublic {entityName}Service(I{entityName}Repository _{firstLetterName})");
                entityContent.AppendLine("\t\t{");
                entityContent.AppendLine($"\t\t\t_{firstLetterName}Repository = _{firstLetterName};");
                entityContent.AppendLine("\t\t}");

                entityContent.AppendLine("\t}");
                entityContent.AppendLine("}");
                var path = System.IO.Path.Combine(filePath, ToolConstant.PatchServiceFile, $"{entityName}Service.cs");
                XMFile.WriteFile(path, entityContent.ToString());

                entityContent.Clear();

                entityContent.AppendLine($"using {spaceNamePrefix}Entity;");
                entityContent.AppendLine("using System;");
                entityContent.AppendLine("using System.Collections.Generic;");
                entityContent.AppendLine("using System.Text;").Append('\n');

                entityContent.AppendLine($"namespace {spaceNamePrefix}IService");
                entityContent.AppendLine("{");
                entityContent.AppendLine("\t/// <summary>");
                entityContent.AppendLine($"\t/// {titem.TableComment}");
                entityContent.AppendLine("\t/// <summary>");
                entityContent.AppendLine($"\tpublic interface I{entityName}Service");
                entityContent.AppendLine("\t{");
                entityContent.AppendLine("\t}");
                entityContent.AppendLine("}");
                path = System.IO.Path.Combine(filePath, ToolConstant.PatchIServiceFile, $"I{entityName}Service.cs");
                XMFile.WriteFile(path, entityContent.ToString());
            });
        }


        public static void BuilderRepository(List<TableEntity> tableList, string spaceNamePrefix, string filePath)
        {
            var entityContent = new StringBuilder();
            tableList.ForEach(titem =>
            {
                entityContent.Clear();
                var entityName = UNCHelper.GenVarName(titem.TableName);
                var firstLetterName = UNCHelper.FirstLetter(entityName);
                entityContent.AppendLine($"using {spaceNamePrefix}Entity;");
                entityContent.AppendLine($"using {spaceNamePrefix}IRepository;");
                entityContent.AppendLine("using System;");
                entityContent.AppendLine("using System.Collections.Generic;");
                entityContent.AppendLine("using System.Text;");
                entityContent.AppendLine("using XCOM.Schema.EDapper.SQLClient;").Append('\n');
   
                entityContent.AppendLine($"namespace {spaceNamePrefix}Repository");
                entityContent.AppendLine("{");
                entityContent.AppendLine("\t/// <summary>");
                entityContent.AppendLine($"\t/// {titem.TableComment}");
                entityContent.AppendLine("\t/// <summary>");
                entityContent.AppendLine($"\tpublic class {entityName}Repository : XMContext<{entityName}Entity>, I{entityName}Repository");
                entityContent.AppendLine("\t{");
                entityContent.AppendLine("\t}");
                entityContent.AppendLine("}");

                var path = System.IO.Path.Combine(filePath, ToolConstant.PatchRepositoryFile, $"{entityName}Service.cs");
                XMFile.WriteFile(path, entityContent.ToString());

                entityContent.Clear();

                entityContent.AppendLine($"using {spaceNamePrefix}Entity;");
                entityContent.AppendLine("using System;");
                entityContent.AppendLine("using System.Collections.Generic;");
                entityContent.AppendLine("using System.Text;");
                entityContent.AppendLine("using XCOM.Schema.EDapper.SQLClient;").Append('\n');
   
                entityContent.AppendLine($"namespace {spaceNamePrefix}IRepository");
                entityContent.AppendLine("{");
                entityContent.AppendLine("\t/// <summary>");
                entityContent.AppendLine($"\t/// {titem.TableComment}");
                entityContent.AppendLine("\t/// <summary>");
                entityContent.AppendLine($"\tpublic interface I{entityName}Repository : IXMContext<{entityName}Entity>");
                entityContent.AppendLine("\t{");
                entityContent.AppendLine("\t}");
                entityContent.AppendLine("}");
                path = System.IO.Path.Combine(filePath, ToolConstant.PatchIRepositoryFile, $"I{entityName}Repository.cs");

                XMFile.WriteFile(path, entityContent.ToString());
            });
        }


    }
}

