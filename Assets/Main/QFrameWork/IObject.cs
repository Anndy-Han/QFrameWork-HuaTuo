using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QFrameWork
{
    public interface IObject
    {
        IApp app { get; }

        IEventDispatcher eventDispatcher { get; }

        IAssetManager assetManager { get; }

        IUIManager uiManager { get; }

        IProcedureManager proceduceManager { get; }

        IAudioManager audioManager { get; }

        INetworkManager networkManager { get; }

        IEntityManager entityManager { get; }
    }
}
