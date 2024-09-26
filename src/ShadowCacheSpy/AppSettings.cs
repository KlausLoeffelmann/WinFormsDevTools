using System.Diagnostics.CodeAnalysis;

namespace ShadowCacheSpy;

public class AppSettings 
{
    public string? WatchFolder { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
}
