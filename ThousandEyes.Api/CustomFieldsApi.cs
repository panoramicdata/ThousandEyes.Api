﻿using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api;

#pragma warning disable CS9113 // Parameter is unread
internal sealed class CustomFieldsApi(HttpClient httpClient) : ICustomFieldsApi { }
#pragma warning restore CS9113 // Parameter is unread
