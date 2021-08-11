using System.Collections.Generic;

public static class Ext
{
    public static HashSet<T> ExceptWith<T>(HashSet<T> a, HashSet<T> b)
    {
        HashSet<T> tempA = new HashSet<T>(a);
        tempA.ExceptWith(b);
        return tempA;
    }

    public static HashSet<T> UnionWith<T>(HashSet<T> a, HashSet<T> b)
    {
        HashSet<T> tempA = new HashSet<T>(a);
        tempA.UnionWith(b);
        return tempA;
    }

    public static HashSet<T> IntersectWith<T>(HashSet<T> a, HashSet<T> b)
    {
        HashSet<T> tempA = new HashSet<T>(a);
        tempA.IntersectWith(b);
        return tempA;
    }

    public static HashSet<T> SymmetricExceptWith<T>(HashSet<T> a, HashSet<T> b)
    {
        HashSet<T> tempA = new HashSet<T>(a);
        tempA.SymmetricExceptWith(b);
        return tempA;
    }
}
