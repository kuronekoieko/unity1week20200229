using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// どこからでも使う便利そうなメソッドをまとめておく
/// </summary>
public class Utils : MonoBehaviour
{
    public static bool TryGetValue<T>(T[] array, int index, out T value)
    {
        if (IsIndexOutOfRange(array, index))
        {
            value = default;
            return false;
        }
        else
        {
            value = array[index];
            return true;
        }
    }
    public static bool IsIndexOutOfRange<T>(T[] array, int index)
    {
        return index < 0 || array.Length < index + 1;
    }

    public static bool IsIndexOutOfRange<T>(List<T> list, int index)
    {
        return index < 0 || list.Count < index + 1;
    }
}
