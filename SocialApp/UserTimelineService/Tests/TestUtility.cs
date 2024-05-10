namespace UserTimelineService.Tests;

public static class TestUtility
{
    public static string EqualCount(string test, IEnumerable<object> l1, IEnumerable<object> l2)
    {
        return test + ": " + (EqualCount(l1, l2) ? "ok" : "failed");
    }

    private static bool EqualCount(IEnumerable<object> l1, IEnumerable<object> l2)
    {
        return l1.Count() == l2.Count();
    }
}