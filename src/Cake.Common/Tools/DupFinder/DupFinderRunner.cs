using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Utilities;

namespace Cake.Common.Tools.DupFinder
{
    /// <summary>
    /// DupFinder runner
    /// </summary>
    public sealed class DupFinderRunner : Tool<DupFinderSettings>
    {
        private readonly ICakeEnvironment _environment;
        private readonly IGlobber _globber;

        /// <summary>
        /// Initializes a new instance of the <see cref="DupFinderRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="globber">The globber</param>
        public DupFinderRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IGlobber globber)
            : base(fileSystem, environment, processRunner, globber)
        {
            _environment = environment;
            _globber = globber;
        }

        /// <summary>
        /// Analyses the specified projects using the specified settings.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <param name="settings">The settings.</param>
        public void Run(IEnumerable<FilePath> projects, DupFinderSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            if (projects == null)
            {
                throw new ArgumentNullException("projects");
            }

            Run(settings, GetArgument(settings, projects));
        }

        /// <summary>
        /// Analyses all files matching the specified pattern using the specified settings.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="settings">The settings.</param>
        public void Run(string pattern, DupFinderSettings settings)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException("pattern");
            }
            var sourceFiles = _globber.Match(pattern).OfType<FilePath>();

            Run(sourceFiles, settings);
        }

        /// <summary>
        /// Analyses according to the specified config file
        /// </summary>
        /// <param name="configFile"></param>
        public void RunFromConfig(FilePath configFile)
        {
            if (configFile == null)
            {
                throw new ArgumentNullException("configFile");
            }

            Run(new DupFinderSettings(), GetConfigArgument(configFile));
        }

        private ProcessArgumentBuilder GetConfigArgument(FilePath configFile)
        {
            var builder = new ProcessArgumentBuilder();
            builder.AppendQuoted(string.Format(CultureInfo.InvariantCulture, "/config={0}",
                configFile.MakeAbsolute(_environment).FullPath));

            return builder;
        }

        private ProcessArgumentBuilder GetArgument(DupFinderSettings settings, IEnumerable<FilePath> files)
        {
            var builder = new ProcessArgumentBuilder();

            if (settings.OutputFile != null)
            {
                builder.AppendQuoted(string.Format(CultureInfo.InvariantCulture, "/output={0}",
                    settings.OutputFile.MakeAbsolute(_environment).FullPath));
            }

            foreach (var file in files)
            {
                builder.AppendQuoted(file.MakeAbsolute(_environment).FullPath);
            }

            return builder;
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>The name of the tool.</returns>
        protected override string GetToolName()
        {
            return "DupFinder";
        }

        /// <summary>
        /// Gets the possible names of the tool executable.
        /// </summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "dupfinder.exe" };
        }
    }
}