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
using System.Collections.Generic;
using System.Data;

namespace ZyGames.Framework.Data
{

    internal static class DataParameterExtend
    {
        /// <summary>
        /// 获得参数的字段名，不带前缀字符
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetFieldName(this IDataParameter parameter)
        {
            string fieldName = parameter.ParameterName ?? "";
            if (fieldName.Length > 0)
            {
                return fieldName.Substring(1);
            }
            return fieldName;
        }
    }
    /// <summary>
    /// 提供数据访问基类
    /// </summary>
    public abstract class DbBaseProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        protected DbBaseProvider(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract IDataReader ExecuteReader(CommandType commandType, string commandText, params IDataParameter[] parameters);
        /// <summary>
        /// 执行Sql语句，返回第一行第一列值
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract object ExecuteScalar(CommandType commandType, string commandText, params IDataParameter[] parameters);
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract int ExecuteQuery(CommandType commandType, string commandText, params IDataParameter[] parameters);
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="identityID"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(int identityID, CommandType commandType, string commandText, params IDataParameter[] parameters);

        /// <summary>
        /// 检查是否有指定表名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public abstract bool CheckTable(string tableName, out DbColumn[] columns);

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public abstract void CreateTable(string tableName, DbColumn[] columns);

        /// <summary>
        /// 创建列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public abstract void CreateColumn(string tableName, DbColumn[] columns);

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameter(string paramName, object value);
        /// <summary>
        /// 创建Guid类型的参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameterByGuid(string paramName, object value);
        /// <summary>
        /// 创建Text类型的参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameterByText(string paramName, object value);

        /// <summary>
        /// 创建CommandStruct对象
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="editType"></param>
        /// <returns></returns>
        public abstract CommandStruct CreateCommandStruct(string tableName, CommandMode editType);

        /// <summary>
        /// 创建CommandFilter对象
        /// </summary>
        /// <returns></returns>
        public abstract CommandFilter CreateCommandFilter();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected T[] ConvertParam<T>(IDataParameter[] parameters) where T : IDataParameter, new()
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            List<T> list = new List<T>();
            foreach (IDataParameter param in parameters)
            {
                if (param != null)
                {
                    list.Add((T)param);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 格式化条件语句中的参数
        /// </summary>
        /// <param name="fieldNname"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public abstract string FormatFilterParam(string fieldNname, string paramName);

        /// <summary>
        /// 格式化Select语句中的列名
        /// </summary>
        /// <param name="splitChat"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public abstract string FormatQueryColumn(string splitChat, string[] columns);
    }
}