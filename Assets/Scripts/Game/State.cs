using System.Collections;
using System.Collections.Generic;
using static Ext;

public struct State : IEnumerable<State>
{
    public HashSet<Coords> LivingCells { get; private set; }

    public State(HashSet<Coords> livingCells)
    {
        this.LivingCells = livingCells;
    }

    public HashSet<Coords> GetAllCellCoords()
    {
        var allCellCoords = new HashSet<Coords>(this.LivingCells);
        foreach (var c in this.LivingCells)
        {
            allCellCoords.UnionWith(c.Neighbors);
        }
        return allCellCoords;
    }

    IEnumerator<State> IEnumerable<State>.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    public override string ToString() => string.Join(",", this.LivingCells);

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override bool Equals(object obj)
    {
        return this == (State)obj;
    }

    public static bool operator ==(State a, State b) =>
        ExceptWith(a.LivingCells, b.LivingCells).Count == 0 && ExceptWith(b.LivingCells, a.LivingCells).Count == 0;
    public static bool operator !=(State a, State b) => !(a == b);

}