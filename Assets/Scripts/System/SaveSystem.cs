using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// 用于提供存档的方法
/// </summary>
public class SaveSystem : MonoBehaviour
{
    /// <summary>
    /// 包含所有需要存储的数据的类
    /// </summary>
    [Serializable]
    public struct SaveData
    {
        public float a;
        public float b;
        public float c;
        public int d;
        public string e;
    }
    /// <summary>
    /// 包含所有需要存储的数据
    /// </summary>
    static public SaveData saveData;

    [ContextMenu("Save")]
    /// <summary>
    /// 存档
    /// </summary>
    static public void Save(int dataNum)
    {
        PackData();
        BinaryWriter bw = new BinaryWriter(File.Open(Application.dataPath + "/test" + dataNum + ".sav", FileMode.Create));
        bw.Write(StructToBytes(saveData, Marshal.SizeOf(saveData)));
        bw.Close();
    }
    [ContextMenu("Load")]
    /// <summary>
    /// 读档
    /// </summary>
    static public void Load(int dataNum)
    {
        BinaryReader br= new BinaryReader(File.Open(Application.dataPath + "/test" + dataNum + ".sav", FileMode.Open));
        Byte[] buffer=new Byte[Marshal.SizeOf(saveData)];
        br.Read(buffer, 0, Marshal.SizeOf(saveData));
        saveData = (SaveData)ByteToStruct(buffer, typeof(SaveData));
        br.Close();
        ApplyData();
    }
    /// <summary>
    /// 将结构体转换为Byte类型
    /// </summary>
    public static byte[] StructToBytes(object structObj, int size)
    {
        byte[] bytes = new byte[size];
        IntPtr structPtr = Marshal.AllocHGlobal(size);
        //将结构体拷到分配好的内存空间
        Marshal.StructureToPtr(structObj, structPtr, false);
        //从内存空间拷贝到byte 数组
        Marshal.Copy(structPtr, bytes, 0, size);
        //释放内存空间
        Marshal.FreeHGlobal(structPtr);
        return bytes;
    }
    ///<summary>
    ///将Byte转换为结构体类型
    ///</summary>
    public static object ByteToStruct(byte[] bytes, Type type)
    {
        int size = Marshal.SizeOf(type);
        if (size > bytes.Length)
        {
            return null;
        }
        //分配结构体内存空间
        IntPtr structPtr = Marshal.AllocHGlobal(size);
        //将byte数组拷贝到分配好的内存空间
        Marshal.Copy(bytes, 0, structPtr, size);
        //将内存空间转换为目标结构体
        object obj = Marshal.PtrToStructure(structPtr, type);
        //释放内存空间
        Marshal.FreeHGlobal(structPtr);
        return obj;
    }
    /// <summary>
    /// 记录游戏数据并打包至SaveData
    /// </summary>
    static private void PackData()
    {

    }
    /// <summary>
    /// 将读取的数据应用到游戏
    /// </summary>
    static private void ApplyData()
    {

    }
}
