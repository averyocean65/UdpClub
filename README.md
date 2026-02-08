# UdpClub
A library for C# designed to help speed up the development of UDP networking apps.

## What?
UdpClub is a C# library, which acts as a wrapper around the `UdpClient` class, with a few extra utilities.
UdpClub automatically handles parsing byte data into packages for you, as well as RPC calls and a standardized way to create network packages for your project.

## Requirements
In order to use any project in `UdpClub`, you will need to have the [.NET Framework 4.8.1](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net481) installed.

## Usage Notice
### ⚠️ UdpClub is a C# DLL. It is not compatible with languages such as C/C++, Rust, or others by default.

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
