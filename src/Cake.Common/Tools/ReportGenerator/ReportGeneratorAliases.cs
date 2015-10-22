using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using System;
using System.Collections.Generic;

namespace Cake.Common.Tools.ReportGenerator
{
    /// <summary>
    /// Contains functionality related to ReportGenerator
    /// </summary>
    [CakeAliasCategory("ReportGenerator")]
    public static class ReportGeneratorAliases
    {
        /// <summary>
        /// Generats a html report from the specified xml report in the specified target directory.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="report">The xml report.</param>
        /// <param name="targetDir">The target directory.</param>
        [CakeMethodAlias]
        public static void ReportGenerator(this ICakeContext context, FilePath report, DirectoryPath targetDir)
        {
            ReportGenerator(context, report, targetDir, new ReportGeneratorSettings());
        }

        /// <summary>
        ///
        /// </summary>
        [CakeMethodAlias]
        public static void ReportGenerator(this ICakeContext context, FilePath report, DirectoryPath targetDir, ReportGeneratorSettings settings)
        {
            ReportGenerator(context, new[] { report }, targetDir, settings);
        }

        /// <summary>
        ///
        /// </summary>
        [CakeMethodAlias]
        public static void ReportGenerator(this ICakeContext context, IEnumerable<FilePath> reports, DirectoryPath targetDir)
        {
            ReportGenerator(context, reports, targetDir, new ReportGeneratorSettings());
        }

        /// <summary>
        ///
        /// </summary>
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