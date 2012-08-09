namespace CurlyWeb.SparkViewLocator.ViewHelper
{
  using System;
  using System.Web;
  using Enums;
  using Interfaces;

  public class MobileCookieHelper : IMobileCookieHelper
  {
    public const string LONG_TERM_VIEW_SELECTION_COOKIE_NAME = "_siteViewMode";

    private static HttpCookie GetMobileCookie()
    {
      return HttpContext.Current.Request.Cookies[ LONG_TERM_VIEW_SELECTION_COOKIE_NAME ];
    }

    public void ForceViewMobileSite()
    {
      SetCookieWithMode( SiteMode.Mobile );
    }

    public void ForceDesktopSite()
    {
      SetCookieWithMode( SiteMode.Desktop );
    }

    public SiteMode GetCurrentMode()
    {
      var cookie = GetMobileCookie();

      if( cookie == null )
        return SiteMode.NotSet;

      return ( SiteMode )Enum.Parse( typeof( SiteMode ), cookie.Value );
    }

    private void SetCookieWithMode( SiteMode mode )
    {
      var newCookie = new HttpCookie( LONG_TERM_VIEW_SELECTION_COOKIE_NAME, mode.ToString() )
                        {Expires = DateTime.Now.AddDays( 5 )};
      HttpContext.Current.Response.Cookies.Add( newCookie );
    }
  }
}