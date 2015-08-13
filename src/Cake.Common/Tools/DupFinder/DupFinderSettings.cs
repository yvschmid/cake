using Cake.Core.IO;

namespace Cake.Common.Tools.DupFinder
{
    /// <summary>
    /// Contains settings used by <see cref="DupFinderRunner"/> .
    /// </summary>
    public sealed class DupFinderSettings
    {
        /*
        Supported options:
        - /output (/o) : Write duplicates report to the specified file.

        Not supported options:
        - /debug (/d) : Show debugging messages.
        - /discard-cost : Complexity threshold for duplicate fragments. Code fragments with lower complexity are discarded (default: 70).
        - /discard-fields : Discard similar fields with different names (default: False).
        - /discard-literals : Discard similar lines of code with different literals (default: False).
        - /discard-local-vars : Discard similar local variables with different name (default: False).
        - /discard-types : Discard similar types with different names (default: False).
        - /idle-priority : Set process priority to idle.
        - /exclude-by-comment : Semicolon-delimited keywords to exclude files that contain the keyword in a file's opening comments.
        - /exclude-code-regions : Semicolon-delimited keywords that exclude regions that contain the keyword in the message substring. (e.g. "generated code" will exclude regions containing "Windows Form Designer generated code".
        - /exclude (/e) : Exclude files by pattern.
        - /properties : MSBuild properties.
        - /normalize-types : Normalize type names to the last subtype (default: False) (default: False).
        - /caches-home : Path to the directory where produced caches will be stored.
        - /show-stats : Show resources usage statistics (CPU and memory).
        - /show-text : Show duplicates text in report.
        - Not supported options:
        - /help (/h) : Show help and exit.
        - /version (/v) : Show tool version and exit.
        - /config : Path to configuration file where parameters are specified (use 'config-create' option to create sample file).
        - /config-create : Write command line parameters to specified file.
         */

        /// <summary>
        /// Gets or sets the location DupFinder should write its output.
        /// </summary>
        /// <value>The location DupFinder should write its output</value>
        public FilePath OutputFile { get; set; }
    }
}