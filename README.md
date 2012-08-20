# CurlyWeb.SparkViewEngineMobileViewLocator #
A nuget package to add searching for additional locations for mobile views with the spark view engine.

When we first implemented this, we found limited useful documentation so we thought it would be a good idea to share our findings with the world, so we have :)

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

## Usage ##
Once configured all you have to do is add a Mobile folder to any of your View paths with the appropriate named view in it and it will be rendered instead of your standard view if either you force it (using the method below) or if the device accessing the site is a mobile device.

For example, if you have a controller named "HomeController" and an action named "Index", if the request has been deemed as a mobile view request, the first view location searched for will be: "/Views/Home/Mobile/Index.spark". If nothing can be found it will then look in the standard place: "/Views/Home/Index.spark".

### Forcing Looking For Mobile Views ###
If you want to force it to look for a mobile view, all you have to do is add ?mobilize=true to your URL. 

This will set a cookie that will force all subsequent requests to use the mobile view.

To manage this manually, all you have to do is use a couple of methods found in a little helper "IMobileCookieHelper".

Add that interface and it's implementation "MobileCookieHelper" to your container and then add a dependency to a new controller to manage the mode and call the respective methods accordingly.

Example: 

	public class MobileModeController : Controller
	{
    	private readonly IMobileCookieHelper mobileCookieHelper;

	    public MobileModeController( IMobileCookieHelper mobileCookieHelper )
	    {
	      this.mobileCookieHelper = mobileCookieHelper;
	    }

	    public RedirectResult Desktop()
	    {
	      mobileCookieHelper.ForceDesktopSite();
	      return Redirect( RedirectTo() );
	    }

	    private string RedirectTo()
	    {
	      var redirectTo = HttpContext.Request.UrlReferrer == null ? "~/" : HttpContext.Request.UrlReferrer.LocalPath;
	      return redirectTo;
	    }

	    public RedirectResult Mobile()
	    {
	      mobileCookieHelper.ForceViewMobileSite();
	      return Redirect( RedirectTo() );
	    }
	}

### Extending ###
If you want it to recognise a few more mobile devices, add the package: http://nuget.org/packages/51Degrees.mobi

	Install-Package 51Degrees.mobi


### Credits ###
Thanks to Curly Web (www.curlyweb.co.uk) for letting this be open source :)