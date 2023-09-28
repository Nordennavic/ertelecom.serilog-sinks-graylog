using System;
using Serilog.Events;
using Serilog.Sinks.Graylog.Core.Helpers;
using Serilog.Sinks.Graylog.Core.Transport;
using System.Text.Json;
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PublicConstructorInAbstractClass
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Serilog.Sinks.Graylog.Core
{

    /// <summary>
    /// Sync options for graylog
    /// </summary>
    public abstract class GraylogSinkOptionsBase
    {
        public const string DefaultFacility = null!;
        public const int DefaultShortMessageMaxLength = 500;
        public const LogEventLevel DefaultMinimumLogEventLevel = LevelAlias.Minimum;
        public const int DefaultStackTraceDepth = 10;
        public const MessageIdGeneratorType DefaultMessageGeneratorType = MessageIdGeneratorType.Timestamp;

        public const int DefaultMaxMessageSizeInUdp = 8192;
        /// <summary>
        /// The default option value (null) for GELF's "host" property. DNS hostname will be used instead.
        /// </summary>
        public const string DefaultHost = null!;

        public const int DefaultPort = 12201;

        public const string DefaultMessageTemplateFieldName = "message_template";

        // ReSharper disable once MemberCanBePrivate.Global
        public static readonly JsonSerializerOptions DefaultSerializerSettings = new()
        {
            WriteIndented = false,
        };

        public GraylogSinkOptionsBase()
        {
            MessageGeneratorType = MessageIdGeneratorType.Timestamp;
            ShortMessageMaxLength = DefaultShortMessageMaxLength;
            MinimumLogEventLevel = DefaultMinimumLogEventLevel;
            Facility = DefaultFacility;
            StackTraceDepth = DefaultStackTraceDepth;
            MaxMessageSizeInUdp = DefaultMaxMessageSizeInUdp;
            HostnameOverride = DefaultHost;
            IncludeMessageTemplate = false;
            ExcludeMessageTemplateProperties = false;
            MessageTemplateFieldName = DefaultMessageTemplateFieldName;
            JsonSerializerOptions = DefaultSerializerSettings;
            ParseArrayValues = false;
            //use gzip by default
            UseGzip = true;
            Port = DefaultPort;
        }

        /// <summary>
        /// Should parse values in arrays
        /// </summary>
        public bool ParseArrayValues { get; set; }

        /// <summary>
        /// Gets or sets the name of the message template field.
        /// </summary>
        /// <value>
        /// The name of the message template field.
        /// </value>
        public string MessageTemplateFieldName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include message template to graylog.
        /// </summary>
        /// <value>
        ///   <c>true</c> if include message template; otherwise, <c>false</c>.
        /// </value>
        /// <autogeneratedoc />
        public bool IncludeMessageTemplate { get; set; }

        /// <summary>
        /// Exclude message template properties.
        /// </summary>
        /// <value>
        ///   <c>true</c> if exclude message template properties; otherwise, <c>false</c>.
        /// </value>
        public bool ExcludeMessageTemplateProperties { get; set; }

        /// <summary>
        /// Gets or sets the minimum log event level.
        /// </summary>
        /// <value>
        /// The minimum log event level.
        /// </value>
        public LogEventLevel MinimumLogEventLevel { get; set; }

        /// <summary>
        /// Gets or sets the hostname or address of graylog server.
        /// </summary>
        /// <value>
        /// The hostname or address.
        /// </value>
        public string? HostnameOrAddress { get; set; }

        /// <summary>
        /// Gets or sets the facility name.
        /// </summary>
        /// <value>
        /// The facility.
        /// </value>
        public string? Facility { get; set; }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the transport.
        /// </summary>
        /// <value>
        /// The transport.
        /// </value>
        /// <remarks>
        /// You can implement another one or use default udp transport
        /// </remarks>
        public TransportType TransportType { get; set; }

        /// <summary>
        /// Gets or sets the gelf converter.
        /// </summary>
        /// <value>
        /// The gelf converter.
        /// </value>
        /// <remarks>
        /// You can implement another one for customize fields or use default
        /// </remarks>
        public IGelfConverter? GelfConverter { get; set; }

        /// <summary>
        /// Gets or sets the maximal length of the ShortMessage
        /// </summary>
        /// <value>
        /// ShortMessage Length
        /// </value>
        public int ShortMessageMaxLength { get; set; }

        /// <summary>
        /// Gets or sets the type of the message generator.
        /// </summary>
        /// <value>
        /// The type of the message generator.
        /// </value>
        /// <remarks>
        /// its timestamp or first 8 bytes of md5 hash
        /// </remarks>
        public MessageIdGeneratorType MessageGeneratorType { get; set; }

        /// <summary>
        /// Gets or sets the stack trace depth.
        /// </summary>
        /// <value>
        /// The stack trace depth.
        /// </value>
        public int StackTraceDepth { get; set; }

        /// <summary>
        /// Gets or sets the maximum udp message size.
        /// </summary>
        /// <value>
        /// The maximum udp message size
        /// </value>
        public int MaxMessageSizeInUdp { get; set; }

        /// <summary>
        /// Gets or sets the host property required by the GELF format. If set to null, DNS hostname will be used instead.
        /// Override computer host name in logs
        /// </summary>
        public string HostnameOverride { get; set; }

        /// <summary>
        /// Is this a secure connection (SSL)? If so, it gets validated with the host <see cref="HostnameOrAddress"/>
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// JsonSerializer options
        /// </summary>
        public JsonSerializerOptions JsonSerializerOptions { get; set; }

        /// <summary>
        /// Gets or sets the username in http
        /// </summary>
        public string? UsernameInHttp { get; set; }

        /// <summary>
        /// Gets or sets the password in http
        /// </summary>
        public string? PasswordInHttp { get; set; }

        /// <summary>
        /// For custom implementations of ITransport
        /// </summary>
        /// <remarks>
        /// <see cref="TransportType"/> should be set to <see cref="Transport.TransportType.Custom"/>
        /// </remarks>
        public Func<ITransport>? TransportFactory { get; set; }

        /// <summary>
        /// Should use gzip for sending logs
        /// </summary>
        public bool UseGzip { get; set; }
    }
}
