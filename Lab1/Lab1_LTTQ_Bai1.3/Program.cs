using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

class Delegate
{
    public delegate int CompareHandler(object a, object b);
    public static object FindMax(Array array, CompareHandler compare)
    {
        object max = array.GetValue(0);
        foreach (object obj in array)
        {
            if (compare(obj, max) == 1) max = obj;
        }
        return max;
    }
    public static object FindMin(Array array, CompareHandler compare)
    {
        object min = array.GetValue(0);
        foreach (object obj in array)
        {
            if (compare(obj, min) == -1) min = obj;
        }
        return min;
    }
    public static int CompareInt(object obj1, object obj2)
    {
        int a = (int)obj1;
        int b = (int)obj2;
        if (a > b) return 1;
        else if (a < b) return -1;
        else return 0;
    }
    public static int CompareDouble(object obj1, object obj2)
    {
        double a = (double)obj1;
        double b = (double)obj2;
        if (a > b) return 1;
        else if (a < b) return -1;
        else return 0;
    }
    public static int CompareString(object obj1, object obj2)
    {
        int a = ((string)obj1).Length;
        int b = ((string)obj2).Length;
        if (a > b) return 1;
        else if (a < b) return -1;
        else return 0;
    }
    static void Main()
    {
        int[] ArrayInt = { 24, 1, 39, 8, 23, 4, 16, 49, 22, 35 };
        double[] ArrayDouble = { 4.9, 11.3, 8.25, 24.23, 15.5, 19.447, 130.22, 69.4465, 115.53145 };
        string[] ArrayString = { "January", "March", "May", "June", "September", "August" };

        CompareHandler compare = CompareInt;
        int MaxInt = (int)FindMax(ArrayInt, compare);
        Console.WriteLine("Max Int = {0}", MaxInt);
        int MinInt = (int)FindMin(ArrayInt, compare);
        Console.WriteLine("Min Int = {0}", MinInt);

        compare = CompareDouble;
        double MaxDouble = (double)FindMax(ArrayDouble, compare);
        Console.WriteLine("Max Double = {0}", MaxDouble);
        double MinDouble = (double)FindMin(ArrayDouble, compare);
        Console.WriteLine("Min Double = {0}", MinDouble);

        compare = CompareString;
        string MinString = (string)FindMin(ArrayString, compare);
        Console.WriteLine("Min String = {0}", MinString);
        string MaxString = (String)FindMax(ArrayString, compare);
        Console.WriteLine("Max String = {0}", MaxString);
    }
}
