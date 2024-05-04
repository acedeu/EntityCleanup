using Oxide.Core;
using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("EntityCleanup", "acedrust", "1.0.8")]
    class EntityCleanup : RustPlugin
    {
        private Timer cleanupTimer;

        private void OnServerInitialized()
        {
            cleanupTimer = timer.Repeat(60f, 0, () =>  // EVERY 60 SECONDS = CLEANUP
            {
                BroadcastCleanupWarning();
                timer.Once(45f, ClearDroppedItems); // EVERY 30 SECONDS = CLEANUP
            });
        }

        private void ClearDroppedItems()
        {
            rust.RunServerCommand("global.cleardroppeditems");
            Puts("Dropped items cleared.");
        }

        private void BroadcastCleanupWarning()
        {
            PrintToChat("Entity cleanup will occur in 45 seconds.");
        }

        private void Unload()
        {
            cleanupTimer?.Destroy();
        }
    }
}
