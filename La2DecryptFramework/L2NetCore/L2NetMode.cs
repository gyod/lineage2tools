using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetCore
{
    public enum L2NetMode
    {
        /// <summary>
        /// Wenn nur gesnifft wird.
        /// </summary>
        Sniffer,
        /// <summary>
        /// Wenn ein Tunnel aufgebaut werden soll
        /// </summary>
        Proxy,
        /// <summary>
        /// Wenn als eigenständiger Client.
        /// </summary>
        Client,
    }
}
