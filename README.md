[![NuGet](https://img.shields.io/nuget/vpre/servicebusspy.svg)](https://www.nuget.org/packages/ServiceBusSpy)

# Service Bus Spy

A simple command line tool to silently inspect Azure Service Bus messages without touching them. As the [Service Bus Explorer](https://github.com/paolosalvatori/ServiceBusExplorer) runs on Winodws only, I descided to quickly write a **cross-platform** command line tool based on .NET Core to explore messages in Azure Service Bus.

### Installation

Make sure to have at least [.NET Core 2.1](https://dotnet.microsoft.com/download) installed on your machine.

```bash
dotnet tool install servicebusspy --global
```

### Update or remove

```bash
# Update
dotnet tool update servicebusspy --global

# Remove
dotnet tool uninstall servicebusspy --global
```

## Usage

### List all messages of a queue

```bash
servicebusspy list --queue "Test" \
                   --connectionString "YOUR_SERVICE_BUS_CONNECTION_STRING" \
                   --verbose # optional
```

### Add a message to a queue

```bash
servicebusspy add "Message Content" \
                  --queue "Test" \
                  --connectionString "YOUR_SERVICE_BUS_CONNECTION_STRING"
```

### Subscribe to new messages in a queue

```bash
servicebusspy subscribe --queue "Test" \
                        --connectionString "YOUR_SERVICE_BUS_CONNECTION_STRING" \
                        --verbose # optional
```
