# Multitask Activity Indicator and LogViewer

## Overview

This repository contains two main components: a multitask activity indicator and a log viewing system with an `ILogger` implementation. Both are designed for ease of use and integration into .NET applications.

### Multitask Activity Indicator

The multitask activity indicator provides a visual cue for asynchronous operations in a nested and structured code environment. It is initiated via statements and adheres to the code's hierarchical structure. The indicator automatically disappears from the user interface upon task completion or disposal.

![image](https://github.com/MrDeej/Eiriklb.Wpf.Tools/assets/16320446/7f432ed1-a0ec-488d-ab16-4c11506b1961)
![image](https://github.com/MrDeej/Eiriklb.Wpf.Tools/assets/16320446/6fb1999c-3e30-4ac4-90bc-fdc83d36899d)


### LogViewer and ILogger

The `ILogger` implementation is tailored for in-memory storage of logs, facilitating quick retrieval and efficient log management. Paired with it is the live log viewer UI component, which provides a real-time display of logged events, making it invaluable for monitoring and debugging purposes.

![image](https://github.com/MrDeej/Eiriklb.Wpf.Tools/assets/16320446/ccde2c82-c9a4-4997-b0ce-cfffca13a3b5)

## Features

- **Activity Indicator**
  - Nestable and contextual task initiation.
  - Auto-dismissal upon task disposal.
  
- **LogViewer and ILogger**
  - In-memory log storage for fast access.
  - Real-time log updates in the UI.
  - Easy integration with existing .NET applications.

## Getting Started

To use these components in your project, follow these steps:

1. Clone the repository to your local machine.
2. Reference the required project or include the components directly into your solution.
3. Follow the example usage provided to implement the activity indicator or log viewer in your application.

## WPF configuration Usage
```xaml
<Application x:Class="Eiriklb.WpfTools.TestApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:wpfTools="clr-namespace:Eiriklb.WpfTools.GlobalVm;assembly=Eiriklb.WpfTools"
             xmlns:local="clr-namespace:Eiriklb.WpfTools.TestApp">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="pack://application:,,,/Eiriklb.WpfTools;component/GlobalVm/RunningTaskViewer.xaml" />
                <ResourceDictionary />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>

</Application
```

```xaml
// app.xaml.cs
public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; } = default!;
    public static IConfigurationRoot Configuration { get; private set; } = default!;

    protected override async void OnStartup(StartupEventArgs e)
    {

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");


        Configuration = builder.Build();


        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection, Configuration);

        ServiceProvider = serviceCollection.BuildServiceProvider();


        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

    }

    private void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddWpfTools();
        services.AddScoped<MainWindow>();
    }

}
```

## Example Usage2
```csharp
using ActivityIndicator;
using LogViewer;

// To start an activity indicator
using var indicator = new GlobalVm.AddTask("Task Name", canUserCancel: true));

// To log an event
ILogger logger = new InMemoryLogger();
logger.Log("Event message");

// To view logs
LogViewer viewer = new LogViewer(logger);
viewer.Show(); // Opens the live log viewer UI

```

```xaml
<globalVm:RunningTaskViewer ObsCollRunningTasks="{Binding InMemoryData.Tasks}" />
```   

