using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.Common.Tools.ReportGenerator
{
    [CakeAliasCategory("ReportGenerator")]
    public sealed class ReportGeneratorSettings
    {
        /*
    [["]-verbosity:<Verbose|Info|Error>["]]

Explanations:
   Verbosity:         The verbosity level of the log messages.
                      Values: Verbose, Info, Error

Default values:
   -reporttypes:Html
   -assemblyfilters:+*
   -classfilters:+*
   -verbosity:Verbose

Examples:
   "-reports:coverage.xml" "-targetdir:C:\report"
   "-reports:target\*\*.xml" "-targetdir:C:\report" -reporttypes:Latex;HtmlSummary
   "-reports:coverage1.xml;coverage2.xml" "-targetdir:report"
   "-reports:coverage.xml" "-targetdir:C:\report" -reporttypes:Latex "-sourcedirs:C:\MyProject"
   "-reports:coverage.xml" "-targetdir:C:\report" "-sourcedirs:C:\MyProject1;C:\MyProject2" "-assemblyfilters:+Included;-Exclude
d.*"
        */

        /// <summary>
        /// Gets or sets the list of report types which will be generated.
        /// </summary>
        public ReportType[] ReportTypes { get; set; }

        /// <summary>
        /// Gets or sets Optional directories which contain the corresponding source code.
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