﻿using Newtonsoft.Json;
using Optsol.Components.Cache.Resolvers;
using System.Text;

namespace System;

public static class StringExtensions
{
    public static T? To<T>(this string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return default;
        }

        return JsonConvert.DeserializeObject<T>(source, new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver()
        });
    }

    public static byte[] ToBytes(this string source)
    {
        return Encoding.UTF8.GetBytes(source);
    }

    public static bool IsEmpty(this string source)
    {
        return string.IsNullOrEmpty(source);
    }
}
