using System.Collections.Concurrent;

namespace MailBoxSystem.Services;

public interface IDeviceHubService
{
    int? GetDoor();
    void OpenDoor(int box);
}

public sealed class DeviceHubService : IDeviceHubService
{
    private readonly ConcurrentBag<int> doors = new ConcurrentBag<int>();

    public void OpenDoor(int box)
    {
        doors.Add(box);
    }

    public int? GetDoor()
    {
        if (doors.Any())
        {
            var door = doors.Last();
            doors.Clear();
            return door;
        }

        return null;
    }
}
