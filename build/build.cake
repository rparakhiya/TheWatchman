#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./output");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore("../TheWatchman.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    DotNetCoreBuild("../TheWatchman.sln", new DotNetCoreBuildSettings {Configuration = configuration});
});

Task("Publish-Server")
    .IsDependentOn("Build")
    .Does(() =>
{
    var publishSettings = new DotNetCorePublishSettings {Configuration = configuration};

    var projectFiles = GetFiles("../TheWatchman.Server/TheWatchman.Server.csproj");

    foreach (var file in projectFiles) {
      var sausageCaseName = file.GetFilenameWithoutExtension().ToString().Replace('.', '-').ToLowerInvariant();

      publishSettings.OutputDirectory = $"{buildDir}/{sausageCaseName}/";
      DotNetCorePublish(file.FullPath, publishSettings);

      Zip($"{buildDir}/{sausageCaseName}", $"{buildDir}/{sausageCaseName}.zip");
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Publish-Server");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
