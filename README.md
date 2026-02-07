# UdpClub
A library for C# designed to help speed up the development of UDP networking apps.

## What?
UdpClub is a C# library, which acts as a wrapper around the `UdpClient` class, with a few extra utilities.
UdpClub automatically handles parsing byte data into packages for you, as well as RPC calls and a standardized way to create network packages for your project.

## Projects in this solution
### UdpClub
The main library, hosting every feature that UdpClub offers.

### TestShared
A shared library for the `TestServer` and `TestClient` applications.

### TestClient / TestServer
Two projects where UdpClub features, such as Package Handling, are actively tested in environments similar to what we expected will be used for UdpClub.

### UdpClubTests
A test library for testing utilities in UdpClub.

### MessagingApp
MessagingApp is a small rudimentary chat client developed with UdpClub at it's core. It's also the demo project for the [Flavortown Page](https://flavortown.hackclub.com/projects/4944).
![MessagingApp](media/img.png)