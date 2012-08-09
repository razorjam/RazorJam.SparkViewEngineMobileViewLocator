namespace CurlyWeb.SparkViewLocator.ViewHelper.Interfaces
{
  using Enums;

  public interface IMobileCookieHelper
  {
    void ForceViewMobileSite();
    void ForceDesktopSite();
    SiteMode GetCurrentMode();
  }
}