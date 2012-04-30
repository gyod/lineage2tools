using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetCore
{
    public interface IL2StreamProvider
    {
        L2NetStream ClientStreamIn
        {
            get;
        }

        L2NetStream ServerStreamIn
        {
            get;
        }

        L2NetStream ClientStreamOut
        {
            get;
        }

        L2NetStream ServerStreamOut
        {
            get;
        }

        L2NetMode ProviderType
        {
            get;
        }
    }
}
