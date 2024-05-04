using Oxide.Core;
using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("EntityCleanup", "acedrust", "1.0.6")]
    class EntityCleanup : RustPlugin
    {
        private Timer cleanupTimer;

        private void OnServerInitialized()
        {
            cleanupTimer = timer.Repeat(60f, 0, () =>  // TIMER LOGIC 60 SECONDS = EVERY CLEANUP = 45 SECONDS ALERT BEFORE HAND
            {
                BroadcastCleanupWarning();
                timer.Once(45f, ClearDroppedItems);
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