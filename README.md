# The Watchman

### To Do
* [x] Add logging
* [ ] Starting windows service gets stuck in starting mode
* [x] Running monitor as windows service sets base path to C:\windows\system32. Hence, referring appsettings.json file from that location. Create symlink as workaround
* [ ] Notifications when a resource stops sending heartbeats (Email, Microsoft Teams, etc)

# Docker

Docker images are at docker hub:

* [Server](https://hub.docker.com/repository/docker/rumitparakhiya/thewatchman-server)
* [Monitor](https://hub.docker.com/repository/docker/rumitparakhiya/thewatchman-monitor)
