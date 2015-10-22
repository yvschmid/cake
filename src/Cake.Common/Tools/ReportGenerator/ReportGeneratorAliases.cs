using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.Common.Tools.ReportGenerator
{
    /// <summary>
    /// Contains functionality related to ReportGenerator.
    /// </summary>
    [CakeAliasCategory("ReportGenerator")]
    public static class ReportGeneratorAliases
    {
        /// <summary>
        /// Converts the coverage report specified by the glob pattern into human readable form.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="pattern">The glob pattern.</param>
        /// <param name="targetDir">The output directory.</param>
        [CakeMethodAlias]
        public static void ReportGenerator(this ICakeContext context, string pattern, DirectoryPath targetDir)
        {
            var reports = context.Globber.GetFiles(pattern);
            ReportGenerator(context, reports, targetDir);
        }

        /// <summary>
        /// Converts the coverage report specified by the glob pattern into human readable form using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="pattern">The glob pattern.</param>
        /// <param name="targetDir">The output directory.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        public static void ReportGenerator(this ICakeContext context, string pattern, DirectoryPath targetDir, ReportGeneratorSettings settings)
        {
            var reports = context.Globber.GetFiles(pattern);
            ReportGenerator(context, reports, targetDir, settings);
        }

        /// <summary>
        /// Converts the specified coverage report into human readable form.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="report">The coverage report.</param>
        /// <param name="targetDir">The output directory.</param>
        [CakeMethodAlias]
        public static void ReportGenerator(this ICakeContext context, FilePath report, DirectoryPath targetDir)
        {
            ReportGenerator(context, report, targetDir, new ReportGeneratorSettings());
        }

        /// <summary>
        /// Converts the specified coverage report into human readable form using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="report">The coverage report.</param>
        /// <param name="targetDir">The output directory.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        public static void ReportGenerator(this ICakeContext context, FilePath report, DirectoryPath targetDir, ReportGeneratorSettings settings)
        {
            ReportGenerator(context, new[] { report }, targetDir, settings);
        }

        /// <summary>
        /// Converts the specified coverage reports into human readable form.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="reports">The coverage reports.</param>
        /// <param name="targetDir">The output directory.</param>>
        [CakeMethodAlias]
        public static void ReportGenerator(this ICakeContext context, IEnumerable<FilePath> reports, DirectoryPath targetDir)
        {
            ReportGenerator(context, reports, targetDir, new ReportGeneratorSettings());
        }

        /// <summary>
        /// Converts the specified coverage reports into human readable form using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="reports">The coverage reports.</param>
        /// <param name="targetDir">The output directory.</param>
        /// <param name="settings">The settings.</param>>
        [CakeMethodAlias]
        public static void ReportGenerator(this ICakeContext context, IEnumerable<FilePath> reports, DirectoryPath targetDir, ReportGeneratorSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var runner = new ReportGeneratorRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            runner.Run(reports, targetDir, settings);
        }
    }
}