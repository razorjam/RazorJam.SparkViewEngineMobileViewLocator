# CurlyWeb.SparkViewEngineMobileViewLocator #
A nuget package to add searching for additional locations for mobile views.

## Getting Started ##
Simply add the package with nuget: 
	Install-Package CurlyWeb.SparkViewEngineMobileViewLocator

As this package is dependand on the spark.web.mvc3 package, it will add a web activator file in your App_Start folder. If this is where you want to configure Spark, great, go ahead and add the required configuration there. If not, do it where you feel it is right in your application.

**BEWARE:** make sure it only happens in one place in your application.

## Configuration ##
Add the filter descriptor to your spark configuration in the following way:

	ViewEngines.Engines.Clear();
	var settings = new SparkSettings {AutomaticEncoding = true};
	settings
		.AddNamespace( "System.Collections.Generic" )
		.AddNamespace( "System.Linq" )
		.AddNamespace( "System.Web.Mvc" )
		.AddNamespace( "System.Web.Mvc.Html" )
		.AddNamespace( "System.Configuration" );

	var container = SparkEngineStarter.CreateContainer( settings );
	container.AddFilter( new MobileDeviceDescriptorFilter() );

	SparkEngineStarter.RegisterViewEngine( container );

The decision was made not to add this as a web activator app start method as not to get in the way of the spark web activator or any other configuration that you may already have.