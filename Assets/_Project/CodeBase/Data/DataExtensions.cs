﻿ 
using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector3)
        {
            return new Vector3Data(vector3.x, vector3.y , vector3.z);
        }
        public static Vector3 AsUnityVector(this Vector3Data vector3Data)
        {
            return new Vector3(vector3Data.x, vector3Data.y, vector3Data.z);
        }
        public static T ToDeserialized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
        public static string ToJson (this object obj)
        {
             return JsonUtility.ToJson(obj);
        }
        public static Vector3 AddY(this Vector3 vector, float y) 
        {
             vector.y += y;
            return vector;
        }
    }
}
