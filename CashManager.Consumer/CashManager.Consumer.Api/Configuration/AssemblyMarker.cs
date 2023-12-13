using System.Reflection;

namespace CashManager.Consumer.Api.Configuration;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}