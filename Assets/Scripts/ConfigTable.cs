using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Reflection;

public class TableDatabase
{
    public int ID;
}

public class ConfigTable<TDatabase,T> : SingleCase<T>
    where TDatabase :TableDatabase , new()
    where T : SingleCase<T>
{
    public Dictionary<int, TDatabase> _cache = new Dictionary<int, TDatabase>();

    protected void load(string tablePath)
    {
        MemoryStream tableStream;

#if UNITY_EDITOR
        var srcPath = Application.dataPath + "/../" + tablePath;
        tableStream = new MemoryStream(File.ReadAllBytes(srcPath));
#else

        var table = Resources.Load<TextAsset>(tablePath);
        tableStream = new MemoryStream(table.bytes);
#endif

        using (var reader = new StreamReader(tableStream, Encoding.GetEncoding("gb2312")))
        {
            var fieldNameStr = reader.ReadLine();
            var fieldNameArray = fieldNameStr.Split(',');
            List<FieldInfo> allFieldInfo = new List<FieldInfo>();
            foreach (var fieldName in fieldNameArray)
            {
                allFieldInfo.Add(typeof(TDatabase).GetField(fieldName));
            }

            var lineStr = reader.ReadLine();
            while (lineStr != null)
            {
                TDatabase DataDB = readLine(allFieldInfo, lineStr);
                _cache[DataDB.ID] = DataDB;
                lineStr = reader.ReadLine();
            }
        }
    }

    private static TDatabase readLine(List<FieldInfo> allFieldInfo, string lineStr)
    {
        var itemStrArray = lineStr.Split(',');
        var DataDB = new TDatabase();
        for (int i = 0; i < allFieldInfo.Count; ++i)
        {
            var field = allFieldInfo[i];
            var data = itemStrArray[i];
            if (field.FieldType == typeof(int))
            {
                field.SetValue(DataDB, int.Parse(data));
            }
            else if (field.FieldType == typeof(string))
            {
                field.SetValue(DataDB, data);
            }
            else if (field.FieldType == typeof(float))
            {
                field.SetValue(DataDB, float.Parse(data));
            }
            else if (field.FieldType == typeof(bool))
            {
                field.SetValue(DataDB, bool.Parse(data));
            }
            else if(field.FieldType == typeof(List<int>))
            {
                var list = new List<int>();
                foreach (var itemStr in data.Split('$'))
                {
                    list.Add(int.Parse(itemStr));
                }
                field.SetValue(DataDB, list);
            }
            else if (field.FieldType == typeof(List<float>))
            {
                var list = new List<float>();
                foreach (var itemStr in data.Split('$'))
                {
                    list.Add(float.Parse(itemStr));
                }
                field.SetValue(DataDB, list);
            }
            else if (field.FieldType == typeof(List<string>))
            {
                field.SetValue(DataDB, new List<string>(data.Split('$')));
            }
        }

        return DataDB;
    }
    public TDatabase this[int index]
    {
        get
        {
            TDatabase db;
            _cache.TryGetValue(index, out db);
            return db;
        }
    }
    public Dictionary<int, TDatabase> GetAll()
    {
        return _cache;
    }
}