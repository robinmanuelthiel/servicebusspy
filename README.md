[![nuget](https://img.shields.io/nuget/v/ServiceBusSpy.svg)](https://www.nuget.org/packages/ServiceBusSpy/)

# Service Bus Spy

A simple command line tool to silently inspect Azure Service Bus messages without touching them.

### Installation

Make sure to have at least [.NET Core 2.1](https://dotnet.microsoft.com/download) installed on your machine.

```bash
dotnet tool install servicebusspy -g 
```

### Update or remove

```bash
# Update
dotnet tool update servicebusspy -g 

# Remove
dotnet tool uninstall servicebusspy -g
```

## Usage

### List all messages of a queue

```bash
servicebusspy list --queue "Test" \
                   --connectionString "YOUR_SERVICE_BUS_CONNECTION_STRING"
```


### Add a messages to a queue

```bash
servicebusspy add "Message Content" \
                  --queue "Test" \
                  --connectionString "YOUR_SERVICE_BUS_CONNECTION_STRING"
```

### Subscribe to new messages in a queue

```bash
servicebusspy subscribe --queue "Test" \
                        --connectionString "YOUR_SERVICE_BUS_CONNECTION_STRING"
```
