using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace LocalAngle.Net
{
    /// <summary>
    /// Represents a file upload request parmeter used in HttpWebRequests, as part of a multipart POST request
    /// </summary>
    public class RequestFileParameter
    {
        #region Constructors

#if !SILVERLIGHT && !NETFX_CORE
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestFileParameter"/> class.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="fileName">The orginal file name.</param>
        /// <param name="contentType">Mime type of the content.</param>
        /// <param name="content">The content.</param>
        public RequestFileParameter(string name, string contentType, FileInfo content) 
        {
            if (content == null)
            {
                throw new ArgumentNullException("content", "The content must be provided");
            }

            this.ContentType = contentType;
            this.FileName = content.Name;
            this.Name = name;
            this.Content = File.ReadAllBytes(content.FullName);
        }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestFileParameter"/> class.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="fileName">The orginal file name.</param>
        /// <param name="contentType">Mime type of the content.</param>
        /// <param name="contentLength">The number of bytes</param>
        /// <param name="content">A stream containing the content.</param>
        public RequestFileParameter(string name, string fileName, string contentType, int contentLength, Stream content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content", "The content must be provided");
            }

            this.ContentType = contentType;
            this.FileName = fileName;
            this.Name = name;

            byte[] rawData = new byte[contentLength];
            content.Read(rawData, 0, contentLength);
            this.Content = rawData;
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the length of the content.
        /// </summary>
        /// <value>
        /// The length of the content.
        /// </value>
        public int ContentLength
        {
            get
            {
                return Content.GetUpperBound(0);
            }
        }

        private string _contentType;
        /// <summary>
        /// Gets the mime type of the content.
        /// </summary>
        /// <value>
        /// The mime type of the content.
        /// </value>
        public string ContentType
        {
            get
            {
                return _contentType;
            }
            protected set
            {
                _contentType = (string.IsNullOrEmpty(value) ? "application/octet-stream" : value);
            }
        }

        private string _fileName;
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName
        {
            get
            {
                return string.IsNullOrEmpty(_fileName) ? "c:\attachment.dat" : _fileName;
            }
            protected set
            {
                _fileName = value;
            }
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name
        {
            get;
            protected set;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        private byte[] Content { get; set; }

        #endregion
    }
}
