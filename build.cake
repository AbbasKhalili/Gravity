#tool "nuget:?package=xunit.runner.console"

var target = Argument("target", "Package");
var configuration = Argument("Configuration", "Release");
var solutionPath = Argument("SolutionPath", @"../Gravity/Gravity.sln");

//var artifacts = MakeAbsolute(Directory("./artifacts"));

var outputDir = Directory("./output");
var buildSettings = new DotNetCoreBuildSettings
     {
         Framework = "netcoreapp2.1",
         Configuration = "Release",
         OutputDirectory = outputDir
     };


Task("Clean")
    .Does(() =>
{
    if (DirectoryExists(outputDir))
        {
            DeleteDirectory(outputDir, recursive:true);
        }
    CleanDirectories(string.Format("../Gravity/**/obj/{0}",configuration));
    CleanDirectories(string.Format("../Gravity/**/bin/{0}",configuration));
});


Task("Restore-Nuget")
    .Does(()=> {
     DotNetCoreRestore(solutionPath);
});


Task("Build")
    .Does(()=>
{
     DotNetCoreBuild(
        solutionPath,
        new DotNetCoreBuildSettings {
            NoRestore = true,
            Configuration = configuration
        }
);
});


Task("Run-Unit-tests")
    .DoesForEach(
        GetFiles("../Gravity/**/*.Tests.Unit.csproj"),
        testProject => 
{
    DotNetCoreTest(
        testProject.FullPath,
        new DotNetCoreTestSettings{
            NoBuild = true,
            NoRestore = true,
            Configuration = configuration
        }
    );
});


Task("Package")
    .DoesForEach(
        GetFiles("./**/*Lib*.dll") - GetFiles("./**/*.Tests.Unit.dll"),
        testProject => 
{
    //DotNetCorePack(
    //    testProject.FullPath,
     //   new DotNetCorePackSettings{
    //        NoBuild = true,
     //       NoRestore = true,
     //       Configuration = configuration,
     //       OutputDirectory = outputDir
    //    }
    //);
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-Nuget")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Package");

RunTarget("Default");
