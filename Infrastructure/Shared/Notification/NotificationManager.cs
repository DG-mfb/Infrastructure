namespace Shared.Notification
{
	using System.Linq;
	using Kernel.Notification;
	using Shared.Configuration;
	using Shared.Logging;

	public abstract class NotificationManager
    {
        protected virtual void SendNotification(object notificationMessage)
        {
            var resolver = ApplicationConfiguration.Instance.DependencyResolver;

            var notifiers = resolver.ResolveAll<INotifier>();

            if (notifiers == null || notifiers.Count() == 0)
            {
                LoggerManager.WriteWarningToEventLog("No objects implementing INotifier have been found in DI container. Make sure they are registered in the container or implement some of the auto - requster interfaces");

                return;
            }

            foreach (var notifier in notifiers)
            {
                using (notifier)
                {
                    notifier.SendNotification(notificationMessage);
                }
            }
        }
    }
}