# ServiceManual

Coding assignment for a backend job application.

## Install dependencies

Reboot might be necessary after installation.

```
pacman -Syu chrpath

git clone https://aur.archlinux.org/mongosh-bin.git
makepkg -sr
pacman -U mongosh-bin-1.6.0-1-x86_64.pkg.tar.zst

git clone https://aur.archlinux.org/mongodb-bin.git
makepkg -sr
pacman -U mongodb-bin-6.0.2-1-x86_64.pkg.tar.zst

systemctl enable mongodb
systemctl start mongodb
systemctl status mongodb
```

## Set up the database

Remove the database folder when you're done.

```
mkdir EtteplanMORE.ServiceManual.Database
mongod --dbpath EtteplanMORE.ServiceManual.Database
mongosh
use ServiceManual
db.createCollection("FactoryDevices")
db.createCollection("Maintenances")
```

## Run the application

The database was correctly initialised when the tests succeed.

```
dotnet run --project EtteplanMORE.ServiceManual.Sample
dotnet test
dotnet run --project EtteplanMORE.ServiceManual.Web
```

## Open browser

Check the port number from the terminal.

```
https://localhost:<port>/api/factorydevices/
https://localhost:<port>/api/maintenances/
https://localhost:<port>/api/maintenances/6367adbc9ae609a9ad57c1f7
```
