// See https://aka.ms/new-console-template for more information

using PortioningMachine.ItemProviders;

Console.WriteLine("Hello, World!");

ItemProvider myItemProvider = new ItemProvider(new GaussianDistribution(100, 10));

myItemProvider.Go();
myItemProvider.ItemArrived += (sender, item) => Console.WriteLine($"Item {item.Id} arrived with weight {item.Weight}");