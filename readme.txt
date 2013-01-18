BZR Revision Labeller
---------------------

BZR Revision Labeller is a plugin for CruiseControl.NET that allows you to generate CruiseControl labels for your builds, based upon the revision number of your Bazaar working copy.

Requirements
------------

* CruiseControl.NET v1.5 - the plugin has been compiled and tested against this version of CC.NET. As such, it uses the .NET Framework 2.0, and is not backwards compatible with previous versions of CC.NET. If you cannot upgrade to this version of CC.NET and want to use this plugin, you can rebuild from the source code, but your will need to replace the solution and project files to work with Visual Studio 2003/2005. This release is *not* compatible with earlier releases of CC.NET, due to breaking changes introduced.

Installation
------------

Two builds are provided - Debug and Release. You should only need the Debug build if you are diganosing technical issues with the plugin. Just drop the contents of either src\ccnet.BzrRevisionLabeller.plugin\bin\Debug or src\ccnet.BzrRevisionLabeller.plugin\bin\Release into the folder that CruiseControl.NET is running from, update ccnet.config with the appropriate configuration (see below), and restart the service.

Configuration
-------------

Below is a sample configuration for BzrRevisionLabeller, showing the mandatory fields:

<labeller type="bzrRevisionLabeller">
	<url>bzr://localhost/repository/trunk</url>
</labeller>

The following sample configuration shows the complete set of fields:

<labeller type="bzrRevisionLabeller">
	<major>8</major>
	<minor>2</minor>
	<build>0</build>
	<pattern>Prerelease {major}.{minor}.{build}.{revision}</pattern>
	<incrementOnFailure>false</incrementOnFailure>
	<resetBuildAfterVersionChange>false</resetBuildAfterVersionChange>
	<url>bzr://localhost/repository/project/branches</url>
	<executable>C:\Bzr\Bin\Bzr.exe</executable>
	<username>ccnetuser</username>
	<password>ccnetpassword</password>
	<startDate>25/10/2011</startDate>
</labeller>

Usage
-----

When CruiseControl.NET begins a project build, it generates a label for the build and stores it in the property CCNetLabel - this property can then be used by NAnt or MSBuild to generate the AssemblyInfo.cs for your assemblies, so that CC.NET displays as its label the same version that the assemblies are built with. So, if the configuration for the labeller is set as:

<labeller type="bzrRevisionLabeller">
	<major>7</major>
	<minor>11</minor>
	<url>bzr://localhost/repository/trunk</url>
</labeller>

and the latest Bazaar revision number is 920, the CCNetLabel will be set to 7.11.0.920. Forcing a build without any changes to the repository will not make any changes to the label. A subsequent commit to the repository would then set the label to 7.11.0.921, and so on.

If you want to generate a more complex label, you use the Pattern field. This contains a number of tokens for the Major, Minor, Build, Revision and Rebuilt numbers,and you can effectively create any label you want. For instance:

<labeller type="bzrRevisionLabeller">
	<major>1</major>
	<minor>2</minor>
	<pattern>Labelling is as easy as {major} - {minor} - 3 - {revision}. See?</pattern>
	<url>bzr://localhost/repository/trunk</url>
</labeller>

and the current revision is 4, then the generated build label be "Labelling is as easy as 1 - 2 - 3 - 4. See?"

The available tokens are:

	{major} - the major build number
	{minor} - the minor build number
	{build} - the build number 
	{revision} - the revision number
	{rebuild} - the number of times the build has been rebuilt (i.e. a forced build)
	{date} - the number of days elapsed since the date specified in the startDate field
	{msrevision} - the revision number that Microsoft calculates - the number of seconds since midnight, divided by two

History
-------

v1.0
* Initial revision ported for Bazaar ported from SvnRevisionLabeller 3.1.0.32163


