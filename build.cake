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
var buildDir = Directory("./src/CachingManager/bin");

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
    NuGetRestore("./src/CachingManager.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./src/CachingManager.sln", settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild("./src/CachingManager.sln", settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3("./**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
        NoResults = true
        });
});

//////////////////////////////////////////////////////////////////////
// PUSH NUGET PACKAGE
//////////////////////////////////////////////////////////////////////

Task("Package")
	.IsDependentOn("Run-Unit-Tests")
	.Does(() => {
		NuGetPack("./CachingManager.nuspec", 
					new NuGetPackSettings() {
									 //Id = "TestNuget",
                                     //Version                 = "0.0.0.1",
                                     //Title                   = "The tile of the package",
                                     //Authors                 = new[] {"John Doe"},
                                     //Owners                  = new[] {"Contoso"},
                                     //Description             = "The description of the package",
                                     //Summary                 = "Excellent summary of what the package does",
                                     //ProjectUrl              = new Uri("https://github.com/SomeUser/TestNuget/"),
                                     //IconUrl                 = new Uri("http://cdn.rawgit.com/SomeUser/TestNuget/master/icons/testnuget.png"),
                                     //LicenseUrl              = new Uri("https://github.com/SomeUser/TestNuget/blob/master/LICENSE.md"),
                                     //Copyright               = "Some company 2015",
                                     //ReleaseNotes            = new [] {"Bug fixes", "Issue fixes", "Typos"},
                                     //Tags                    = new [] {"Cake", "Script", "Build"},
                                     //RequireLicenseAcceptance= false,
                                     //Symbols                 = false,
                                     //NoPackageAnalysis       = true,
                                     Files                   = new [] {
                                                                          new NuSpecContent {Source = "./src/CachingManager/bin/Release/netcoreapp2.0/CachingManager.dll", Target = "bin"},
                                                                       },
                                     //BasePath                = "./src/CachingManager/bin/Release/netcoreapp2.0/",
                                     //OutputDirectory         = "./nuget"
					});
				});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Package");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);