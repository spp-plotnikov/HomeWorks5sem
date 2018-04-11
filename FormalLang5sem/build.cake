#tool nuget:?package=Antlr&version=3.5.0.2
#tool nuget:?package=DotNet.Contracts&version=1.10.20606.1
#tool nuget:?package=DotParser&version=1.0.6
#tool nuget:?package=FSharp.Core&version=4.3.4
#tool nuget:?package=FSharpx.Collections&version=1.17.0
#tool nuget:?package=FSharpx.Collections.Experimental&version=1.17.0
#tool nuget:?package=FSharpx.Core&version=1.8.32
#tool nuget:?package=GraphViz4Net&version=2.0.33
#tool nuget:?package=System.ValueTuple&version=4.4.0


var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");
var buildDir = Directory("./FormalLang5sem/bin") 
             + Directory(configuration);


Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});


Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./FormalLang5sem.sln");
});


Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
      MSBuild("./FormalLang5sem.sln", settings =>
        settings.SetConfiguration(configuration));
});


RunTarget(target);