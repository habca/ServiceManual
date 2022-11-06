using System;
using System.Text.Json;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using MongoDB.Driver;

var mongoClient = new MongoClient("mongodb://localhost:27017");
var mongoDatabase = mongoClient.GetDatabase("ServiceManual");

try
{
    using (var stream = new StreamReader("EtteplanMORE.ServiceManual.Sample/FactoryDeviceSample.json"))
    {
        var collection = mongoDatabase.GetCollection<FactoryDevice>("FactoryDevices");
        string json = stream.ReadToEnd();
        var array = JsonSerializer.Deserialize<FactoryDevice[]>(json);
        await collection.InsertManyAsync(array);
        Console.WriteLine(json);
    }

    using (var stream = new StreamReader("EtteplanMORE.ServiceManual.Sample/MaintenanceSample.json"))
    {
        var collection = mongoDatabase.GetCollection<Maintenance>("Maintenances");
        string json = stream.ReadToEnd();
        var array = JsonSerializer.Deserialize<Maintenance[]>(json);
        await collection.InsertManyAsync(array);
        Console.WriteLine(json);
    }
}
catch (IOException e)
{
    Console.WriteLine(e.Message);
}