using UnityEngine;

public static class ExceptionsTest
{
    public static void NullRefTest(string className, string methodName, params object[] testObjects)
    {
        string log = $"[{Time.time}] [{nameof(NullRefTest)}] ClassName = [{className}] MethodName = [{methodName}]:\n";
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
}
