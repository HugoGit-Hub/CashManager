using System.Reflection;

namespace CashManager.Banking.Api.Configuration;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}
