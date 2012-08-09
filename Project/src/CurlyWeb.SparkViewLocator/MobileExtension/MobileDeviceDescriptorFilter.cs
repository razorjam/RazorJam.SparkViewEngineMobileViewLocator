namespace CurlyWeb.SparkViewLocator.MobileExtension
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.Mvc;
  using Helpers;
  using Spark.Web.Mvc.Descriptors;

  public class MobileDeviceDescriptorFilter : DescriptorFilterBase
  {
    private readonly MobileViewHelper mobileViewHelper = new MobileViewHelper();

    public override void ExtraParameters( ControllerContext context, IDictionary< string, object > extra )
    {
      var request = context.HttpContext.Request;


      if( mobileViewHelper.RequestWantsToBeMobile( request ) )
        AddIsMobileDevice( extra );
    }

    public override IEnumerable< string > PotentialLocations( IEnumerable< string > locations,
                                                              IDictionary< string, object > extra )
    {
      if( !extra.ContainsKey( "mobile" ) )
        return locations;

      var mobileLocations = locations.Select( x =>
                                              InsertMobileStuffInViewLocation( x, "Mobile\\" ) );
      return mobileLocations.Concat( locations );
    }

    private static string InsertMobileStuffInViewLocation( string location, string mobileStuff )
    {
      if( !location.Contains( "\\" ) )
        return location;

      return location.Substring( 0, location.LastIndexOf( '\\' ) + 1 )
             + mobileStuff
             + location.Substring( location.LastIndexOf( '\\' ) + 1 );
    }

    private void AddIsMobileDevice( IDictionary< string, object > extra )
    {
      extra[ "mobile" ] = true;
    }
  }
}