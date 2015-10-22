using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.Common.Tools.ReportGenerator
{
    /// <summary>
    /// Contains settings used by <see cref="ReportGeneratorRunner"/>.
    /// </summary>
    [CakeAliasCategory("ReportGenerator")]
    public sealed class ReportGeneratorSettings
    {
        /// <summary>
        /// Gets or sets the list of coverage reports that should be parsed.
        /// </summary>
        public ReportType[] ReportTypes { get; set; }

        /// <summary>
        /// Gets or sets the directories which contain the corresponding source code.
        /// The source files are used if coverage report contains classes without path information.
        /// </summary>
        public DirectoryPath[] SourceDirectories { get; set; }

        /// <summary>
        /// Gets or sets the directory for storing persistent coverage information.
        /// Can be used in future reports to show coverage evolution.
        /// </summary>
        public DirectoryPath HistoryDirectory { get; set; }

        /// <summary>
        /// Gets or sets the list of assemblies that should be included or excluded in the report.
        /// Exclusion filters take precedence over inclusion filters.
        /// Wildcards are allowed.
        /// </summary>
        public string[] AssemblyFilters { get; set; }

        /// <summary>
        /// Gets or sets the list of classes that should be included or excluded in the report.
        /// Exclusion filters take precedence over inclusion filters.
        /// Wildcards are allowed.
        /// </summary>
        public string[] ClassFilters { get; set; }

        /// <summary>
        /// Gets or sets the verbosity level of the log messages.
        /// </summary>
        public Verbosity? Verbosity { get; set; }
    }
}