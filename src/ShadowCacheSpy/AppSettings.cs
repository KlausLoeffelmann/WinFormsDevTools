﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace ShadowCacheSpy;

public partial class AppSettings : ObservableObject
{
    [ObservableProperty]
    private string? _watchFolder;

    [ObservableProperty]
    private DateTime? _dateCreated;
    
    [ObservableProperty]
    private DateTime? _dateModified;
}
