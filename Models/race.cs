using System.Diagnostics;

namespace Marathon.Models;

public class RaceCollection
{
    public race[] races { get; set; } //array of races
}

public class race // I thought all classes should be capitalized?
{
    public int id { get; set; }
    public string race_name { get; set; }
}