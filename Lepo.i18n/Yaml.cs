// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lepo.i18n
{
    /// <summary>
    /// Some weird YAML implementation. Don't ask me, it was supposed to be simple...
    /// </summary>
    internal class Yaml
    {
        /// <summary>
        /// Used to calculate a simple hash of a key in a dictionary to make searches faster.
        /// </summary>
        private static readonly MD5 Hasher = MD5.Create();

        /// <summary>
        /// Creates a hashed <see langword="int"/> representation of <see langword="string"/>.
        /// </summary>
        /// <param name="value">Value to be hashed.</param>
        /// <returns></returns>
        public static uint Map(string value)
        {
            return BitConverter.ToUInt32(
                Hasher.ComputeHash(Encoding.UTF8.GetBytes(value)),
                0
            );
        }

        /// <summary>
        /// Creates new collection of mapped keys with translated values.
        /// </summary>
        /// <param name="rawYamlContent">String containing Yaml.</param>
        public static IDictionary<uint, string> FromString(string rawYamlContent)
        {
            Dictionary<uint, string> keyValueCollection = new() { };

            if (String.IsNullOrEmpty(rawYamlContent))
                return keyValueCollection;

            string[] splittedYamlLines = rawYamlContent.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            // TODO: Recognize tab stops as subsections

            if (splittedYamlLines.Length < 1)
                return keyValueCollection;

            foreach (string yamlLine in splittedYamlLines)
            {
                if (yamlLine.StartsWith("#") || String.IsNullOrEmpty(yamlLine))
                    continue;

                string[] pair = yamlLine.Split(new[] { ':' }, 2);

                if (pair.Length < 2)
                    continue;

                uint mappedKey = Map(pair[0].Trim());

                var translatedValue = pair[1].Trim();

                if (translatedValue.StartsWith("'") && translatedValue.EndsWith("'") || translatedValue.StartsWith("\"") && translatedValue.EndsWith("\""))
                    translatedValue = translatedValue.Substring(1, translatedValue.Length - 2);

                if (!keyValueCollection.ContainsKey(mappedKey))
                    keyValueCollection.Add(mappedKey, translatedValue);
            }

            return keyValueCollection;
        }
    }
}