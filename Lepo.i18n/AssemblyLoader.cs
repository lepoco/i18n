// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Lepo.i18n
{
    /// <summary>
    /// Loads <see cref="Yaml"/> files from <see cref="System.Windows.Application"/> manifest resources.
    /// </summary>
    internal class AssemblyLoader
    {
        /// <summary>
        /// Try to get dictionary from YAML file located in <see cref="System.Windows.Application"/> resources.
        /// </summary>
        /// <param name="applicationAssembly">Assembly of the main application.</param>
        /// <param name="resourceStreamPath">Path to the application resource.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IDictionary<uint, string> TryLoad(Assembly applicationAssembly, string resourceStreamPath)
        {
            var lowerResourcePath = resourceStreamPath.ToLower().Trim();
            if (!(lowerResourcePath.EndsWith(".yml") || lowerResourcePath.EndsWith(".yaml")))
                throw new ArgumentException(
                    $"Parameter {nameof(resourceStreamPath)} in {nameof(TryLoad)} must be path to the YAML file.");

            using Stream stream = applicationAssembly.GetManifestResourceStream(resourceStreamPath);

            if (stream == null)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR | Could not load path {resourceStreamPath}, the file probably does not exist.", "Lepo.i18n");

                return null;
            }


            using StreamReader reader = new StreamReader(stream);

            return Yaml.FromString(reader.ReadToEnd());
        }
    }
}