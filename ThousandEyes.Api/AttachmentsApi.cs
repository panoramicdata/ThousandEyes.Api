﻿using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api;

#pragma warning disable CS9113 // Parameter is unread
internal sealed class AttachmentsApi(HttpClient httpClient) : IAttachmentsApi { }
#pragma warning restore CS9113 // Parameter is unread
