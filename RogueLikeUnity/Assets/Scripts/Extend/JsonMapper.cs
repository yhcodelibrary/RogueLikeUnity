using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Extend
{
    public class JsonMapper
    {
        static JsonMapper()
        {
            LitJson.JsonMapper.RegisterExporter<float>((obj, writer) => { writer.Write(System.Convert.ToDouble(obj)); });
            LitJson.JsonMapper.RegisterImporter<double, float>((input) => { return System.Convert.ToSingle(input); });
            LitJson.JsonMapper.RegisterImporter<System.Int32, long>((input) => { return System.Convert.ToInt64(input); });
        }

        public static T ToObject<T>(string json)
        {
            return LitJson.JsonMapper.ToObject<T>(json);
        }

        public static string ToJson(object obj)
        {
            string json = LitJson.JsonMapper.ToJson(obj);


            return json;
        }
    }
}
