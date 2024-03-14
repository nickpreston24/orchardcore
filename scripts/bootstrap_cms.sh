# adaptation of : https://www.dotnetthailand.com/web-frameworks/orchard-core-cms/create-a-custom-orchard-core-cms-module

cd ./orchardcore
mkdir -p orchard-example/src
cd orchard-example/src
dotnet new occms --name OrchardExample.Cms --framework net8.0


#cd orchard-example/src
mkdir Modules
cd Modules
dotnet new ocmodulecms --name OrchardExample.Map --AddPart True --PartName Map

cat > nuget.config << EOL
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="NuGet" value="https://api.nuget.org/v3/index.json" />
    <add key="OrchardCorePreview" value="https://nuget.cloudsmith.io/orchardcore/preview/v3/index.json" />
  </packageSources>
  <disabledPackageSources />
</configuration>

EOL