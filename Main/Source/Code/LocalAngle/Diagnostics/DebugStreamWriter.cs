using System.IO;
using System.Text;

namespace LocalAngle.Diagnostics
{
    /// <summary>
    /// TextWriter that can be used with the Linq-to-SQL datacontexts to dump logging to the debug console
    /// </summary>
    public class DebugTextWriter : StreamWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugTextWriter"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors"), 
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public DebugTextWriter()
            : base(new DebugOutStream(), Encoding.Unicode, 1024)
        {
            this.AutoFlush = true;
        }
    }
}
