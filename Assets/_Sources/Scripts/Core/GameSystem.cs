using System.Threading;
using CerberusFramework.Core;
using CerberusFramework.Core.Systems;
using Cysharp.Threading.Tasks;

namespace CFGameClient.Core
{
    public abstract class GameSystem : SystemBase
    {
        protected GameSession Session;

        public override UniTask Initialize(GameSessionBase gameSessionBase, CancellationToken cancellationToken)
        {
            Session = (GameSession)gameSessionBase;
            return base.Initialize(gameSessionBase, cancellationToken);
        }
    }
}
