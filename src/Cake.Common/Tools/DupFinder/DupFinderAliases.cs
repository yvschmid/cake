using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.Common.Tools.DupFinder
{
    /// <summary>
    ///  Contain's functionality related to Resharper's duplication finder
    /// </summary>
    [CakeAliasCategory("ReSharper")]
    public static class DupFinderAliases
    {
        /// <summary>
        /// Analyses the specified solution/project with Resharper's DupFinder.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="solution">The solution/project.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("DupFinder")]
        public static void DupFinder(this ICakeContext context, FilePath solution)
        {
            DupFinder(context, new[] { solution });
        }

        /// <summary>
        /// Analyses the specified solution/project with Resharper's DupFinder,
        /// using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="solution">The solution/project.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("DupFinder")]
        public static void DupFinder(this ICakeContext context, FilePath solution, DupFinderSettings settings)
        {
            DupFinder(context, new[] { solution }, settings);
        }

        /// <summary>
        /// Analyses the specified projects with Resharper's DupFinder.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projects">The projects.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("DupFinder")]
        public static void DupFinder(this ICakeContext context, IEnumerable<FilePath> projects)
        {
            DupFinder(context, projects, new DupFinderSettings());
        }

        /// <summary>
        /// Analyses the specified projects with Resharper's DupFinder,
        /// using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projects">The projects.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("DupFinder")]
        public static void DupFinder(this ICakeContext context, IEnumerable<FilePath> projects, DupFinderSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var runner = new DupFinderRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            runner.Run(projects, settings);
        }

        /// <summary>
        /// Analyses all files matching the specified pattern with Resharper's DupFinder.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="pattern">The pattern.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("DupFinder")]
        public static void DupFinder(this ICakeContext context, string pattern)
        {
            DupFinder(context, pattern, new DupFinderSettings());
        }

        /// <summary>
        /// Analyses all files matching the specified pattern with Resharper's DupFinder,
        /// using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("DupFinder")]
        public static void DupFinder(this ICakeContext context, string pattern, DupFinderSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var runner = new DupFinderRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            runner.Run(pattern, settings);
        }

        /// <summary>
        /// Analyses according to the provided config file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configFile">The config file.</param>
        public static void DupFinderFromConfig(this ICakeContext context, FilePath configFile)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var runner = new DupFinderRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            runner.RunFromConfig(configFile);
        }
    }
}