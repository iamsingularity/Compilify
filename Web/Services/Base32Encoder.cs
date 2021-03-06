﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Compilify.Web.Infrastructure;

namespace Compilify.Web.Services
{
    public static class Base32Encoder
    {
        private const string Characters = "0123456789abcdefghijklmnopqrstuvwxyz";

        public static string Encode(int i) {
            if (i == 0)
            {
                return Characters[0].ToString(CultureInfo.InvariantCulture);
            }

            int @base = Characters.Length;
            var slug = new Stack<char>(7);

            while (i > 0)
            {
                slug.Push(Characters[i % @base]);
                i /= @base;
            }

            return new string(slug.ToArray());
        }

        public static Task<string> EncodeAsync(Task<int> task) {
            return task.ContinueWith(t => Encode(t.Result));

        }

        public static int? Decode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            var @base = Characters.Length;
            return str.ToLowerInvariant()
                      .Aggregate(0, (current, c) => current * @base + Characters.IndexOf(c));
        }
    }
}
