using System.Collections.Generic;
using UnityEngine;

public static class ExceptionsTest
{
    public const string ConstructorName = "constructor";

    public static bool Enabled = false;

    public static void NullRefMethodTest(string className, string methodName, params object[] testObjects)
    {
        if (Enabled == false)
            return;

        string log = $"[{Time.time}] [{nameof(NullRefMethodTest)}] ClassName = [{className}] MethodName = [{methodName}]:\n";
        string nullObjects = string.Empty;
        string testCompleted = "Test completed correctly";

        for (int i = 0; i < testObjects.Length; i++)
        {
            if (testObjects[i] is null)
                nullObjects += $"[{i + 1}] is null";
        }

        if (nullObjects != string.Empty)
        {
            log += nullObjects;
            Debug.LogError(log);
        }
        else
        {
            log += testCompleted;
            Debug.Log(log);
        }
    }

    public static void NullRefConstructorTest(string className, params object[] testObjects)
    {
        NullRefMethodTest(className, ConstructorName, testObjects);
    }

    public static void EmptyListTest<T>(string className, string methodName, List<T> list) where T 
        : class
    {
        if (Enabled == false)
            return;

        string log = $"[{Time.time}] [{nameof(EmptyListTest)}] ClassName = [{className}] MethodName = [{methodName}]:\n";
        string nullObjects = string.Empty;
        string testCompleted = "Test completed correctly";
        object[] testObjects = new object[list.Count];

        for (int i = 0; i < list.Count; i++)
            testObjects[i] = list[i];

        for (int i = 0; i < testObjects.Length; i++)
        {
            if (testObjects[i] is null)
                nullObjects += $"[{i + 1}] is null";
        }

        if (nullObjects != string.Empty)
        {
            log += nullObjects;
            Debug.LogError(log);
        }
        else
        {
            log += testCompleted;
            Debug.Log(log);
        }
    }
}
