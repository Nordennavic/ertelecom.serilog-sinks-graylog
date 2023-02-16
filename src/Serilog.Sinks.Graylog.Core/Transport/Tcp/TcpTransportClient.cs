using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Serilog.Sinks.Graylog.Core.Transport.Tcp
{
    public class TcpTransportClient : ITransportClient<byte[]>
    {
        private readonly IPAddress _address;
        private readonly int _port;
        private readonly string _sslHost;
        //private TcpClient _client;
        //private Stream _stream;

        /// <inheritdoc />
        public TcpTransportClient(IPAddress address, int port, string sslHost)
        {
            _address = address;
            _port = port;
            _sslHost = sslHost;
        }

        /// <inheritdoc />
        public async Task Send(byte[] payload)
        {
            using var client = new TcpClient();
            {
                using var stream = await Connect(client).ConfigureAwait(false);
                {
                    await stream.WriteAsync(payload, 0, payload.Length).ConfigureAwait(false);
                    await stream.FlushAsync().ConfigureAwait(false);
                }
            }
            client.Dispose();
        }

        //private TcpClient CheckSocketConnection()
        //{
        //    if (_client != null)
        //    {
        //        if (_client.Connected)
        //            return;
        //        else
        //            CloseClient();
        //    }

        //    _client = new TcpClient();
        //    await Connect().ConfigureAwait(false);
        //}

        private async Task<Stream> Connect(TcpClient client)
        {
            await client.ConnectAsync(_address, _port).ConfigureAwait(false);
            Stream stream = client.GetStream();

            if (!string.IsNullOrWhiteSpace(_sslHost))
            {
                var _sslStream = new SslStream(stream, false);

                await _sslStream.AuthenticateAsClientAsync(_sslHost).ConfigureAwait(false);

                X509Certificate remoteCertificate = _sslStream.RemoteCertificate;
                if (_sslStream.RemoteCertificate != null)
                {
                    Debug.WriteLine("Remote cert was issued to {0} and is valid from {1} until {2}.",
                        remoteCertificate.Subject,
                        remoteCertificate.GetEffectiveDateString(),
                        remoteCertificate.GetExpirationDateString());

                    return _sslStream;
                }
                else
                {
                    Debug.Fail("Remote certificate is null.");
                }
            }

            return stream;
        }

        private void CloseClient(TcpClient client, Stream stream)
        {
#if NETFRAMEWORK
            //_client?.Close();
#else
            client?.Dispose();
#endif
            stream?.Dispose();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            //CloseClient(client,  stream);
        }

    }
}
