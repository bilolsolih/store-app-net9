namespace StoreApp;

public static class Extensions
{
  public static string GetUploadsBaseUrl(this HttpContext httpContext)
  {
    var request = httpContext.Request;
    return $"{request.Scheme}://{request.Host}/uploads";
  }

  public static string GetUploadBasePath(this IWebHostEnvironment env)
  {
    return Path.Combine(env.ContentRootPath, "uploads");
  }
}