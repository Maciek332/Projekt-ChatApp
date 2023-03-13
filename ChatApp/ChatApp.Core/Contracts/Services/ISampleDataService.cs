﻿using ChatApp.Core.Models;

namespace ChatApp.Core.Contracts.Services;

// Remove this class once your pages/features are using your data.
public interface ISampleDataService
{
    Task<IEnumerable<SampleUser>> GetListDetailsDataAsync();
}
