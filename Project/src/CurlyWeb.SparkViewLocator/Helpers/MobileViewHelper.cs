namespace CurlyWeb.SparkViewLocator.Helpers
{
  using System.Web;
  using Enums;
  using ViewHelper;
  using ViewHelper.Interfaces;

  public class MobileViewHelper
  {
    private readonly IMobileCookieHelper cookieHelper = new MobileCookieHelper();

    public bool RequestWantsToBeMobile( HttpRequestBase request )
    {
      if( IsForcedMobileView( request ) )
      {
        cookieHelper.ForceViewMobileSite();
        return true;
      }

      var requestMode = cookieHelper.GetCurrentMode();

      switch( requestMode )
      {
        case SiteMode.NotSet:
          return IsMobileDevice( request );

        case SiteMode.Mobile:
          return true;

        case SiteMode.Desktop:
          return false;

        default:
          return false;
      }
    }

    private static bool IsMobileDevice( HttpRequestBase request )
    {
      return request.Browser.IsMobileDevice;
    }

    private static bool IsForcedMobileView( HttpRequestBase request )
    {
      return
        ( !string.IsNullOrEmpty( request.QueryString[ "mobilize" ] ) && request.QueryString[ "mobilize" ] == "true" );
    }
  }
}