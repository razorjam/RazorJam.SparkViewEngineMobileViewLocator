namespace $rootnamespace$.Controllers
{
  using System.Web.Mvc;
  using CurlyWeb.SparkViewLocator.ViewHelper.Interfaces;

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
}