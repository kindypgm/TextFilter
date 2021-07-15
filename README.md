# TextFilter

### Basic Usage Example ###
```csharp
TextFilter filter = new TextFilter('*', char.IsNumber);

filter.Add("noob");

Console.WriteLine(filter.Filter("Noobserve").filtrate); // ****serve
Console.WriteLine(filter.Filter("No Observe").filtrate); // No Observe

filter.noiseFilter = char.IsWhiteSpace;

Console.WriteLine(filter.Filter("No Observe").filtrate); // *****serve

filter.Invalidate("noobserve");

Console.WriteLine(filter.Filter("Noobserve").filtrate); // Noobserve
Console.WriteLine(filter.Filter("No Observe").filtrate); // No Observe
```
