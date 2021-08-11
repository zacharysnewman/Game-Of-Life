using System.Collections.Generic;
using System.Linq;

public partial struct Coords
{
    public readonly int x;
    public readonly int y;

    public Coords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public HashSet<Coords> Neighbors
    {
        get
        {
            var (x, y) = this;
            return new HashSet<Coords>()
            {
                (x-1,y-1),(x-1,y),(x-1,y+1),
                (x,y-1),/*	(x,y),*/(x,y+1),
                (x+1,y-1),(x+1,y),(x+1,y+1)
            };
        }
    }

    public int LivingNeighborCount(HashSet<Coords> livingCells) =>
        this.Neighbors
            .Where((x) => livingCells.Contains(x))
            .Count();

    public override string ToString() => $"({x},{y})";

    public void Deconstruct(out int x, out int y)
    {
        x = this.x;
        y = this.y;
    }

    public static implicit operator Coords((int, int) t) => new Coords(t.Item1, t.Item2);
}